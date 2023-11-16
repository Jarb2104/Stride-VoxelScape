using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Stride.Utility.Pact.Meshing;

/// <summary>
/// Provides extension methods for <see cref="IMeshData{TVertex}"/>.
/// </summary>
public static class IMeshDataExtensions
{
	public static bool IsEmpty<TVertex>(this IMeshData<TVertex> meshData)
		where TVertex : struct
	{
		Contracts.Requires.That(meshData != null);

		return meshData.VerticesCount == 0;
	}
}
