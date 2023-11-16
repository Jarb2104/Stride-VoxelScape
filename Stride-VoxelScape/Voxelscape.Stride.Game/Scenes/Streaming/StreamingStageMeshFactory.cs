using Voxelscape.Common.Contouring.Core.Meshing;
using Voxelscape.Common.Contouring.Core.Vertices;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Procedural.Core.Noise;
using Voxelscape.Stages.Management.Core.Chunks;
using Voxelscape.Stages.Management.Core.Stages;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Core.Meshing;
using Voxelscape.Stages.Voxels.Core.Templates.CubeWorld;
using Voxelscape.Stages.Voxels.Core.Templates.NoiseWorld;
using Voxelscape.Stages.Voxels.Core.Templates.SkyIsland;
using Voxelscape.Stages.Voxels.Core.Terrain;
using Voxelscape.Stages.Voxels.Core.Voxels.Grid;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;
using Voxelscape.Utility.Common.Core.Factories;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Concurrency.Core.LifeCycle;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;
using Voxelscape.Utility.Data.Core.Pools;
using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Stride.Game.Scenes.Streaming
{
	/// <summary>
	///
	/// </summary>
	public class StreamingStageMeshFactory : AsyncCompletableChunkFactory<
		ChunkKey, IDisposableValue<IDivisibleMesh<NormalColorTextureVertex>>>
	{
		private StreamingStageMeshFactory(
			IAsyncFactory<ChunkKey, IDisposableValue<IDivisibleMesh<NormalColorTextureVertex>>> factory,
			IAsyncCompletable completable)
			: base(factory, completable)
		{
		}

		public float ChunkLength => VoxelChunkConfig.SideLength;

		public int ThreadsCount => StaticThreadsCount;

		private static int StaticThreadsCount => 10;

		private static IRasterChunkConfig<Index3D> VoxelChunkConfig { get; } = new VoxelGridChunkConfig(treeDepth: 6);

		private static long CacheSizeInMegaBytes => 800;

		private static long CacheSizeInBytes => CacheSizeInMegaBytes * 1000000L;

		public static StreamingStageMeshFactory CreateCubes()
		{
			var stageIdentity = new StageIdentity("Test Stage");
			var multiNoise = new NoiseFactory(seed: 0);

			var voxelChunkPopulator = new CubeVoxelGridChunkPopulator(
				new TerrainVoxel(TerrainMaterial.Stone, byte.MaxValue));

			return CreateMeshFactory(multiNoise, voxelChunkPopulator.WrapWithAsync());
		}

		public static StreamingStageMeshFactory CreateCubeOutlines()
		{
			var stageIdentity = new StageIdentity("Test Stage");
			var multiNoise = new NoiseFactory(seed: 0);

			var voxelChunkPopulator = new CubeOutlineVoxelGridChunkPopulator(
				bufferWidth: 1,
				voxel: new TerrainVoxel(TerrainMaterial.Stone, byte.MaxValue));

			return CreateMeshFactory(multiNoise, voxelChunkPopulator.WrapWithAsync());
		}

		public static StreamingStageMeshFactory CreateNoise()
		{
			var stageIdentity = new StageIdentity("Test Stage");
			var multiNoise = new NoiseFactory(seed: 0);

			var voxelChunkPopulator = new NoiseWorldVoxelGridChunkPopulator(
				multiNoise.Create(0),
				noiseScaling: .05,
				numberOfOctaves: 2,
				material: TerrainMaterial.Grass);

			return CreateMeshFactory(multiNoise, voxelChunkPopulator.WrapWithAsync());
		}

		public static StreamingStageMeshFactory CreateSkyIsland()
		{
			var stageIdentity = new StageIdentity("Test Stage");
			var multiNoise = new NoiseFactory(seed: 0);

			// maps
			var mapChunkConfig = new SkyIslandMapChunkConfig(VoxelChunkConfig.TreeDepth);
			var stageBounds = new StageBounds(new ChunkKey(0, 0, 0), new Index3D(1, 8, 1));
			var mapConfig = SkyIslandMapGenerationConfigService.CreatePreconfigured(stageBounds, mapChunkConfig).Build();

			var mapChunkPopulator = new AsyncChunkPopulator<ISkyIslandMapChunk>(
				new SkyIslandMapChunkPopulator(mapConfig, stageBounds, multiNoise));

			var mapChunkCacheBuilder = new SkyIslandMapChunkCacheBuilder(mapChunkConfig, mapChunkPopulator);
			var mapCacheOptions = new ChunkCacheBuilderOptions<SkyIslandMapChunkResources>()
			{
				StashCapacityMultiplier = 2,
				EagerFillPool = true,
			};

			mapChunkCacheBuilder.CreateAndAssignStandardResourceStash(CacheSizeInBytes / 10, mapCacheOptions);
			var mapChunkCache = mapChunkCacheBuilder.Build();

			// voxels
			var voxelChunkPopulator = new SkyIslandVoxelGridChunkPopulator(
				SkyIslandVoxelGridGenerationConfigService.CreatePreconfigured().Build(),
				mapChunkCache.AsReadOnly,
				multiNoise);

			return CreateMeshFactory(multiNoise, voxelChunkPopulator, mapChunkCache);
		}

		private static StreamingStageMeshFactory CreateMeshFactory(
			NoiseFactory noise, IAsyncChunkPopulator<IVoxelGridChunk> populator, IAsyncCompletable additional = null)
		{
			Contracts.Requires.That(noise != null);
			Contracts.Requires.That(populator != null);

			var voxelCache = CreateVoxelChunkCache(populator);
			var meshPool = CreateMeshBuilderPool();

			var meshFactory = new ContourMeshFactory<TerrainSurfaceData>(
				VoxelChunkConfig,
				voxelCache.AsReadOnly,
				meshPool,
				new TerrainDualContourer<TerrainSurfaceData>(noise));

			var completable = new AggregateAsyncCompletable(voxelCache, new DisposableAsyncCompletable(meshPool));
			if (additional != null)
			{
				completable = new AggregateAsyncCompletable(additional, completable);
			}

			return new StreamingStageMeshFactory(meshFactory, completable);
		}

		private static IChunkCache<ChunkKey, IVoxelGridChunk, IReadOnlyVoxelGridChunk> CreateVoxelChunkCache(
			IAsyncChunkPopulator<IVoxelGridChunk> populator)
		{
			Contracts.Requires.That(populator != null);

			var voxelChunkCacheBuilder = new VoxelGridChunkCacheBuilder(VoxelChunkConfig, populator);
			var voxelCacheOptions = new ChunkCacheBuilderOptions<VoxelGridChunkResources>()
			{
				StashCapacityMultiplier = 2,
				EagerFillPool = true,
			};

			voxelChunkCacheBuilder.CreateAndAssignStandardResourceStash(CacheSizeInBytes, voxelCacheOptions);
			return voxelChunkCacheBuilder.Build();
		}

		private static IPool<IMutableDivisibleMesh<NormalColorTextureVertex>> CreateMeshBuilderPool()
		{
			var options = new PoolOptions<IMutableDivisibleMesh<NormalColorTextureVertex>>()
			{
				ResetAction = mesh => mesh.Clear(),
				BoundedCapacity = StaticThreadsCount,
			};

			var initialSize = MeshConstants.MaxVerticesSupportedBy16BitIndices * 2;

			var pool = Pool.New(options);
			pool.GiveUntilFull(Factory.From<IMutableDivisibleMesh<NormalColorTextureVertex>>(
				() => new MutableDivisibleMesh<NormalColorTextureVertex>(
					initialGroups: initialSize / 4,
					initialOffsets: initialSize,
					initialVertices: initialSize / 2)));

			return pool;
		}
	}
}
