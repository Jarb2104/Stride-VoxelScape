using System.Diagnostics;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Stages.Management.Pact.Contouring
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="TChunk">The type of the voxel chunk.</typeparam>
	/// <typeparam name="TContour">The type of the contoured chunk.</typeparam>
	public interface IChunkContourer<TChunk, TContour>
		where TChunk : IKeyed<ChunkKey>
		where TContour : IKeyed<ChunkKey>
	{
		TContour Contour(IContourChunkView<TChunk> view);
	}

	public static class IChunkContourerContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Contour<TVoxelChunk>(IContourChunkView<TVoxelChunk> view)
			where TVoxelChunk : IKeyed<ChunkKey>
		{
			Contracts.Requires.That(view != null);
		}
	}
}
