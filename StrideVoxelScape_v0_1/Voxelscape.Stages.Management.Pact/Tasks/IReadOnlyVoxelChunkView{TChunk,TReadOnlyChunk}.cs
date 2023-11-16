////using System;
////using System.Collections.Generic;
////using System.Diagnostics;
////using System.Threading.Tasks;
////using Voxelscape.Stages.Management.Core.Chunks;
////using Voxelscape.Utility.Common.Pact.Diagnostics;
////using Voxelscape.Utility.Common.Pact.Disposables;
////using Voxelscape.Common.Indexing.Pact.Bounds;
////using Voxelscape.Utility.Data.Pact.Caching;

////namespace Voxelscape.Stages.Management.Simulation.Pact.Views
////{
////	public interface IReadOnlyVoxelChunkView<TChunk, TReadOnlyChunk> :
////		IIndexingBounds<VoxelChunkKey>, IVisiblyDisposable
////		where TChunk : TReadOnlyChunk
////	{
////		IVoxelChunkView<TChunk> MutableView { get; }

////		// maybe this should be Task<IEnumerable<TReadOnlyChunk>> instead because it is intended
////		// to happen only once when the mutable view is disposed
////		IObservable<IEnumerable<TReadOnlyChunk>> ChunksUpdated { get; }

////		// maybe remove the pinned value part here and have this method auto pin and hold the chunk
////		// until this view is disposed?
////		Task<IPinnedValue<VoxelChunkKey, TReadOnlyChunk>> GetChunkAsync(VoxelChunkKey chunkKey);
////	}

////	public static class IReadOnlyVoxelChunkViewContracts
////	{
////		[Conditional(Contracts.Requires.CompilationSymbol)]
////		public static void GetChunkAsync<TChunk, TReadOnlyChunk>(
////			IReadOnlyVoxelChunkView<TChunk, TReadOnlyChunk> instance, VoxelChunkKey chunkKey)
////			where TChunk : TReadOnlyChunk
////		{
////			Contracts.Requires.That(instance != null);
////			Contracts.Requires.That(!instance.IsDisposed);
////			Contracts.Requires.That(instance.IsIndexInBounds(chunkKey));
////		}
////	}
////}
