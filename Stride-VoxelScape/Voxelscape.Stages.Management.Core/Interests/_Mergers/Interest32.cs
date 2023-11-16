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
	public struct Interest32 : IEquatable<Interest32>, IEnumerable<int>
	{
		private static readonly int SegmentEnd = PrimitiveTypeSizes.IntNumberOfBits;

		private readonly int segment;

		private Interest32(int segment)
		{
			this.segment = segment;
		}

		public static IInterestMerger<Interest32> Merger { get; } = new InterestMerger();

		public static Interest32 None => new Interest32(0);

		public static Interest32 operator +(Interest32 lhs, Interest32 rhs) =>
			new Interest32(lhs.segment | rhs.segment);

		public static Interest32 operator -(Interest32 lhs, Interest32 rhs) =>
			new Interest32(lhs.segment & ~rhs.segment);

		public static Interest32 New(int interest)
		{
			Contracts.Requires.That(interest.IsIn(Range.FromLength(SegmentEnd)));

			return new Interest32(1 << interest);
		}

		public bool HasFlag(Interest32 flags) => (this.segment & flags.segment) == flags.segment;

		public bool HasAnyFlag(Interest32 flags) => (this.segment & flags.segment) != 0;

		/// <inheritdoc />
		public bool Equals(Interest32 other) => this.segment == other.segment;

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => this.segment.GetHashCode();

		/// <inheritdoc />
		public override string ToString() => InterestUtilities.ToString(this, SegmentEnd);

		/// <inheritdoc />
		public IEnumerator<int> GetEnumerator()
		{
			int bitmask = 1;
			int bits = this.segment;
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

		private class InterestMerger : IInterestMerger<Interest32>
		{
			/// <inheritdoc />
			public IEqualityComparer<Interest32> Comparer => EqualityComparer<Interest32>.Default;

			/// <inheritdoc />
			public Interest32 None => Interest32.None;

			/// <inheritdoc />
			public Interest32 GetInterestByAdding(Interest32 current, Interest32 add) => current + add;

			/// <inheritdoc />
			public Interest32 GetInterestByRemoving(Interest32 current, Interest32 remove) => current - remove;
		}
	}
}
