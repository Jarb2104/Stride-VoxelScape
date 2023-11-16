using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Collections;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// Provides equality comparisons for <see cref="KeyValuePair{TKey, TValue}"/> based on only the Key properties.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	public class KeyEqualityComparer<TKey, TValue> : EqualityComparer<KeyValuePair<TKey, TValue>>
	{
		/// <summary>
		/// Prevents a default instance of the <see cref="KeyEqualityComparer{TKey, TValue}"/> class from being created.
		/// </summary>
		private KeyEqualityComparer()
		{
		}

		/// <summary>
		/// Gets the singleton instance of the <see cref="KeyEqualityComparer{TKey, TValue}"/> comparer.
		/// </summary>
		/// <value>
		/// The singleton comparer instance.
		/// </value>
		public static KeyEqualityComparer<TKey, TValue> Instance { get; } = new KeyEqualityComparer<TKey, TValue>();

		#region Overriding EqualityComparer<KeyValuePair<TKey,TValue>> Members

		/// <inheritdoc />
		public override bool Equals(KeyValuePair<TKey, TValue> lhs, KeyValuePair<TKey, TValue> rhs)
		{
			return lhs.Key.EqualsNullSafe(rhs.Key);
		}

		/// <inheritdoc />
		public override int GetHashCode(KeyValuePair<TKey, TValue> pair)
		{
			IEqualityComparerContracts.GetHashCode(pair);

			return pair.Key.GetHashCodeNullSafe();
		}

		#endregion
	}
}
