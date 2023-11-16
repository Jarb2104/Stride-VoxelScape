using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Pact.Textures;

namespace Voxelscape.Common.Contouring.Core.Textures
{
	/// <summary>
	///
	/// </summary>
	public class RectangleTextureSwatch : ITextureSwatch
	{
		private readonly float left;
		private readonly float right;
		private readonly float top;
		private readonly float bottom;

		public RectangleTextureSwatch(
			float left,
			float right,
			float top,
			float bottom)
		{
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		#region ITextureSwatch Members

		public Vector2 TopLeftCoordinate
		{
			get { return new Vector2(this.left, this.top); }
		}

		public Vector2 TopRightCoordinate
		{
			get { return new Vector2(this.right, this.top); }
		}

		public Vector2 BottomLeftCoordinate
		{
			get { return new Vector2(this.left, this.bottom); }
		}

		public Vector2 BottomRightCoordinate
		{
			get { return new Vector2(this.right, this.bottom); }
		}

		#endregion
	}
}
