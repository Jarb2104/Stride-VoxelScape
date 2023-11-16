using System.Threading.Tasks;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Stages.Management.Core.Chunks;
using Voxelscape.Stages.Management.Core.Generation;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Stages.Management.Pact.Persistence;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Core.Pools;
using Voxelscape.Utility.Data.Pact.Pools;
using static Voxelscape.Stages.Management.Core.Generation.BatchSerializeGenerationOptions;

namespace Voxelscape.Stages.Voxels.Core.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public class VoxelGridAbsolutePhase : AbstractGenerationPhase
	{
		private readonly IPool<VoxelGridChunkResources> pool;

		public VoxelGridAbsolutePhase(
			IStageIdentity stageIdentity,
			IStageBounds stageBounds,
			IRasterChunkConfig<Index3D> chunkConfig,
			IAsyncChunkPopulator<IVoxelGridChunk> chunkPopulator,
			IChunkStore<ChunkKey, SerializedVoxelGridChunk> chunkStore,
			int maxBufferedChunks,
			BatchSerializeGenerationOptions options = null)
		{
			Contracts.Requires.That(stageIdentity != null);
			Contracts.Requires.That(stageBounds != null);
			Contracts.Requires.That(chunkConfig != null);
			Contracts.Requires.That(chunkPopulator != null);
			Contracts.Requires.That(chunkStore != null);
			Contracts.Requires.That(maxBufferedChunks > 0);

			var phaseIdentity = new GenerationPhaseIdentity(nameof(VoxelGridAbsolutePhase));
			var chunkKeys = new ChunkKeyCollection(stageBounds);

			var poolOptions = new PoolOptions<VoxelGridChunkResources>()
			{
			};

			this.pool = Pool.WithFactory.New(new VoxelGridChunkResourcesFactory(chunkConfig), poolOptions);

			var chunkFactory = new VoxelGridChunkPoolingFactory(this.pool, chunkPopulator);
			var serializer = new VoxelGridChunkResourcesSerializer(
				TerrainVoxelSerializer.Get[options?.SerializationEndianness ?? DefaultSerializationEndianness],
				chunkConfig);
			var persister = new VoxelGridChunkPersister(serializer);
			var persistableFactory = ChunkFactory.Persister.Create(chunkFactory, persister);

			this.Phase = new ChunkedBatchingPhase<ChunkKey, SerializedVoxelGridChunk>(
				stageIdentity,
				phaseIdentity,
				chunkKeys,
				persistableFactory,
				chunkStore,
				maxBufferedChunks,
				options);

			this.Completion = this.CompleteAsync();
		}

		/// <inheritdoc />
		public override Task Completion { get; }

		/// <inheritdoc />
		protected override IGenerationPhase Phase { get; }

		private async Task CompleteAsync()
		{
			try
			{
				await this.Phase.Completion.DontMarshallContext();
			}
			finally
			{
				this.pool.Dispose();
			}
		}
	}
}
