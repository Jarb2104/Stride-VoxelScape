using System;
using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Core.Generation
{
	/// <summary>
	///
	/// </summary>
	public class GenerationPhaseDisposalWrapper : AbstractGenerationPhase
	{
		private readonly IDisposable disposable;

		public GenerationPhaseDisposalWrapper(IGenerationPhase phase, IDisposable disposable)
		{
			Contracts.Requires.That(phase != null);
			Contracts.Requires.That(disposable != null);

			this.Phase = phase;
			this.disposable = disposable;

			this.Completion = this.CompleteAsync();
		}

		/// <inheritdoc />
		public override Task Completion { get; }

		/// <inheritdoc />
		protected override IGenerationPhase Phase { get; }

		private async Task CompleteAsync()
		{
			try
			{
				await this.Phase.Completion.DontMarshallContext();
			}
			finally
			{
				this.disposable.Dispose();
			}
		}
	}
}
