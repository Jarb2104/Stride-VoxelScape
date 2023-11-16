using Voxelscape.Stages.Management.Pact.Stages;

namespace Voxelscape.Stages.Management.Pact.Generation
{
	/// <summary>
	///
	/// </summary>
	public interface IGenerationPhaseIdentifiable : IStageIdentifiable
	{
		IGenerationPhaseIdentity PhaseIdentity { get; }
	}
}
