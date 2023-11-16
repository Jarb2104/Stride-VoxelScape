using Stride.Core.Mathematics;
using Voxelscape.Common.Indexing.Core.Indices;

/// <summary>
/// Provides extension methods for <see cref="Index4D"/>.
/// </summary>
public static class Index4DExtensions
{
	public static Vector4 ToVector(this Index4D index) => new Vector4(index.X, index.Y, index.Z, index.W);
}
