using Stride.Core.Mathematics;

namespace Voxelscape.Common.Contouring.Core.Vertices
{
	/// <summary>
	///
	/// </summary>
	public struct NormalColorTextureVertex
	{
		public NormalColorTextureVertex(
			Vector3 position, Vector3 normal, Color color, Vector2 textureCoordinate)
		{
			this.Position = position;
			this.Normal = normal;
			this.Color = color;
			this.TextureCoordinate = textureCoordinate;
		}

		public Vector3 Position { get; }

		public Vector3 Normal { get; }

		public Color Color { get; }

		public Vector2 TextureCoordinate { get; }
	}
}
