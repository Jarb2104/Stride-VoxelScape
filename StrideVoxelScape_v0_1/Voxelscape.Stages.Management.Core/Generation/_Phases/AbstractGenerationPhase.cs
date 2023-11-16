using System;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Stages.Management.Pact.Stages;

namespace Voxelscape.Stages.Management.Core.Generation
{
	/// <summary>
	///
	/// </summary>
	public abstract class AbstractGenerationPhase : IGenerationPhase
	{
		/// <inheritdoc />
		public virtual IGenerationPhaseIdentity PhaseIdentity => this.Phase.PhaseIdentity;

		/// <inheritdoc />
		public virtual IStageIdentity StageIdentity => this.Phase.StageIdentity;

		/// <inheritdoc />
		public virtual long ProgressTotalCount => this.Phase.ProgressTotalCount;

		/// <inheritdoc />
		public virtual IObservable<GenerationPhaseProgress> Progress => this.Phase.Progress;

		/// <inheritdoc />
		public virtual CancellationToken CancellationToken => this.Phase.CancellationToken;

		/// <inheritdoc />
		public virtual Task Completion => this.Phase.Completion;

		protected abstract IGenerationPhase Phase { get; }

		/// <inheritdoc />
		public virtual void Cancel() => this.Phase.Cancel();

		/// <inheritdoc />
		public virtual void Complete() => this.Phase.Complete();
	}
}
