using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Trees;

namespace Voxelscape.Stages.Voxels.Pact.Voxels.Tree
{
	/// <summary>
	///
	/// </summary>
	public interface IVoxelTreeChunk : IReadOnlyVoxelTreeChunk
	{
		new IIndexableTree<Index3D, TerrainVoxel> VoxelsLocalView { get; }

		////new IIndexableTree<Index3D, TerrainVoxel> VoxelsStageView { get; }
	}
}
