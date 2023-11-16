using System.Threading.Tasks;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Stages.Management.Core.Chunks;
using Voxelscape.Stages.Management.Core.Generation;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Stages.Management.Pact.Persistence;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Core.Pools;
using Voxelscape.Utility.Data.Pact.Pools;
using static Voxelscape.Stages.Management.Core.Generation.BatchSerializeGenerationOptions;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapAbsolutePhase : AbstractGenerationPhase
	{
		private readonly IPool<SkyIslandMapChunkResources> pool;

		public SkyIslandMapAbsolutePhase(
			IStageIdentity stageIdentity,
			IStageBounds stageBounds,
			IRasterChunkConfig<Index2D> chunkConfig,
			IAsyncChunkPopulator<ISkyIslandMapChunk> chunkPopulator,
			IChunkStore<ChunkOverheadKey, SerializedSkyIslandMapChunk> chunkStore,
			int maxBufferedChunks,
			BatchSerializeGenerationOptions options = null)
		{
			Contracts.Requires.That(stageIdentity != null);
			Contracts.Requires.That(stageBounds != null);
			Contracts.Requires.That(chunkConfig != null);
			Contracts.Requires.That(chunkPopulator != null);
			Contracts.Requires.That(chunkStore != null);
			Contracts.Requires.That(maxBufferedChunks > 0);

			var phaseIdentity = new GenerationPhaseIdentity(nameof(SkyIslandMapAbsolutePhase));
			var chunkKeys = new ChunkOverheadKeyCollection(stageBounds);

			var poolOptions = new PoolOptions<SkyIslandMapChunkResources>()
			{
			};

			this.pool = Pool.WithFactory.New(new SkyIslandMapChunkResourcesFactory(chunkConfig), poolOptions);

			var chunkFactory = new SkyIslandMapChunkPoolingFactory(this.pool, chunkPopulator);
			var serializer = new SkyIslandMapChunkResourcesSerializer(
				SkyIslandMapsSerializer.Get[options?.SerializationEndianness ?? DefaultSerializationEndianness],
				chunkConfig);
			var persister = new SkyIslandMapChunkPersister(serializer);
			var persistableFactory = ChunkFactory.Persister.Create(chunkFactory, persister);

			this.Phase = new ChunkedBatchingPhase<ChunkOverheadKey, SerializedSkyIslandMapChunk>(
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
