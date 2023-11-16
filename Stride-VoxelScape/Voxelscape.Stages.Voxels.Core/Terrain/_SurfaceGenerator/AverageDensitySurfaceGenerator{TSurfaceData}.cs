using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Stages.Voxels.Pact.Voxels;

namespace Voxelscape.Stages.Voxels.Core.Terrain
{
	// TODO Steven - optimize the way this class passes voxel data around its methods
	// TODO Steven - this isn't a very good name... maybe something about vertex placement?
	public class AverageDensitySurfaceGenerator<TSurfaceData> :
		ISurfaceValueGenerator<TerrainVoxel, TSurfaceData, Vector3>
		where TSurfaceData : class
	{
		/// <inheritdoc />
		public void GenerateValues(
			IVoxelProjection<TerrainVoxel, TSurfaceData> projection,
			out Vector3 topLeftValue,
			out Vector3 topRightValue,
			out Vector3 bottomLeftValue,
			out Vector3 bottomRightValue)
		{
			topLeftValue = CreateTopLeft(projection);
			topRightValue = CreateTopRight(projection);
			bottomLeftValue = CreateBottomLeft(projection);
			bottomRightValue = CreateBottomRight(projection);
		}

		#region Private Static Methods

		private static Vector3 CreateTopLeft(
			IVoxelProjection<TerrainVoxel, TSurfaceData> projection)
		{
			Vector3 vertex = CalculateVertex(
				projection[Projection.TopLeftAbove],
				projection[Projection.TopLeftBelow],
				projection[Projection.TopAbove],
				projection[Projection.TopBelow],
				projection[Projection.LeftAbove],
				projection[Projection.LeftBelow],
				projection[Projection.Above],
				projection[Projection.Below]);

			return projection.ConvertToAbsoluteVectorPosition(vertex - new Vector3(1, 0, 1));
		}

		private static Vector3 CreateTopRight(
			IVoxelProjection<TerrainVoxel, TSurfaceData> projection)
		{
			Vector3 vertex = CalculateVertex(
				projection[Projection.TopAbove],
				projection[Projection.TopBelow],
				projection[Projection.TopRightAbove],
				projection[Projection.TopRightBelow],
				projection[Projection.Above],
				projection[Projection.Below],
				projection[Projection.RightAbove],
				projection[Projection.RightBelow]);

			return projection.ConvertToAbsoluteVectorPosition(vertex - new Vector3(0, 0, 1));
		}

		private static Vector3 CreateBottomLeft(
			IVoxelProjection<TerrainVoxel, TSurfaceData> projection)
		{
			Vector3 vertex = CalculateVertex(
				projection[Projection.LeftAbove],
				projection[Projection.LeftBelow],
				projection[Projection.Above],
				projection[Projection.Below],
				projection[Projection.BotLeftAbove],
				projection[Projection.BotLeftBelow],
				projection[Projection.BotAbove],
				projection[Projection.BotBelow]);

			return projection.ConvertToAbsoluteVectorPosition(vertex - new Vector3(1, 1, 1));
		}

		private static Vector3 CreateBottomRight(
			IVoxelProjection<TerrainVoxel, TSurfaceData> projection)
		{
			Vector3 vertex = CalculateVertex(
				projection[Projection.Above],
				projection[Projection.Below],
				projection[Projection.RightAbove],
				projection[Projection.RightBelow],
				projection[Projection.BotAbove],
				projection[Projection.BotBelow],
				projection[Projection.BotRightAbove],
				projection[Projection.BotRightBelow]);

			return projection.ConvertToAbsoluteVectorPosition(vertex - new Vector3(0, 1, 1));
		}

		private static Vector3 CalculateVertex(
			TerrainVoxel tlAbove,
			TerrainVoxel tlBelow,
			TerrainVoxel trAbove,
			TerrainVoxel trBelow,
			TerrainVoxel blAbove,
			TerrainVoxel blBelow,
			TerrainVoxel brAbove,
			TerrainVoxel brBelow)
		{
			float x = CalculateAxis(tlAbove, trAbove, tlBelow, trBelow, blAbove, brAbove, blBelow, brBelow);
			float y = CalculateAxis(blAbove, tlAbove, brAbove, trAbove, blBelow, tlBelow, brBelow, trBelow);
			float z = CalculateAxis(tlAbove, tlBelow, trAbove, trBelow, blAbove, blBelow, brAbove, brBelow);
			return new Vector3(x, y, z);
		}

		private static float CalculateAxis(
			TerrainVoxel negativeEdgeA,
			TerrainVoxel positiveEdgeA,
			TerrainVoxel negativeEdgeB,
			TerrainVoxel positiveEdgeB,
			TerrainVoxel negativeEdgeC,
			TerrainVoxel positiveEdgeC,
			TerrainVoxel negativeEdgeD,
			TerrainVoxel positiveEdgeD)
		{
			float sum =
				CalculateEdge(negativeEdgeA, positiveEdgeA) +
				CalculateEdge(negativeEdgeB, positiveEdgeB) +
				CalculateEdge(negativeEdgeC, positiveEdgeC) +
				CalculateEdge(negativeEdgeD, positiveEdgeD);

			return sum / 4f;
		}

		private static float CalculateEdge(TerrainVoxel negativeEnd, TerrainVoxel positiveEnd)
		{
			return ((float)negativeEnd.Density) / ((float)positiveEnd.Density + negativeEnd.Density);
		}

		#endregion
	}
}
