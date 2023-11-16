using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// Provides comparison and equality logic for <see cref="IReadOnlyDictionary{TKey, TValue}" />.
	/// Two dictionaries are considered equivalent if they contain the same set of <see cref="KeyValuePair{TKey, TValue}"/>.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	public class ReadOnlyDictionaryEqualityComparer<TKey, TValue> : EqualityComparer<IReadOnlyDictionary<TKey, TValue>>
	{
		/// <summary>
		/// The key equality comparer.
		/// </summary>
		private readonly IEqualityComparer<TKey> keyComparer;

		/// <summary>
		/// The value equality comparer.
		/// </summary>
		private readonly IEqualityComparer<TValue> valueComparer;

		/// <summary>
		/// Initializes a new instance of the <see cref="ReadOnlyDictionaryEqualityComparer{TKey, TValue}"/> class.
		/// </summary>
		/// <param name="keyComparer">The key equality comparer.</param>
		public ReadOnlyDictionaryEqualityComparer(IEqualityComparer<TKey> keyComparer)
			: this(keyComparer, EqualityComparer<TValue>.Default)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReadOnlyDictionaryEqualityComparer{TKey, TValue}"/> class.
		/// </summary>
		/// <param name="keyComparer">The key equality comparer.</param>
		/// <param name="valueComparer">The value equality comparer.</param>
		public ReadOnlyDictionaryEqualityComparer(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			Contracts.Requires.That(keyComparer != null);
			Contracts.Requires.That(valueComparer != null);

			this.keyComparer = keyComparer;
			this.valueComparer = valueComparer;
		}

		/// <summary>
		/// Gets the singleton instance of the <see cref="ReadOnlyDictionaryEqualityComparer{TKey, TValue}"/> comparer.
		/// </summary>
		/// <value>
		/// The singleton comparer instance.
		/// </value>
		public static ReadOnlyDictionaryEqualityComparer<TKey, TValue> Instance { get; } =
			new ReadOnlyDictionaryEqualityComparer<TKey, TValue>(EqualityComparer<TKey>.Default);

		#region Overriding EqualityComparer<IReadOnlyDictionary<TKey, TValue>> Members

		/// <inheritdoc />
		public override bool Equals(IReadOnlyDictionary<TKey, TValue> a, IReadOnlyDictionary<TKey, TValue> b)
		{
			if (a == b)
			{
				return true;
			}

			if (a == null || b == null)
			{
				return false;
			}

			if (a.Count != b.Count)
			{
				return false;
			}

			foreach (KeyValuePair<TKey, TValue> pair in a)
			{
				TValue value;
				if (!b.TryGetValue(pair.Key, out value))
				{
					return false;
				}

				if (!this.valueComparer.Equals(pair.Value, value))
				{
					return false;
				}
			}

			return true;
		}

		/// <inheritdoc />
		public override int GetHashCode(IReadOnlyDictionary<TKey, TValue> obj)
		{
			IEqualityComparerContracts.GetHashCode(obj);

			int result = 0;

			foreach (KeyValuePair<TKey, TValue> pair in obj)
			{
				result ^= this.keyComparer.GetHashCode(pair.Key);
				result ^= this.valueComparer.GetHashCode(pair.Value);
			}

			return result;
		}

		#endregion
	}
}
