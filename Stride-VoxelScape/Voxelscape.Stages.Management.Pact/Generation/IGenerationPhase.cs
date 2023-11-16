using Voxelscape.Utility.Common.Pact.Progress;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

namespace Voxelscape.Stages.Management.Pact.Generation
{
	/// <summary>
	///
	/// </summary>
	public interface IGenerationPhase :
		IGenerationPhaseIdentifiable, IAsyncCancelable, IObservableDiscreteProgress<GenerationPhaseProgress>
	{
		// try to have the Completion task and observable Progress end in StageGenerationException
		// if there is an exception
	}
}
