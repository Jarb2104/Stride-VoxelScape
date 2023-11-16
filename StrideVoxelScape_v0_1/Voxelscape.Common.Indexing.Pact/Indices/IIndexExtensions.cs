using System;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;
using Voxelscape.Utility.Common.Pact.Mathematics;

/// <summary>
/// Provides extension methods for use with implementations of the <see cref="IIndex"/> interface.
/// </summary>
public static class IIndexExtensions
{
	public static bool Equals<TIndex>(this TIndex index, TIndex other)
		where TIndex : IIndex
	{
		if (index == null)
		{
			return other == null;
		}

		if (other == null)
		{
			return false;
		}

		if (index.Rank != other.Rank)
		{
			return false;
		}

		for (int dimension = 0; dimension < index.Rank; dimension++)
		{
			if (index[dimension] != other[dimension])
			{
				return false;
			}
		}

		return true;
	}

	#region SumCoordinates

	/// <summary>
	/// Sums all the coordinates of the specified index.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <param name="index">The index to sum the coordinates of.</param>
	/// <returns>The sum of all the coordinates of the index.</returns>
	public static int SumCoordinates<TIndex>(this TIndex index)
		where TIndex : IIndex
	{
		Contracts.Requires.That(index != null);

		int result = 0;
		for (int dimension = 0; dimension < index.Rank; dimension++)
		{
			result += index[dimension];
		}

		return result;
	}

	/// <summary>
	/// Sums all the coordinates of the specified index as a long value.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <param name="index">The index to sum the coordinates of.</param>
	/// <returns>The long sum of all the coordinates of the index.</returns>
	public static long SumCoordinatesLong<TIndex>(this TIndex index)
		where TIndex : IIndex
	{
		Contracts.Requires.That(index != null);

		long result = 0;
		for (int dimension = 0; dimension < index.Rank; dimension++)
		{
			result += index[dimension];
		}

		return result;
	}

	#endregion

	#region MultiplyCoordinates

	/// <summary>
	/// Multiplies all the coordinates of the specified index.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <param name="index">The index to multiply the coordinates of.</param>
	/// <returns>The result of multiplying all of the coordinates of the index.</returns>
	public static int MultiplyCoordinates<TIndex>(this TIndex index)
		where TIndex : IIndex
	{
		Contracts.Requires.That(index != null);

		int result = 1;
		for (int dimension = 0; dimension < index.Rank; dimension++)
		{
			result *= index[dimension];
		}

		return result;
	}

	/// <summary>
	/// Multiplies all the coordinates of the specified index as a long value.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <param name="index">The index to multiply the coordinates of.</param>
	/// <returns>The long result of multiplying all of the coordinates of the index.</returns>
	public static long MultiplyCoordinatesLong<TIndex>(this TIndex index)
		where TIndex : IIndex
	{
		Contracts.Requires.That(index != null);

		long result = 1;
		for (int dimension = 0; dimension < index.Rank; dimension++)
		{
			result *= index[dimension];
		}

		return result;
	}

	#endregion

	#region IsAllPositive/Negative/OrZero

