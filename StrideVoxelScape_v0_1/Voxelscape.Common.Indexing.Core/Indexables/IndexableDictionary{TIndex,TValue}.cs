using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Indexables
{
	/// <summary>
	/// Allows an <see cref="IDictionary{TIndex, TValue}"/> to be treated as an <see cref="IIndexable{TIndex, TValue}"/>.
	/// The primary use case for this is to have an array backed by an dictionary that consumes less memory when significant
	/// portions of the array don't have values assigned to them.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index used for both the array and the dictionary interfaces.</typeparam>
	/// <typeparam name="TValue">The type of the value stored in the dictionary array.</typeparam>
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "Grouping by interface.")]
	public class IndexableDictionary<TIndex, TValue> : IIndexable<TIndex, TValue>, IDictionary<TIndex, TValue>
		where TIndex : IIndex, new()
	{
		/// <summary>
		/// The dictionary being wrapped in an indexable interface.
		/// </summary>
		private readonly IDictionary<TIndex, TValue> dictionary;

		/// <summary>
		/// Initializes a new instance of the <see cref="IndexableDictionary{TIndex, TValue}"/> class.
		/// </summary>
		/// <param name="dictionary">The dictionary to wrap.</param>
		public IndexableDictionary(IDictionary<TIndex, TValue> dictionary)
		{
			Contracts.Requires.That(dictionary != null);
			Contracts.Requires.That(!dictionary.IsReadOnly);

			this.dictionary = dictionary;
		}

		#region IIndexable<TIndex,TValue> Members

		/// <inheritdoc />
		public int Rank
		{
			get { return new TIndex().Rank; }
		}

		/// <inheritdoc />
		public TValue this[TIndex index]
		{
			get
			{
				IReadOnlyIndexableContracts.IndexerGet(this, index);

				TValue result;
				return this.dictionary.TryGetValue(index, out result) ? result : default(TValue);
			}

			set
			{
				IIndexableContracts.IndexerSet(this, index);

				this.dictionary[index] = value;
			}
		}

		/// <inheritdoc />
		public bool IsIndexValid(TIndex index)
		{
			IReadOnlyIndexableContracts.IsIndexValid(this, index);

			return true;
		}

		/// <inheritdoc />
		public bool TryGetValue(TIndex index, out TValue value)
		{
			IReadOnlyIndexableContracts.TryGetValue(this, index);

			if (!this.dictionary.TryGetValue(index, out value))
			{
				value = default(TValue);
			}

			return true;
		}

		/// <inheritdoc />
		public bool TrySetValue(TIndex index, TValue value)
		{
			IIndexableContracts.TrySetValue(this, index);

			this.dictionary[index] = value;
			return true;
		}

		#endregion

		#region IEnumerable<KeyValuePair<TIndex,TValue>> Members

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<TIndex, TValue>> GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion

		#region IDictionary<TIndex,TValue> Members

		/// <inheritdoc />
		TValue IDictionary<TIndex, TValue>.this[TIndex index]
		{
			get
			{
				IDictionaryContracts.IndexerGet(this, index);

				return this.dictionary[index];
			}

			set
			{
				IDictionaryContracts.IndexerSet(this, index);

				this.dictionary[index] = value;
			}
		}

		/// <inheritdoc />
		bool IDictionary<TIndex, TValue>.TryGetValue(TIndex index, out TValue value)
		{
			IDictionaryContracts.TryGetValue(index);

			return this.dictionary.TryGetValue(index, out value);
		}

		/// <inheritdoc />
		public void Add(TIndex key, TValue value)
		{
			IDictionaryContracts.Add(this, key);

			this.dictionary.Add(key, value);
		}

		/// <inheritdoc />
		public bool ContainsKey(TIndex key)
		{
			IDictionaryContracts.ContainsKey(key);

			return this.dictionary.ContainsKey(key);
		}

		/// <inheritdoc />
		public bool Remove(TIndex key)
		{
			IDictionaryContracts.Remove(this, key);

			return this.dictionary.Remove(key);
		}

		/// <inheritdoc />
		public ICollection<TIndex> Keys
		{
			get { return this.dictionary.Keys; }
		}

		/// <inheritdoc />
		public ICollection<TValue> Values
		{
			get { return this.dictionary.Values; }
		}

		#endregion

		#region ICollection<KeyValuePair<TIndex,TValue>> Members

		/// <inheritdoc />
		public void Add(KeyValuePair<TIndex, TValue> item)
		{
			ICollectionContracts.Add(this);

			this.dictionary.Add(item);
		}

		/// <inheritdoc />
		public bool Contains(KeyValuePair<TIndex, TValue> item)
		{
			return this.dictionary.Contains(item);
		}

		/// <inheritdoc />
		public bool Remove(KeyValuePair<TIndex, TValue> item)
		{
			ICollectionContracts.Remove(this);

			return this.dictionary.Remove(item);
		}

		/// <inheritdoc />
		public void Clear()
		{
			ICollectionContracts.Clear(this);

			this.dictionary.Clear();
		}

		/// <inheritdoc />
		public void CopyTo(KeyValuePair<TIndex, TValue>[] array, int arrayIndex)
		{
			ICollectionContracts.CopyTo(this, array, arrayIndex);

			this.dictionary.CopyTo(array, arrayIndex);
		}

		/// <inheritdoc />
		public int Count
		{
			get { return this.dictionary.Count; }
		}

		/// <inheritdoc />
		public bool IsReadOnly
		{
			get { return this.dictionary.IsReadOnly; }
		}

		#endregion
	}
}
