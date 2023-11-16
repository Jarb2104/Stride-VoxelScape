using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Core.DualContouring
{
	public class VertexSurfaceWriter<TVoxel, TSurfaceData> : ISurfaceValueGenerator<TVoxel, TSurfaceData, Vector3>
		where TVoxel : struct
		where TSurfaceData : class, IVertexWritable
	{
		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, Vector3> generator;

		public VertexSurfaceWriter(ISurfaceValueGenerator<TVoxel, TSurfaceData, Vector3> generator)
		{
			Contracts.Requires.That(generator != null);

			this.generator = generator;
		}

		#region ISurfaceValueGenerator<...> Members

		public void GenerateValues(
			IVoxelProjection<TVoxel, TSurfaceData> projection,
			out Vector3 topLeftValue,
			out Vector3 topRightValue,
			out Vector3 bottomLeftValue,
			out Vector3 bottomRightValue)
		{
			this.generator.GenerateValues(
				projection,
				out topLeftValue,
				out topRightValue,
				out bottomLeftValue,
				out bottomRightValue);

			projection.SurfaceData.TopLeftVertex = topLeftValue;
			projection.SurfaceData.TopRightVertex = topRightValue;
			projection.SurfaceData.BottomLeftVertex = bottomLeftValue;
			projection.SurfaceData.BottomRightVertex = bottomRightValue;
		}

		#endregion
	}
}
