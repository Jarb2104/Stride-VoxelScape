using Voxelscape.Common.Indexing.Pact.Indices;

namespace Voxelscape.Stages.Management.Pact.Chunks
{
	public interface IChunkKey<TIndex>
		where TIndex : IIndex
	{
		TIndex Index { get; }
	}
}
