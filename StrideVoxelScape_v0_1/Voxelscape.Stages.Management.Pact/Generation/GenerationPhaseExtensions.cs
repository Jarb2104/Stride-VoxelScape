using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides general purpose extension methods for generation phase types.
/// </summary>
public static class GenerationPhaseExtensions
{
	public static IObservable<FaultableValue<GenerationPhaseProgress, StageGenerationException>> WrapExceptions(
		this IObservable<GenerationPhaseProgress> progress,
		Func<Exception, StageGenerationException> wrapException)
	{
		Contracts.Requires.That(progress != null);
		Contracts.Requires.That(wrapException != null);

		return progress
			.Select(value => new FaultableValue<GenerationPhaseProgress, StageGenerationException>(value))
			.Catch((Exception error) => Observable.Return(
				new FaultableValue<GenerationPhaseProgress, StageGenerationException>(
					error as StageGenerationException ?? wrapException(error))));
	}

	public static IObservable<GenerationPhaseProgress> Accumulate(this IObservable<GenerationPhaseProgress> progress)
	{
		Contracts.Requires.That(progress != null);

		return progress.Scan((accumulate, next) => accumulate + next);
	}

	public static IReadOnlyList<IGenerationPhaseIdentity> GetIdentities(this IReadOnlyList<IGenerationPhase> phases)
	{
		Contracts.Requires.That(phases.AllAndSelfNotNull());

		return phases.Select(phase => phase.PhaseIdentity).ToReadOnlyList();
	}
}
