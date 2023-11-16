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
	public class SkyIslandMapImagesPhase : AbstractGenerationPhase
	{
		public SkyIslandMapImagesPhase(
			IStageIdentity stageIdentity,
			IStageBounds stageBounds,
			IAsyncFactory<ChunkOverheadKey, IDisposableValue<IReadOnlySkyIslandMapChunk>> chunkFactory,
			IKeyValueStore statsStore,
			SkyIslandMapImagesOptions imageOptions = null,
			GenerationOptions generationOptions = null)
		{
			Contracts.Requires.That(stageIdentity != null);
			Contracts.Requires.That(stageBounds != null);
			Contracts.Requires.That(chunkFactory != null);
			Contracts.Requires.That(statsStore != null);

			var phaseIdentity = new GenerationPhaseIdentity(nameof(SkyIslandMapImagesPhase));
			var chunkKeys = new ChunkOverheadKeyCollection(stageBounds);

			var chunkProcessor = new SkyIslandMapChunkImagesProcessor(stageBounds, statsStore, imageOptions);

			this.Phase = new ChunkedPhase<ChunkOverheadKey, IReadOnlySkyIslandMapChunk>(
				stageIdentity,
				phaseIdentity,
				chunkKeys,
				chunkFactory,
				chunkProcessor,
				generationOptions);
		}

		/// <inheritdoc />
		protected override IGenerationPhase Phase { get; }
	}
}
