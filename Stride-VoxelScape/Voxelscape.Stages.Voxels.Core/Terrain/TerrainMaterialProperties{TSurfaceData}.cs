using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Terrain
{
	public class TerrainMaterialProperties<TSurfaceData> : ITerrainMaterialProperties<TSurfaceData>
		where TSurfaceData : class
	{
		public TerrainMaterialProperties(
			bool isSolid,
			ISurfaceValueGenerator<TerrainVoxel, TSurfaceData, Color> colorer,
			ISurfaceValueGenerator<TerrainVoxel, TSurfaceData, Vector2> texturer)
		{
			Contracts.Requires.That(colorer != null);
			Contracts.Requires.That(texturer != null);

			this.IsSolid = isSolid;
			this.Colorer = colorer;
			this.Texturer = texturer;
		}

		/// <inheritdoc />
		public bool IsSolid { get; }

		/// <inheritdoc />
		public ISurfaceValueGenerator<TerrainVoxel, TSurfaceData, Color> Colorer { get; }

		/// <inheritdoc />
		public ISurfaceValueGenerator<TerrainVoxel, TSurfaceData, Vector2> Texturer { get; }
	}
}
