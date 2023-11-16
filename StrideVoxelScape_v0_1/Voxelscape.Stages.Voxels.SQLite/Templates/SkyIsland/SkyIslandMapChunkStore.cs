using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Persistence;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Stages.Voxels.SQLite.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapChunkStore : IChunkStore<ChunkOverheadKey, SerializedSkyIslandMapChunk>
	{
		private readonly IAsyncStore store;

		public SkyIslandMapChunkStore(IAsyncStore store)
		{
			Contracts.Requires.That(store != null);

			this.store = store;
		}

		/// <inheritdoc />
		public Task AddOrUpdateAllAsync(
			IEnumerable<SerializedSkyIslandMapChunk> chunks,
			CancellationToken cancellation = default(CancellationToken))
		{
			IChunkStoreContracts.AddOrUpdateAllAsync(chunks);

			var entities = chunks.Select(chunk => new SkyIslandMapChunkEntity()
			{
				ChunkKey = chunk.Key,
				SerializedData = chunk.SerializedData,
			});

			return this.store.AddOrUpdateAllAsync(entities, cancellation);
		}

		public async Task<TryValue<SerializedSkyIslandMapChunk>> TryGetAsync(
			ChunkOverheadKey key, CancellationToken cancellation = default(CancellationToken))
		{
			IChunkStoreContracts.TryGetAsync(key);

			var tryEntity = await this.store.WhereAsync<SkyIslandMapChunkEntity>(
				entity => entity.X == key.Index.X && entity.Y == key.Index.Y,
				cancellation).SingleOrNoneAsync().DontMarshallContext();

			return tryEntity.Select(entity => new SerializedSkyIslandMapChunk(key, entity.SerializedData));
		}
	}
}
