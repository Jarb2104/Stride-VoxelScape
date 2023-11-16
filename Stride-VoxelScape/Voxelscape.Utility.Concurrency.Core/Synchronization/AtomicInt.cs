using System.Threading;
using Voxelscape.Utility.Concurrency.Pact.Synchronization;

namespace Voxelscape.Utility.Concurrency.Core.Synchronization
{
	/// <summary>
	///
	/// </summary>
	public class AtomicInt : IAtomicValue<int>
	{
		private int value;

		public AtomicInt(int value)
		{
			this.value = value;
		}

		/// <inheritdoc />
		public int Add(int value) => Interlocked.Add(ref this.value, value);

		/// <inheritdoc />
		public int CompareExchange(int value, int comparand) =>
			Interlocked.CompareExchange(ref this.value, value, comparand);

		/// <inheritdoc />
		public int Decrement() => Interlocked.Decrement(ref this.value);

		/// <inheritdoc />
		public int Exchange(int value) => Interlocked.Exchange(ref this.value, value);

		/// <inheritdoc />
		public int Increment() => Interlocked.Increment(ref this.value);

		/// <inheritdoc />
		public int Read() => InterlockedValue.Read(ref this.value);

		/// <inheritdoc />
		public void Write(int value) => this.Exchange(value);
	}
}
