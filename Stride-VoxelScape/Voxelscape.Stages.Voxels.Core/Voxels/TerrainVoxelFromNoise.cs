using System;
using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Stages.Voxels.Pact.Voxels;

namespace Voxelscape.Stages.Voxels.Core.Voxels
{
	/// <summary>
	///
	/// </summary>
	public static class TerrainVoxelFromNoise
	{
		public static TerrainVoxel New(TerrainMaterial material, double noise)
		{
			if (noise <= 0)
			{
				material = TerrainMaterial.Air;
			}

			byte density = (byte)((int)(Math.Abs(noise) * byte.MaxValue)).Clamp(1, byte.MaxValue);

			return new TerrainVoxel(material, density);
		}
	}
}
