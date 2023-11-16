using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Core.DualContouring
{
	public class TextureSurfaceWriter<TVoxel, TSurfaceData> : ISurfaceValueGenerator<TVoxel, TSurfaceData, Vector2>
		where TVoxel : struct
		where TSurfaceData : class, ITextureWritable
	{
		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, Vector2> generator;

		public TextureSurfaceWriter(ISurfaceValueGenerator<TVoxel, TSurfaceData, Vector2> generator)
		{
			Contracts.Requires.That(generator != null);

			this.generator = generator;
		}

		#region ISurfaceValueGenerator<...> Members

		public void GenerateValues(
			IVoxelProjection<TVoxel, TSurfaceData> projection,
			out Vector2 topLeftValue,
			out Vector2 topRightValue,
			out Vector2 bottomLeftValue,
			out Vector2 bottomRightValue)
		{
			this.generator.GenerateValues(
				projection,
				out topLeftValue,
				out topRightValue,
				out bottomLeftValue,
				out bottomRightValue);

			projection.SurfaceData.TopLeftTexture = topLeftValue;
			projection.SurfaceData.TopRightTexture = topRightValue;
			projection.SurfaceData.BottomLeftTexture = bottomLeftValue;
			projection.SurfaceData.BottomRightTexture = bottomRightValue;
		}

		#endregion
	}
}
