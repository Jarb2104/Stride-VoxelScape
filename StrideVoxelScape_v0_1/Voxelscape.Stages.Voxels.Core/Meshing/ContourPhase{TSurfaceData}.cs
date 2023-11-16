using System.Threading.Tasks;
using Voxelscape.Common.Contouring.Core.Meshing;
using Voxelscape.Common.Contouring.Core.Serialization;
using Voxelscape.Common.Contouring.Core.Vertices;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Stages.Management.Core.Generation;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Stages.Management.Pact.Persistence;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Stages.Voxels.Pact.Meshing;
using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;
using Voxelscape.Utility.Common.Core.Factories;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Core.Pools;
using Voxelscape.Utility.Data.Pact.Pools;
using static Voxelscape.Stages.Management.Core.Generation.BatchSerializeGenerationOptions;

namespace Voxelscape.Stages.Voxels.Core.Meshing
{
	public class ContourPhase<TSurfaceData> : AbstractGenerationPhase
		where TSurfaceData : class, ITerrainSurfaceData, new()
	{
		private readonly IPool<IMutableDivisibleMesh<NormalColorTextureVertex>> pool;

		public ContourPhase(
			IStageIdentity stageIdentity,
			IStageBounds stageBounds,
			IRasterChunkConfig<Index3D> voxelChunkConfig,
			IAsyncFactory<ChunkKey, IDisposableValue<IReadOnlyVoxelGridChunk>> voxelChunkFactory,
			IDualContourer<TerrainVoxel, TSurfaceData, NormalColorTextureVertex> contourer,
			IChunkStore<ChunkKey, SerializedMeshChunk> meshChunkStore,
			int maxBufferedChunks,
			BatchSerializeGenerationOptions options = null)
		{
			Contracts.Requires.That(stageIdentity != null);
			Contracts.Requires.That(stageBounds != null);
			Contracts.Requires.That(voxelChunkConfig != null);
			Contracts.Requires.That(voxelChunkFactory != null);
			Contracts.Requires.That(contourer != null);
			Contracts.Requires.That(meshChunkStore != null);
			Contracts.Requires.That(maxBufferedChunks > 0);

			var phaseIdentity = new GenerationPhaseIdentity(nameof(ContourPhase<TSurfaceData>));
			var chunkKeys = new ChunkKeyCollection(stageBounds);

			var poolOptions = new PoolOptions<IMutableDivisibleMesh<NormalColorTextureVertex>>()
			{
				ResetAction = mesh => mesh.Clear(),
			};

			this.pool = Pool.WithFactory.New(
				Factory.From<IMutableDivisibleMesh<NormalColorTextureVertex>>(
					() => new MutableDivisibleMesh<NormalColorTextureVertex>()),
				poolOptions);

			var chunkFactory = new ContourMeshFactory<TSurfaceData>(
				voxelChunkConfig, voxelChunkFactory, this.pool, contourer);
			var serializer = DivisibleMeshSerializer.NormalColorTextureVertices.WithColorAlpha[
				options?.SerializationEndianness ?? DefaultSerializationEndianness];
			var persistableFactory = new SerializeMeshChunkFactory(chunkFactory, serializer);

			this.Phase = new ChunkedBatchingPhase<ChunkKey, SerializedMeshChunk>(
				stageIdentity,
				phaseIdentity,
				chunkKeys,
				persistableFactory,
				meshChunkStore,
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
