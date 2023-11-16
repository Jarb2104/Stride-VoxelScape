using System.Diagnostics;
using Voxelscape.Common.Contouring.Core.Meshing;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stride.Utility.Pact.Meshing
{
	public interface IMeshDataTransfer<TVertex>
		where TVertex : struct
	{
		int MaxVertices { get; }

		void CopyIn(IDivisibleMesh<TVertex> mesh, VertexWindingOrder winding);
	}

	public static class IMeshDataTransferContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyIn<TVertex>(
			IMeshDataTransfer<TVertex> instance, IDivisibleMesh<TVertex> mesh, VertexWindingOrder winding)
			where TVertex : struct
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(mesh != null);
			Contracts.Requires.That(
				mesh.Vertices.Count <= instance.MaxVertices || instance.MaxVertices == Capacity.Unbounded);
			Contracts.Requires.That(winding.IsValidEnumValue());

			DivisibleMesh.VerifyContracts(mesh);
		}
	}
}
