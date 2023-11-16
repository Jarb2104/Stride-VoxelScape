using System.Collections;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Collections;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// Provides comparison and equality logic for <see cref="BitArray"/>s.
	/// </summary>
	public class BitArrayEqualityComparer : EqualityComparer<BitArray>
	{
		/// <summary>
		/// Prevents a default instance of the <see cref="BitArrayEqualityComparer"/> class from being created.
		/// </summary>
		private BitArrayEqualityComparer()
		{
		}

		/// <summary>
		/// Gets the singleton instance of the <see cref="BitArrayEqualityComparer"/> comparer.
		/// </summary>
		/// <value>
		/// The singleton comparer instance.
		/// </value>
		public static BitArrayEqualityComparer Instance { get; } = new BitArrayEqualityComparer();

		#region Overriding EqualityComparer<BitArray> Members

		/// <inheritdoc />
		public override bool Equals(BitArray a, BitArray b)
		{
			if (a == b)
			{
				return true;
			}

			int[] valuesA = a.CopyToIntArray();
			int[] valuesB = b.CopyToIntArray();

			if (valuesA.Length != valuesB.Length)
			{
				return false;
			}

			for (int index = 0; index < valuesA.Length; index++)
			{
				if (valuesA[index] != valuesB[index])
				{
					return false;
				}
			}

			return true;
		}

		/// <inheritdoc />
		public override int GetHashCode(BitArray obj)
		{
			IEqualityComparerContracts.GetHashCode(obj);

			unchecked
			{
				int[] values = obj.CopyToIntArray();

				int hash = 13;

				foreach (int value in values)
				{
					hash = (hash * 7) + value;
				}

				return hash;
			}
		}

		#endregion
	}
}
