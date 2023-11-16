using Voxelscape.Stages.Management.Core.Generation;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapStatsPhase : AbstractGenerationPhase
	{
		public SkyIslandMapStatsPhase(
			IStageIdentity stageIdentity,
			IStageBounds stageBounds,
			IAsyncFactory<ChunkOverheadKey, IDisposableValue<IReadOnlySkyIslandMapChunk>> chunkFactory,
			IKeyValueStore statsStore,
			GenerationOptions options = null)
		{
			Contracts.Requires.That(stageIdentity != null);
			Contracts.Requires.That(stageBounds != null);
			Contracts.Requires.That(chunkFactory != null);
			Contracts.Requires.That(statsStore != null);

			var phaseIdentity = new GenerationPhaseIdentity(nameof(SkyIslandMapStatsPhase));
			var chunkKeys = new ChunkOverheadKeyCollection(stageBounds);
			var chunkProcessor = new SkyIslandMapChunkStatsProcessor(statsStore);

			this.Phase = new ChunkedPhase<ChunkOverheadKey, IReadOnlySkyIslandMapChunk>(
				stageIdentity,
				phaseIdentity,
				chunkKeys,
				chunkFactory,
				chunkProcessor,
				options);
		}

		/// <inheritdoc />
		protected override IGenerationPhase Phase { get; }
	}
}
