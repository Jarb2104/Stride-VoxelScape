using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Stages.Voxels.Pact.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public interface IReadOnlyVoxelGridChunk : IKeyed<ChunkKey>
	{
		IBoundedReadOnlyIndexable<Index3D, TerrainVoxel> VoxelsLocalView { get; }

		IBoundedReadOnlyIndexable<Index3D, TerrainVoxel> VoxelsStageView { get; }
	}
}
