using System.Diagnostics;
using System.Linq;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Stride.Utility.Pact.Meshing;

namespace Voxelscape.Stride.Utility.Core.Meshing
{
	/// <summary>
	///
	/// </summary>
	public static class MeshData
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void VerifyContracts<TVertex>(IMeshData16<TVertex> mesh)
			where TVertex : struct
		{
			VerifyCommonContracts(mesh);

			Contracts.Requires.That(mesh.VerticesCount <= MeshConstants.MaxVerticesSupportedBy16BitIndices);
			Contracts.Requires.That(mesh.Indices16 != null);
			Contracts.Requires.That(mesh.IndicesCount.IsIn(Range.New(0, mesh.Indices16.Length)));

			var range = Range.FromLength(mesh.VerticesCount);
			Contracts.Requires.That(mesh.Indices16.Take(mesh.IndicesCount).All(index => ((int)index).IsIn(range)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void VerifyContracts<TVertex>(IMeshData32<TVertex> mesh)
			where TVertex : struct
		{
			VerifyCommonContracts(mesh);

			Contracts.Requires.That(mesh.Indices32 != null);
			Contracts.Requires.That(mesh.IndicesCount.IsIn(Range.New(0, mesh.Indices32.Length)));

			var range = Range.FromLength(mesh.VerticesCount);
			Contracts.Requires.That(mesh.Indices32.Take(mesh.IndicesCount).All(index => index.IsIn(range)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		private static void VerifyCommonContracts<TVertex>(IMeshData<TVertex> mesh)
			where TVertex : struct
		{
			Contracts.Requires.That(mesh != null);
			Contracts.Requires.That(mesh.Vertices != null);
			Contracts.Requires.That(mesh.VerticesCount.IsIn(Range.New(0, mesh.Vertices.Length)));
			Contracts.Requires.That(mesh.IndicesCount.IsDivisibleBy(MeshConstants.VerticesPerTriangle));
		}
	}
}
