using System.Collections;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	public class DictionaryWrapper<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>, IDictionary<TKey, TValue>
	{
		public DictionaryWrapper(IDictionary<TKey, TValue> dictionary)
		{
			Contracts.Requires.That(dictionary != null);

			this.Dictionary = dictionary;
		}

		/// <inheritdoc />
		public virtual int Count => this.Dictionary.Count;

		/// <inheritdoc />
		public virtual bool IsReadOnly => this.Dictionary.IsReadOnly;

		/// <inheritdoc />
		public virtual IEnumerable<TKey> Keys => this.Dictionary.Keys;

		/// <inheritdoc />
		public virtual IEnumerable<TValue> Values => this.Dictionary.Values;

		/// <inheritdoc />
		ICollection<TKey> IDictionary<TKey, TValue>.Keys => this.Dictionary.Keys;

		/// <inheritdoc />
		ICollection<TValue> IDictionary<TKey, TValue>.Values => this.Dictionary.Values;

		protected IDictionary<TKey, TValue> Dictionary { get; }

		/// <inheritdoc />
		public virtual TValue this[TKey key]
		{
			get
			{
				IDictionaryContracts.IndexerGet(this, key);

				return this.Dictionary[key];
			}

			set
			{
				IDictionaryContracts.IndexerSet(this, key);

				this.Dictionary[key] = value;
			}
		}

		/// <inheritdoc />
		public virtual void Add(KeyValuePair<TKey, TValue> item)
		{
			ICollectionContracts.Add(this);

			this.Dictionary.Add(item);
		}

		/// <inheritdoc />
		public virtual void Add(TKey key, TValue value)
		{
			IDictionaryContracts.Add(this, key);

			this.Dictionary.Add(key, value);
		}

		/// <inheritdoc />
		public virtual bool Remove(KeyValuePair<TKey, TValue> item)
		{
			ICollectionContracts.Remove(this);

			return this.Dictionary.Remove(item);
		}

		/// <inheritdoc />
		public virtual bool Remove(TKey key)
		{
			IDictionaryContracts.Remove(this, key);

			return this.Dictionary.Remove(key);
		}

		/// <inheritdoc />
		public virtual bool TryGetValue(TKey key, out TValue value)
		{
			IDictionaryContracts.TryGetValue(key);

			return this.Dictionary.TryGetValue(key, out value);
		}

		/// <inheritdoc />
		public virtual bool Contains(KeyValuePair<TKey, TValue> item) => this.Dictionary.Contains(item);

		/// <inheritdoc />
		public virtual bool ContainsKey(TKey key)
		{
			IDictionaryContracts.ContainsKey(key);

			return this.Dictionary.ContainsKey(key);
		}

		/// <inheritdoc />
		public virtual void Clear()
		{
			ICollectionContracts.Clear(this);

			this.Dictionary.Clear();
		}

		/// <inheritdoc />
		public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			ICollectionContracts.CopyTo(this, array, arrayIndex);

			this.Dictionary.CopyTo(array, arrayIndex);
		}

		/// <inheritdoc />
		public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this.Dictionary.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
