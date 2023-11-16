using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Stages.Voxels.Pact.Voxels;

namespace Voxelscape.Stages.Voxels.Pact.Terrain
{
	public interface ITerrainMaterialProperties<TSurfaceData>
		where TSurfaceData : class
	{
		bool IsSolid { get; }

		ISurfaceValueGenerator<TerrainVoxel, TSurfaceData, Color> Colorer { get; }

		ISurfaceValueGenerator<TerrainVoxel, TSurfaceData, Vector2> Texturer { get; }
	}
}
