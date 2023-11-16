using System.Threading;
using Voxelscape.Utility.Concurrency.Pact.Synchronization;

namespace Voxelscape.Utility.Concurrency.Core.Synchronization
{
	/// <summary>
	///
	/// </summary>
	public class AtomicLong : IAtomicValue<long>
	{
		private long value;

		public AtomicLong(long value)
		{
			this.value = value;
		}

		/// <inheritdoc />
		public long Add(long value) => Interlocked.Add(ref this.value, value);

		/// <inheritdoc />
		public long CompareExchange(long value, long comparand) =>
			Interlocked.CompareExchange(ref this.value, value, comparand);

		/// <inheritdoc />
		public long Decrement() => Interlocked.Decrement(ref this.value);

		/// <inheritdoc />
		public long Exchange(long value) => Interlocked.Exchange(ref this.value, value);

		/// <inheritdoc />
		public long Increment() => Interlocked.Increment(ref this.value);

		/// <inheritdoc />
		public long Read() => InterlockedValue.Read(ref this.value);

		/// <inheritdoc />
		public void Write(long value) => this.Exchange(value);
	}
}
