using System.Diagnostics;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	/// <summary>
	/// Defines an algorithm for running on a source of contourable data to produce a list of mesh data
	/// that represents that contour.
	/// </summary>
	/// <typeparam name="TVoxel">The type of the voxels.</typeparam>
	/// <typeparam name="TSurfaceData">The type of the surface data.</typeparam>
	/// <typeparam name="TVertex">The type of the vertices.</typeparam>
	public interface IDualContourer<TVoxel, TSurfaceData, TVertex>
		where TVoxel : struct
		where TSurfaceData : class
		where TVertex : struct
	{
		/// <summary>
		/// Contours the specified contourable instance.
		/// </summary>
		/// <param name="contourable">The contourable instance to dual contour.</param>
		/// <param name="meshBuilder">The mesh builder to build the mesh with.</param>
		void Contour(IDualContourable<TVoxel, TSurfaceData> contourable, IMutableDivisibleMesh<TVertex> meshBuilder);
	}

	public static class IDualContourerContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Contour<TVoxel, TSurfaceData, TVertex>(
			IDualContourable<TVoxel, TSurfaceData> contourable, IMutableDivisibleMesh<TVertex> meshBuilder)
			where TVoxel : struct
			where TSurfaceData : class
			where TVertex : struct
		{
			Contracts.Requires.That(contourable != null);
			Contracts.Requires.That(meshBuilder != null);
		}
	}
}
