using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Persistence;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapChunkStorePopulator : IAsyncChunkPopulator<ISkyIslandMapChunk>
	{
		private readonly IChunkStore<ChunkOverheadKey, SerializedSkyIslandMapChunk> store;

		private readonly IChunkPersister<ISkyIslandMapChunk, SerializedSkyIslandMapChunk> persister;

		private readonly IChunkPopulator<ISkyIslandMapChunk> fallback;

		public SkyIslandMapChunkStorePopulator(
			IChunkStore<ChunkOverheadKey, SerializedSkyIslandMapChunk> store,
			IChunkPersister<ISkyIslandMapChunk, SerializedSkyIslandMapChunk> persister,
			IChunkPopulator<ISkyIslandMapChunk> fallback)
		{
			Contracts.Requires.That(store != null);
			Contracts.Requires.That(persister != null);
			Contracts.Requires.That(fallback != null);

			this.store = store;
			this.persister = persister;
			this.fallback = fallback;
		}

		/// <inheritdoc />
		public async Task PopulateAsync(ISkyIslandMapChunk chunk)
		{
			IAsyncChunkPopulatorContracts.PopulateAsync(chunk);

			var chunkEntity = await this.store.TryGetAsync(chunk.Key).DontMarshallContext();

			if (chunkEntity.HasValue)
			{
				this.persister.FromPersistable(chunkEntity.Value, chunk);
			}
			else
			{
				this.fallback.Populate(chunk);
			}
		}
	}
}
