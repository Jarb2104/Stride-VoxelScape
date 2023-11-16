using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Comparisons;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for types that implement the <see cref="IComparer{T}"/> interface.
/// </summary>
public static class IComparerExtensions
{
	public static ComparisonResult CompareValues<T>(this IComparer<T> comparer, T x, T y)
	{
		Contracts.Requires.That(comparer != null);

		return ComparisonUtilities.ConvertToEnum(comparer.Compare(x, y));
	}

	/// <summary>
	/// Determines whether x is less than y using the specified comparer.
	/// </summary>
	/// <typeparam name="T">The type of values to compare.</typeparam>
	/// <param name="comparer">The comparer to use for the comparison.</param>
	/// <param name="x">The first value to compare.</param>
	/// <param name="y">The second value to compare.</param>
	/// <returns>True if x is less than y; otherwise false.</returns>
	public static bool IsLessThan<T>(this IComparer<T> comparer, T x, T y)
	{
		Contracts.Requires.That(comparer != null);

		return comparer.Compare(x, y) < 0;
	}

	/// <summary>
	/// Determines whether x is less than or equal to y using the specified comparer.
	/// </summary>
	/// <typeparam name="T">The type of values to compare.</typeparam>
	/// <param name="comparer">The comparer to use for the comparison.</param>
	/// <param name="x">The first value to compare.</param>
	/// <param name="y">The second value to compare.</param>
	/// <returns>True if x is less than or equal to y; otherwise false.</returns>
	public static bool IsLessThanOrEqual<T>(this IComparer<T> comparer, T x, T y)
	{
		Contracts.Requires.That(comparer != null);

		return comparer.Compare(x, y) <= 0;
	}

	/// <summary>
	/// Determines whether x is greater than y using the specified comparer.
	/// </summary>
	/// <typeparam name="T">The type of values to compare.</typeparam>
	/// <param name="comparer">The comparer to use for the comparison.</param>
	/// <param name="x">The first value to compare.</param>
	/// <param name="y">The second value to compare.</param>
	/// <returns>True if x is greater than y; otherwise false.</returns>
	public static bool IsGreaterThan<T>(this IComparer<T> comparer, T x, T y)
	{
		Contracts.Requires.That(comparer != null);

		return comparer.Compare(x, y) > 0;
	}

	/// <summary>
	/// Determines whether x is greater than or equal to y using the specified comparer.
	/// </summary>
	/// <typeparam name="T">The type of values to compare.</typeparam>
	/// <param name="comparer">The comparer to use for the comparison.</param>
	/// <param name="x">The first value to compare.</param>
	/// <param name="y">The second value to compare.</param>
	/// <returns>True if x is greater than or equal to y; otherwise false.</returns>
	public static bool IsGreaterThanOrEqual<T>(this IComparer<T> comparer, T x, T y)
	{
		Contracts.Requires.That(comparer != null);

		return comparer.Compare(x, y) >= 0;
	}

	/// <summary>
	/// Determines whether x is equal to y using the specified comparer.
	/// </summary>
	/// <typeparam name="T">The type of values to compare.</typeparam>
	/// <param name="comparer">The comparer to use for the comparison.</param>
	/// <param name="x">The first value to compare.</param>
	/// <param name="y">The second value to compare.</param>
	/// <returns>True if x is equal to y; otherwise false.</returns>
	public static bool IsEqual<T>(this IComparer<T> comparer, T x, T y)
	{
		Contracts.Requires.That(comparer != null);

		return comparer.Compare(x, y) == 0;
	}
}
