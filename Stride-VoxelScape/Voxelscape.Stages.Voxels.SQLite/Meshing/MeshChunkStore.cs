using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Persistence;
using Voxelscape.Stages.Voxels.Pact.Meshing;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Stages.Voxels.SQLite.Meshing
{
	/// <summary>
	///
	/// </summary>
	public class MeshChunkStore : IChunkStore<ChunkKey, SerializedMeshChunk>
	{
		private readonly IAsyncStore store;

		public MeshChunkStore(IAsyncStore store)
		{
			Contracts.Requires.That(store != null);

			this.store = store;
		}

		/// <inheritdoc />
		public Task AddOrUpdateAllAsync(
			IEnumerable<SerializedMeshChunk> chunks,
			CancellationToken cancellation = default(CancellationToken))
		{
			IChunkStoreContracts.AddOrUpdateAllAsync(chunks);

			var entities = chunks.Select(chunk => new MeshChunkEntity()
			{
				ChunkKey = chunk.Key,
				SerializedData = chunk.SerializedData,
			});

			return this.store.AddOrUpdateAllAsync(entities, cancellation);
		}

		public async Task<TryValue<SerializedMeshChunk>> TryGetAsync(
			ChunkKey key, CancellationToken cancellation = default(CancellationToken))
		{
			IChunkStoreContracts.TryGetAsync(key);

			var tryEntity = await this.store.WhereAsync<MeshChunkEntity>(
				entity => entity.X == key.Index.X && entity.Y == key.Index.Y && entity.Z == key.Index.Z,
				cancellation).SingleOrNoneAsync().DontMarshallContext();

			return tryEntity.Select(entity => new SerializedMeshChunk(key, entity.SerializedData));
		}
	}
}
