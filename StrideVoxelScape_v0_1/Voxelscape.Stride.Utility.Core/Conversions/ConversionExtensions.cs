using Voxelscape.Common.Contouring.Core.Vertices;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Stride.Utility.Core.Vertices;
using Stride.Core.Mathematics;
using MonoColor = Stride.Core.Mathematics.Color;
using MonoVector2 = Stride.Core.Mathematics.Vector2;
using MonoVector3 = Stride.Core.Mathematics.Vector3;
using MonoVector4 = Stride.Core.Mathematics.Vector4;

/// <summary>
/// Provides extension methods for converting basic types.
/// </summary>
public static class StrideConversionExtensions
{
	public static Vector2 ToStrideVector(this Index2D index) => new Vector2(index.X, index.Y);

	public static Vector3 ToStrideVector(this Index3D index) => new Vector3(index.X, index.Y, index.Z);

	public static Vector4 ToStrideVector(this Index4D index) => new Vector4(index.X, index.Y, index.Z, index.W);

	public static MonoColor ToMono(this Color color) => new MonoColor(color.R, color.G, color.B, color.A);

	public static Color ToStride(this MonoColor color) => new Color(color.R, color.G, color.B, color.A);

	public static MonoVector2 ToMono(this Vector2 vector) => new MonoVector2(vector.X, vector.Y);

	public static Vector2 ToStride(this MonoVector2 vector) => new Vector2(vector.X, vector.Y);

	public static MonoVector3 ToMono(this Vector3 vector) => new MonoVector3(vector.X, vector.Y, vector.Z);

	public static Vector3 ToStride(this MonoVector3 vector) => new Vector3(vector.X, vector.Y, vector.Z);

	public static MonoVector4 ToMono(this Vector4 vector) => new MonoVector4(vector.X, vector.Y, vector.Z, vector.W);

	public static Vector4 ToStride(this MonoVector4 vector) => new Vector4(vector.X, vector.Y, vector.Z, vector.W);

	public static NormalColorTextureVertex ToMono(this VertexPositionNormalColorTexture vertex) =>
		new NormalColorTextureVertex(
			vertex.Position.ToMono(),
			vertex.Normal.ToMono(),
			vertex.Color.ToMono(),
			vertex.TextureCoordinate.ToMono());

	public static VertexPositionNormalColorTexture ToStride(this NormalColorTextureVertex vertex) =>
		new VertexPositionNormalColorTexture(
			vertex.Position.ToStride(),
			vertex.Normal.ToStride(),
			vertex.Color.ToStride(),
			vertex.TextureCoordinate.ToStride());
}
