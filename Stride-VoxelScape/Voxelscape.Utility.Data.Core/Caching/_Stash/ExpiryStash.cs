using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Utility.Data.Core.Caching
{
	/// <summary>
	///
	/// </summary>
	/// <threadsafety static="true" instance="true" />
	public static class ExpiryStash
	{
		public static IExpiryStash<TKey> CreateInstantExpiry<TKey>() => new InstantExpiryStash<TKey>();

		public static IExpiryStash<TKey> CreateInfiniteCapacity<TKey>() => new QueueStash<TKey>();

		public static IExpiryStash<TKey> CreateCapacity<TKey>(int capacity)
		{
			Contracts.Requires.That(capacity >= 0 || capacity == Capacity.Unbounded);

			switch (capacity)
			{
				case 0: return new InstantExpiryStash<TKey>();
				case Capacity.Unbounded: return new QueueStash<TKey>();
				default: return new CompactingQueueStash<TKey>(capacity);
			}
		}
	}
}
