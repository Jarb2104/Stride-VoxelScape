using Stride.Core.Mathematics;
using Voxelscape.Common.Indexing.Core.Indices;

/// <summary>
/// Provides extension methods for <see cref="Index2D"/>.
/// </summary>
public static class Index2DExtensions
{
	public static Vector2 ToVector(this Index2D index) => new Vector2(index.X, index.Y);
}
