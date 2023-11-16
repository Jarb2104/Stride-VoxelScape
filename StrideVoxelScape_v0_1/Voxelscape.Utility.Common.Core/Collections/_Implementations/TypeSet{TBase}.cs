using System;
using System.Collections;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	public class TypeSet<TBase> : ITypeSet<TBase>
	{
		private readonly IDictionary<Type, TBase> values;

		#region Constructors

		public TypeSet()
			: this(0, null)
		{
		}

		public TypeSet(int capacity)
			: this(capacity, null)
		{
		}

		public TypeSet(IEqualityComparer<Type> comparer)
			: this(0, comparer)
		{
		}

		public TypeSet(int capacity, IEqualityComparer<Type> comparer)
		{
			Contracts.Requires.That(capacity >= 0);

			this.values = new Dictionary<Type, TBase>(capacity, comparer);
		}

		#endregion

		#region Collection Properties

		/// <inheritdoc />
		public int Count => this.values.Count;

		/// <inheritdoc />
		public bool IsReadOnly => false;

		/// <inheritdoc />
		ICollection<Type> IDictionary<Type, TBase>.Keys => this.values.Keys;

		/// <inheritdoc />
		ICollection<TBase> IDictionary<Type, TBase>.Values => this.values.Values;

		/// <inheritdoc />
		public IEnumerable<Type> Keys => this.values.Keys;

		/// <inheritdoc />
		public IEnumerable<TBase> Values => this.values.Values;

		#endregion

		#region Collection Indexer/Methods

		/// <inheritdoc />
		public TBase this[Type key]
		{
			get
			{
				ITypeSetContracts.IndexerGet(this, key);

				return this.values[key];
			}

			set
			{
				ITypeSetContracts.IndexerSet(this, key, value);

				this.values[key] = value;
			}
		}

		/// <inheritdoc />
		public bool Add(KeyValuePair<Type, TBase> item)
		{
			ITypeSetContracts.Add(this, item);

			return this.Add(item.Key, item.Value);
		}

		/// <inheritdoc />
		public bool Add(TBase value)
		{
			ITypeSetContracts.Add(this, value);

			return this.Add(value.GetType(), value);
		}

		/// <inheritdoc />
		public bool Add(Type key, TBase value)
		{
			ITypeSetContracts.Add(this, key, value);

			if (this.values.ContainsKey(key))
			{
				return false;
			}
			else
			{
				this.values[key] = value;
				return true;
			}
		}

		/// <inheritdoc />
		public bool Add<T>(T value)
			where T : TBase
		{
			ITypeSetContracts.Add(this, value);

			return this.Add(typeof(T), value);
		}

		/// <inheritdoc />
		void ICollection<KeyValuePair<Type, TBase>>.Add(KeyValuePair<Type, TBase> item) => this.Add(item);

		/// <inheritdoc />
		void ICollection<TBase>.Add(TBase item) => this.Add(item);

		/// <inheritdoc />
		void IDictionary<Type, TBase>.Add(Type key, TBase value) => this.Add(key, value);

		/// <inheritdoc />
		public void Clear()
		{
			ICollectionContracts.Clear<KeyValuePair<Type, TBase>>(this);

			this.values.Clear();
		}

		/// <inheritdoc />
		public bool Contains(KeyValuePair<Type, TBase> item) => this.values.Contains(item);

		/// <inheritdoc />
		public bool Contains(TBase item) => this.values.Contains(new KeyValuePair<Type, TBase>(item?.GetType(), item));

		/// <inheritdoc />
		public bool ContainsKey(Type key)
		{
			IDictionaryContracts.ContainsKey(key);

			return this.values.ContainsKey(key);
		}

		/// <inheritdoc />
		public bool ContainsKey<T>()
			where T : TBase => this.values.ContainsKey(typeof(T));

		/// <inheritdoc />
		public void CopyTo(KeyValuePair<Type, TBase>[] array, int arrayIndex)
		{
			ICollectionContracts.CopyTo(this, array, arrayIndex);

			this.values.CopyTo(array, arrayIndex);
		}

		/// <inheritdoc />
		public void CopyTo(TBase[] array, int arrayIndex)
		{
			ICollectionContracts.CopyTo(this, array, arrayIndex);

			this.values.Values.CopyTo(array, arrayIndex);
		}

		/// <inheritdoc />
		public bool Remove(KeyValuePair<Type, TBase> item)
		{
			ICollectionContracts.Remove<KeyValuePair<Type, TBase>>(this);

			return this.values.Remove(item);
		}

		/// <inheritdoc />
		public bool Remove(Type key)
		{
			ICollectionContracts.Remove<KeyValuePair<Type, TBase>>(this);

			return this.values.Remove(key);
		}

		/// <inheritdoc />
		public bool Remove(TBase item)
		{
			ICollectionContracts.Remove<TBase>(this);

			return this.values.Remove(new KeyValuePair<Type, TBase>(item?.GetType(), item));
		}

		/// <inheritdoc />
		public bool Remove<T>()
			where T : TBase
		{
			ITypeSetContracts.Remove(this);

			return this.values.Remove(typeof(T));
		}

		/// <inheritdoc />
		public bool TryGetValue(Type key, out TBase value)
		{
			IDictionaryContracts.TryGetValue(key);

			return this.values.TryGetValue(key, out value);
		}

		/// <inheritdoc />
		public bool TryGetValue<T>(out T value)
			where T : TBase
		{
			TBase baseValue;
			if (this.values.TryGetValue(typeof(T), out baseValue))
			{
				value = (T)baseValue;
				return true;
			}
			else
			{
				value = default(T);
				return false;
			}
		}

		#endregion

		#region ISet Methods

		/// <inheritdoc />
		public void ExceptWith(IEnumerable<TBase> other) => SetUtilities.ExceptWith(this, other);

		/// <inheritdoc />
		public void IntersectWith(IEnumerable<TBase> other) => SetUtilities.IntersectWith(this, other);

		/// <inheritdoc />
		public bool IsProperSubsetOf(IEnumerable<TBase> other) => SetUtilities.IsProperSubsetOf(this, other);

		/// <inheritdoc />
		public bool IsProperSupersetOf(IEnumerable<TBase> other) => SetUtilities.IsProperSupersetOf(this, other);

		/// <inheritdoc />
		public bool IsSubsetOf(IEnumerable<TBase> other) => SetUtilities.IsSubsetOf(this, other);

		/// <inheritdoc />
		public bool IsSupersetOf(IEnumerable<TBase> other) => SetUtilities.IsSupersetOf(this, other);

		/// <inheritdoc />
		public bool Overlaps(IEnumerable<TBase> other) => SetUtilities.Overlaps(this, other);

		/// <inheritdoc />
		public bool SetEquals(IEnumerable<TBase> other) => SetUtilities.SetEquals(this, other);

		/// <inheritdoc />
		public void SymmetricExceptWith(IEnumerable<TBase> other) => SetUtilities.SymmetricExceptWith(this, other);

		/// <inheritdoc />
		public void UnionWith(IEnumerable<TBase> other) => SetUtilities.UnionWith(this, other);

		#endregion

		#region Enumeration

		/// <inheritdoc />
		public IEnumerator<TBase> GetEnumerator() => this.values.Values.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		/// <inheritdoc />
		IEnumerator<KeyValuePair<Type, TBase>> IEnumerable<KeyValuePair<Type, TBase>>.GetEnumerator() =>
			this.values.GetEnumerator();

		#endregion
	}
}
