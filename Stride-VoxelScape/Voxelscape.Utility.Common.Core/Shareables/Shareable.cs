using System.Threading;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Shareables;

namespace Voxelscape.Utility.Common.Core.Shareables
{
	/// <summary>
	///
	/// </summary>
	/// <threadsafety static="true" instance="true" />
	public class Shareable : IShareable
	{
		private int count;

		public Shareable()
			: this(0)
		{
		}

		public Shareable(int count)
		{
			Contracts.Requires.That(count >= 0);

			this.count = count;
		}

		/// <inheritdoc />
		public bool IsShared => Thread.VolatileRead(ref this.count) > 0;

		/// <inheritdoc />
		public int ShareCount => Thread.VolatileRead(ref this.count);

		/// <inheritdoc />
		public void Share() => Interlocked.Increment(ref this.count);

		/// <inheritdoc />
		public bool Unshare()
		{
			IShareableContracts.Unshare(this);

			// contract requires that is instance is shared before calling unshare
			// so count should never go below 0
			return Interlocked.Decrement(ref this.count) <= 0;
		}
	}
}
