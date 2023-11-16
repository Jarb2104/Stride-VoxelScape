using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Core.DualContouring
{
	public class ColorSurfaceWriter<TVoxel, TSurfaceData> : ISurfaceValueGenerator<TVoxel, TSurfaceData, Color>
		where TVoxel : struct
		where TSurfaceData : class, IColorWritable
	{
		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, Color> generator;

		public ColorSurfaceWriter(ISurfaceValueGenerator<TVoxel, TSurfaceData, Color> generator)
		{
			Contracts.Requires.That(generator != null);

			this.generator = generator;
		}

		#region ISurfaceValueGenerator<...> Members

		public void GenerateValues(
			IVoxelProjection<TVoxel, TSurfaceData> projection,
			out Color topLeftValue,
			out Color topRightValue,
			out Color bottomLeftValue,
			out Color bottomRightValue)
		{
			this.generator.GenerateValues(
				projection,
				out topLeftValue,
				out topRightValue,
				out bottomLeftValue,
				out bottomRightValue);

			projection.SurfaceData.TopLeftColor = topLeftValue;
			projection.SurfaceData.TopRightColor = topRightValue;
			projection.SurfaceData.BottomLeftColor = bottomLeftValue;
			projection.SurfaceData.BottomRightColor = bottomRightValue;
		}

		#endregion
	}
}
