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
	public struct Interest8 : IEquatable<Interest8>, IEnumerable<int>
	{
		private static readonly int SegmentEnd = PrimitiveTypeSizes.ByteNumberOfBits;

		private readonly byte segment;

		private Interest8(int segment)
		{
			this.segment = (byte)segment;
		}

		public static IInterestMerger<Interest8> Merger { get; } = new InterestMerger();

		public static Interest8 None => new Interest8(0);

		public static Interest8 operator +(Interest8 lhs, Interest8 rhs) =>
			new Interest8(lhs.segment | rhs.segment);

		public static Interest8 operator -(Interest8 lhs, Interest8 rhs) =>
			new Interest8(lhs.segment & ~rhs.segment);

		public static Interest8 New(int interest)
		{
			Contracts.Requires.That(interest.IsIn(Range.FromLength(SegmentEnd)));

			return new Interest8(1 << interest);
		}

		public bool HasFlag(Interest8 flags) => (this.segment & flags.segment) == flags.segment;

		public bool HasAnyFlag(Interest8 flags) => (this.segment & flags.segment) != 0;

		/// <inheritdoc />
		public bool Equals(Interest8 other) => this.segment == other.segment;

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

		private class InterestMerger : IInterestMerger<Interest8>
		{
			/// <inheritdoc />
			public IEqualityComparer<Interest8> Comparer => EqualityComparer<Interest8>.Default;

			/// <inheritdoc />
			public Interest8 None => Interest8.None;

			/// <inheritdoc />
			public Interest8 GetInterestByAdding(Interest8 current, Interest8 add) => current + add;

			/// <inheritdoc />
			public Interest8 GetInterestByRemoving(Interest8 current, Interest8 remove) => current - remove;
		}
	}
}
