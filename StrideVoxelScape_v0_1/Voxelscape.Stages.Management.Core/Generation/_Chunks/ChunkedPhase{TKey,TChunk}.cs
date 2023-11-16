using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Common.Pact.Types;
using Voxelscape.Utility.Concurrency.AsyncEx;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;
using static Voxelscape.Stages.Management.Core.Generation.GenerationOptions;

namespace Voxelscape.Stages.Management.Core.Generation
{
	public class ChunkedPhase<TKey, TChunk> : AbstractAsyncCancelable, IGenerationPhase
		where TChunk : IKeyed<TKey>
	{
		private static readonly string ProgressMessage = "Processing chunks";

		private readonly Subject<GenerationPhaseProgress> progress = new Subject<GenerationPhaseProgress>();

		private readonly IReadOnlyLargeCollection<TKey> chunkKeys;

		private readonly IAsyncFactory<TKey, IDisposableValue<TChunk>> chunkFactory;

		private readonly IChunkProcessor<TChunk> chunkProcessor;

		private readonly ActionBlock<TKey> processChunks;

		private readonly AsyncLongCountdownEvent chunksCompleted;

		public ChunkedPhase(
			IStageIdentity stageIdentity,
			IGenerationPhaseIdentity phaseIdentity,
			IReadOnlyLargeCollection<TKey> chunkKeys,
			IAsyncFactory<TKey, IDisposableValue<TChunk>> chunkFactory,
			IChunkProcessor<TChunk> chunkProcessor,
			GenerationOptions options = null)
			: base(options?.CancellationToken ?? DefaultCancellationToken)
		{
			Contracts.Requires.That(stageIdentity != null);
			Contracts.Requires.That(phaseIdentity != null);
			Contracts.Requires.That(chunkKeys != null);
			Contracts.Requires.That(chunkFactory != null);
			Contracts.Requires.That(chunkProcessor != null);

			this.StageIdentity = stageIdentity;
			this.PhaseIdentity = phaseIdentity;
			this.chunkKeys = chunkKeys;
			this.chunkFactory = chunkFactory;
			this.chunkProcessor = chunkProcessor;

			this.Progress = this.progress.AsObservable();

			var dataflowOptions = new ExecutionDataflowBlockOptions()
			{
				MaxDegreeOfParallelism = options?.MaxDegreeOfParallelism ?? DefaultMaxDegreeOfParallelism,
				CancellationToken = this.CancellationToken,
			};

			this.processChunks = new ActionBlock<TKey>(this.ProcessChunk, dataflowOptions);
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

				// initialize ChunkProcessor
				await this.chunkProcessor.InitializeAsync().DontMarshallContext();

				await this.processChunks.SendAllAsync(this.chunkKeys, cancellation).DontMarshallContext();

				// wait for all chunks to finish, then shut down the pipeline
				await this.chunksCompleted.WaitAsync().DontMarshallContext();
				await this.processChunks.CompleteAndAwaitAsync().DontMarshallContext();

				// complete ChunkProcessor
				await this.chunkProcessor.CompleteAsync().DontMarshallContext();

				this.progress.OnCompleted();
			}
			catch (Exception exception)
			{
				this.Fault(exception);
				this.progress.OnError(exception);
				throw;
			}
		}

		private async Task ProcessChunk(TKey chunkKey)
		{
			Contracts.Requires.That(chunkKey != null);

			try
			{
				using (var chunkPin = await this.chunkFactory.CreateAsync(chunkKey).DontMarshallContext())
				{
					this.chunkProcessor.ProcessChunk(chunkPin.Value);
				}

				this.chunksCompleted.TrySignal();
				this.progress.OnNext(
					new GenerationPhaseProgress(this, ProgressMessage, 1, this.ProgressTotalCount));
			}
			catch (Exception exception)
			{
				this.Fault(exception);
				throw;
			}
		}

		private void Fault(Exception exception)
		{
			((IDataflowBlock)this.processChunks).Fault(exception);
			this.chunksCompleted.TrySignalAllRemaining();
		}
	}
}
