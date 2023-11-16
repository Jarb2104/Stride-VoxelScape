using System;
using Voxelscape.Utility.Common.Core.Hashing;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;
using HashCode = Voxelscape.Utility.Common.Core.Hashing.HashCode;

namespace Voxelscape.Utility.Common.Core.Mathematics
{
	public struct Range<T> : IEquatable<Range<T>>
		where T : IComparable<T>, IEquatable<T>
	{
		public Range(T min, T max, Clusivity minBounds, Clusivity maxBounds)
			: this(min, max, RangeClusivityUtilities.Combine(minBounds, maxBounds))
		{
		}

		public Range(T min, T max, RangeClusivity bounds = RangeClusivity.Inclusive)
		{
			Contracts.Requires.That(min != null);
			Contracts.Requires.That(max != null);
			Contracts.Requires.That(min.IsLessThanOrEqual(max));
			Contracts.Requires.That(bounds.IsValidEnumValue());

			this.Min = min;
			this.Max = max;
			this.Bounds = bounds;
		}

		public T Min { get; }

		public T Max { get; }

		public Clusivity MinBounds => this.Bounds.GetMinClusivity();

		public Clusivity MaxBounds => this.Bounds.GetMaxClusivity();

		public RangeClusivity Bounds { get; }

		public static bool operator ==(Range<T> lhs, Range<T> rhs) => lhs.Equals(rhs);

		public static bool operator !=(Range<T> lhs, Range<T> rhs) => !lhs.Equals(rhs);

		/// <inheritdoc />
		public bool Equals(Range<T> other) =>
			this.Min.Equals(other.Min) && this.Max.Equals(other.Max) && this.Bounds == other.Bounds;

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => HashCode.Start.And(this.Min).And(this.Max).And(this.Bounds);

		/// <inheritdoc />
		public override string ToString()
		{
			switch (this.Bounds)
			{
				case RangeClusivity.Inclusive: return $"[{this.Min}, {this.Max}]";
				case RangeClusivity.Exclusive: return $"({this.Min}, {this.Max})";
				case RangeClusivity.InclusiveMin: return $"[{this.Min}, {this.Max})";
				case RangeClusivity.InclusiveMax: return $"({this.Min}, {this.Max}]";
				default: throw InvalidEnumArgument.CreateException(nameof(this.Bounds), this.Bounds);
			}
		}

		public bool Contains(T value)
		{
			Contracts.Requires.That(value != null);

			switch (this.Bounds)
			{
				case RangeClusivity.Exclusive:
					return value.IsGreaterThan(this.Min) && value.IsLessThan(this.Max);

				case RangeClusivity.InclusiveMin:
					return value.IsGreaterThanOrEqual(this.Min) && value.IsLessThan(this.Max);

				case RangeClusivity.InclusiveMax:
					return value.IsGreaterThan(this.Min) && value.IsLessThanOrEqual(this.Max);

				case RangeClusivity.Inclusive:
					return value.IsGreaterThanOrEqual(this.Min) && value.IsLessThanOrEqual(this.Max);

				default:
					throw InvalidEnumArgument.CreateException(nameof(this.Bounds), this.Bounds);
			}
		}

		public bool Contains(Range<T> range)
		{
			if (this.MinBounds == Clusivity.Exclusive && range.MinBounds == Clusivity.Inclusive)
			{
				if (range.Min.IsLessThanOrEqual(this.Min))
				{
					return false;
				}
			}
			else
			{
				if (range.Min.IsLessThan(this.Min))
				{
					return false;
				}
			}

			if (this.MaxBounds == Clusivity.Exclusive && range.MaxBounds == Clusivity.Inclusive)
			{
				return !range.Max.IsGreaterThanOrEqual(this.Max);
			}
			else
			{
				return !range.Max.IsGreaterThan(this.Max);
			}
		}

		public bool Overlaps(Range<T> range)
		{
			if (this.MinBounds == Clusivity.Inclusive && range.MaxBounds == Clusivity.Inclusive)
			{
				if (!this.Min.IsLessThanOrEqual(range.Max))
				{
					return false;
				}
			}
			else
			{
				if (!this.Min.IsLessThan(range.Max))
				{
					return false;
				}
			}

			if (this.MaxBounds == Clusivity.Inclusive && range.MinBounds == Clusivity.Inclusive)
			{
				return range.Min.IsLessThanOrEqual(this.Max);
			}
			else
			{
				return range.Min.IsLessThan(this.Max);
			}
		}
	}
}
