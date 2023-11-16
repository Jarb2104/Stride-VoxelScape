using System;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Core.Progress;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Pact.Generation
{
	/// <summary>
	///
	/// </summary>
	public class GenerationPhaseProgress : DiscreteProgress, IGenerationPhaseIdentifiable
	{
		public GenerationPhaseProgress(
			IGenerationPhaseIdentifiable phase, string message, long countCompleted, long totalCount)
			: this(phase?.StageIdentity, phase?.PhaseIdentity, message, countCompleted, totalCount)
		{
			Contracts.Requires.That(phase != null);
		}

		public GenerationPhaseProgress(
			IStageIdentity stage, IGenerationPhaseIdentity phase, string message, long countCompleted, long totalCount)
			: base(countCompleted, totalCount)
		{
			Contracts.Requires.That(stage != null);
			Contracts.Requires.That(phase != null);
			Contracts.Requires.That(!message.IsNullOrWhiteSpace());

			this.StageIdentity = stage;
			this.PhaseIdentity = phase;
			this.Message = message;
		}

		/// <inheritdoc />
		public IStageIdentity StageIdentity { get; }

		/// <inheritdoc />
		public IGenerationPhaseIdentity PhaseIdentity { get; }

		public string Message { get; }

		public static GenerationPhaseProgress operator +(GenerationPhaseProgress acc, GenerationPhaseProgress next) =>
			new GenerationPhaseProgress(
				next.StageIdentity,
				next.PhaseIdentity,
				next.Message,
				acc.CountCompleted + next.CountCompleted,
				Math.Max(acc.TotalCount, next.TotalCount));

		/// <inheritdoc />
		public override string ToString() =>
			$"{this.StageIdentity} - {this.PhaseIdentity} - {this.Message} ({this.CountCompleted} of {this.TotalCount} ({this.GetPercentCompleted():P2}))";
	}
}
