using System.Collections.Generic;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Terrain
{
	public class TerrainContourDeterminer<TSurfaceData> : IContourDeterminer<TerrainVoxel>
		where TSurfaceData : class
	{
		private readonly IReadOnlyDictionary<TerrainMaterial, ITerrainMaterialProperties<TSurfaceData>>
			voxelPropertiesTable;

		public TerrainContourDeterminer(
			IReadOnlyDictionary<TerrainMaterial, ITerrainMaterialProperties<TSurfaceData>> voxelPropertiesTable)
		{
			Contracts.Requires.That(voxelPropertiesTable != null);

			this.voxelPropertiesTable = voxelPropertiesTable;
		}

		/// <inheritdoc />
		public ContourFacingDirection DetermineContour(TerrainVoxel towardsNegative, TerrainVoxel towardsPositive)
		{
			bool isTowardsNegativeSolid = this.voxelPropertiesTable[towardsNegative.Material].IsSolid;
			bool isTowardsPositiveSolid = this.voxelPropertiesTable[towardsPositive.Material].IsSolid;

			if (isTowardsNegativeSolid == isTowardsPositiveSolid)
			{
				// if both are filled or both are not filled
				return ContourFacingDirection.None;
			}

			return isTowardsNegativeSolid ?
				ContourFacingDirection.TowardsPositive : ContourFacingDirection.TowardsNegative;
		}
	}
}
