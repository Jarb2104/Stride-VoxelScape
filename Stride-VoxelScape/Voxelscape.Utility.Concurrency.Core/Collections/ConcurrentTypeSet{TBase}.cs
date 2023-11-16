using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Concurrency.Core.Collections
{
	public class ConcurrentTypeSet<TBase>
	{
		private readonly ConcurrentDictionary<Type, TBase> values;

		public ConcurrentTypeSet()
			: this(EqualityComparer<Type>.Default)
		{
		}

		public ConcurrentTypeSet(IEqualityComparer<Type> comparer)
		{
			Contracts.Requires.That(comparer != null);

			this.values = new ConcurrentDictionary<Type, TBase>(comparer);
		}

		public bool IsEmpty => this.values.IsEmptyLockFree();

		public int Count => this.values.Count;

		public void Clear() => this.values.Clear();

		public T AddOrUpdate<T>(T addValue, Func<T, T> updateValueFactory)
			where T : TBase
		{
			Contracts.Requires.That(updateValueFactory != null);

			return (T)this.values.AddOrUpdate(typeof(T), addValue, (type, old) => updateValueFactory((T)old));
		}

		public T AddOrUpdate<T>(T addValue, Func<T> addValueFactory, Func<T, T> updateValueFactory)
			where T : TBase
		{
			Contracts.Requires.That(addValueFactory != null);
			Contracts.Requires.That(updateValueFactory != null);

			return (T)this.values.AddOrUpdate(
				typeof(T), type => addValueFactory(), (type, old) => updateValueFactory((T)old));
		}

		public T GetOrAdd<T>(T value)
			where T : TBase => (T)this.values.GetOrAdd(typeof(T), value);

		public T GetOrAdd<T>(Func<T> valueFactory)
			where T : TBase
		{
			Contracts.Requires.That(valueFactory != null);

			return (T)this.values.GetOrAdd(typeof(T), type => valueFactory());
		}

		public bool TryAdd<T>(T value)
			where T : TBase => this.values.TryAdd(typeof(T), value);

		public bool TryGetValue<T>(out T value)
			where T : TBase
		{
			TBase baseValue;
			bool result = this.values.TryGetValue(typeof(T), out baseValue);
			value = (T)baseValue;
			return result;
		}

		public bool TryRemove<T>(out T value)
			where T : TBase
		{
			TBase baseValue;
			bool result = this.values.TryRemove(typeof(T), out baseValue);
			value = (T)baseValue;
			return result;
		}

		public bool TryRemove<T>()
			where T : TBase => this.values.TryRemove(typeof(T));

		public bool TryUpdate<T>(T newValue, T comparisonValue)
			where T : TBase => this.values.TryUpdate(typeof(T), newValue, comparisonValue);
	}
}
