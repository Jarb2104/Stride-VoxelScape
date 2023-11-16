using Stride.Core.Mathematics;
using System.Collections.Generic;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Terrain
{
	public class TerrainMaterialSurfaceGenerator<TSurfaceData> :
		ISurfaceValueGenerator<TerrainVoxel, TSurfaceData, Color>,
		ISurfaceValueGenerator<TerrainVoxel, TSurfaceData, Vector2>
		where TSurfaceData : class
	{
		private readonly IReadOnlyDictionary<TerrainMaterial, ITerrainMaterialProperties<TSurfaceData>>
			propertiesTable;

		public TerrainMaterialSurfaceGenerator(
			IReadOnlyDictionary<TerrainMaterial, ITerrainMaterialProperties<TSurfaceData>> propertiesTable)
		{
			Contracts.Requires.That(propertiesTable != null);
			Contracts.Requires.That(EnumDictionary.ReadOnly.HasKeyForEachEnum(propertiesTable));

			this.propertiesTable = propertiesTable;
		}

		/// <inheritdoc />
		public void GenerateValues(
			IVoxelProjection<TerrainVoxel, TSurfaceData> projection,
			out Color topLeftValue,
			out Color topRightValue,
			out Color bottomLeftValue,
			out Color bottomRightValue)
		{
			this.propertiesTable[projection[Projection.Below].Material].Colorer.GenerateValues(
				projection,
				out topLeftValue,
				out topRightValue,
				out bottomLeftValue,
				out bottomRightValue);
		}

		/// <inheritdoc />
		public void GenerateValues(
			IVoxelProjection<TerrainVoxel, TSurfaceData> projection,
			out Vector2 topLeftValue,
			out Vector2 topRightValue,
			out Vector2 bottomLeftValue,
			out Vector2 bottomRightValue)
		{
			this.propertiesTable[projection[Projection.Below].Material].Texturer.GenerateValues(
				projection,
				out topLeftValue,
				out topRightValue,
				out bottomLeftValue,
				out bottomRightValue);
		}
	}
}
