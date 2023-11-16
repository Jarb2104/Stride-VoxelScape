using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Collections;

/// <summary>
/// Provides extension methods for wrapping mutable collection interfaces in their read only counterparts.
/// </summary>
public static class AsReadOnlyExtensions
{
	public static IReadOnlyCollection<T> AsReadOnlyCollection<T>(this ICollection<T> collection) =>
		new ReadOnlyCollectionWrapper<T>(collection);

	public static IReadOnlyList<T> AsReadOnlyList<T>(this IList<T> list) =>
		new ReadOnlyListWrapper<T>(list);

	public static IReadOnlyDictionary<TKey, TValue> AsReadOnlyDictionary<TKey, TValue>(
		this IDictionary<TKey, TValue> dictionary) => new ReadOnlyDictionaryWrapper<TKey, TValue>(dictionary);
}
