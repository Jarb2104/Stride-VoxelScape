using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public struct VoxelGridChunkResources
	{
		public VoxelGridChunkResources(IBoundedIndexable<Index3D, TerrainVoxel> voxels)
		{
			Contracts.Requires.That(voxels != null);

			this.Voxels = voxels;
		}

		public IBoundedIndexable<Index3D, TerrainVoxel> Voxels { get; }
	}
}
