using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Trees;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Stages.Voxels.Pact.Voxels.Tree
{
	/// <summary>
	///
	/// </summary>
	public interface IReadOnlyVoxelTreeChunk : IKeyed<ChunkKey>
	{
		IReadOnlyIndexableTree<Index3D, TerrainVoxel> VoxelsLocalView { get; }

		////IReadOnlyIndexableTree<Index3D, TerrainVoxel> VoxelsStageView { get; }
	}
}
