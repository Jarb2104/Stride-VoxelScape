﻿using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for common mathematical functions for <see cref="short"/>.
/// </summary>
public static class ShortExtensions
{
	/// <summary>
	/// Determines whether the specified value is even.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// True if even, false otherwise.
	/// </returns>
	public static bool IsEven(this short value) => value % 2 == 0;

	/// <summary>
	/// Determines whether the specified value is odd.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// True if odd, false otherwise.
	/// </returns>
	public static bool IsOdd(this short value) => value % 2 != 0;

	/// <summary>
	/// Determines whether the specified value is divisible by the given divisor.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <param name="divisor">The divisor.</param>
	/// <returns>
	/// True if divisible by the divisor, false otherwise.
	/// </returns>
	public static bool IsDivisibleBy(this short value, int divisor)
	{
		Contracts.Requires.That(divisor != 0);

		return value % divisor == 0;
	}

	/// <summary>
	/// Divides the specified value by the divisor, rounding up to the next integral value instead of truncating.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <param name="divisor">The divisor.</param>
	/// <returns>The result of the division, rounded up.</returns>
	public static short DivideByRoundUp(this short value, short divisor) => (short)(((value - 1) / divisor) + 1);
}
