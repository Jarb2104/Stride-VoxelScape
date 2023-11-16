using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public class VoxelGridChunk : IVoxelGridChunk
	{
		public VoxelGridChunk(ChunkKey key, IBoundedIndexable<Index3D, TerrainVoxel> voxels)
		{
			Contracts.Requires.That(voxels != null);

			this.Key = key;
			this.VoxelsLocalView = voxels;
			this.VoxelsStageView = new OffsetArray3D<TerrainVoxel>(voxels, voxels.Dimensions * key.Index);
		}

		/// <inheritdoc />
		public ChunkKey Key { get; }

		/// <inheritdoc />
		public IBoundedIndexable<Index3D, TerrainVoxel> VoxelsLocalView { get; }

		/// <inheritdoc />
		public IBoundedIndexable<Index3D, TerrainVoxel> VoxelsStageView { get; }

		/// <inheritdoc />
		IBoundedReadOnlyIndexable<Index3D, TerrainVoxel> IReadOnlyVoxelGridChunk.VoxelsLocalView => this.VoxelsLocalView;

		/// <inheritdoc />
		IBoundedReadOnlyIndexable<Index3D, TerrainVoxel> IReadOnlyVoxelGridChunk.VoxelsStageView => this.VoxelsStageView;
	}
}
