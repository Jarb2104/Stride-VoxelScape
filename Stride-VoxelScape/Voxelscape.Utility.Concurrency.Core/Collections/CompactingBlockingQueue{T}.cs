using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Concurrency.Core.Collections
{
	public class CompactingBlockingQueue<T>
	{
		private static readonly int UninitializedIndex = -1;

		private readonly T[] values;

		private readonly Predicate<T> isValueStale;

		/// <summary>
		/// The index of the first value in the queue, or <see cref="UninitializedIndex"/> if empty.
		/// </summary>
		private int head = UninitializedIndex;

		/// <summary>
		/// The index of the last value in the queue, or <see cref="UninitializedIndex"/> if empty.
		/// </summary>
		private int tail = UninitializedIndex;

		public CompactingBlockingQueue(Predicate<T> isValueStale, int capacity)
		{
			Contracts.Requires.That(isValueStale != null);
			Contracts.Requires.That(capacity >= 0);

			this.isValueStale = isValueStale;
			this.values = new T[capacity];
		}

		private bool IsEmpty => this.head == UninitializedIndex;

		private bool IsFull => this.Count == this.values.Length;

		private int Count
		{
			get
			{
				if (this.IsEmpty)
				{
					return 0;
				}

				if (this.head <= this.tail)
				{
					return this.tail - this.head + 1;
				}
				else
				{
					return this.values.Length - (this.head - this.tail - 1);
				}
			}
		}

		public bool TryAdd(T value)
		{
			lock (this.values)
			{
				if (this.values.Length == 0)
				{
					return false;
				}

				if (this.isValueStale(value))
				{
					// queue still accepts stale values but no need to bother storing them
					// because TryTake would refuse to return it anyway
					return true;
				}

				if (this.IsFull)
				{
					this.Compact();

					if (this.isValueStale(value))
					{
						// compacting can take awhile so check if stale again
						return true;
					}

					if (this.IsFull)
					{
						return false;
					}
				}

				this.IncrementIndex(ref this.tail);
				this.values[this.tail] = value;

				if (this.IsEmpty)
				{
					this.head = 0;
				}

				return true;
			}
		}

		public bool TryTake(out T value)
		{
			lock (this.values)
			{
				while (this.DoTryTake(out value))
				{
					if (!this.isValueStale(value))
					{
						return true;
					}
				}

				return false;
			}
		}

		private bool DoTryTake(out T value)
		{
			if (this.IsEmpty)
			{
				value = default(T);
				return false;
			}

			value = this.values[this.head];
			this.values[this.head] = default(T);

			if (this.head == this.tail)
			{
				// the only remaining value was removed
				this.head = UninitializedIndex;
				this.tail = UninitializedIndex;
			}
			else
			{
				this.IncrementIndex(ref this.head);
			}

			return true;
		}

		private void IncrementIndex(ref int index) => index = (index == this.values.Length - 1) ? 0 : index + 1;

		private void Compact()
		{
			bool isEmpty = true;
			int source = this.head - 1;
			int newTail = source;
			int totalCount = this.Count;

			for (int count = 0; count < totalCount; count++)
			{
				this.IncrementIndex(ref source);

				var value = this.values[source];
				if (this.isValueStale(value))
				{
					this.values[source] = default(T);
				}
				else
				{
					isEmpty = false;
					this.IncrementIndex(ref newTail);

					if (newTail != source)
					{
						this.values[newTail] = value;
						this.values[source] = default(T);
					}
				}
			}

			if (isEmpty)
			{
				this.head = UninitializedIndex;
				this.tail = UninitializedIndex;
			}
			else
			{
				this.tail = newTail;
			}
		}
	}
}
