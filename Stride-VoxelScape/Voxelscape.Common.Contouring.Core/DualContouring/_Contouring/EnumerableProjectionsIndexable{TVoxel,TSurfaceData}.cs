using System.Collections;
using System.Collections.Generic;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Common.Contouring.Core.DualContouring
{
	/// <summary>
	/// Provides an enumerable sequence of <see cref="IVoxelProjection{TVoxel, TSurfaceData}" /> from an
	/// <see cref="IIndexable{Index3D, TVoxel}" /> for use in dual contouring.
	/// </summary>
	/// <typeparam name="TVoxel">The type of the voxel.</typeparam>
	/// <typeparam name="TSurfaceData">The type of the surface data.</typeparam>
	public class EnumerableProjectionsIndexable<TVoxel, TSurfaceData> : IEnumerable<IVoxelProjection<TVoxel, TSurfaceData>>
		where TVoxel : struct
		where TSurfaceData : class, IResettable, new()
	{
		#region Private Fields

		/// <summary>
		/// The delegate used to determine the contour.
		/// </summary>
		private readonly IContourDeterminer<TVoxel> determiner;

		/// <summary>
		/// The indexable volume of voxels to contour.
		/// </summary>
		private readonly IReadOnlyIndexable<Index3D, TVoxel> voxels;

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
		/// Initializes a new instance of the <see cref="EnumerableProjectionsIndexable{TVoxel, TSurfaceData}" /> class.
		/// </summary>
		/// <param name="contourDeterminer">The delegate used to determine the contour.</param>
		/// <param name="voxels">The voxels to contour.</param>
		/// <param name="startContourAtIndex">The index to start the contour at.</param>
		/// <param name="dimensionsToContour">The dimensions of the bounds to contour.</param>
		/// <param name="stageIndexOrigin">The stage index of the zero index origin of the indexable voxels.</param>
		public EnumerableProjectionsIndexable(
			IContourDeterminer<TVoxel> contourDeterminer,
			IReadOnlyIndexable<Index3D, TVoxel> voxels,
			Index3D startContourAtIndex,
			Index3D dimensionsToContour,
			Index3D stageIndexOrigin)
		{
			Contracts.Requires.That(contourDeterminer != null);
			Contracts.Requires.That(voxels != null);
			Contracts.Requires.That(voxels.IsIndexValid(startContourAtIndex));
			Contracts.Requires.That(dimensionsToContour.IsAllPositiveOrZero());

			this.determiner = contourDeterminer;
			this.voxels = voxels;
			this.start = startContourAtIndex;
			this.end = startContourAtIndex + dimensionsToContour;
			this.stageIndexOrigin = stageIndexOrigin;
		}

		/// <inheritdoc />
		public IEnumerator<IVoxelProjection<TVoxel, TSurfaceData>> GetEnumerator()
		{
			ReusableVoxelProjection<TVoxel, TSurfaceData> projection = new ReusableVoxelProjection<TVoxel, TSurfaceData>(
				new TSurfaceData(), this.voxels, this.stageIndexOrigin);

			for (int iY = this.start.Y; iY < this.end.Y; iY++)
			{
				for (int iZ = this.start.Z; iZ < this.end.Z; iZ++)
				{
					for (int iX = this.start.X; iX < this.end.X; iX++)
					{
						Index3D negativeEndOfEdge = new Index3D(iX, iY, iZ);
						Index3D positiveEndOfEdge;

						// x-axis
						positiveEndOfEdge = new Index3D(iX + 1, iY, iZ);
						switch (this.determiner.DetermineContour(this.voxels[negativeEndOfEdge], this.voxels[positiveEndOfEdge]))
						{
							case ContourFacingDirection.TowardsNegative:
								projection.SetupProjectionTowardsNegativeXAxis(positiveEndOfEdge);
								yield return projection;
								break;

							case ContourFacingDirection.TowardsPositive:
								projection.SetupProjectionTowardsPositiveXAxis(negativeEndOfEdge);
								yield return projection;
								break;

							case ContourFacingDirection.Both:
								projection.SetupProjectionTowardsNegativeXAxis(positiveEndOfEdge);
								yield return projection;
								projection.SetupProjectionTowardsPositiveXAxis(negativeEndOfEdge);
								yield return projection;
								break;
						}

						// z-axis
						positiveEndOfEdge = new Index3D(iX, iY, iZ + 1);
						switch (this.determiner.DetermineContour(this.voxels[negativeEndOfEdge], this.voxels[positiveEndOfEdge]))
						{
							case ContourFacingDirection.TowardsNegative:
								projection.SetupProjectionTowardsNegativeZAxis(positiveEndOfEdge);
								yield return projection;
								break;

							case ContourFacingDirection.TowardsPositive:
								projection.SetupProjectionTowardsPositiveZAxis(negativeEndOfEdge);
								yield return projection;
								break;

							case ContourFacingDirection.Both:
								projection.SetupProjectionTowardsNegativeZAxis(positiveEndOfEdge);
								yield return projection;
								projection.SetupProjectionTowardsPositiveZAxis(negativeEndOfEdge);
								yield return projection;
								break;
						}

						// y-axis
						positiveEndOfEdge = new Index3D(iX, iY + 1, iZ);
						switch (this.determiner.DetermineContour(this.voxels[negativeEndOfEdge], this.voxels[positiveEndOfEdge]))
						{
							case ContourFacingDirection.TowardsNegative:
								projection.SetupProjectionTowardsNegativeYAxis(positiveEndOfEdge);
								yield return projection;
								break;

							case ContourFacingDirection.TowardsPositive:
								projection.SetupProjectionTowardsPositiveYAxis(negativeEndOfEdge);
								yield return projection;
								break;

							case ContourFacingDirection.Both:
								projection.SetupProjectionTowardsNegativeYAxis(positiveEndOfEdge);
								yield return projection;
								projection.SetupProjectionTowardsPositiveYAxis(negativeEndOfEdge);
								yield return projection;
								break;
						}
					}
				}
			}
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
