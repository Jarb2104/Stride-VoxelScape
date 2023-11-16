using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Templates.CubeWorld
{
	/// <summary>
	///
	/// </summary>
	public class CubeOutlineVoxelGridChunkPopulator : IChunkPopulator<IVoxelGridChunk>
	{
		private readonly int bufferWidth;

		private readonly TerrainVoxel voxel;

		public CubeOutlineVoxelGridChunkPopulator(int bufferWidth, TerrainVoxel voxel)
		{
			Contracts.Requires.That(bufferWidth >= 0);

			this.bufferWidth = bufferWidth;
			this.voxel = voxel;
		}

		/// <inheritdoc />
		public void Populate(IVoxelGridChunk chunk)
		{
			IChunkPopulatorContracts.Populate(chunk);

			foreach (var pair in chunk.VoxelsLocalView)
			{
				chunk.VoxelsLocalView[pair.Key] = new TerrainVoxel(TerrainMaterial.Air, byte.MaxValue);
			}

			var start = this.bufferWidth;
			var end = chunk.VoxelsLocalView.Dimensions[0] - this.bufferWidth - 1;

			for (int count = start; count <= end; count++)
			{
				chunk.VoxelsLocalView[new Index3D(count, start, start)] = this.voxel;
				chunk.VoxelsLocalView[new Index3D(count, start, end)] = this.voxel;
				chunk.VoxelsLocalView[new Index3D(count, end, start)] = this.voxel;
				chunk.VoxelsLocalView[new Index3D(count, end, end)] = this.voxel;

				chunk.VoxelsLocalView[new Index3D(start, start, count)] = this.voxel;
				chunk.VoxelsLocalView[new Index3D(start, end, count)] = this.voxel;
				chunk.VoxelsLocalView[new Index3D(end, start, count)] = this.voxel;
				chunk.VoxelsLocalView[new Index3D(end, end, count)] = this.voxel;

				chunk.VoxelsLocalView[new Index3D(start, count, start)] = this.voxel;
				chunk.VoxelsLocalView[new Index3D(start, count, end)] = this.voxel;
				chunk.VoxelsLocalView[new Index3D(end, count, start)] = this.voxel;
				chunk.VoxelsLocalView[new Index3D(end, count, end)] = this.voxel;
			}
		}
	}
}
