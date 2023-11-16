using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class ExteriorSkyIslandMapChunkFactory : IFactory<ChunkOverheadKey, ISkyIslandMapChunk>
	{
		private readonly IBoundedIndexable<Index2D, SkyIslandMaps> outOfBoundsChunk;

		public ExteriorSkyIslandMapChunkFactory(
			IRasterChunkConfig<Index2D> config, SkyIslandMaps outOfBoundsValue, bool modifyingThrowsException)
		{
			Contracts.Requires.That(config != null);

			this.outOfBoundsChunk = new ConstantArray2D<SkyIslandMaps>(
				config.Bounds.Dimensions, outOfBoundsValue, modifyingThrowsException);
		}

		/// <inheritdoc />
		public ISkyIslandMapChunk Create(ChunkOverheadKey key) => new SkyIslandMapChunk(key, this.outOfBoundsChunk);
	}
}
