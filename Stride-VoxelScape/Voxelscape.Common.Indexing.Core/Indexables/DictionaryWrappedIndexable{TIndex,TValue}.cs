using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Indexables
{
	/// <summary>
	/// Allows an <see cref="IIndexable{TIndex, TValue}"/> to be treated as an <see cref="IDictionary{TIndex, TValue}"/>.
	/// The primary use case for this is to have a dictionary backed by an array that use straight array lookups instead
	/// of computing potentially expensive hash codes.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index used for both the array and the dictionary interfaces.</typeparam>
	/// <typeparam name="TValue">The type of the value stored in the dictionary array.</typeparam>
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "Grouping by interface.")]
	public class DictionaryWrappedIndexable<TIndex, TValue> :
		IIndexable<TIndex, TValue>,
		IDictionary<TIndex, TValue>,
		IEnumerable<KeyValuePair<TIndex, TryValue<TValue>>>
		where TIndex : IIndex
	{
		/// <summary>
		/// The indexable being wrapped in a dictionary interface.
		/// </summary>
		private readonly IIndexable<TIndex, TryValue<TValue>> indexable;

		/// <summary>
		/// The count of elements entered in the dictionary. This state can be derived from the indexable,
		/// but doing so is computationally expensive to determine dynamically so instead the state is duplicated here.
		/// </summary>
		private int count = 0;

		/// <summary>
		/// Initializes a new instance of the <see cref="DictionaryWrappedIndexable{TIndex, TValue}" /> class.
		/// </summary>
		/// <param name="indexable">The indexable instance to wrap.</param>
		public DictionaryWrappedIndexable(IIndexable<TIndex, TryValue<TValue>> indexable)
		{
			Contracts.Requires.That(indexable != null);

			this.indexable = indexable;
		}

		#region IIndexable<TIndex,TValue> Members

		/// <inheritdoc />
		public int Rank
		{
			get { return this.indexable.Rank; }
		}

		/// <inheritdoc />
		public bool IsIndexValid(TIndex index)
		{
			IReadOnlyIndexableContracts.IsIndexValid(this, index);

			return this.indexable.IsIndexValid(index);
		}

		/// <inheritdoc />
		public bool TrySetValue(TIndex index, TValue value)
		{
			IIndexableContracts.TrySetValue(this, index);

			if (this.IsIndexValid(index))
			{
				// delegate to IDictionary implementation to ensure count is maintained properly
				this[index] = value;
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region IEnumerable<KeyValuePair<TIndex,TValue>> Members

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<TIndex, TValue>> GetEnumerator()
		{
			foreach (var entry in this.indexable)
			{
				if (entry.Value.HasValue)
				{
					yield return new KeyValuePair<TIndex, TValue>(entry.Key, entry.Value.Value);
				}
			}
		}

		#endregion

		#region IEnumerable Members

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion

		#region IEnumerable<KeyValuePair<TIndex, AssignableValue<TValue>>> Members

		/// <inheritdoc />
		IEnumerator<KeyValuePair<TIndex, TryValue<TValue>>>
			IEnumerable<KeyValuePair<TIndex, TryValue<TValue>>>.GetEnumerator()
		{
			return this.indexable.GetEnumerator();
		}

		#endregion

		#region IDictionary<TIndex,TValue> Members

		/// <inheritdoc />
		public TValue this[TIndex index]
		{
			// this method's contracts will check that the dictionary contains the key first
			get
			{
				IDictionaryContracts.IndexerGet(this, index);
				IReadOnlyIndexableContracts.IndexerGet(this, index);

				return this.indexable[index].Value;
			}

			set
			{
				IDictionaryContracts.IndexerSet(this, index);
				IIndexableContracts.IndexerSet(this, index);

				if (!this.indexable[index].HasValue)
				{
					this.count++;
				}

				this.indexable[index] = TryValue.New(value);
			}
		}

		/// <inheritdoc />
		public void Add(TIndex key, TValue value)
		{
			IDictionaryContracts.Add(this, key);

			// this method's contracts checks that the dictionary doesn't already contain this key
			// thus if it passes the contract a new value will always be added
			this.indexable[key] = TryValue.New(value);
			this.count++;
		}

		/// <inheritdoc />
		public bool Remove(TIndex key)
		{
			IDictionaryContracts.Remove(this, key);

			if (!this.ContainsKey(key))
			{
				return false;
			}

			this.indexable[key] = TryValue.None<TValue>();
			this.count--;
			return true;
		}

		/// <inheritdoc />
		public bool TryGetValue(TIndex key, out TValue value)
		{
			IDictionaryContracts.TryGetValue(key);
			IReadOnlyIndexableContracts.TryGetValue(this, key);

			TryValue<TValue> result;
			if (this.indexable.TryGetValue(key, out result))
			{
				if (result.HasValue)
				{
					value = result.Value;
					return true;
				}
			}

			// either the try get failed or the result had no value. Failure either way
			value = default(TValue);
			return false;
		}

		/// <inheritdoc />
		public bool ContainsKey(TIndex key)
		{
			IDictionaryContracts.ContainsKey(key);

			TValue unused;
			return this.TryGetValue(key, out unused);
		}

		/// <inheritdoc />
		public ICollection<TIndex> Keys
		{
			get
			{
				return new ReadOnlyCollection<TIndex>((
					from entry in this.indexable
					where entry.Value.HasValue
					select entry.Key).ToArray());
			}
		}

		/// <inheritdoc />
		public ICollection<TValue> Values
		{
			get
			{
				return new ReadOnlyCollection<TValue>((
					from entry in this.indexable
					where entry.Value.HasValue
					select entry.Value.Value).ToArray());
			}
		}

		#endregion

		#region ICollection<KeyValuePair<TIndex,TValue>> Members

		/// <inheritdoc />
		public void Add(KeyValuePair<TIndex, TValue> item)
		{
			ICollectionContracts.Add(this);

			this.Add(item.Key, item.Value);
		}

		/// <inheritdoc />
		public bool Remove(KeyValuePair<TIndex, TValue> item)
		{
			ICollectionContracts.Remove(this);

			if (!this.Contains(item))
			{
				return false;
			}

			this.indexable[item.Key] = TryValue.None<TValue>();
			this.count--;
			return true;
		}

		/// <inheritdoc />
		public bool Contains(KeyValuePair<TIndex, TValue> item)
		{
			TValue result;
			if (this.TryGetValue(item.Key, out result))
			{
				return result.EqualsNullSafe(item.Value);
			}
			else
			{
				return false;
			}
		}

		/// <inheritdoc />
		public void Clear()
		{
			ICollectionContracts.Clear(this);

			foreach (var entry in this.indexable)
			{
				this.indexable[entry.Key] = TryValue.None<TValue>();
			}

			this.count = 0;
		}

		/// <inheritdoc />
		public void CopyTo(KeyValuePair<TIndex, TValue>[] array, int arrayIndex)
		{
			ICollectionContracts.CopyTo(this, array, arrayIndex);

			foreach (KeyValuePair<TIndex, TValue> entry in this)
			{
				array[arrayIndex] = entry;
				arrayIndex++;
			}
		}

		/// <inheritdoc />
		public int Count
		{
			get { return this.count; }
		}

		/// <inheritdoc />
		public bool IsReadOnly
		{
			get { return false; }
		}

		#endregion
	}
}
