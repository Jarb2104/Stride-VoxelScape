using Voxelscape.Common.Indexing.Core.Bounds;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Stages.Management.Pact.Chunks;

namespace Voxelscape.Stages.Management.Core.Chunks
{
	/// <summary>
	///
	/// </summary>
	public abstract class AbstractRasterChunkConfig3D : AbstractRasterChunkConfig, IRasterChunkConfig<Index3D>
	{
		public AbstractRasterChunkConfig3D(int treeDepth)
			: base(treeDepth)
		{
			this.Bounds = new IndexingBounds3D(new Index3D(this.SideLength));
		}

		/// <inheritdoc />
		public IIndexingBounds<Index3D> Bounds { get; }
	}
}
