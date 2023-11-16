using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Core.LifeCycle;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

namespace Voxelscape.Stages.Management.Core.Generation
{
	/// <summary>
	///
	/// </summary>
	public class StageSeed : AbstractAsyncCancelable, IStageSeed
	{
		private readonly IReadOnlyList<ExceptionFilteredPhase> phasesToGenerate;

		private readonly IAsyncCompletable postGeneration;

		public StageSeed(IStageSeedConfig config)
			: this(config, new StageSeedOptions())
		{
		}

		public StageSeed(IStageSeedConfig config, StageSeedOptions options)
			: base(options?.CancellationToken ?? CancellationToken.None)
		{
			Contracts.Requires.That(config != null);
			Contracts.Requires.That(options != null);

			this.StageIdentity = config.StageIdentity;
			this.GeneratingPhases = config.PhasesToGenerate.GetIdentities();
			this.phasesToGenerate = config.PhasesToGenerate.AddExceptionFiltering();
			this.postGeneration = options.PostGeneration ?? new NullAsyncCompletable();

			this.ProgressTotalCount = this.phasesToGenerate.Select(phase => phase.ProgressTotalCount).Sum();

			// Enumerable.Aggregate is used to combine all the phases' progresses into a single observable
			this.Progress = this.phasesToGenerate.Aggregate(
				Observable.Empty<GenerationPhaseProgress>(),
				(progress, phase) => progress
					.Concat(Observable.Return(new GenerationPhaseProgress(
						phase, $"Initializing {phase.PhaseIdentity.Name}", 0, this.ProgressTotalCount)))
					.Concat(phase.Progress)
					.Concat(Observable.Return(new GenerationPhaseProgress(
						phase, $"Completed {phase.PhaseIdentity.Name}", 0, this.ProgressTotalCount))),
				progress => progress.Accumulate());
		}

		/// <inheritdoc />
		public IStageIdentity StageIdentity { get; }

		/// <inheritdoc />
		public IReadOnlyList<IGenerationPhaseIdentity> GeneratingPhases { get; }

		/// <inheritdoc />
		public IObservable<GenerationPhaseProgress> Progress { get; }

		/// <inheritdoc />
		public long ProgressTotalCount { get; }

		/// <inheritdoc />
		protected override async Task CompleteAsync(CancellationToken cancellation)
		{
			try
			{
				foreach (var phase in this.phasesToGenerate)
				{
					using (phase.LinkCancellation(cancellation))
					{
						await phase.CompleteAndAwaitAsync().DontMarshallContext();
					}
				}
			}
			finally
			{
				await this.postGeneration.CompleteAndAwaitAsync().DontMarshallContext();
			}
		}
	}
}
