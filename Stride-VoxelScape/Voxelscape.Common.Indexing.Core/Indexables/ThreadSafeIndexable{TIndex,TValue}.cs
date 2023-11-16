using System.Collections;
using System.Collections.Generic;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Indexables
{
	/// <summary>
	/// Decorates an <see cref="IIndexable{TIndex, TValue}" /> to add thread safety.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <remarks>
	/// <para>
	/// The thread safety provided by this decorator is a single lock used to synchronize all mutable access to the decorated
	/// instance. Thus only a single thread may ever be reading from or writing to the decorated instance. This is the simplest
	/// sharing policy that should be sufficient for the majority of use cases.
	/// </para><para>
	/// Only 'self contained' implementations of <see cref="IIndexable{TIndex, TValue}" /> can be guaranteed to be made thread
	/// safe using this wrapper. 'Self contained' means that the implementation does not interact with shared external
	/// dependencies and does not possess the ability to execute arbitrary code. For example, it does not exposes events or
	/// observables. Exposing such allows arbitrary code to be ran under the lock provided by this decorator and could
	/// result in dead locks.
	/// </para>
	/// </remarks>
	/// <threadsafety static="true" instance="true" />
	public class ThreadSafeIndexable<TIndex, TValue> : IIndexable<TIndex, TValue>
		where TIndex : IIndex
	{
		/// <summary>
		/// The lock guarding the wrapped indexable instance.
		/// </summary>
		private readonly object indexableLock = new object();

		/// <summary>
		/// The wrapped indexable instance.
		/// </summary>
		private readonly IIndexable<TIndex, TValue> indexable;

		/// <summary>
		/// Initializes a new instance of the <see cref="ThreadSafeIndexable{TIndex, TValue}"/> class.
		/// </summary>
		/// <param name="indexable">The indexable to decorate with thread safety.</param>
		public ThreadSafeIndexable(IIndexable<TIndex, TValue> indexable)
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
		public TValue this[TIndex index]
		{
			get
			{
				IReadOnlyIndexableContracts.IndexerGet(this, index);

				lock (this.indexableLock)
				{
					return this.indexable[index];
				}
			}

			set
			{
				IIndexableContracts.IndexerSet(this, index);

				lock (this.indexableLock)
				{
					this.indexable[index] = value;
				}
			}
		}

		/// <inheritdoc />
		public bool IsIndexValid(TIndex index)
		{
			IReadOnlyIndexableContracts.IsIndexValid(this, index);

			lock (this.indexableLock)
			{
				return this.indexable.IsIndexValid(index);
			}
		}

		/// <inheritdoc />
		public bool TryGetValue(TIndex index, out TValue value)
		{
			IReadOnlyIndexableContracts.TryGetValue(this, index);

			lock (this.indexableLock)
			{
				return this.indexable.TryGetValue(index, out value);
			}
		}

		/// <inheritdoc />
		public bool TrySetValue(TIndex index, TValue value)
		{
			IIndexableContracts.TrySetValue(this, index);

			lock (this.indexableLock)
			{
				return this.indexable.TrySetValue(index, value);
			}
		}

		#endregion

		#region IEnumerable<KeyValuePair<TIndex,TValue>> Members

		/// <inheritdoc />
		/// <remarks>
		/// This enumerator implementation uses snapshot semantics and has an upfront cost of O(N) performance.
		/// </remarks>
		public IEnumerator<KeyValuePair<TIndex, TValue>> GetEnumerator()
		{
			List<KeyValuePair<TIndex, TValue>> snapshot = new List<KeyValuePair<TIndex, TValue>>();

			lock (this.indexableLock)
			{
				snapshot.AddMany(this.indexable);
			}

			return snapshot.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion
	}
}
