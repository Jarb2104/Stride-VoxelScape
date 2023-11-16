namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	/// <summary>
	/// Defines a type used to determine if there is a surface intersecting the edge between two voxels to contour.
	/// </summary>
	/// <typeparam name="TVoxel">The type of the voxel.</typeparam>
	public interface IContourDeterminer<TVoxel>
		where TVoxel : struct
	{
		/// <summary>
		/// Determine if there is a surface intersecting the edge between two voxels to contour.
		/// </summary>
		/// <param name="towardsNegative">The voxel on the edge that is closer to the negative infinity index.</param>
		/// <param name="towardsPositive">The voxel on the edge that is closer to the positive infinity index.</param>
		/// <returns>
		/// An enumeration value indicating whether there is a surface intersecting this edge to contour
		/// and which way it is facing if there is.
		/// </returns>
		ContourFacingDirection DetermineContour(TVoxel towardsNegative, TVoxel towardsPositive);
	}
}
