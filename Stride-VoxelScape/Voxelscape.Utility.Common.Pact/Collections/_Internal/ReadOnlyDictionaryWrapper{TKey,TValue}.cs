using System.Collections;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Collections
{
	internal class ReadOnlyDictionaryWrapper<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
	{
		private readonly IDictionary<TKey, TValue> dictionary;

		public ReadOnlyDictionaryWrapper(IDictionary<TKey, TValue> dictionary)
		{
			Contracts.Requires.That(dictionary != null);

			this.dictionary = dictionary;
		}

		/// <inheritdoc />
		public int Count => this.dictionary.Count;

		/// <inheritdoc />
		public IEnumerable<TKey> Keys => this.dictionary.Keys;

		/// <inheritdoc />
		public IEnumerable<TValue> Values => this.dictionary.Values;

		/// <inheritdoc />
		public TValue this[TKey key]
		{
			get
			{
				IReadOnlyDictionaryContracts.Indexer(this, key);

				return this.dictionary[key];
			}
		}

		/// <inheritdoc />
		public bool ContainsKey(TKey key)
		{
			IReadOnlyDictionaryContracts.ContainsKey(key);

			return this.dictionary.ContainsKey(key);
		}

		/// <inheritdoc />
		public bool TryGetValue(TKey key, out TValue value)
		{
			IReadOnlyDictionaryContracts.TryGetValue(key);

			return this.dictionary.TryGetValue(key, out value);
		}

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this.dictionary.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
