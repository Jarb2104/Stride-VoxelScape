using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Core.Generation
{
	/// <summary>
	///
	/// </summary>
	public class GenerationPhaseWrapper : AbstractGenerationPhase
	{
		public GenerationPhaseWrapper(IGenerationPhase phase)
		{
			Contracts.Requires.That(phase != null);

			this.Phase = phase;
		}

		/// <inheritdoc />
		protected override IGenerationPhase Phase { get; }
	}
}
