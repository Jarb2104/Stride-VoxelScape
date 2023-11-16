using System.Diagnostics;
using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Progress;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

namespace Voxelscape.Stages.Management.Pact.Generation
{
	/// <summary>
	///
	/// </summary>
	public interface IStageGenerator :
		IAsyncCancelable, IObservableProgress<FaultableValue<GenerationPhaseProgress, StageGenerationException>>
	{
		Task GenerateStageAsync(IStageSeed stageSeed);
	}

	/// <summary>
	/// Provides contracts for <see cref="IStageGenerator"/>.
	/// </summary>
	public static partial class IStageGeneratorContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void GenerateStageAsync(IStageSeed stageSeed)
		{
			Contracts.Requires.That(stageSeed != null);
		}
	}
}
