using System.Collections.Generic;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides range extension methods for <see cref="IList{T}"/> and <see cref="IReadOnlyList{T}"/>.
/// </summary>
public static class IListRangeExtensions
{
	public static Range<int> GetIndexRange<T>(this IReadOnlyList<T> list)
	{
		Contracts.Requires.That(list != null);

		return Range.New(0, list.Count, RangeClusivity.InclusiveMin);
	}

	public static Range<int> GetListIndexRange<T>(this IList<T> list)
	{
		Contracts.Requires.That(list != null);

		return Range.New(0, list.Count, RangeClusivity.InclusiveMin);
	}
}
