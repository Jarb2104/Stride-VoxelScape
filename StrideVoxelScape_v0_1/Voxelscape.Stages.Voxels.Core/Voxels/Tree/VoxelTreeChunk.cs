using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Trees;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Stages.Voxels.Pact.Voxels.Tree;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Voxels.Tree
{
	/// <summary>
	///
	/// </summary>
	public class VoxelTreeChunk : IVoxelTreeChunk
	{
		public VoxelTreeChunk(ChunkKey key, IIndexableTree<Index3D, TerrainVoxel> voxels)
		{
			Contracts.Requires.That(voxels != null);

			this.Key = key;
			this.VoxelsLocalView = voxels;
		}

		/// <inheritdoc />
		public ChunkKey Key { get; }

		/// <inheritdoc />
		public IIndexableTree<Index3D, TerrainVoxel> VoxelsLocalView { get; }

		/////// <inheritdoc />
		////public IIndexableTree<Index3D, TerrainVoxel> VoxelsStageView { get; }

		/// <inheritdoc />
		IReadOnlyIndexableTree<Index3D, TerrainVoxel> IReadOnlyVoxelTreeChunk.VoxelsLocalView => this.VoxelsLocalView;

		/////// <inheritdoc />
		////IReadOnlyIndexableTree<Index3D, TerrainVoxel> IReadOnlyVoxelTreeChunk.VoxelsStageView => this.VoxelsStageView;
	}
}
