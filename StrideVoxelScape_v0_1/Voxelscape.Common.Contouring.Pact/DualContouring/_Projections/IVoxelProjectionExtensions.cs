using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for the <see cref="IVoxelProjection{TVoxel, TSurfaceData}"/> interface.
/// </summary>
public static class IVoxelProjectionExtensions
{
	/// <summary>
	/// Determines whether the surface contour represented by the projection is horizontal (perpendicular to the y axis).
	/// </summary>
	/// <typeparam name="TVoxel">The type of the voxel.</typeparam>
	/// <typeparam name="TSurfaceData">The type of the surface data.</typeparam>
	/// <param name="projection">The projection.</param>
	/// <returns>True if the projection is horizontal.</returns>
	public static bool IsHorizontal<TVoxel, TSurfaceData>(this IVoxelProjection<TVoxel, TSurfaceData> projection)
		where TVoxel : struct
		where TSurfaceData : class
	{
		Contracts.Requires.That(projection != null);

		return projection.AxisDirection.GetAxis() == Axis3D.Y;
	}

	/// <summary>
	/// Determines whether the surface contour represented by the projection is vertical (perpendicular to the x or z axis).
	/// </summary>
	/// <typeparam name="TVoxel">The type of the voxel.</typeparam>
	/// <typeparam name="TSurfaceData">The type of the surface data.</typeparam>
	/// <param name="projection">The projection.</param>
	/// <returns>True if the projection is vertical.</returns>
	public static bool IsVertical<TVoxel, TSurfaceData>(this IVoxelProjection<TVoxel, TSurfaceData> projection)
		where TVoxel : struct
		where TSurfaceData : class
	{
		Contracts.Requires.That(projection != null);

		return !projection.IsHorizontal();
	}
}
