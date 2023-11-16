using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Pact.Generation
{
	/// <summary>
	///
	/// </summary>
	public interface IStageSeedConfig : IStageIdentifiable
	{
		IReadOnlyList<IGenerationPhase> PhasesToGenerate { get; }
	}

	public static class IStageSeedConfigContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Constructor(IStageIdentity stage, IEnumerable<IGenerationPhase> phases)
		{
			Contracts.Requires.That(stage != null);
			Contracts.Requires.That(phases.AllAndSelfNotNull());

			foreach (var phase in phases)
			{
				Contracts.Requires.That(
					phase.StageIdentity.Equals(stage), "All phases must belong to the same stage.");
			}
		}
	}
}
