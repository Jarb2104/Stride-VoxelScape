using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Core.Synchronization;
using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Utility.Data.Core.Pools
{
	/// <summary>
	///
	/// </summary>
	internal static class BoundedFactoryUtilities
	{
		public static AtomicInt GetRemainingCount<T>(IPool<T> pool)
		{
			Contracts.Requires.That(pool != null);

			if (pool.BoundedCapacity == Capacity.Unbounded)
			{
				return null;
			}

			return new AtomicInt((pool.BoundedCapacity - pool.AvailableCount).ClampLower(0));
		}
	}
}
