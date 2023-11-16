using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Persistence;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public class VoxelGridChunkStorePopulator : IAsyncChunkPopulator<IVoxelGridChunk>
	{
		private readonly IChunkStore<ChunkKey, SerializedVoxelGridChunk> store;

		private readonly IChunkPersister<IVoxelGridChunk, SerializedVoxelGridChunk> persister;

		private readonly IChunkPopulator<IVoxelGridChunk> fallback;

		public VoxelGridChunkStorePopulator(
			IChunkStore<ChunkKey, SerializedVoxelGridChunk> store,
			IChunkPersister<IVoxelGridChunk, SerializedVoxelGridChunk> persister,
			IChunkPopulator<IVoxelGridChunk> fallback)
		{
			Contracts.Requires.That(store != null);
			Contracts.Requires.That(persister != null);
			Contracts.Requires.That(fallback != null);

			this.store = store;
			this.persister = persister;
			this.fallback = fallback;
		}

		/// <inheritdoc />
		public async Task PopulateAsync(IVoxelGridChunk chunk)
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
