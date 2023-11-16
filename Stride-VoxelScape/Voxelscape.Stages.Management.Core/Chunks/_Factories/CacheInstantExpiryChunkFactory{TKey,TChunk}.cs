using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Core.Caching;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Stages.Management.Core.Chunks
{
	public class CacheInstantExpiryChunkFactory<TKey, TChunk> : IFactory<TKey, ICacheValue<TKey, TChunk>>
	{
		private readonly IFactory<TKey, TChunk> factory;

		public CacheInstantExpiryChunkFactory(IFactory<TKey, TChunk> factory)
		{
			Contracts.Requires.That(factory != null);

			this.factory = factory;
		}

		/// <inheritdoc />
		public ICacheValue<TKey, TChunk> Create(TKey key) =>
			CacheValue.CreateInstantExpiry(key, this.factory.Create(key));
	}
}
