using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Core.Caching;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Stages.Management.Core.Chunks
{
	public class CacheStashedChunkFactory<TKey, TChunk> : IAsyncFactory<TKey, ICacheValue<TKey, TChunk>>
	{
		private readonly IAsyncFactory<TKey, IDisposableValue<TChunk>> factory;

		private readonly IExpiryStash<TKey> stash;

		public CacheStashedChunkFactory(
			IAsyncFactory<TKey, IDisposableValue<TChunk>> factory, IExpiryStash<TKey> stash)
		{
			Contracts.Requires.That(factory != null);
			Contracts.Requires.That(stash != null);

			this.factory = factory;
			this.stash = stash;
		}

		/// <inheritdoc />
		public async Task<ICacheValue<TKey, TChunk>> CreateAsync(TKey key) => CacheValue.CreateStashed(
			key, await this.factory.CreateAsync(key).DontMarshallContext(), this.stash);
	}
}
