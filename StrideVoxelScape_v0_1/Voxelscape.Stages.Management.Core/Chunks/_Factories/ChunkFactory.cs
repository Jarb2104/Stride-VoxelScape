using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Persistence;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Stages.Management.Core.Chunks
{
	/// <summary>
	///
	/// </summary>
	public static class ChunkFactory
	{
		public static class Persister
		{
			public static IAsyncFactory<TKey, TPersistable> Create<TKey, TChunk, TPersistable>(
				IAsyncFactory<TKey, IDisposableValue<TChunk>> factory,
				IChunkPersister<TChunk, TPersistable> converter) =>
				new PersistableChunkFactory<TKey, TChunk, TPersistable>(factory, converter);
		}

		public static class StageBoundary
		{
			public static IAsyncFactory<ChunkOverheadKey, TChunk> Create<TChunk>(
				IStageBounds bounds,
				IAsyncFactory<ChunkOverheadKey, TChunk> interiorFactory,
				IFactory<ChunkOverheadKey, TChunk> exteriorFactory) =>
				new StageBoundaryChunkOverheadFactory<TChunk>(bounds, interiorFactory, exteriorFactory);

			public static IAsyncFactory<ChunkKey, TChunk> Create<TChunk>(
				IStageBounds bounds,
				IAsyncFactory<ChunkKey, TChunk> interiorFactory,
				IFactory<ChunkKey, TChunk> exteriorFactory) =>
				new StageBoundaryChunkFactory<TChunk>(bounds, interiorFactory, exteriorFactory);
		}

		public static class Caching
		{
			public static IFactory<TKey, ICacheValue<TKey, TChunk>> CreateInstantExpiry<TKey, TChunk>(
				IFactory<TKey, TChunk> factory) => new CacheInstantExpiryChunkFactory<TKey, TChunk>(factory);

			public static IAsyncFactory<TKey, ICacheValue<TKey, TChunk>> CreateStashed<TKey, TChunk>(
				IAsyncFactory<TKey, IDisposableValue<TChunk>> factory, IExpiryStash<TKey> stash) =>
				new CacheStashedChunkFactory<TKey, TChunk>(factory, stash);
		}
	}
}
