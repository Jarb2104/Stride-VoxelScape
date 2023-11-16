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
	public struct Interest16 : IEquatable<Interest16>, IEnumerable<int>
	{
		private static readonly int SegmentEnd = PrimitiveTypeSizes.UShortNumberOfBits;

		private readonly ushort segment;

		private Interest16(int segment)
		{
			this.segment = (ushort)segment;
		}

		public static IInterestMerger<Interest16> Merger { get; } = new InterestMerger();

		public static Interest16 None => new Interest16(0);

		public static Interest16 operator +(Interest16 lhs, Interest16 rhs) =>
			new Interest16(lhs.segment | rhs.segment);

		public static Interest16 operator -(Interest16 lhs, Interest16 rhs) =>
			new Interest16(lhs.segment & ~rhs.segment);

		public static Interest16 New(int interest)
		{
			Contracts.Requires.That(interest.IsIn(Range.FromLength(SegmentEnd)));

			return new Interest16(1 << interest);
		}

		public bool HasFlag(Interest16 flags) => (this.segment & flags.segment) == flags.segment;

		public bool HasAnyFlag(Interest16 flags) => (this.segment & flags.segment) != 0;

		/// <inheritdoc />
		public bool Equals(Interest16 other) => this.segment == other.segment;

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

		private class InterestMerger : IInterestMerger<Interest16>
		{
			/// <inheritdoc />
			public IEqualityComparer<Interest16> Comparer => EqualityComparer<Interest16>.Default;

			/// <inheritdoc />
			public Interest16 None => Interest16.None;

			/// <inheritdoc />
			public Interest16 GetInterestByAdding(Interest16 current, Interest16 add) => current + add;

			/// <inheritdoc />
			public Interest16 GetInterestByRemoving(Interest16 current, Interest16 remove) => current - remove;
		}
	}
}
