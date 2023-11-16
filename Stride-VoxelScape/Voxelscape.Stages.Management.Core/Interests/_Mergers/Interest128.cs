using System;
using System.Collections;
using System.Collections.Generic;
using Voxelscape.Stages.Management.Pact.Interests;
using Voxelscape.Utility.Common.Core.Hashing;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;
using HashCode = Voxelscape.Utility.Common.Core.Hashing.HashCode;
using Range = Voxelscape.Utility.Common.Core.Mathematics.Range;

namespace Voxelscape.Stages.Management.Core.Interests
{
	/// <summary>
	///
	/// </summary>
	public struct Interest128 : IEquatable<Interest128>, IEnumerable<int>
	{
		private static readonly int Segment1End = PrimitiveTypeSizes.LongNumberOfBits;

		private static readonly int Segment2End = Segment1End * 2;

		private readonly long segment1;

		private readonly long segment2;

		private Interest128(long segment1, long segment2)
		{
			this.segment1 = segment1;
			this.segment2 = segment2;
		}

		public static IInterestMerger<Interest128> Merger { get; } = new InterestMerger();

		public static Interest128 None => new Interest128(0, 0);

		public static Interest128 operator +(Interest128 lhs, Interest128 rhs) =>
			new Interest128(lhs.segment1 | rhs.segment1, lhs.segment2 | rhs.segment2);

		public static Interest128 operator -(Interest128 lhs, Interest128 rhs) =>
			new Interest128(lhs.segment1 & ~rhs.segment1, lhs.segment2 & ~rhs.segment2);

		public static Interest128 New(int interest)
		{
			Contracts.Requires.That(interest.IsIn(Range.FromLength(Segment2End)));

			if (interest < Segment1End)
			{
				return new Interest128(1L << interest, 0);
			}
			else
			{
				return new Interest128(0, 1L << (interest - Segment1End));
			}
		}

		public bool HasFlag(Interest128 flags) =>
			((this.segment1 & flags.segment1) == flags.segment1) &&
			((this.segment2 & flags.segment2) == flags.segment2);

		public bool HasAnyFlag(Interest128 flags) =>
			((this.segment1 & flags.segment1) != 0) ||
			((this.segment2 & flags.segment2) != 0);

		/// <inheritdoc />
		public bool Equals(Interest128 other) => this.segment1 == other.segment1 && this.segment2 == other.segment2;

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => HashCode.Start.And(this.segment1).And(this.segment2);

		/// <inheritdoc />
		public override string ToString() => InterestUtilities.ToString(this, Segment2End);

		/// <inheritdoc />
		public IEnumerator<int> GetEnumerator()
		{
			long bitmask = 1;

			var bits = this.segment1;
			for (int count = 0; count < Segment1End; count++)
			{
				if ((bits & bitmask) == bitmask)
				{
					yield return count;
				}

				bits = bits >> 1;
			}

			bits = this.segment2;
			for (int count = Segment1End; count < Segment2End; count++)
			{
				if ((bits & bitmask) == bitmask)
				{
					yield return count;
				}

				bits = bits >> 1;
			}
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		private class InterestMerger : IInterestMerger<Interest128>
		{
			/// <inheritdoc />
			public IEqualityComparer<Interest128> Comparer => EqualityComparer<Interest128>.Default;

			/// <inheritdoc />
			public Interest128 None => Interest128.None;

			/// <inheritdoc />
			public Interest128 GetInterestByAdding(Interest128 current, Interest128 add) => current + add;

			/// <inheritdoc />
			public Interest128 GetInterestByRemoving(Interest128 current, Interest128 remove) => current - remove;
		}
	}
}