	/// <summary>
	/// Determines whether all coordinate values of this index are positive.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <param name="index">The index.</param>
	/// <returns>True if all coordinates are positive, otherwise false.</returns>
	public static bool IsAllPositive<TIndex>(this TIndex index)
		where TIndex : IIndex
	{
		Contracts.Requires.That(index != null);

		for (int dimension = 0; dimension < index.Rank; dimension++)
		{
			if (!(index[dimension] > 0))
			{
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// Determines whether all coordinate values of this index are positive or zero.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <param name="index">The index.</param>
	/// <returns>True if all coordinates are positive or zero, otherwise false.</returns>
	public static bool IsAllPositiveOrZero<TIndex>(this TIndex index)
		where TIndex : IIndex
	{
		Contracts.Requires.That(index != null);

		for (int dimension = 0; dimension < index.Rank; dimension++)
		{
			if (!(index[dimension] >= 0))
			{
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// Determines whether all coordinate values of this index are negative.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <param name="index">The index.</param>
	/// <returns>True if all coordinates are negative, otherwise false.</returns>
	public static bool IsAllNegative<TIndex>(this TIndex index)
		where TIndex : IIndex
	{
		Contracts.Requires.That(index != null);

		for (int dimension = 0; dimension < index.Rank; dimension++)
		{
			if (!(index[dimension] < 0))
			{
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// Determines whether all coordinate values of this index are negative or zero.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <param name="index">The index.</param>
	/// <returns>True if all coordinates are negative or zero, otherwise false.</returns>
	public static bool IsAllNegativeOrZero<TIndex>(this TIndex index)
		where TIndex : IIndex
	{
		Contracts.Requires.That(index != null);

		for (int dimension = 0; dimension < index.Rank; dimension++)
		{
			if (!(index[dimension] <= 0))
			{
				return false;
			}
		}

		return true;
	}

	#endregion

	#region CalculateVolume

	/// <summary>
	/// Calculates the volume between the two indices.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <param name="index">The first index defining the bounds of the volume.</param>
	/// <param name="otherIndex">The second index defining the bounds of the volume.</param>
	/// <param name="rangeOptions">The inclusive/exclusive range options of the volume.</param>
	/// <returns>The total volume of indices between the two indices.</returns>
	public static int CalculateVolume<TIndex>(
		this TIndex index,
		TIndex otherIndex,
		RangeClusivity rangeOptions = RangeClusivity.Inclusive)
		where TIndex : IIndex
	{
		Contracts.Requires.That(index != null);
		Contracts.Requires.That(otherIndex != null);
		Contracts.Requires.That(index.Rank == otherIndex.Rank);

		int result = 1;
		for (int dimension = 0; dimension < index.Rank; dimension++)
		{
			result *= CalculateDistance(index[dimension], otherIndex[dimension], rangeOptions);
		}

		return result;
	}

	/// <summary>
	/// Calculates the volume as a long between the two indices.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <param name="index">The first index defining the bounds of the volume.</param>
	/// <param name="otherIndex">The second index defining the bounds of the volume.</param>
	/// <param name="rangeOptions">The inclusive/exclusive range options of the volume.</param>
	/// <returns>The total volume of indices between the two indices.</returns>
	public static long CalculateLongVolume<TIndex>(
		this TIndex index,
		TIndex otherIndex,
		RangeClusivity rangeOptions = RangeClusivity.Inclusive)
		where TIndex : IIndex
	{
		Contracts.Requires.That(index != null);
		Contracts.Requires.That(otherIndex != null);
		Contracts.Requires.That(index.Rank == otherIndex.Rank);

		long result = 1;
		for (int dimension = 0; dimension < index.Rank; dimension++)
		{
			result *= CalculateLongDistance(index[dimension], otherIndex[dimension], rangeOptions);
		}

		return result;
	}

	/// <summary>
	/// Calculates the distance between two integers.
	/// </summary>
	/// <param name="a">The first value.</param>
	/// <param name="b">The second value.</param>
	/// <param name="rangeOptions">The range options.</param>
	/// <returns>The distance between the two values.</returns>
	private static int CalculateDistance(int a, int b, RangeClusivity rangeOptions)
	{
		int result = Math.Abs(a - b);

		switch (rangeOptions)
		{
			case RangeClusivity.Exclusive:
				return (result - 1).ClampLower(0);

			case RangeClusivity.InclusiveMin:
				return result;

			case RangeClusivity.InclusiveMax:
				return result;

			case RangeClusivity.Inclusive:
				return result + 1;

			default:
				throw InvalidEnumArgument.CreateException(nameof(rangeOptions), rangeOptions);
		}
	}

	/// <summary>
	/// Calculates the distance as a long between two long values.
	/// </summary>
	/// <param name="a">The first value.</param>
	/// <param name="b">The second value.</param>
	/// <param name="rangeOptions">The range options.</param>
	/// <returns>The distance as a long between the two long values.</returns>
	private static long CalculateLongDistance(long a, long b, RangeClusivity rangeOptions)
	{
		long result = Math.Abs(a - b);

		switch (rangeOptions)
		{
			case RangeClusivity.Exclusive:
				return (result - 1).ClampLower(0);

			case RangeClusivity.InclusiveMin:
				return result;

			case RangeClusivity.InclusiveMax:
				return result;

			case RangeClusivity.Inclusive:
				return result + 1;

			default:
				throw InvalidEnumArgument.CreateException(nameof(rangeOptions), rangeOptions);
		}
	}

	#endregion
}
