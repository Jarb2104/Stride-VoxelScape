////using System.Diagnostics;
////using System.Threading.Tasks;
////using Voxelscape.Stages.Management.Core.Chunks;
////using Voxelscape.Utility.Common.Pact.Diagnostics;
////using Voxelscape.Utility.Common.Pact.Disposables;
////using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

////namespace Voxelscape.Stages.Management.Simulation.Pact.Views
////{
////	public interface IVoxelChunkViewSource<TChunk, TReadOnlyChunk> : IAsyncCompletable, IDisposed
////		where TChunk : TReadOnlyChunk
////	{
////		Task<IReadOnlyVoxelChunkView<TChunk, TReadOnlyChunk>> GetVoxelChunkViewAsync(RegionChunkKey regionKey);
////	}

////	public static class IVoxelChunkViewSourceContracts
////	{
////		[Conditional(Contracts.Requires.CompilationSymbol)]
////		public static void GetVoxelChunkViewAsync<TChunk, TReadOnlyChunk>(
////			IVoxelChunkViewSource<TChunk, TReadOnlyChunk> instance)
////			where TChunk : TReadOnlyChunk
////		{
////			Contracts.Requires.That(instance != null);
////			Contracts.Requires.That(!instance.IsDisposed);
////		}
////	}
////}
