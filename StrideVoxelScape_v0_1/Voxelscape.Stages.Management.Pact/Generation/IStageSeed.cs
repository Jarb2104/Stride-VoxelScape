using System.Collections.Generic;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Pact.Progress;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

namespace Voxelscape.Stages.Management.Pact.Generation
{
	/// <summary>
	///
	/// </summary>
	public interface IStageSeed :
		IStageIdentifiable, IAsyncCancelable, IObservableDiscreteProgress<GenerationPhaseProgress>
	{
		IReadOnlyList<IGenerationPhaseIdentity> GeneratingPhases { get; }

		// try to have the Completion task and observable Progress end in StageGenerationException
		// if there is an exception
	}
}
