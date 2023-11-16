using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Caching;
using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Utility.Data.Core.Caching
{
	/// <summary>
	///
	/// </summary>
	public static class PoolStash
	{
		public static PoolStash<TKey, TValue> Create<TKey, TValue>(
			IPool<TValue> pool, double stashCapacityMultiplier = 1)
		{
			Contracts.Requires.That(pool != null);
			Contracts.Requires.That(stashCapacityMultiplier >= 0);

			IExpiryStash<TKey> stash;
			if (pool.BoundedCapacity == Capacity.Unbounded)
			{
				stash = ExpiryStash.CreateInfiniteCapacity<TKey>();
			}
			else
			{
				var temp = pool.BoundedCapacity * stashCapacityMultiplier;
				var capacity = temp < int.MaxValue ? (int)temp : int.MaxValue;
				stash = ExpiryStash.CreateCapacity<TKey>(capacity);
			}

			return new PoolStash<TKey, TValue>(pool, stash);
		}
	}
}
