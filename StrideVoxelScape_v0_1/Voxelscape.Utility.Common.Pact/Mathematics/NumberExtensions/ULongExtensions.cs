using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;

// Special fringe case in this class's IsDivisbleBy implementation.

/// <summary>
/// Provides extension methods for common mathematical functions for <see cref="ulong"/>.
/// </summary>
public static class ULongExtensions
{
	/// <summary>
	/// Determines whether the specified value is even.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// True if even, false otherwise.
	/// </returns>
	public static bool IsEven(this ulong value) => value % 2 == 0;

	/// <summary>
	/// Determines whether the specified value is odd.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// True if odd, false otherwise.
	/// </returns>
	public static bool IsOdd(this ulong value) => value % 2 != 0;

	/// <summary>
	/// Determines whether the specified value is divisible by the given divisor.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <param name="divisor">The divisor.</param>
	/// <returns>
	/// True if divisible by the divisor, false otherwise.
	/// </returns>
	public static bool IsDivisibleBy(this ulong value, long divisor)
	{
		Contracts.Requires.That(divisor != 0);

		// divisor must be unsigned in order for this to compile, and sign of divisor doesn't matter
		// for the % operator anyway so discarding sign should still produce the same results
		return value % (ulong)Math.Abs(divisor) == 0;
	}

	/// <summary>
	/// Divides the specified value by the divisor, rounding up to the next integral value instead of truncating.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <param name="divisor">The divisor.</param>
	/// <returns>The result of the division, rounded up.</returns>
	public static ulong DivideByRoundUp(this ulong value, ulong divisor) => ((value - 1) / divisor) + 1;
}
