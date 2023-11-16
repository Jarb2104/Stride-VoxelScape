using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Persistence;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Stages.Voxels.SQLite.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public class VoxelGridChunkStore : IChunkStore<ChunkKey, SerializedVoxelGridChunk>
	{
		private readonly IAsyncStore store;

		public VoxelGridChunkStore(IAsyncStore store)
		{
			Contracts.Requires.That(store != null);

			this.store = store;
		}

		/// <inheritdoc />
		public Task AddOrUpdateAllAsync(
			IEnumerable<SerializedVoxelGridChunk> chunks,
			CancellationToken cancellation = default(CancellationToken))
		{
			IChunkStoreContracts.AddOrUpdateAllAsync(chunks);

			var entities = chunks.Select(chunk => new VoxelGridChunkEntity()
			{
				ChunkKey = chunk.Key,
				SerializedData = chunk.SerializedData,
			});

			return this.store.AddOrUpdateAllAsync(entities, cancellation);
		}

		public async Task<TryValue<SerializedVoxelGridChunk>> TryGetAsync(
			ChunkKey key, CancellationToken cancellation = default(CancellationToken))
		{
			IChunkStoreContracts.TryGetAsync(key);

			var tryEntity = await this.store.WhereAsync<VoxelGridChunkEntity>(
				entity => entity.X == key.Index.X && entity.Y == key.Index.Y && entity.Z == key.Index.Z,
				cancellation).SingleOrNoneAsync().DontMarshallContext();

			return tryEntity.Select(entity => new SerializedVoxelGridChunk(key, entity.SerializedData));
		}
	}
}
