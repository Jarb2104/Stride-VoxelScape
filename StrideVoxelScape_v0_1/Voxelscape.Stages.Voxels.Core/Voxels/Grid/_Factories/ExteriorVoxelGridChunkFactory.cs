using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Stages.Voxels.Core.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public class ExteriorVoxelGridChunkFactory : IFactory<ChunkKey, IVoxelGridChunk>
	{
		private readonly IBoundedIndexable<Index3D, TerrainVoxel> outOfBoundsChunk;

		public ExteriorVoxelGridChunkFactory(
			IRasterChunkConfig<Index3D> config, TerrainVoxel outOfBoundsValue, bool modifyingThrowsException)
		{
			Contracts.Requires.That(config != null);

			this.outOfBoundsChunk = new ConstantArray3D<TerrainVoxel>(
				config.Bounds.Dimensions, outOfBoundsValue, modifyingThrowsException);
		}

		/// <inheritdoc />
		public IVoxelGridChunk Create(ChunkKey key) => new VoxelGridChunk(key, this.outOfBoundsChunk);
	}
}
