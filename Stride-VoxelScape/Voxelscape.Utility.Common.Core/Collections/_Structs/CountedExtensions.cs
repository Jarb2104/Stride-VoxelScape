using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Core.Collections;

/// <summary>
/// Provides extension methods for creating <see cref="CountedEnumerable{T}"/> from other standard interfaces.
/// </summary>
public static class CountedExtensions
{
	public static CountedEnumerable<T> AsCounted<T>(this IReadOnlyCollection<T> values) =>
		new CountedEnumerable<T>(values);

	public static CountedEnumerable<T> AsCountedEnumerable<T>(this ICollection<T> values) =>
		new CountedEnumerable<T>(values);

	public static CountedEnumerable<T> ToCounted<T>(this IEnumerable<T> values, int count) =>
		new CountedEnumerable<T>(values, count);

	public static CountedEnumerable<T> ToCounted<T>(this IEnumerable<T> values) =>
		new CountedEnumerable<T>(values, values.Count());
}
