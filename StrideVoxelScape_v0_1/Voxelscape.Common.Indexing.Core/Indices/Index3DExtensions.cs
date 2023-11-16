using Voxelscape.Common.Indexing.Core.Indices;

/// <summary>
/// Provides extension methods for <see cref="Index3D"/>.
/// </summary>
public static class CoreIndex3DExtensions
{
	public static Index2D ProjectDownYAxis(this Index3D index) => new Index2D(index.X, index.Z);
}
