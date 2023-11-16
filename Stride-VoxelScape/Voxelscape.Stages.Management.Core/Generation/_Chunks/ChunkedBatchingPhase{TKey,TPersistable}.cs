using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Stages.Management.Pact.Persistence;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Common.Pact.Types;
using Voxelscape.Utility.Concurrency.AsyncEx;
using Voxelscape.Utility.Concurrency.Dataflow.Blocks;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;
using static Voxelscape.Stages.Management.Core.Generation.BatchGenerationOptions;
using static Voxelscape.Stages.Management.Core.Generation.GenerationOptions;

namespace Voxelscape.Stages.Management.Core.Generation
{
	public class ChunkedBatchingPhase<TKey, TPersistable> : AbstractAsyncCancelable, IGenerationPhase
		where TPersistable : IKeyed<TKey>
	{
		private static readonly string ProgressMessage = "Generating chunks";

		private readonly Subject<GenerationPhaseProgress> progress = new Subject<GenerationPhaseProgress>();

		private readonly IReadOnlyLargeCollection<TKey> chunkKeys;

		private readonly IAsyncFactory<TKey, TPersistable> chunkFactory;

		private readonly IChunkStore<TKey, TPersistable> chunkStore;

		private readonly TransformBlock<TKey, TPersistable> generateChunks;

		private readonly ITargetBlock<TPersistable> saveChunks;

		private readonly AsyncLongCountdownEvent chunksCompleted;

		public ChunkedBatchingPhase(
			IStageIdentity stageIdentity,
			IGenerationPhaseIdentity phaseIdentity,
			IReadOnlyLargeCollection<TKey> chunkKeys,
			IAsyncFactory<TKey, TPersistable> chunkFactory,
			IChunkStore<TKey, TPersistable> chunkStore,
			int maxBatchedChunks,
			BatchGenerationOptions options = null)
			: base(options?.CancellationToken ?? DefaultCancellationToken)
		{
			Contracts.Requires.That(stageIdentity != null);
			Contracts.Requires.That(phaseIdentity != null);
			Contracts.Requires.That(chunkKeys != null);
			Contracts.Requires.That(chunkFactory != null);
			Contracts.Requires.That(chunkStore != null);
			Contracts.Requires.That(maxBatchedChunks > 0);

			this.StageIdentity = stageIdentity;
			this.PhaseIdentity = phaseIdentity;
			this.chunkKeys = chunkKeys;
			this.chunkFactory = chunkFactory;
			this.chunkStore = chunkStore;

			this.Progress = this.progress.AsObservable();

			var dataflowOptions = new ExecutionDataflowBlockOptions()
			{
				MaxDegreeOfParallelism = options?.MaxDegreeOfParallelism ?? DefaultMaxDegreeOfParallelism,
				BoundedCapacity = maxBatchedChunks,
				CancellationToken = this.CancellationToken,
			};

			this.generateChunks = new TransformBlock<TKey, TPersistable>(
				chunkIndex => this.chunkFactory.CreateAsync(chunkIndex), dataflowOptions);

			this.saveChunks = BatchActionBlock.Create<TPersistable>(
				this.SaveChunks,
				maxBatchedChunks,
				options?.Batching ?? DefaultBatching,
				dataflowOptions);

			var linkOptions = new DataflowLinkOptions() { PropagateCompletion = true };
			this.generateChunks.LinkTo(this.saveChunks, linkOptions);

			this.chunksCompleted = new AsyncLongCountdownEvent(this.ProgressTotalCount);
		}

		/// <inheritdoc />
		public IStageIdentity StageIdentity { get; }

		/// <inheritdoc />
		public IGenerationPhaseIdentity PhaseIdentity { get; }

		/// <inheritdoc />
		public IObservable<GenerationPhaseProgress> Progress { get; }

		/// <inheritdoc />
		public long ProgressTotalCount => this.chunkKeys.Count;

		/// <inheritdoc />
		protected override async Task CompleteAsync(CancellationToken cancellation)
		{
			try
			{
				this.progress.OnNext(
					new GenerationPhaseProgress(this, ProgressMessage, 0, this.ProgressTotalCount));

				await this.generateChunks.SendAllAsync(this.chunkKeys, cancellation).DontMarshallContext();

				// wait for all chunks to finish, then shut down the pipeline
				await this.chunksCompleted.WaitAsync().DontMarshallContext();
				await this.generateChunks.CompleteAndAwaitAsync().DontMarshallContext();
				await this.saveChunks.Completion.DontMarshallContext();
				this.progress.OnCompleted();
			}
			catch (Exception exception)
			{
				this.Fault(exception);
				this.progress.OnError(exception);
				throw;
			}
			finally
			{
				this.progress.Dispose();
			}
		}

		private async Task SaveChunks(IReadOnlyList<TPersistable> chunks)
		{
			Contracts.Requires.That(chunks != null);

			try
			{
				await this.chunkStore.AddOrUpdateAllAsync(
					chunks, this.CancellationToken).DontMarshallContext();

				this.chunksCompleted.TrySignal(chunks.Count);
				this.progress.OnNext(
					new GenerationPhaseProgress(this, ProgressMessage, chunks.Count, this.ProgressTotalCount));
			}
			catch (Exception exception)
			{
				this.Fault(exception);
				throw;
			}
		}

		private void Fault(Exception exception)
		{
			((IDataflowBlock)this.generateChunks).Fault(exception);
			this.chunksCompleted.TrySignalAllRemaining();
		}
	}
}
