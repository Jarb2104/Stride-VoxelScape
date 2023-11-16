using System;
using System.Collections;
using System.Collections.Generic;
using Voxelscape.Stages.Management.Pact.Interests;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;
using Range = Voxelscape.Utility.Common.Core.Mathematics.Range;

namespace Voxelscape.Stages.Management.Core.Interests
{
	/// <summary>
	///
	/// </summary>
	public struct Interest64 : IEquatable<Interest64>, IEnumerable<int>
	{
		private static readonly int SegmentEnd = PrimitiveTypeSizes.LongNumberOfBits;

		private readonly long segment;

		private Interest64(long segment)
		{
			this.segment = segment;
		}

		public static IInterestMerger<Interest64> Merger { get; } = new InterestMerger();

		public static Interest64 None => new Interest64(0);

		public static Interest64 operator +(Interest64 lhs, Interest64 rhs) =>
			new Interest64(lhs.segment | rhs.segment);

		public static Interest64 operator -(Interest64 lhs, Interest64 rhs) =>
			new Interest64(lhs.segment & ~rhs.segment);

		public static Interest64 New(int interest)
		{
			Contracts.Requires.That(interest.IsIn(Range.FromLength(SegmentEnd)));

			return new Interest64(1L << interest);
		}

		public bool HasFlag(Interest64 flags) => (this.segment & flags.segment) == flags.segment;

		public bool HasAnyFlag(Interest64 flags) => (this.segment & flags.segment) != 0;

		/// <inheritdoc />
		public bool Equals(Interest64 other) => this.segment == other.segment;

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => this.segment.GetHashCode();

		/// <inheritdoc />
		public override string ToString() => InterestUtilities.ToString(this, SegmentEnd);

		/// <inheritdoc />
		public IEnumerator<int> GetEnumerator()
		{
			long bitmask = 1;
			long bits = this.segment;
			for (int count = 0; count < SegmentEnd; count++)
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

		private class InterestMerger : IInterestMerger<Interest64>
		{
			/// <inheritdoc />
			public IEqualityComparer<Interest64> Comparer => EqualityComparer<Interest64>.Default;

			/// <inheritdoc />
			public Interest64 None => Interest64.None;

			/// <inheritdoc />
			public Interest64 GetInterestByAdding(Interest64 current, Interest64 add) => current + add;

			/// <inheritdoc />
			public Interest64 GetInterestByRemoving(Interest64 current, Interest64 remove) => current - remove;
		}
	}
}
