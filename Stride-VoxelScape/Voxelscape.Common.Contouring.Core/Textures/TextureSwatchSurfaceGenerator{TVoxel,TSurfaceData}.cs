using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Common.Contouring.Pact.Textures;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Core.Textures
{
	public class TextureSwatchSurfaceGenerator<TVoxel, TSurfaceData> : ISurfaceValueGenerator<TVoxel, TSurfaceData, Vector2>
		where TVoxel : struct
		where TSurfaceData : class
	{
		private readonly ITextureSwatch textureSwatch;

		public TextureSwatchSurfaceGenerator(ITextureSwatch textureSwatch)
		{
			Contracts.Requires.That(textureSwatch != null);

			this.textureSwatch = textureSwatch;
		}

		#region ISurfaceValueGenerator<...> Members

		public void GenerateValues(
			IVoxelProjection<TVoxel, TSurfaceData> projection,
			out Vector2 topLeftValue,
			out Vector2 topRightValue,
			out Vector2 bottomLeftValue,
			out Vector2 bottomRightValue)
		{
			topLeftValue = this.textureSwatch.TopLeftCoordinate;
			topRightValue = this.textureSwatch.TopRightCoordinate;
			bottomLeftValue = this.textureSwatch.BottomLeftCoordinate;
			bottomRightValue = this.textureSwatch.BottomRightCoordinate;
		}

		#endregion
	}
}
