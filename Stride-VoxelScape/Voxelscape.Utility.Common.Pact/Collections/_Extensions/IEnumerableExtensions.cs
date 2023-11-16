using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for the <see cref="IEnumerable"/> and <see cref="IEnumerable{T}"/> interfaces.
/// </summary>
public static class IEnumerableExtensions
{
	#region IsNull/Empty

	/// <summary>
	/// Determines whether the specified enumerable is empty.
	/// </summary>
	/// <param name="source">The source enumerable.</param>
	/// <returns>True if the enumerable is empty, false otherwise.</returns>
	public static bool IsEmpty(this IEnumerable source)
	{
		Contracts.Requires.That(source != null);

		return !source.Cast<object>().Any();
	}

	/// <summary>
	/// Determines whether the specified enumerable is empty.
	/// </summary>
	/// <typeparam name="T">The type of enumerable objects.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <returns>True if the enumerable is empty, false otherwise.</returns>
	public static bool IsEmpty<T>(this IEnumerable<T> source)
	{
		Contracts.Requires.That(source != null);

		return !source.Any();
	}

	/// <summary>
	/// Determines whether the specified enumerable is null or empty.
	/// </summary>
	/// <param name="source">The source enumerable.</param>
	/// <returns>True if the enumerable is null or empty, false otherwise.</returns>
	public static bool IsNullOrEmpty(this IEnumerable source) => source == null || !source.Cast<object>().Any();

