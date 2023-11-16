using Voxelscape.Common.Indexing.Core.Bounds;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Core.Stages
{
	/// <summary>
	///
	/// </summary>
	public class StageBounds : IStageBounds
	{
		public StageBounds(ChunkKey minChunkKey, Index3D dimensionsInChunks)
		{
			Contracts.Requires.That(dimensionsInChunks.IsAllPositive());

			this.InChunks = new IndexingBounds3D(minChunkKey.Index, dimensionsInChunks);
			this.InOverheadChunks = new IndexingBounds2D(
				minChunkKey.Index.ProjectDownYAxis(), dimensionsInChunks.ProjectDownYAxis());
		}

		/// <inheritdoc />
		public IIndexingBounds<Index3D> InChunks { get; }

		/// <inheritdoc />
		public IIndexingBounds<Index2D> InOverheadChunks { get; }
	}
}
