using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Common.MonoGame.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Core.DualContouring
{
	public class AreaThresholdSurfaceGenerator<TVoxel, TSurfaceData, TValue> :
		ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue>
		where TVoxel : struct
		where TSurfaceData : class, IDiagonalReadable, IVertexReadable, ISurfaceAreaWritable
		where TValue : struct
	{
		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> aboveThresholdGenerator;

		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> belowThresholdGenerator;

		private readonly float threshold;

		public AreaThresholdSurfaceGenerator(
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> aboveThresholdGenerator,
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> belowThresholdGenerator,
			float threshold)
		{
			Contracts.Requires.That(aboveThresholdGenerator != null);
			Contracts.Requires.That(belowThresholdGenerator != null);
			Contracts.Requires.That(threshold >= 0);

			this.aboveThresholdGenerator = aboveThresholdGenerator;
			this.belowThresholdGenerator = belowThresholdGenerator;
			this.threshold = threshold;
		}

		#region ISurfaceValueGenerator<...> Members

		public void GenerateValues(
			IVoxelProjection<TVoxel, TSurfaceData> projection,
			out TValue topLeftValue,
			out TValue topRightValue,
			out TValue bottomLeftValue,
			out TValue bottomRightValue)
		{
			if (projection.SurfaceData.Diagonal == QuadDiagonal.Ascending)
			{
				projection.SurfaceData.SurfaceArea =
					PolyMath.GetArea(
						projection.SurfaceData.TopLeftVertex,
						projection.SurfaceData.BottomLeftVertex,
						projection.SurfaceData.TopRightVertex) +
					PolyMath.GetArea(
						projection.SurfaceData.BottomLeftVertex,
						projection.SurfaceData.BottomRightVertex,
						projection.SurfaceData.TopRightVertex);
			}
			else
			{
				projection.SurfaceData.SurfaceArea =
					PolyMath.GetArea(
						projection.SurfaceData.TopLeftVertex,
						projection.SurfaceData.BottomLeftVertex,
						projection.SurfaceData.BottomRightVertex) +
					PolyMath.GetArea(
						projection.SurfaceData.TopLeftVertex,
						projection.SurfaceData.BottomRightVertex,
						projection.SurfaceData.TopRightVertex);
			}

			if (projection.SurfaceData.SurfaceArea >= this.threshold)
			{
				this.aboveThresholdGenerator.GenerateValues(
					projection,
					out topLeftValue,
					out topRightValue,
					out bottomLeftValue,
					out bottomRightValue);
			}
			else
			{
				this.belowThresholdGenerator.GenerateValues(
					projection,
					out topLeftValue,
					out topRightValue,
					out bottomLeftValue,
					out bottomRightValue);
			}
		}

		#endregion
	}
}
