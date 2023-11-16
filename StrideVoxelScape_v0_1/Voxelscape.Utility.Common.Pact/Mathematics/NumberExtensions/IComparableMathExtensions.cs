using System;

/// <summary>
/// Provides extension methods for common mathematical functions for types that implement the
/// <see cref="IComparable{T}"/> interface.
/// </summary>
/// <remarks>
/// These implementations are less efficient than the native extension methods for the primitive types.
/// Thus favor calling the non-generic overload if there is one available. These methods are to support
/// any other types that implements <see cref="IComparable{T}"/> but aren't a primitive type.
/// </remarks>
public static class IComparableMathExtensions
{
	/// <summary>
	/// Clamps the value to be no greater than the maximum value.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="value">The value.</param>
	/// <param name="max">The maximum value.</param>
	/// <returns>The initial value if it is less than the maximum value; otherwise the maximum value.</returns>
	public static T ClampUpper<T>(this T value, T max)
		where T : IComparable<T> => value.IsGreaterThan(max) ? max : value;

	/// <summary>
	/// Clamps the value to be no less than the minimum value.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="value">The value.</param>
	/// <param name="min">The minimum value.</param>
	/// <returns>The initial value if it is greater than the minimum value; otherwise the minimum value.</returns>
	public static T ClampLower<T>(this T value, T min)
		where T : IComparable<T> => value.IsLessThan(min) ? min : value;

	/// <summary>
	/// Clamps the value to be no less than the minimum value and no greater than the maximum value.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="value">The value.</param>
	/// <param name="min">The minimum value.</param>
	/// <param name="max">The maximum value.</param>
	/// <returns>The value clamped to the specified range.</returns>
	public static T Clamp<T>(this T value, T min, T max)
		where T : IComparable<T>
	{
		if (value.IsLessThanOrEqual(min))
		{
			return min;
		}

		if (value.IsGreaterThanOrEqual(max))
		{
			return max;
		}

		return value;
	}
}
