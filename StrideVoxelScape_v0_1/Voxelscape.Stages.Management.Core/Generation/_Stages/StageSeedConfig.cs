using System.Collections.Generic;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Stages.Management.Pact.Stages;

namespace Voxelscape.Stages.Management.Core.Generation
{
	/// <summary>
	///
	/// </summary>
	public class StageSeedConfig : IStageSeedConfig
	{
		public StageSeedConfig(IStageIdentity stage, params IGenerationPhase[] phases)
			: this(stage, (IEnumerable<IGenerationPhase>)phases)
		{
		}

		public StageSeedConfig(IStageIdentity stage, IEnumerable<IGenerationPhase> phases)
		{
			IStageSeedConfigContracts.Constructor(stage, phases);

			this.StageIdentity = stage;
			this.PhasesToGenerate = phases.ToReadOnlyList();
		}

		/// <inheritdoc />
		public IStageIdentity StageIdentity { get; }

		/// <inheritdoc />
		public IReadOnlyList<IGenerationPhase> PhasesToGenerate { get; }
	}
}
