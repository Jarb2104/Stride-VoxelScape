using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;

namespace Voxelscape.Stages.Voxels.Core.Templates.CubeWorld
{
	/// <summary>
	///
	/// </summary>
	public class CubeVoxelGridChunkPopulator : IChunkPopulator<IVoxelGridChunk>
	{
		private static readonly TerrainVoxel Air = new TerrainVoxel(TerrainMaterial.Air, byte.MaxValue);

		private readonly TerrainVoxel voxel;

		public CubeVoxelGridChunkPopulator(TerrainVoxel voxel)
		{
			this.voxel = voxel;
		}

		/// <inheritdoc />
		public void Populate(IVoxelGridChunk chunk)
		{
			IChunkPopulatorContracts.Populate(chunk);

			foreach (var pair in chunk.VoxelsLocalView)
			{
				chunk.VoxelsLocalView[pair.Key] = this.voxel;
			}

			var start = 0;
			var end = chunk.VoxelsLocalView.UpperBounds[0];

			for (int iX = start; iX <= end; iX++)
			{
				for (int iY = start; iY <= end; iY++)
				{
					chunk.VoxelsLocalView[new Index3D(iX, iY, start)] = Air;
					chunk.VoxelsLocalView[new Index3D(iX, iY, end)] = Air;

					chunk.VoxelsLocalView[new Index3D(iX, start, iY)] = Air;
					chunk.VoxelsLocalView[new Index3D(iX, end, iY)] = Air;

					chunk.VoxelsLocalView[new Index3D(start, iX, iY)] = Air;
					chunk.VoxelsLocalView[new Index3D(end, iX, iY)] = Air;
				}
			}
		}
	}
}
