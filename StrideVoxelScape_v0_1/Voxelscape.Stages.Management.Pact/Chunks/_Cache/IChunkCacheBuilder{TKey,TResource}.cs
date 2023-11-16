using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Stages.Management.Pact.Chunks
{
	public interface IChunkCacheBuilder<TKey, TResource>
	{
		IRasterChunkConfig ChunkConfig { get; }

		IFactory<TResource> ResourceFactory { get; }

		IExpiryStashPool<TKey, TResource> ResourceStash { get; set; }

		bool DisposeResourceStashWithCache { get; set; }
	}
}
