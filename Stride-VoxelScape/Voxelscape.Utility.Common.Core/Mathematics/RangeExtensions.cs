using System;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Exceptions;

/// <summary>
/// Provides extension methods for working with ranges.
/// </summary>
public static class RangeExtensions
{
	public static bool IsIn<T>(this T value, Range<T> range)
		where T : IComparable<T>, IEquatable<T> => range.Contains(value);

	public static bool IsIn<T>(this Range<T> range, Range<T> other)
		where T : IComparable<T>, IEquatable<T> => other.Contains(range);

	public static float GetLength(this Range<float> range) => range.Max - range.Min;

	public static double GetLength(this Range<double> range) => range.Max - range.Min;

	public static decimal GetLength(this Range<decimal> range) => range.Max - range.Min;

	public static int GetLength(this Range<byte> range)
	{
		int length = range.Max - range.Min;
		switch (range.Bounds)
		{
			case RangeClusivity.Inclusive: return length + 1;
			case RangeClusivity.Exclusive: return length == 0 ? 0 : length - 1;
			case RangeClusivity.InclusiveMin: return length;
			case RangeClusivity.InclusiveMax: return length;
			default: throw InvalidEnumArgument.CreateException(nameof(range.Bounds), range.Bounds);
		}
	}

	public static int GetLength(this Range<sbyte> range)
	{
		int length = range.Max - range.Min;
		switch (range.Bounds)
		{
			case RangeClusivity.Inclusive: return length + 1;
			case RangeClusivity.Exclusive: return length == 0 ? 0 : length - 1;
			case RangeClusivity.InclusiveMin: return length;
			case RangeClusivity.InclusiveMax: return length;
			default: throw InvalidEnumArgument.CreateException(nameof(range.Bounds), range.Bounds);
		}
	}

	public static int GetLength(this Range<short> range)
	{
		int length = range.Max - range.Min;
		switch (range.Bounds)
		{
			case RangeClusivity.Inclusive: return length + 1;
			case RangeClusivity.Exclusive: return length == 0 ? 0 : length - 1;
			case RangeClusivity.InclusiveMin: return length;
			case RangeClusivity.InclusiveMax: return length;
			default: throw InvalidEnumArgument.CreateException(nameof(range.Bounds), range.Bounds);
		}
	}

	public static int GetLength(this Range<ushort> range)
	{
		int length = range.Max - range.Min;
		switch (range.Bounds)
		{
			case RangeClusivity.Inclusive: return length + 1;
			case RangeClusivity.Exclusive: return length == 0 ? 0 : length - 1;
			case RangeClusivity.InclusiveMin: return length;
			case RangeClusivity.InclusiveMax: return length;
			default: throw InvalidEnumArgument.CreateException(nameof(range.Bounds), range.Bounds);
		}
	}

	public static int GetLength(this Range<int> range)
	{
		int length = range.Max - range.Min;
		switch (range.Bounds)
		{
			case RangeClusivity.Inclusive: return length == int.MaxValue ? int.MaxValue : length + 1;
			case RangeClusivity.Exclusive: return length == 0 ? 0 : length - 1;
			case RangeClusivity.InclusiveMin: return length;
			case RangeClusivity.InclusiveMax: return length;
			default: throw InvalidEnumArgument.CreateException(nameof(range.Bounds), range.Bounds);
		}
	}

	public static long GetLength(this Range<uint> range)
	{
		long length = range.Max - range.Min;
		switch (range.Bounds)
		{
			case RangeClusivity.Inclusive: return length + 1;
			case RangeClusivity.Exclusive: return length == 0 ? 0 : length - 1;
			case RangeClusivity.InclusiveMin: return length;
			case RangeClusivity.InclusiveMax: return length;
			default: throw InvalidEnumArgument.CreateException(nameof(range.Bounds), range.Bounds);
		}
	}

	public static long GetLength(this Range<long> range)
	{
		long length = range.Max - range.Min;
		switch (range.Bounds)
		{
			case RangeClusivity.Inclusive: return length == long.MaxValue ? long.MaxValue : length + 1;
			case RangeClusivity.Exclusive: return length == 0 ? 0 : length - 1;
			case RangeClusivity.InclusiveMin: return length;
			case RangeClusivity.InclusiveMax: return length;
			default: throw InvalidEnumArgument.CreateException(nameof(range.Bounds), range.Bounds);
		}
	}

	public static ulong GetLength(this Range<ulong> range)
	{
		ulong length = range.Max - range.Min;
		switch (range.Bounds)
		{
			case RangeClusivity.Inclusive: return length == ulong.MaxValue ? ulong.MaxValue : length + 1;
			case RangeClusivity.Exclusive: return length == 0 ? 0 : length - 1;
			case RangeClusivity.InclusiveMin: return length;
			case RangeClusivity.InclusiveMax: return length;
			default: throw InvalidEnumArgument.CreateException(nameof(range.Bounds), range.Bounds);
		}
	}
}
