using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Pact.Textures;

namespace Voxelscape.Common.Contouring.Core.Textures
{
	/// <summary>
	///
	/// </summary>
	public class TextureSwatch : ITextureSwatch
	{
		private readonly Vector2 topLeftCoordinate;

		private readonly Vector2 topRightCoordinate;

		private readonly Vector2 bottomLeftCoordinate;

		private readonly Vector2 bottomRightCoordinate;

		public TextureSwatch(
			Vector2 topLeftCoordinate,
			Vector2 topRightCoordinate,
			Vector2 bottomLeftCoordinate,
			Vector2 bottomRightCoordinate)
		{
			this.topLeftCoordinate = topLeftCoordinate;
			this.topRightCoordinate = topRightCoordinate;
			this.bottomLeftCoordinate = bottomLeftCoordinate;
			this.bottomRightCoordinate = bottomRightCoordinate;
		}

		#region ITextureSwatch Members

		public Vector2 TopLeftCoordinate
		{
			get { return this.topLeftCoordinate; }
		}

		public Vector2 TopRightCoordinate
		{
			get { return this.topRightCoordinate; }
		}

		public Vector2 BottomLeftCoordinate
		{
			get { return this.bottomLeftCoordinate; }
		}

		public Vector2 BottomRightCoordinate
		{
			get { return this.bottomRightCoordinate; }
		}

		#endregion
	}
}
