////using System.Diagnostics;
////using System.Threading.Tasks;
////using Voxelscape.Stages.Management.Core.Chunks;
////using Voxelscape.Utility.Common.Pact.Diagnostics;
////using Voxelscape.Utility.Common.Pact.Disposables;
////using Voxelscape.Common.Indexing.Pact.Bounds;
////using Voxelscape.Utility.Data.Pact.Caching;

////namespace Voxelscape.Stages.Management.Simulation.Pact.Views
////{
////	public interface IVoxelChunkView<TChunk> : IIndexingBounds<VoxelChunkKey>, IVisiblyDisposable
////	{
////		// maybe remove the pinned value part here and have this method auto pin and hold the chunk
////		// until this view is disposed?
////		Task<IPinnedValue<VoxelChunkKey, TChunk>> GetChunkAsync(VoxelChunkKey chunkKey);
////	}

////	public static class IVoxelChunkViewContracts
////	{
////		[Conditional(Contracts.Requires.CompilationSymbol)]
////		public static void GetChunkAsync<TChunk>(IVoxelChunkView<TChunk> instance, VoxelChunkKey chunkKey)
////		{
////			Contracts.Requires.That(instance != null);
////			Contracts.Requires.That(!instance.IsDisposed);
////			Contracts.Requires.That(instance.IsIndexInBounds(chunkKey));
////		}
////	}
////}
