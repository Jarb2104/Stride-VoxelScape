using System;

namespace Voxelscape.Utility.Common.Core.Mathematics
{
	/// <summary>
	///
	/// </summary>
	public static class Range
	{
		public static Range<T> New<T>(T min, T max, RangeClusivity bounds = RangeClusivity.Inclusive)
			where T : IComparable<T>, IEquatable<T> => new Range<T>(min, max, bounds);

		public static Range<T> New<T>(T min, T max, Clusivity minBounds, Clusivity maxBounds)
			where T : IComparable<T>, IEquatable<T> => new Range<T>(min, max, minBounds, maxBounds);

		// TODO maybe all these FromLength overloads should not accept a RangeClusivity bounds at all?
		public static Range<float> FromLength(
			float start, float length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<float>(start, start + length, bounds);

		public static Range<double> FromLength(
			double start, double length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<double>(start, start + length, bounds);

		public static Range<decimal> FromLength(
			decimal start, decimal length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<decimal>(start, start + length, bounds);

		public static Range<byte> FromLength(
			byte start, byte length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<byte>(start, (byte)(start + length), bounds);

		public static Range<sbyte> FromLength(
			sbyte start, sbyte length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<sbyte>(start, (sbyte)(start + length), bounds);

		public static Range<short> FromLength(
			short start, short length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<short>(start, (short)(start + length), bounds);

		public static Range<ushort> FromLength(
			ushort start, ushort length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<ushort>(start, (ushort)(start + length), bounds);

		public static Range<int> FromLength(
			int start, int length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<int>(start, start + length, bounds);

		public static Range<uint> FromLength(
			uint start, uint length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<uint>(start, start + length, bounds);

		public static Range<long> FromLength(
			long start, long length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<long>(start, start + length, bounds);

		public static Range<ulong> FromLength(
			ulong start, ulong length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<ulong>(start, start + length, bounds);

		public static Range<float> FromLength(
			float length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<float>(0, length, bounds);

		public static Range<double> FromLength(
			double length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<double>(0, length, bounds);

		public static Range<decimal> FromLength(
			decimal length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<decimal>(0, length, bounds);

		public static Range<byte> FromLength(
			byte length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<byte>(0, length, bounds);

		public static Range<sbyte> FromLength(
			sbyte length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<sbyte>(0, length, bounds);

		public static Range<short> FromLength(
			short length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<short>(0, length, bounds);

		public static Range<ushort> FromLength(
			ushort length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<ushort>(0, length, bounds);

		public static Range<int> FromLength(
			int length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<int>(0, length, bounds);

		public static Range<uint> FromLength(
			uint length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<uint>(0, length, bounds);

		public static Range<long> FromLength(
			long length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<long>(0, length, bounds);

		public static Range<ulong> FromLength(
			ulong length, RangeClusivity bounds = RangeClusivity.InclusiveMin) =>
			new Range<ulong>(0, length, bounds);
	}
}
