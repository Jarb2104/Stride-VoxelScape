using System;
using Voxelscape.Utility.Common.Pact.Comparisons;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for types that implement the <see cref="IComparable{T}"/> interface.
/// </summary>
public static class IComparableExtensions
{
	public static ComparisonResult CompareWith<T>(this T value, T other)
		where T : IComparable<T>
	{
		Contracts.Requires.That(value != null);

		return ComparisonUtilities.ConvertToEnum(value.CompareTo(other));
	}

	/// <summary>
	/// Determines whether this value is less than the specified value.
	/// </summary>
	/// <typeparam name="T">The type of the comparable value.</typeparam>
	/// <param name="value">The value.</param>
	/// <param name="other">The other value.</param>
	/// <returns>True if this value is less than the other value; otherwise false.</returns>
	public static bool IsLessThan<T>(this T value, T other)
		where T : IComparable<T>
	{
		Contracts.Requires.That(value != null);

		return value.CompareTo(other) < 0;
	}

	/// <summary>
	/// Determines whether this value is less than or equal to the specified value.
	/// </summary>
	/// <typeparam name="T">The type of the comparable value.</typeparam>
	/// <param name="value">The value.</param>
	/// <param name="other">The other value.</param>
	/// <returns>True if this value is less than or equal to the other value; otherwise false.</returns>
	public static bool IsLessThanOrEqual<T>(this T value, T other)
		where T : IComparable<T>
	{
		Contracts.Requires.That(value != null);

		return value.CompareTo(other) <= 0;
	}

	/// <summary>
	/// Determines whether this value is greater than the specified value.
	/// </summary>
	/// <typeparam name="T">The type of the comparable value.</typeparam>
	/// <param name="value">The value.</param>
	/// <param name="other">The other value.</param>
	/// <returns>True if this value is greater than the other value; otherwise false.</returns>
	public static bool IsGreaterThan<T>(this T value, T other)
		where T : IComparable<T>
	{
		Contracts.Requires.That(value != null);

		return value.CompareTo(other) > 0;
	}

	/// <summary>
	/// Determines whether this value is greater than or equal to than the specified value.
	/// </summary>
	/// <typeparam name="T">The type of the comparable value.</typeparam>
	/// <param name="value">The value.</param>
	/// <param name="other">The other value.</param>
	/// <returns>True if this value is greater than or equal to the other value; otherwise false.</returns>
	public static bool IsGreaterThanOrEqual<T>(this T value, T other)
		where T : IComparable<T>
	{
		Contracts.Requires.That(value != null);

		return value.CompareTo(other) >= 0;
	}

	/// <summary>
	/// Determines whether this value is equal to the specified value.
	/// </summary>
	/// <typeparam name="T">The type of the comparable value.</typeparam>
	/// <param name="value">The value.</param>
	/// <param name="other">The other value.</param>
	/// <returns>True if this value is equal to the other value; otherwise false.</returns>
	public static bool IsEqual<T>(this T value, T other)
		where T : IComparable<T>
	{
		Contracts.Requires.That(value != null);

		return value.CompareTo(other) == 0;
	}
}
