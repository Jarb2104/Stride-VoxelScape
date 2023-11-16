using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;

namespace Voxelscape.Stages.Voxels.Pact.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public interface IVoxelGridChunk : IReadOnlyVoxelGridChunk
	{
		new IBoundedIndexable<Index3D, TerrainVoxel> VoxelsLocalView { get; }

		new IBoundedIndexable<Index3D, TerrainVoxel> VoxelsStageView { get; }
	}
}
