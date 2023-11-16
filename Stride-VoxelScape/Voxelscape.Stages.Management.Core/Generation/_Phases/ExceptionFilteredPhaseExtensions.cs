using System.Collections.Generic;
using System.Linq;
using Voxelscape.Stages.Management.Core.Generation;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for <see cref="ExceptionFilteredPhase"/>.
/// </summary>
public static class ExceptionFilteredPhaseExtensions
{
	public static IReadOnlyList<ExceptionFilteredPhase> AddExceptionFiltering(
		this IReadOnlyList<IGenerationPhase> phases)
	{
		Contracts.Requires.That(phases.AllAndSelfNotNull());

		return phases.Select(
			phase => phase as ExceptionFilteredPhase ?? new ExceptionFilteredPhase(phase)).ToReadOnlyList();
	}
}
