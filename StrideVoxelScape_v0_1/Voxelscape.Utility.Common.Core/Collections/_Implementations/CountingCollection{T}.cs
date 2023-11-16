using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// Counts the number of times each item has been added or removed from the collection.
	/// Use the <see cref="CountOf"/> method to retrieve the count per item.
	/// The count of an item stops at 0 and never goes negative.
	/// When a count reaches 0 the item is removed from the collection.
	/// </summary>
	/// <typeparam name="T">The type of item to count.</typeparam>
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "Grouped by interface.")]
	public class CountingCollection<T> : ICollection<T>
	{
		/// <summary>
		/// The dictionary used to keep the count per item.
		/// </summary>
		private readonly IDictionary<T, int> countPerItem;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CountingCollection{T}"/> class.
		/// </summary>
		public CountingCollection()
			: this(Enumerable.Empty<T>(), null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CountingCollection{T}"/> class.
		/// </summary>
		/// <param name="comparer">The equality comparer to use.</param>
		public CountingCollection(IEqualityComparer<T> comparer)
			: this(Enumerable.Empty<T>(), comparer)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CountingCollection{T}" /> class.
		/// </summary>
		/// <param name="values">The initial values to populate the collection with.</param>
		public CountingCollection(IEnumerable<T> values)
			: this(values, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CountingCollection{T}"/> class.
		/// </summary>
		/// <param name="values">The initial values to populate the collection with.</param>
		/// <param name="comparer">The equality comparer to use.</param>
		public CountingCollection(IEnumerable<T> values, IEqualityComparer<T> comparer)
		{
			Contracts.Requires.That(values != null);

			this.countPerItem = new Dictionary<T, int>(comparer);

			foreach (T value in values)
			{
				this.Add(value);
			}
		}

		#endregion

		#region ICollection<T> Members

		/// <inheritdoc />
		public int Count => this.countPerItem.Count;

		/// <inheritdoc />
		public bool IsReadOnly => false;

		/// <inheritdoc />
		public void Add(T item)
		{
			ICollectionContracts.Add(this);

			int count;
			if (this.countPerItem.TryGetValue(item, out count))
			{
				this.countPerItem[item] = count + 1;
			}
			else
			{
				this.countPerItem[item] = 1;
			}
		}

		/// <inheritdoc />
		public void Clear()
		{
			ICollectionContracts.Clear(this);

			this.countPerItem.Clear();
		}

		/// <inheritdoc />
		public bool Contains(T item) => this.countPerItem.ContainsKey(item);

		/// <inheritdoc />
		public void CopyTo(T[] array, int arrayIndex)
		{
			ICollectionContracts.CopyTo(this, array, arrayIndex);

			this.countPerItem.Keys.CopyTo(array, arrayIndex);
		}

		/// <inheritdoc />
		public bool Remove(T item)
		{
			ICollectionContracts.Remove(this);

			int count;
			if (this.countPerItem.TryGetValue(item, out count))
			{
				if (count > 1)
				{
					this.countPerItem[item] = count - 1;
				}
				else
				{
					this.countPerItem.Remove(item);
				}

				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

		/// <inheritdoc />
		public IEnumerator<T> GetEnumerator() => this.countPerItem.Keys.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		#region Extra Public Members

		/// <summary>
		/// Gets an enumerable of all the counts of all the items in this <see cref="CountingCollection{T}"/> as
		/// <see cref="KeyValuePair{TKey, TValue}"/>s where the key is the item and the value is the count of that item.
		/// </summary>
		/// <value>
		/// An enumerable of the counts of all the items.
		/// </value>
		public IEnumerable<KeyValuePair<T, int>> ItemCounts => this.countPerItem;

		/// <summary>
		/// Gets an enumerable of all the items in this <see cref="CountingCollection{T}"/> where each item is repeated
		/// a number of times equal to its count in this collection.
		/// </summary>
		/// <value>
		/// The expanded enumeration of all items.
		/// </value>
		public IEnumerable<T> Expand
		{
			get
			{
				foreach (KeyValuePair<T, int> entry in this.countPerItem)
				{
					for (int count = 0; count < entry.Value; count++)
					{
						yield return entry.Key;
					}
				}
			}
		}

		/// <summary>
		/// Gets the count of the specified item.
		/// </summary>
		/// <param name="item">The item to get the count of.</param>
		/// <returns>The count of the specified item.</returns>
		public int CountOf(T item)
		{
			int count;
			if (this.countPerItem.TryGetValue(item, out count))
			{
				return count;
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Determines whether this collection contains the same count of the same elements as the specified collection.
		/// </summary>
		/// <param name="other">The collection to check this collection against.</param>
		/// <returns>True if both collections contain the same count of the same elements; otherwise false.</returns>
		public bool ContainsSameCountAs(CountingCollection<T> other)
		{
			Contracts.Requires.That(other != null);

			// this is just an optimization
			if (this == other)
			{
				return true;
			}

			// This is both an optimization and ensures that both collections have the same set
			// of KeyValuePairs given the foreach loop below. Without this check the foreach below
			// would enumerate each KeyValuePair in this collection, but the other could contain
			// more that we don't check.
			if (other.Count != this.Count)
			{
				return false;
			}

			foreach (var pair in this.countPerItem)
			{
				int countOfItem;
				if (other.countPerItem.TryGetValue(pair.Key, out countOfItem))
				{
					// check if the count of the other collection's entry matches this collection's current entry count
					if (countOfItem != pair.Value)
					{
						return false;
					}
				}
				else
				{
					// the other collection doesn't contain an entry to match this collection's current entry
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Subtracts the counts of items from the other collection from this collection.
		/// </summary>
		/// <param name="other">The other collection to subtract from this collection.</param>
		public void SubtractCountFrom(CountingCollection<T> other)
		{
			Contracts.Requires.That(other != null);

			foreach (KeyValuePair<T, int> entry in other.countPerItem)
			{
				// if this collection contains the item then subtract from it
				int count;
				if (this.countPerItem.TryGetValue(entry.Key, out count))
				{
					// this collection's item count equals itself minus the other collection's item count
					count -= entry.Value;

					if (count >= 1)
					{
						this.countPerItem[entry.Key] = count;
					}
					else
					{
						this.countPerItem.Remove(entry.Key);
					}
				}
			}
		}

		#endregion
	}
}
