using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Core.Generation
{
	/// <summary>
	///
	/// </summary>
	public class ExceptionFilteredPhase : IGenerationPhase
	{
		private readonly IGenerationPhase phase;

		public ExceptionFilteredPhase(IGenerationPhase phase)
		{
			Contracts.Requires.That(phase != null);

			this.phase = phase;

			this.Progress = this.phase.Progress.Catch((Exception error) => Observable.Throw<GenerationPhaseProgress>(
				error as StageGenerationException ?? new StageGenerationException(this, this.ErrorMessage, error)));

			this.Completion = this.CompleteAsync();
		}

		/// <inheritdoc />
		public IStageIdentity StageIdentity => this.phase.StageIdentity;

		/// <inheritdoc />
		public IGenerationPhaseIdentity PhaseIdentity => this.phase.PhaseIdentity;

		/// <inheritdoc />
		public IObservable<GenerationPhaseProgress> Progress { get; }

		/// <inheritdoc />
		public long ProgressTotalCount => this.phase.ProgressTotalCount;

		/// <inheritdoc />
		public CancellationToken CancellationToken => this.phase.CancellationToken;

		/// <inheritdoc />
		public Task Completion { get; }

		private string ErrorMessage => $"Exception generating {this.PhaseIdentity.Name}";

		/// <inheritdoc />
		public void Complete() => this.phase.Complete();

		/// <inheritdoc />
		public void Cancel() => this.phase.Cancel();

		private async Task CompleteAsync()
		{
			try
			{
				await this.phase.Completion.DontMarshallContext();
			}
			catch (Exception error)
			{
				if (error is StageGenerationException)
				{
					// rethrow without losing the stack
					throw;
				}

				throw new StageGenerationException(this, this.ErrorMessage, error);
			}
		}
	}
}
