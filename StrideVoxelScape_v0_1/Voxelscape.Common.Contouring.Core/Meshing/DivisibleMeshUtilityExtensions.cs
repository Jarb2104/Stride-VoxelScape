using System;
using System.Linq;
using Voxelscape.Common.Contouring.Core.Meshing;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for creating copies of <see cref="IDivisibleMesh{TVertex}"/>.
/// </summary>
public static class DivisibleMeshUtilityExtensions
{
	/// <summary>
	/// Creates a deep copy of the specified mesh.
	/// </summary>
	/// <typeparam name="TVertex">The type of the vertex.</typeparam>
	/// <param name="mesh">The mesh to copy.</param>
	/// <returns>A deep copy of the mesh.</returns>
	public static IDivisibleMesh<TVertex> Copy<TVertex>(this IDivisibleMesh<TVertex> mesh)
		where TVertex : struct
	{
		Contracts.Requires.That(mesh != null);

		return new DivisibleMesh<TVertex>(mesh.Groups.ToArray(), mesh.Offsets.ToArray(), mesh.Vertices.ToArray());
	}

	public static IDivisibleMesh<TResult> Convert<TSource, TResult>(
		this IDivisibleMesh<TSource> mesh, Converter<TSource, TResult> vertexConverter)
		where TSource : struct
		where TResult : struct
	{
		Contracts.Requires.That(mesh != null);
		Contracts.Requires.That(vertexConverter != null);

		return new DivisibleMesh<TResult>(
			mesh.Groups,
			mesh.Offsets,
			new ReadOnlyCollection<TResult>(mesh.Vertices.Convert(vertexConverter), mesh.Vertices.Count));
	}

	public static IDivisibleMesh<TResult> CopyConverted<TSource, TResult>(
		this IDivisibleMesh<TSource> mesh, Converter<TSource, TResult> vertexConverter)
		where TSource : struct
		where TResult : struct
	{
		Contracts.Requires.That(mesh != null);
		Contracts.Requires.That(vertexConverter != null);

		return new DivisibleMesh<TResult>(
			mesh.Groups.ToArray(),
			mesh.Offsets.ToArray(),
			mesh.Vertices.Convert(vertexConverter).ToCounted(mesh.Vertices.Count).ToArray());
	}
}
