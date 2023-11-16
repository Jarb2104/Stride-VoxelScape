using System.Collections.Generic;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
///
/// </summary>
public static class IEnumerableCountingExtensions
{
	#region RemovePerOccurrence

	/// <summary>
	/// Removes the elements from the specified enumerable on a per occurrence basis from this enumerable.
	/// </summary>
	/// <typeparam name="T">The type of enumerable values.</typeparam>
	/// <param name="source">The source enumerable to subtract from.</param>
	/// <param name="subtract">The enumerable of elements to subtract.</param>
	/// <returns>An enumeration of the remaining elements after subtracting.</returns>
	/// <remarks>
	/// <para>
	/// Per occurrence subtraction means for example that if the subtract enumerable contains an element once
	/// and the source enumerable contains it twice, the result from subtracting will contain the element once.
	/// </para><para>
	/// The resulting enumerable makes no guarantees about the order in which the resulting elements are enumerated.
	/// </para>
	/// </remarks>
	public static IEnumerable<T> RemovePerOccurrence<T>(this IEnumerable<T> source, IEnumerable<T> subtract) =>
		source.RemovePerOccurrence(subtract, EqualityComparer<T>.Default);

	/// <summary>
	/// Removes the elements from the specified enumerable on a per occurrence basis from this enumerable.
	/// </summary>
	/// <typeparam name="T">The type of enumerable values.</typeparam>
	/// <param name="source">The source enumerable to subtract from.</param>
	/// <param name="subtract">The enumerable of elements to subtract.</param>
	/// <param name="comparer">The comparer to use for equality checks.</param>
	/// <returns>An enumeration of the remaining elements after subtracting.</returns>
	/// <remarks>
	/// <para>
	/// Per occurrence subtraction means for example that if the subtract enumerable contains an element once
	/// and the source enumerable contains it twice, the result from subtracting will contain the element once.
	/// </para><para>
	/// The resulting enumerable makes no guarantees about the order in which the resulting elements are enumerated.
	/// </para>
	/// </remarks>
	public static IEnumerable<T> RemovePerOccurrence<T>(
		this IEnumerable<T> source, IEnumerable<T> subtract, IEqualityComparer<T> comparer)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(subtract != null);
		Contracts.Requires.That(comparer != null);

		var sourceCount = new CountingCollection<T>(source, comparer);
		var subtractCount = new CountingCollection<T>(subtract, comparer);
		sourceCount.SubtractCountFrom(subtractCount);
		return sourceCount.Expand;
	}

	#endregion

	#region ContainsPerOccurrence

	/// <summary>
	/// Determines whether the source contains the elements from the specified enumerable on a per occurrence basis.
	/// </summary>
	/// <typeparam name="T">The type of the enumerable values.</typeparam>
	/// <param name="source">The source enumerable to check.</param>
	/// <param name="contains">The sequence of elements to check the source for.</param>
	/// <returns>True if the sequence contains the same element counts in any order; otherwise false.</returns>
	public static bool ContainsPerOccurrence<T>(this IEnumerable<T> source, IEnumerable<T> contains) =>
		ContainsPerOccurrence(source, contains, EqualityComparer<T>.Default);

	/// <summary>
	/// Determines whether the source contains the elements from the specified enumerable on a per occurrence basis.
	/// </summary>
	/// <typeparam name="T">The type of the enumerable values.</typeparam>
	/// <param name="source">The source enumerable to check.</param>
	/// <param name="contains">The sequence of elements to check the source for.</param>
	/// <param name="comparer">The comparer to use for equality checks.</param>
	/// <returns>True if the sequence contains the same element counts in any order; otherwise false.</returns>
	public static bool ContainsPerOccurrence<T>(
		this IEnumerable<T> source, IEnumerable<T> contains, IEqualityComparer<T> comparer)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(contains != null);
		Contracts.Requires.That(comparer != null);

		var containsCount = new CountingCollection<T>(contains, comparer);
		foreach (T value in source)
		{
			if (containsCount.Remove(value) && containsCount.Count == 0)
			{
				return true;
			}
		}

		// still something left in containsCount
		return false;
	}

	#endregion

	#region ElementsEqualPerOccurrence

	/// <summary>
	/// Determines whether the source contains the same elements as the specified sequence in any order.
	/// Both enumerable sequences must contain the exact same count of each element for this to return true.
	/// </summary>
	/// <typeparam name="T">The type of the enumerable values.</typeparam>
	/// <param name="source">The source enumerable to check.</param>
	/// <param name="contains">The sequence of elements to check the source for in any order.</param>
	/// <returns>True if the sequence contains the same elements in any order; otherwise false.</returns>
	public static bool ElementsEqualPerOccurrence<T>(this IEnumerable<T> source, IEnumerable<T> contains) =>
		source.ElementsEqualPerOccurrence(contains, EqualityComparer<T>.Default);

	/// <summary>
	/// Determines whether the source contains the same elements as the specified sequence in any order.
	/// Both enumerable sequences must contain the exact same count of each element for this to return true.
	/// </summary>
	/// <typeparam name="T">The type of the enumerable values.</typeparam>
	/// <param name="source">The source enumerable to check.</param>
	/// <param name="contains">The sequence of elements to check the source for in any order.</param>
	/// <param name="comparer">The comparer to use for equality checks.</param>
	/// <returns>True if the sequence contains the same elements in any order; otherwise false.</returns>
	public static bool ElementsEqualPerOccurrence<T>(
		this IEnumerable<T> source, IEnumerable<T> contains, IEqualityComparer<T> comparer)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(contains != null);
		Contracts.Requires.That(comparer != null);

		var sourceCount = new CountingCollection<T>(source, comparer);
		var sequenceCount = new CountingCollection<T>(contains, comparer);

		return sourceCount.ContainsSameCountAs(sequenceCount);
	}

	#endregion
}
