using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	/// <summary>
	/// Defines a type capable of being dual contoured by a <see cref="IDualContourer{TVoxel, TSurfaceData, TMesh}" />
	/// implementation.
	/// </summary>
	/// <typeparam name="TVoxel">The type of the voxel.</typeparam>
	/// <typeparam name="TSurfaceData">The type of the surface data.</typeparam>
	public interface IDualContourable<TVoxel, TSurfaceData>
		where TVoxel : struct
		where TSurfaceData : class
	{
		/// <summary>
		/// Gets an enumerable sequence of projections of the underlying voxel data used in dual contouring
		/// to generate the mesh data.
		/// </summary>
		/// <param name="contourDeterminer">
		/// The function used to determine where the contour lies within the volume of voxels.
		/// </param>
		/// <returns>
		/// The enumerable sequence of projections of the underlying voxel data.
		/// </returns>
		IEnumerable<IVoxelProjection<TVoxel, TSurfaceData>> GetContourableProjections(
			IContourDeterminer<TVoxel> contourDeterminer);
	}

	public static class IDualContourableContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void GetContourableProjections<TVoxel>(IContourDeterminer<TVoxel> contourDeterminer)
			where TVoxel : struct
		{
			Contracts.Requires.That(contourDeterminer != null);
		}
	}
}
