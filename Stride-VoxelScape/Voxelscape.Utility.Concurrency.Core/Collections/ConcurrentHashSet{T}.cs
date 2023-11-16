using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Core.Synchronization;

namespace Voxelscape.Utility.Concurrency.Core.Collections
{
	public class ConcurrentHashSet<T> : IEnumerable<T>
	{
		private readonly ConcurrentDictionary<T, T> set;

		private readonly AtomicInt count = new AtomicInt(0);

		public ConcurrentHashSet()
			: this(EqualityComparer<T>.Default)
		{
		}

		public ConcurrentHashSet(IEqualityComparer<T> comparer)
		{
			Contracts.Requires.That(comparer != null);

			this.set = new ConcurrentDictionary<T, T>(comparer);
		}

		public int Count => this.count.Read();

		public IEnumerable<T> ValuesSnapshot => this.set.Keys;

		public bool Contains(T value)
		{
			Contracts.Requires.That(value != null);

			return this.set.ContainsKey(value);
		}

		public bool TryAdd(T value)
		{
			Contracts.Requires.That(value != null);

			bool success = this.set.TryAdd(value, value);
			if (success)
			{
				this.count.Increment();
			}

			return success;
		}

		public bool TryGet(T value, out T result) => this.set.TryGetValue(value, out result);

		public T GetOrAdd(T value) => this.set.GetOrAdd(value, value);

		public bool TryRemove(T value)
		{
			Contracts.Requires.That(value != null);

			bool success = this.set.TryRemove(value);
			if (success)
			{
				this.count.Decrement();
			}

			return success;
		}

		public void Clear()
		{
			// ConcurrentDictionary's Clear method can't be used because of potential race conditions when trying to update
			// this.count to 0, so instead take a snapshot of the current values in the hashset and only remove those
			// (this does acquire all locks). New values added while clear is running will not be removed.
			foreach (var value in this.set.Keys)
			{
				// try remove handles maintaining the count properly
				this.TryRemove(value);
			}
		}

		/// <inheritdoc />
		/// <remarks>
		/// <para>
		/// This is a lock free implementation that does not use snapshot semantics.
		/// </para><para>
		/// Values can be removed from the set the moment this enumerator returns them or added
		/// while the enumerator is running without returning them.
		/// </para>
		/// </remarks>
		public IEnumerator<T> GetEnumerator() => this.set.KeysLockFree().GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
