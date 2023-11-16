using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indices;

namespace Voxelscape.Stages.Management.Pact.Chunks
{
	public interface IRasterChunkConfig<TDimensions> : IRasterChunkConfig
		where TDimensions : IIndex
	{
		IIndexingBounds<TDimensions> Bounds { get; }
	}
}
