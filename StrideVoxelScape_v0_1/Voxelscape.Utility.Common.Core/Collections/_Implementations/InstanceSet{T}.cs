using System.Collections;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// A set of unique instances to retrieve by providing logically equivalent instances to acts as keys.
	/// </summary>
	/// <typeparam name="T">The type of instances contained in the set.</typeparam>
	public class InstanceSet<T> : ISet<T>
		where T : class
	{
		/// <summary>
		/// The set instances contained in this collection.
		/// </summary>
		private readonly IDictionary<T, T> instances;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="InstanceSet{T}"/> class.
		/// </summary>
		/// <param name="comparer">The comparer to use for equality comparisons.</param>
		public InstanceSet(IEqualityComparer<T> comparer)
		{
			this.instances = new Dictionary<T, T>(comparer);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InstanceSet{T}"/> class.
		/// </summary>
		/// <param name="capacity">The initial capacity of the set.</param>
		/// <param name="comparer">The comparer to use for equality comparisons.</param>
		public InstanceSet(int capacity, IEqualityComparer<T> comparer)
		{
			Contracts.Requires.That(capacity >= 0);

			this.instances = new Dictionary<T, T>(capacity, comparer);
		}

		#endregion

		#region ICollection<T> Members

		/// <inheritdoc />
		public int Count
		{
			get { return this.instances.Count; }
		}

		/// <inheritdoc />
		public bool IsReadOnly
		{
			get { return false; }
		}

		/// <inheritdoc />
		void ICollection<T>.Add(T item)
		{
			ICollectionContracts.Add(this);

			this.Add(item);
		}

		/// <inheritdoc />
		public void Clear()
		{
			ICollectionContracts.Clear(this);

			this.instances.Clear();
		}

		/// <inheritdoc />
		public bool Contains(T item)
		{
			if (item == null)
			{
				return false;
			}

			return this.instances.ContainsKey(item);
		}

		/// <inheritdoc />
		public void CopyTo(T[] array, int arrayIndex)
		{
			ICollectionContracts.CopyTo(this, array, arrayIndex);

			this.instances.Keys.CopyTo(array, arrayIndex);
		}

		/// <inheritdoc />
		public bool Remove(T item)
		{
			ICollectionContracts.Remove(this);

			return this.instances.Remove(item);
		}

		#endregion

		#region ISet<T> Members

		/// <inheritdoc />
		public bool Add(T item)
		{
			if (item == null)
			{
				return false;
			}

			return this.instances.AddIfNewKey(item, item);
		}

		/// <inheritdoc />
		public void ExceptWith(IEnumerable<T> other)
		{
			SetUtilities.ExceptWith(this, other);
		}

		/// <inheritdoc />
		public void IntersectWith(IEnumerable<T> other)
		{
			SetUtilities.IntersectWith(this, other);
		}

		/// <inheritdoc />
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return SetUtilities.IsProperSubsetOf(this, other);
		}

		/// <inheritdoc />
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return SetUtilities.IsProperSupersetOf(this, other);
		}

		/// <inheritdoc />
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return SetUtilities.IsSubsetOf(this, other);
		}

		/// <inheritdoc />
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return SetUtilities.IsSupersetOf(this, other);
		}

		/// <inheritdoc />
		public bool Overlaps(IEnumerable<T> other)
		{
			return SetUtilities.Overlaps(this, other);
		}

		/// <inheritdoc />
		public bool SetEquals(IEnumerable<T> other)
		{
			return SetUtilities.SetEquals(this, other);
		}

		/// <inheritdoc />
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			SetUtilities.SymmetricExceptWith(this, other);
		}

		/// <inheritdoc />
		public void UnionWith(IEnumerable<T> other)
		{
			SetUtilities.UnionWith(this, other);
		}

		#endregion

		#region IEnumerable<T> Members

		/// <inheritdoc />
		public IEnumerator<T> GetEnumerator()
		{
			return this.instances.Keys.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion

		#region InstanceSet<T> Members

		/// <summary>
		/// Gets the instance contained within this set that is logically equivalent to the specified instance.
		/// </summary>
		/// <param name="item">The item that is logically equivalent to the instance to retrieve.</param>
		/// <returns>The equivalent instance contained within this set.</returns>
		public T GetInstanceOf(T item)
		{
			Contracts.Requires.That(item != null);
			Contracts.Requires.That(this.Contains(item));

			return this.instances[item];
		}

		/// <summary>
		/// Tries to get the instance contained within this set that is logically equivalent to the specified instance.
		/// </summary>
		/// <param name="item">The item that is logically equivalent to the instance to retrieve.</param>
		/// <param name="result">The equivalent instance contained within this set if one is found; otherwise null.</param>
		/// <returns>True if a logically equivalent instance is found; otherwise false.</returns>
		public bool TryGetInstanceOf(T item, out T result)
		{
			if (item == null)
			{
				result = default(T);
				return false;
			}

			return this.instances.TryGetValue(item, out result);
		}

		#endregion
	}
}
