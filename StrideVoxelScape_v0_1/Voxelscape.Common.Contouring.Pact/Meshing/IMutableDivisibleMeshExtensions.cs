using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;

/// <summary>
/// Provides extension methods for <see cref="IMutableDivisibleMesh{TVertex}"/>.
/// </summary>
public static class IMutableDivisibleMeshExtensions
{
	// always specify the vertices in clockwise order!
	public static void AddTriangle<TVertex>(
		this IMutableDivisibleMesh<TVertex> builder, TVertex a, TVertex b, TVertex c)
		where TVertex : struct
	{
		Contracts.Requires.That(builder != null);

		using (var group = builder.AddTriangleGroup(new TriangleGroup(triangles: 1, vertices: 3)))
		{
			byte offA = group.AddVertex(a);
			byte offB = group.AddVertex(b);
			byte offC = group.AddVertex(c);
			group.AddTriangleOffsets(offA, offB, offC);
		}
	}

	public static void AddFlatQuad<TVertex>(
		this IMutableDivisibleMesh<TVertex> builder,
		TVertex topLeft,
		TVertex topRight,
		TVertex bottomLeft,
		TVertex bottomRight,
		QuadDiagonal diagonal)
		where TVertex : struct
	{
		Contracts.Requires.That(builder != null);

		using (var group = builder.AddTriangleGroup(new TriangleGroup(triangles: 2, vertices: 4)))
		{
			byte offTL = group.AddVertex(topLeft);
			byte offTR = group.AddVertex(topRight);
			byte offBL = group.AddVertex(bottomLeft);
			byte offBR = group.AddVertex(bottomRight);

			// vertices are added in clockwise winding order
			switch (diagonal)
			{
				case QuadDiagonal.Ascending:
					group.AddTriangleOffsets(offTL, offTR, offBL).AddTriangleOffsets(offBL, offTR, offBR);
					return;

				case QuadDiagonal.Descending:
					group.AddTriangleOffsets(offTL, offBR, offBL).AddTriangleOffsets(offTL, offTR, offBR);
					return;

				default: throw InvalidEnumArgument.CreateException(nameof(diagonal), diagonal);
			}
		}
	}
}
