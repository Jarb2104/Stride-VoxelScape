using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Stages.Management.Core.Chunks;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Core.Caching;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapChunkCacheBuilder : IChunkCacheBuilder<
		ChunkOverheadKey, ISkyIslandMapChunk, IReadOnlySkyIslandMapChunk, SkyIslandMapChunkResources>
	{
		private readonly IRasterChunkConfig<Index2D> chunkConfig;

		private readonly IAsyncChunkPopulator<ISkyIslandMapChunk> chunkPopulator;

		private IStageBounds stageBounds;

		private SkyIslandMaps outOfBoundsValue;

		private bool modifyingThrowsException;

		public SkyIslandMapChunkCacheBuilder(
			IRasterChunkConfig<Index2D> chunkConfig, IAsyncChunkPopulator<ISkyIslandMapChunk> chunkPopulator)
		{
			Contracts.Requires.That(chunkConfig != null);
			Contracts.Requires.That(chunkPopulator != null);

			this.chunkConfig = chunkConfig;
			this.ResourceFactory = new SkyIslandMapChunkResourcesFactory(chunkConfig);
			this.chunkPopulator = chunkPopulator;
		}

		/// <inheritdoc />
		public IRasterChunkConfig ChunkConfig => this.chunkConfig;

		/// <inheritdoc />
		public IFactory<SkyIslandMapChunkResources> ResourceFactory { get; }

		/// <inheritdoc />
		public IExpiryStashPool<ChunkOverheadKey, SkyIslandMapChunkResources> ResourceStash { get; set; }

		/// <inheritdoc />
		public bool DisposeResourceStashWithCache { get; set; } = true;

		/// <inheritdoc />
		public IChunkCache<ChunkOverheadKey, ISkyIslandMapChunk, IReadOnlySkyIslandMapChunk> Build()
		{
			IChunkCacheBuilderContracts.Build(this);

			var cache = this.CreateCache();

			if (this.stageBounds != null)
			{
				var outOfBounds = new ExteriorSkyIslandMapChunkFactory(
					this.chunkConfig, this.outOfBoundsValue, this.modifyingThrowsException);

				cache = new BoundedChunkCache<ChunkOverheadKey, ISkyIslandMapChunk>(
					this.stageBounds.Contains, cache, outOfBounds);
			}

			return new ChunkCache<ChunkOverheadKey, ISkyIslandMapChunk, IReadOnlySkyIslandMapChunk>(cache);
		}

		public void SetFiniteBounds(
			IStageBounds stageBounds, SkyIslandMaps outOfBoundsValue, bool modifyingThrowsException = true)
		{
			Contracts.Requires.That(stageBounds != null);

			this.stageBounds = stageBounds;
			this.outOfBoundsValue = outOfBoundsValue;
			this.modifyingThrowsException = modifyingThrowsException;
		}

		private IAsyncCache<ChunkOverheadKey, ISkyIslandMapChunk> CreateCache()
		{
			Contracts.Requires.That(this.ResourceStash != null);

			var chunkFactory = ChunkFactory.Caching.CreateStashed(
				new SkyIslandMapChunkPoolingFactory(this.ResourceStash, this.chunkPopulator),
				this.ResourceStash);

			var cache = new AsyncCache<ChunkOverheadKey, ISkyIslandMapChunk>(chunkFactory);

			if (this.DisposeResourceStashWithCache)
			{
				return new CacheDisposalWrapper<ChunkOverheadKey, ISkyIslandMapChunk>(cache, this.ResourceStash);
			}
			else
			{
				return cache;
			}
		}
	}
}
