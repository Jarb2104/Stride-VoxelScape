using System;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Utility.Data.Core.Pools
{
	/// <summary>
	///
	/// </summary>
	/// <threadsafety static="true" instance="true" />
	public static class PoolOptions
	{
		public static IPoolOptions<T> Default<T>() => DefaultOptions<T>.Instance;

		private static class DefaultOptions<T>
		{
			public static IPoolOptions<T> Instance { get; } = CreateInstance();

			private static IPoolOptions<T> CreateInstance()
			{
				Action<T> action = value => { };
				return new ImmutablePoolOptions<T>(action, action, Capacity.Unbounded);
			}
		}
	}
}
