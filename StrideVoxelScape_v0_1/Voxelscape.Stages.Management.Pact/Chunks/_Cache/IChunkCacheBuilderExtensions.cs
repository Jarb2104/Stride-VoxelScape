using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Core.Caching;
using Voxelscape.Utility.Data.Core.Pools;
using Voxelscape.Utility.Data.Pact.Pools;

/// <summary>
/// Provides extension methods for <see cref="IChunkCacheBuilder{TKey, TResource}"/>.
/// </summary>
public static class IChunkCacheBuilderExtensions
{
	public static int CalculateResourcePoolCapacity<TKey, TResource>(
		this IChunkCacheBuilder<TKey, TResource> builder, long approximateCapacityInBytes)
	{
		Contracts.Requires.That(builder != null);
		Contracts.Requires.That(approximateCapacityInBytes > 0);

		long result = approximateCapacityInBytes / builder.ChunkConfig.ApproximateSizeInBytes;
		return (int)result.Clamp(1, int.MaxValue);
	}

	public static IPool<TResource> CreateAndAssignStandardResourceStash<TKey, TResource>(
		this IChunkCacheBuilder<TKey, TResource> builder,
		long approximateCapacityInBytes,
		ChunkCacheBuilderOptions<TResource> options = null)
	{
		Contracts.Requires.That(builder != null);
		Contracts.Requires.That(approximateCapacityInBytes > 0);

		var poolOptions = new PoolOptions<TResource>();
		options = HandleOptions(poolOptions, options);
		poolOptions.BoundedCapacity = builder.CalculateResourcePoolCapacity(approximateCapacityInBytes);

		IPool<TResource> pool;
		if (options.EagerFillPool)
		{
			pool = Pool.New(poolOptions);
			pool.GiveUntilFull(builder.ResourceFactory);
		}
		else
		{
			pool = Pool.WithFactory.Bounded.New(builder.ResourceFactory, poolOptions);
		}

		builder.ResourceStash = PoolStash.Create<TKey, TResource>(pool, options.StashCapacityMultiplier);
		return pool;
	}

	public static TrackingPool<TResource> CreateAndAssignTrackingResourceStash<TKey, TResource>(
		this IChunkCacheBuilder<TKey, TResource> builder,
		long approximateCapacityInBytes,
		ChunkCacheBuilderOptions<TResource> options = null)
	{
		Contracts.Requires.That(builder != null);
		Contracts.Requires.That(approximateCapacityInBytes > 0);

		var poolOptions = new TrackingPoolOptions<TResource>();
		options = HandleOptions(poolOptions, options);
		poolOptions.BoundedCapacity = builder.CalculateResourcePoolCapacity(approximateCapacityInBytes);

		TrackingPool<TResource> pool;
		if (options.EagerFillPool)
		{
			pool = Pool.Tracking.New(poolOptions);
			pool.GiveUntilFull(builder.ResourceFactory);
		}
		else
		{
			pool = Pool.Tracking.WithFactory.Bounded.New(builder.ResourceFactory, poolOptions);
		}

		builder.ResourceStash = PoolStash.Create<TKey, TResource>(pool, options.StashCapacityMultiplier);
		return pool;
	}

	private static ChunkCacheBuilderOptions<TResource> HandleOptions<TResource>(
		PoolOptions<TResource> poolOptions, ChunkCacheBuilderOptions<TResource> options)
	{
		Contracts.Requires.That(poolOptions != null);

		options = options ?? Options<TResource>.Default;
		poolOptions.ResetAction = options.ResetResource ?? Options<TResource>.Default.ResetResource;
		poolOptions.ReleaseAction = options.ReleaseResource ?? Options<TResource>.Default.ReleaseResource;
		return options;
	}

	private static class Options<TResource>
	{
		public static ChunkCacheBuilderOptions<TResource> Default { get; } = new ChunkCacheBuilderOptions<TResource>();
	}
}
