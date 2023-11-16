using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Concurrency.Core.Synchronization
{
	/// <summary>
	/// A threadsafe counter that can only be disposed when the count is 0.
	/// Once disposed the count can no longer change.
	/// </summary>
	/// <threadsafety static="true" instance="true" />
	public class LockableCounter
	{
		private static readonly int Locked = -1;

		private readonly object lockThis = new object();

		private int count;

		public LockableCounter(int count = 0)
		{
			Contracts.Requires.That(count >= 0);

			this.count = count;
		}

		public bool IsLocked
		{
			get
			{
				lock (this.lockThis)
				{
					return this.count == Locked;
				}
			}
		}

		public int CurrentCount
		{
			get
			{
				lock (this.lockThis)
				{
					return this.count.ClampLower(0);
				}
			}
		}

		public bool TryLock()
		{
			lock (this.lockThis)
			{
				return this.DoTryLock();
			}
		}

		#region TryAddCount

		public bool TryAddCount(int count = 1)
		{
			int unused;
			return this.TryAddCount(count, out unused);
		}

		public bool TryAddCount(out int currentCount) => this.TryAddCount(1, out currentCount);

		public bool TryAddCount(int count, out int currentCount)
		{
			Contracts.Requires.That(count >= 0);

			lock (this.lockThis)
			{
				if (this.count == Locked)
				{
					currentCount = 0;
					return false;
				}

				this.count += count;
				currentCount = this.count;
				return true;
			}
		}

		#endregion

		#region TryRemoveCount

		public bool TryRemoveCount(int count = 1)
		{
			int unused;
			return this.TryRemoveCount(count, out unused);
		}

		public bool TryRemoveCount(out int currentCount) => this.TryRemoveCount(1, out currentCount);

		public bool TryRemoveCount(int count, out int currentCount)
		{
			Contracts.Requires.That(count >= 0);

			lock (this.lockThis)
			{
				if (this.count <= 0)
				{
					currentCount = 0;
					return false;
				}

				this.RemoveCount(count);
				currentCount = this.count;
				return true;
			}
		}

		#endregion

		#region TryRemoveCountAndLock

		public bool TryRemoveCountAndLock(int count = 1)
		{
			int unused;
			return this.TryRemoveCountAndLock(count, out unused);
		}

		public bool TryRemoveCountAndLock(out int currentCount) => this.TryRemoveCountAndLock(1, out currentCount);

		public bool TryRemoveCountAndLock(int count, out int currentCount)
		{
			Contracts.Requires.That(count >= 0);

			lock (this.lockThis)
			{
				if (this.count <= 0)
				{
					currentCount = 0;
					return false;
				}

				this.RemoveCount(count);
				currentCount = this.count;
				return this.DoTryLock();
			}
		}

		#endregion

		private bool DoTryLock()
		{
			if (this.count == 0)
			{
				this.count = Locked;
				return true;
			}
			else
			{
				return false;
			}
		}

		private void RemoveCount(int count) => this.count = (this.count - count).ClampLower(0);
	}
}
