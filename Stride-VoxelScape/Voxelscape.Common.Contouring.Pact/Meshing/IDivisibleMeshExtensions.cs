using System.Collections.Generic;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;

/// <summary>
/// Provides extension methods for <see cref="IDivisibleMesh{TVertex}"/>.
/// </summary>
public static class IDivisibleMeshExtensions
{
	public static bool IsEmpty<TVertex>(this IDivisibleMesh<TVertex> mesh)
		where TVertex : struct
	{
		Contracts.Requires.That(mesh != null);

		return mesh.Groups.Count == 0;
	}

	public static IReadOnlyCollection<int> GetTriangles<TVertex>(
		this IDivisibleMesh<TVertex> mesh, VertexWindingOrder winding)
		where TVertex : struct
	{
		Contracts.Requires.That(mesh != null);
		Contracts.Requires.That(winding.IsValidEnumValue());

		return new ReadOnlyCollection<int>(mesh.EnumerateTriangles(winding), mesh.Offsets.Count);
	}

	public static IEnumerable<int> EnumerateTriangles<TVertex>(
		this IDivisibleMesh<TVertex> mesh, VertexWindingOrder winding)
		where TVertex : struct
	{
		Contracts.Requires.That(mesh != null);
		Contracts.Requires.That(winding.IsValidEnumValue());

		var offsets = mesh.Offsets.GetEnumerator();
		int globalOffset = 0;

		switch (winding)
		{
			case VertexWindingOrder.Clockwise:
				foreach (var group in mesh.Groups)
				{
					for (int count = 0; count < group.Offsets; count++)
					{
						var success = offsets.MoveNext();
						Contracts.Assert.That(success);
						yield return globalOffset + offsets.Current;
					}

					globalOffset += group.Vertices;
				}

				yield break;

			case VertexWindingOrder.Counterclockwise:
				foreach (var group in mesh.Groups)
				{
					// IDivisibleMesh<TVertex> is always stored in clockwise winding order
					// so the last 2 offsets of each triangle need to be swapped to become counterclockwise
					for (int count = 0; count < group.Triangles; count++)
					{
						// return the first offset as normal
						var success = offsets.MoveNext();
						Contracts.Assert.That(success);
						yield return globalOffset + offsets.Current;

						// temporarily store the second offset
						success = offsets.MoveNext();
						Contracts.Assert.That(success);
						var tempOffset = offsets.Current;

						// return the third offset as though it were the second offset
						success = offsets.MoveNext();
						Contracts.Assert.That(success);
						yield return globalOffset + offsets.Current;

						// return the second offset (stored) as though it were the third offset
						yield return globalOffset + tempOffset;
					}

					globalOffset += group.Vertices;
				}

				yield break;

			default: throw InvalidEnumArgument.CreateException(nameof(winding), winding);
		}
	}
}
