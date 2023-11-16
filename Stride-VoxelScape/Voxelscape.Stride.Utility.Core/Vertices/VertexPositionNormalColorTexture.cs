using System;
using System.Runtime.InteropServices;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Stride.Utility.Pact.Vertices;
using Stride.Core.Mathematics;
using Stride.Graphics;

namespace Voxelscape.Stride.Utility.Core.Vertices
{
	/// <summary>
	/// Describes a custom vertex format structure that contains position, normal and color information.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct VertexPositionNormalColorTexture : IEquatable<VertexPositionNormalColorTexture>, IVertex
	{
		/// <summary>
		/// XYZ position.
		/// </summary>
		public Vector3 Position;

		/// <summary>
		/// The vertex normal.
		/// </summary>
		public Vector3 Normal;

		/// <summary>
		/// The color.
		/// </summary>
		public Color Color;

		/// <summary>
		/// UV texture coordinates.
		/// </summary>
		public Vector2 TextureCoordinate;

		/// <summary>
		/// Initializes a new instance of the <see cref="VertexPositionNormalColorTexture"/> struct.
		/// </summary>
		/// <param name="position">The position of this vertex.</param>
		/// <param name="normal">The vertex normal.</param>
		/// <param name="color">the color.</param>
		/// <param name="textureCoordinate">The UV texture coordinate.</param>
		public VertexPositionNormalColorTexture(
			Vector3 position, Vector3 normal, Color color, Vector2 textureCoordinate)
		{
			this.Position = position;
			this.Normal = normal;
			this.Color = color;
			this.TextureCoordinate = textureCoordinate;
		}

		public static IVertexFormat<VertexPositionNormalColorTexture> Format { get; } = new VertexFormat();

		public static bool operator ==(VertexPositionNormalColorTexture left, VertexPositionNormalColorTexture right) =>
			left.Equals(right);

		public static bool operator !=(VertexPositionNormalColorTexture left, VertexPositionNormalColorTexture right) =>
			!left.Equals(right);

		/// <inheritdoc />
		public bool Equals(VertexPositionNormalColorTexture other) =>
			this.Position == other.Position &&
			this.Normal == other.Normal &&
			this.Color == other.Color &&
			this.TextureCoordinate == other.TextureCoordinate;

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = this.Position.GetHashCode();
				hashCode = (hashCode * 397) ^ this.Normal.GetHashCode();
				hashCode = (hashCode * 397) ^ this.Color.GetHashCode();
				hashCode = (hashCode * 397) ^ this.TextureCoordinate.GetHashCode();
				return hashCode;
			}
		}

		/// <inheritdoc />
		public VertexDeclaration GetLayout() => Format.Layout;

		/// <inheritdoc />
		public void FlipWinding() => this.TextureCoordinate.X = 1.0f - this.TextureCoordinate.X;

		/// <inheritdoc />
		public override string ToString() =>
			$"Position: {this.Position}, Normal: {this.Normal}, Color: {this.Color}, TexCoord: {this.TextureCoordinate}";

		private class VertexFormat : IVertexFormat<VertexPositionNormalColorTexture>
		{
			/// <inheritdoc />
			public VertexDeclaration Layout { get; } = new VertexDeclaration(
				VertexElement.Position<Vector3>(),
				VertexElement.Normal<Vector3>(),
				VertexElement.Color<Color>(),
				VertexElement.TextureCoordinate<Vector2>());

			/// <inheritdoc />
			public Vector3 GetPosition(VertexPositionNormalColorTexture vertex) => vertex.Position;
		}
	}
}
