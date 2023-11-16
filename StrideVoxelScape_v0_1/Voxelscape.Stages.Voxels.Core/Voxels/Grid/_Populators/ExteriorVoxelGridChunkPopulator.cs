using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;

namespace Voxelscape.Stages.Voxels.Core.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public class ExteriorVoxelGridChunkPopulator : IChunkPopulator<IVoxelGridChunk>
	{
		private readonly TerrainVoxel outOfBoundsValue;

		public ExteriorVoxelGridChunkPopulator(TerrainVoxel outOfBoundsValue)
		{
			this.outOfBoundsValue = outOfBoundsValue;
		}

		/// <inheritdoc />
		public void Populate(IVoxelGridChunk chunk)
		{
			IChunkPopulatorContracts.Populate(chunk);

			foreach (var pair in chunk.VoxelsLocalView)
			{
				chunk.VoxelsLocalView[pair.Key] = this.outOfBoundsValue;
			}
		}
	}
}
