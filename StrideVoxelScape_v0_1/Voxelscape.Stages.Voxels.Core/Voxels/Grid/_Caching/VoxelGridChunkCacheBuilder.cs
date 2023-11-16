using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Stages.Management.Core.Chunks;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Core.Caching;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Stages.Voxels.Core.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public class VoxelGridChunkCacheBuilder :
		IChunkCacheBuilder<ChunkKey, IVoxelGridChunk, IReadOnlyVoxelGridChunk, VoxelGridChunkResources>
	{
		private readonly IRasterChunkConfig<Index3D> chunkConfig;

		private readonly IAsyncChunkPopulator<IVoxelGridChunk> chunkPopulator;

		private IStageBounds stageBounds;

		private TerrainVoxel outOfBoundsValue;

		private bool modifyingThrowsException;

		public VoxelGridChunkCacheBuilder(
			IRasterChunkConfig<Index3D> chunkConfig, IAsyncChunkPopulator<IVoxelGridChunk> chunkPopulator)
		{
			Contracts.Requires.That(chunkConfig != null);
			Contracts.Requires.That(chunkPopulator != null);

			this.chunkConfig = chunkConfig;
			this.ResourceFactory = new VoxelGridChunkResourcesFactory(chunkConfig);
			this.chunkPopulator = chunkPopulator;
		}

		/// <inheritdoc />
		public IRasterChunkConfig ChunkConfig => this.chunkConfig;

		/// <inheritdoc />
		public IFactory<VoxelGridChunkResources> ResourceFactory { get; }

		/// <inheritdoc />
		public IExpiryStashPool<ChunkKey, VoxelGridChunkResources> ResourceStash { get; set; }

		/// <inheritdoc />
		public bool DisposeResourceStashWithCache { get; set; } = true;

		/// <inheritdoc />
		public IChunkCache<ChunkKey, IVoxelGridChunk, IReadOnlyVoxelGridChunk> Build()
		{
			IChunkCacheBuilderContracts.Build(this);

			var cache = this.CreateCache();

			if (this.stageBounds != null)
			{
				var outOfBounds = new ExteriorVoxelGridChunkFactory(
					this.chunkConfig, this.outOfBoundsValue, this.modifyingThrowsException);

				cache = new BoundedChunkCache<ChunkKey, IVoxelGridChunk>(
					this.stageBounds.Contains, cache, outOfBounds);
			}

			return new ChunkCache<ChunkKey, IVoxelGridChunk, IReadOnlyVoxelGridChunk>(cache);
		}

		public void SetFiniteBounds(
			IStageBounds stageBounds, TerrainVoxel outOfBoundsValue, bool modifyingThrowsException = true)
		{
			Contracts.Requires.That(stageBounds != null);

			this.stageBounds = stageBounds;
			this.outOfBoundsValue = outOfBoundsValue;
			this.modifyingThrowsException = modifyingThrowsException;
		}

		private IAsyncCache<ChunkKey, IVoxelGridChunk> CreateCache()
		{
			Contracts.Requires.That(this.ResourceStash != null);

			var chunkFactory = ChunkFactory.Caching.CreateStashed(
				new VoxelGridChunkPoolingFactory(this.ResourceStash, this.chunkPopulator),
				this.ResourceStash);

			var cache = new AsyncCache<ChunkKey, IVoxelGridChunk>(chunkFactory);

			if (this.DisposeResourceStashWithCache)
			{
				return new CacheDisposalWrapper<ChunkKey, IVoxelGridChunk>(cache, this.ResourceStash);
			}
			else
			{
				return cache;
			}
		}
	}
}
