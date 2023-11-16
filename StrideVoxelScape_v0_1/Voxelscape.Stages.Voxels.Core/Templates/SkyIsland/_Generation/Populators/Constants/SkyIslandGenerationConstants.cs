using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Stages.Voxels.Pact.Voxels;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public static class SkyIslandGenerationConstants
	{
		// max density air is required in order to gradiate smoothly from the island voxels
		public static TerrainVoxel EmptyAir { get; } = new TerrainVoxel(TerrainMaterial.Air, byte.MaxValue);
	}
}