	/// <summary>
	/// Determines whether the specified enumerable is null or empty.
	/// </summary>
	/// <typeparam name="T">The type of enumerable objects.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <returns>True if the enumerable is null or empty, false otherwise.</returns>
	public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) => source == null || !source.Any();

	#endregion

	#region IsUnique

	/// <summary>
	/// Determines whether the specified enumerable contains only unique values (no duplicates).
	/// </summary>
	/// <typeparam name="T">The type of the enumerable values.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <returns>True if the sequences contains at least one duplicate value, otherwise false.</returns>
	public static bool IsUnique<T>(this IEnumerable<T> source) => source.IsUnique(EqualityComparer<T>.Default);

	/// <summary>
	/// Determines whether the specified enumerable contains only unique values (no duplicates).
	/// </summary>
	/// <typeparam name="T">The type of the enumerable values.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <param name="comparer">The comparer to use for equality comparisons.</param>
	/// <returns>True if the sequences contains at least one duplicate value, otherwise false.</returns>
	public static bool IsUnique<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(comparer != null);

		HashSet<T> uniqueElements = new HashSet<T>(comparer);
		foreach (T element in source)
		{
			if (!uniqueElements.Add(element))
			{
				// a duplicate was found
				return false;
			}
		}

		return true;
	}

	#endregion

	#region AllEqual(To)

	public static bool AllEqual<T>(this IEnumerable<T> source) =>
		source.AllEqual(EqualityComparer<T>.Default);

	public static bool AllEqual<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(comparer != null);

		T equalTo = source.FirstOrDefault();
		return source.All(value => comparer.Equals(value, equalTo));
	}

	public static bool AllEqualTo<T>(this IEnumerable<T> source, T value) =>
		source.AllEqualTo(value, EqualityComparer<T>.Default);

	public static bool AllEqualTo<T>(this IEnumerable<T> source, T value, IEqualityComparer<T> comparer)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(comparer != null);

		return source.All(checking => comparer.Equals(checking, value));
	}

	#endregion

	#region AllAndSelfNotNull (NullSafe)

	public static bool AllAndSelfNotNull<T>(this IEnumerable<T> source)
		=> source?.All(value => value != null) ?? false;

	#endregion

	#region ContainsAny/All

	/// <summary>
	/// Determines if the source enumerable contains any value from the other enumerable sequence.
	/// </summary>
	/// <typeparam name="T">The type of the enumerable values.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <param name="sequence">
	/// The enumerable containing the values for the source sequence to check for containing any of.
	/// </param>
	/// <returns>True if the source enumerable contains any value from the other enumerable; otherwise false.</returns>
	public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> sequence) =>
		source.ContainsAny(sequence, EqualityComparer<T>.Default);

	/// <summary>
	/// Determines if the source enumerable contains any value from the other enumerable sequence.
	/// </summary>
	/// <typeparam name="T">The type of the enumerable values.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <param name="sequence">
	/// The enumerable containing the values for the source sequence to check for containing any of.
	/// </param>
	/// <param name="comparer">The comparer to use for equality comparisons.</param>
	/// <returns>True if the source enumerable contains any value from the other enumerable; otherwise false.</returns>
	public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> sequence, IEqualityComparer<T> comparer)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(sequence != null);
		Contracts.Requires.That(comparer != null);

		return source.Intersect(sequence, comparer).Any();
	}

	/// <summary>
	/// Determines whether the enumerable source contains all the elements of the specified enumerable sequence
	/// in any order, ignoring duplicates.
	/// </summary>
	/// <typeparam name="T">The type of enumerable values.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <param name="other">The other enumerable.</param>
	/// <returns>True if the sequence contains the same elements in any order ignoring duplicates; otherwise false.</returns>
	public static bool ContainsAll<T>(this IEnumerable<T> source, IEnumerable<T> other) =>
		source.ContainsAll(other, EqualityComparer<T>.Default);

	/// <summary>
	/// Determines whether the enumerable source contains all the elements of the specified enumerable sequence
	/// in any order, ignoring duplicates.
	/// </summary>
	/// <typeparam name="T">The type of enumerable values.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <param name="other">The other enumerable.</param>
	/// <param name="comparer">The comparer to use for equality checks.</param>
	/// <returns>True if the sequence contains the same elements in any order ignoring duplicates; otherwise false.</returns>
	public static bool ContainsAll<T>(this IEnumerable<T> source, IEnumerable<T> other, IEqualityComparer<T> comparer)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(other != null);
		Contracts.Requires.That(comparer != null);

		var sourceSet = new HashSet<T>(source, comparer);
		foreach (var value in other)
		{
			if (!sourceSet.Contains(value))
			{
				return false;
			}
		}

		return true;
	}

	#endregion

	#region ElementsEqual

	/// <summary>
	/// Determines whether the enumerable source contains only the same elements as the specified enumerable sequence
	/// in any order, ignoring duplicates.
	/// </summary>
	/// <typeparam name="T">The type of enumerable values.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <param name="other">The other enumerable.</param>
	/// <returns>True if the sequence contains the same elements in any order ignoring duplicates; otherwise false.</returns>
	public static bool ElementsEqual<T>(this IEnumerable<T> source, IEnumerable<T> other) =>
		source.ElementsEqual(other, EqualityComparer<T>.Default);

	/// <summary>
	/// Determines whether the enumerable source contains only the same elements as the specified enumerable sequence
	/// in any order, ignoring duplicates.
	/// </summary>
	/// <typeparam name="T">The type of enumerable values.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <param name="other">The other enumerable.</param>
	/// <param name="comparer">The comparer to use for equality checks.</param>
	/// <returns>True if the sequence contains the same elements in any order ignoring duplicates; otherwise false.</returns>
	public static bool ElementsEqual<T>(this IEnumerable<T> source, IEnumerable<T> other, IEqualityComparer<T> comparer)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(other != null);
		Contracts.Requires.That(comparer != null);

		return new HashSet<T>(source, comparer).SetEquals(other);
	}

	#endregion

	#region CopyTo

	/// <summary>
	/// Copies the elements of the enumerable to an array, starting at a particular array index.
	/// </summary>
	/// <typeparam name="T">The type of the enumerable values.</typeparam>
	/// <param name="source">The source enumerable to copy to an array.</param>
	/// <param name="array">The array to copy the enumerable values to.</param>
	/// <param name="startingIndex">Index of the array to start copying to.</param>
	public static void CopyTo<T>(this IEnumerable<T> source, T[] array, int startingIndex)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(array != null);
		Contracts.Requires.That(startingIndex >= 0);
		Contracts.Requires.That(source.Count() + startingIndex <= array.Length);

		int index = startingIndex;
		foreach (T value in source)
		{
			array[index] = value;
			index++;
		}
	}

	#endregion

	#region RemoveDuplicates

	/// <summary>
	/// Returns as enumerable sequence with all duplicate values from the source enumerable removed.
	/// </summary>
	/// <typeparam name="T">The type of the enumerable values.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <returns>An enumerable sequence with only the unique values present.</returns>
	/// <remarks>
	/// Values are yielded on their first unique occurrences in the source enumerable. All duplicates thereafter are ignored.
	/// </remarks>
	public static IEnumerable<T> RemoveDuplicates<T>(this IEnumerable<T> source) =>
		source.RemoveDuplicates(EqualityComparer<T>.Default);

	/// <summary>
	/// Returns as enumerable sequence with all duplicate values from the source enumerable removed.
	/// </summary>
	/// <typeparam name="T">The type of the enumerable values.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <param name="comparer">The comparer to use for equality comparisons.</param>
	/// <returns>An enumerable sequence with only the unique values present.</returns>
	/// <remarks>
	/// Values are yielded on their first unique occurrences in the source enumerable. All duplicates thereafter are ignored.
	/// </remarks>
	public static IEnumerable<T> RemoveDuplicates<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(comparer != null);

		HashSet<T> uniqueElements = new HashSet<T>(comparer);
		foreach (T element in source)
		{
			if (uniqueElements.Add(element))
			{
				// element is not a duplicate
				yield return element;
			}
		}
	}

	#endregion

	#region Additional Set-like Methods

	/// <summary>
	/// Returns an enumeration that contains only elements that are present either in the current enumeration or
	/// in the specified enumeration, but not both.
	/// </summary>
	/// <typeparam name="T">The type of enumerable values.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <param name="other">The other enumerable.</param>
	/// <returns>
	/// An enumeration that contains only elements that are present either in the current enumeration or in the
	/// specified enumeration, but not both.
	/// </returns>
	public static IEnumerable<T> SymmetricExcept<T>(this IEnumerable<T> source, IEnumerable<T> other) =>
		source.SymmetricExcept(other, EqualityComparer<T>.Default);

	/// <summary>
	/// Returns an enumeration that contains only elements that are present either in the current enumeration or
	/// in the specified enumeration, but not both.
	/// </summary>
	/// <typeparam name="T">The type of enumerable values.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <param name="other">The other enumerable.</param>
	/// <param name="comparer">The comparer to use for equality checks.</param>
	/// <returns>
	/// An enumeration that contains only elements that are present either in the current enumeration or in the
	/// specified enumeration, but not both.
	/// </returns>
	public static IEnumerable<T> SymmetricExcept<T>(
		this IEnumerable<T> source, IEnumerable<T> other, IEqualityComparer<T> comparer)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(other != null);
		Contracts.Requires.That(comparer != null);

		HashSet<T> sourceSet = new HashSet<T>(source, comparer);
		HashSet<T> otherSet = new HashSet<T>(other, comparer);

		foreach (T value in sourceSet)
		{
			// only yield the value if the other set doesn't contain it
			// and if both sets do contain it remove it from the other set
			// so it won't be yielded later either
			if (!otherSet.Remove(value))
			{
				yield return value;
			}
		}

		foreach (T value in otherSet)
		{
			yield return value;
		}
	}

	#endregion

	/// <summary>
	/// Gets an enumerable sequence of indices along with their associated values of an enumerable source.
	/// </summary>
	/// <typeparam name="T">The type of enumerable values.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <returns>The enumerable sequence of indices paired with their values.</returns>
	public static IEnumerable<KeyValuePair<int, T>> SelectIndexValuePairs<T>(this IEnumerable<T> source)
	{
		Contracts.Requires.That(source != null);

		return source.Select((value, index) => new KeyValuePair<int, T>(index, value));
	}

	public static IEnumerable<TResult> Convert<TSource, TResult>(
		this IEnumerable<TSource> source, Converter<TSource, TResult> converter)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(converter != null);

		return source.Select(value => converter(value));
	}

	public static bool TryGetSingle<T>(this IEnumerable<T> source, out T result)
	{
		Contracts.Requires.That(source != null);

		int count = 0;
		result = default(T);

		foreach (var value in source)
		{
			count++;
			if (count == 2)
			{
				result = default(T);
				return false;
			}

			result = value;
		}

		return count == 1;
	}
}
