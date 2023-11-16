using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Core.Utility;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for use with implementations of the <see cref="IIndexingBounds{Index3D}"/> interface.
/// </summary>
public static class IIndexingBoundsExtensions3D
{
	/// <summary>
	/// Determines whether the specified index is within bounds.
	/// </summary>
	/// <param name="bounds">The bounds to check against.</param>
	/// <param name="index">The index to check.</param>
	/// <returns>
	///   <c>true</c> if the index is within bounds; otherwise, <c>false</c>.
	/// </returns>
	public static bool IsIndexInBounds(this IIndexingBounds<Index3D> bounds, Index3D index)
	{
		Contracts.Requires.That(bounds != null);

		return index.IsIn(bounds.LowerBounds, bounds.UpperBounds);
	}

	/// <summary>
	/// Gets a 32-bit integer that represents the length of the specified dimension of the bounds.
	/// </summary>
	/// <param name="bounds">The bounds to check against.</param>
	/// <param name="axis">The axis of the dimension whose length needs to be determined.</param>
	/// <returns>
	/// A 32-bit integer that represents the length of the specified dimension.
	/// </returns>
	public static int GetLength(this IIndexingBounds<Index3D> bounds, Axis3D axis)
	{
		Contracts.Requires.That(bounds != null);

		return bounds.GetLength((int)axis);
	}

	/// <summary>
	/// Gets the index of the first valid slot of the specified dimension within the specified bounds.
	/// </summary>
	/// <param name="bounds">The bounds to check against.</param>
	/// <param name="axis">The axis of the dimension whose starting index needs to be determined.</param>
	/// <returns>
	/// The index of the first slot of the specified dimension.
	/// </returns>
	public static int GetLowerBound(this IIndexingBounds<Index3D> bounds, Axis3D axis)
	{
		Contracts.Requires.That(bounds != null);

		return bounds.GetLowerBound((int)axis);
	}

	/// <summary>
	/// Gets the index of the last slot of the specified dimension within the specified bounds.
	/// </summary>
	/// <param name="bounds">The bounds to check against.</param>
	/// <param name="axis">The axis of the dimension whose ending index needs to be determined.</param>
	/// <returns>
	/// The index of the last slot of the specified dimension.
	/// </returns>
	public static int GetUpperBound(this IIndexingBounds<Index3D> bounds, Axis3D axis)
	{
		Contracts.Requires.That(bounds != null);

		return bounds.GetUpperBound((int)axis);
	}

	public static Index3D GetMiddleIndex(this IIndexingBounds<Index3D> bounds, bool roundUp = false)
	{
		Contracts.Requires.That(bounds != null);

		return MiddleIndex.Get(bounds.LowerBounds, bounds.UpperBounds, roundUp);
	}
}
