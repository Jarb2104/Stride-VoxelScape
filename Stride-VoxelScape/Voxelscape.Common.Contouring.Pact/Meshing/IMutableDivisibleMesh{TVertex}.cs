using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Pact.Meshing
{
	public interface IMutableDivisibleMesh<TVertex> : IDivisibleMesh<TVertex>
		where TVertex : struct
	{
		ITriangleGroupBuilder<TVertex> AddTriangleGroup(TriangleGroup group);

		void Clear();
	}

	public static class IDivisibleMeshBuilderContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddTriangleGroup(TriangleGroup group)
		{
			Contracts.Requires.That(group.Triangles >= 1);
			Contracts.Requires.That(group.Vertices >= MeshConstants.VerticesPerTriangle);
		}
	}
}
