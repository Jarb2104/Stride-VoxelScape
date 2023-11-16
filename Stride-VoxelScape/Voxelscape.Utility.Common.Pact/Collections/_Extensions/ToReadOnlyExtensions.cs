using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Collections;

/// <summary>
/// Provides extension methods for converting collection interfaces to their read only counterparts.
/// These conversions require allocating memory and copying values.
/// </summary>
public static class ToReadOnlyExtensions
{
	public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> values) =>
		new ReadOnlyListWrapper<T>(values?.ToArray());
}
