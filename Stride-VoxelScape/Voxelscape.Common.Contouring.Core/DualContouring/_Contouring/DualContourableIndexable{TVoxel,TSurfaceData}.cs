using System.Collections.Generic;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Common.Contouring.Core.DualContouring
{
	/// <summary>
	/// Wraps an <see cref="IIndexable{Index3D, TVoxel}" /> in a <see cref="IDualContourable{TVoxel, TSurfaceData}" />
	/// to be consumed by a <see cref="IDualContourer{TVoxel, TSurfaceData, TMeshData}" /> implementation.
	/// </summary>
	/// <typeparam name="TVoxel">The type of the voxel.</typeparam>
	/// <typeparam name="TSurfaceData">The type of the surface data.</typeparam>
	public class DualContourableIndexable<TVoxel, TSurfaceData> : IDualContourable<TVoxel, TSurfaceData>
		where TVoxel : struct
		where TSurfaceData : class, IResettable, new()
	{
		#region Private Fields

		/// <summary>
		/// The indexable volume of voxels to contour.
		/// </summary>
		private readonly IIndexable<Index3D, TVoxel> voxels;

		/// <summary>
		/// The index to start the contour at.
		/// </summary>
		private readonly Index3D start;

		/// <summary>
		/// The index end to end the contour at.
		/// </summary>
		private readonly Index3D end;

		/// <summary>
		/// The stage index of the zero index origin of the indexable voxels.
		/// </summary>
		private readonly Index3D stageIndexOrigin;

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="DualContourableIndexable{TVoxel, TSurfaceData}"/> class.
		/// </summary>
		/// <param name="voxels">The voxels to contour.</param>
		/// <param name="startContourAtIndex">The index to start the contour at.</param>
		/// <param name="dimensionsToContour">The dimensions of the bounds to contour.</param>
		/// <param name="stageIndexOrigin">The stage index of the zero index origin of the indexable voxels.</param>
		public DualContourableIndexable(
			IIndexable<Index3D, TVoxel> voxels,
			Index3D startContourAtIndex,
			Index3D dimensionsToContour,
			Index3D stageIndexOrigin)
		{
			Contracts.Requires.That(voxels != null);
			Contracts.Requires.That(dimensionsToContour.IsAllPositiveOrZero());

			this.voxels = voxels;
			this.start = startContourAtIndex;
			this.end = startContourAtIndex + dimensionsToContour;
			this.stageIndexOrigin = stageIndexOrigin;
		}

		#region IDualContourable<TVoxel> Members

		/// <inheritdoc />
		public IEnumerable<IVoxelProjection<TVoxel, TSurfaceData>> GetContourableProjections(
			IContourDeterminer<TVoxel> contourDeterminer)
		{
			IDualContourableContracts.GetContourableProjections(contourDeterminer);

			return new EnumerableProjectionsIndexable<TVoxel, TSurfaceData>(
				contourDeterminer, this.voxels, this.start, this.end, this.stageIndexOrigin);
		}

		#endregion
	}
}
