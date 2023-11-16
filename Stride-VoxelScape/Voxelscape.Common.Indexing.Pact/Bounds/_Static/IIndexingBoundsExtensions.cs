using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for use with implementations of the <see cref="IIndexingBounds{TIndex}"/> interface.
/// </summary>
public static class IIndexingBoundsExtensions
{
	/// <summary>
	/// Determines whether the specified index is within the bounds of the <see cref="IIndexingBounds{TIndex}"/>.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <param name="bounded">The bounds to check against.</param>
	/// <param name="index">The index to check.</param>
	/// <returns>
	///   <c>true</c> if the index is within bounds; otherwise, <c>false</c>.
	/// </returns>
	public static bool IsIndexInBounds<TIndex>(this IIndexingBounds<TIndex> bounded, TIndex index)
		where TIndex : IIndex
	{
		Contracts.Requires.That(bounded != null);
		Contracts.Requires.That(index != null);
		Contracts.Requires.That(bounded.Rank == index.Rank);

		return IndexUtilities.IsIn(index, bounded.LowerBounds, bounded.UpperBounds);
	}
}
