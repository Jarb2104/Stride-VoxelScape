using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for use with implementations of the <see cref="IDynamicIndexingBounds{Index3D}"/> interface.
/// </summary>
public static class IDynamicIndexingBoundsExtensions3D
{
	/// <summary>
	/// Determines whether the specified index is within the current bounds.
	/// </summary>
	/// <param name="bounds">The bounds to check against.</param>
	/// <param name="index">The index to check.</param>
	/// <returns>
	///   <c>true</c> if the index is within the current bounds; otherwise, <c>false</c>.
	/// </returns>
	public static bool IsIndexInCurrentBounds(this IDynamicIndexingBounds<Index3D> bounds, Index3D index)
	{
		Contracts.Requires.That(bounds != null);

		return index.IsIn(bounds.CurrentLowerBounds, bounds.CurrentUpperBounds);
	}

	/// <summary>
	/// Gets a 32-bit integer that represents the current length of the specified dimension of the bounds.
	/// </summary>
	/// <param name="bounds">The bounds to check against.</param>
	/// <param name="axis">The axis of the dimension whose length needs to be determined.</param>
	/// <returns>
	/// A 32-bit integer that represents the current length of the specified dimension.
	/// </returns>
	public static int GetCurrentLength(this IDynamicIndexingBounds<Index3D> bounds, Axis3D axis)
	{
		Contracts.Requires.That(bounds != null);

		return bounds.GetCurrentLength((int)axis);
	}

	/// <summary>
	/// Gets the current index of the first valid slot of the specified dimension within the specified bounds.
	/// </summary>
	/// <param name="bounds">The bounds to check against.</param>
	/// <param name="axis">The axis of the dimension whose starting index needs to be determined.</param>
	/// <returns>
	/// The current index of the first slot of the specified dimension.
	/// </returns>
	public static int GetCurrentLowerBound(this IDynamicIndexingBounds<Index3D> bounds, Axis3D axis)
	{
		Contracts.Requires.That(bounds != null);

		return bounds.GetCurrentLowerBound((int)axis);
	}

	/// <summary>
	/// Gets the current index of the last slot of the specified dimension within the specified bounds.
	/// </summary>
	/// <param name="bounds">The bounds to check against.</param>
	/// <param name="axis">The axis of the dimension whose ending index needs to be determined.</param>
	/// <returns>
	/// The current index of the last slot of the specified dimension.
	/// </returns>
	public static int GetCurrentUpperBound(this IDynamicIndexingBounds<Index3D> bounds, Axis3D axis)
	{
		Contracts.Requires.That(bounds != null);

		return bounds.GetCurrentUpperBound((int)axis);
	}
}
