using Voxelscape.Common.Indexing.Core.Bounds;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Stages.Management.Pact.Chunks;

namespace Voxelscape.Stages.Management.Core.Chunks
{
	/// <summary>
	///
	/// </summary>
	public abstract class AbstractRasterChunkConfig2D : AbstractRasterChunkConfig, IRasterChunkConfig<Index2D>
	{
		public AbstractRasterChunkConfig2D(int treeDepth)
			: base(treeDepth)
		{
			this.Bounds = new IndexingBounds2D(new Index2D(this.SideLength));
		}

		/// <inheritdoc />
		public IIndexingBounds<Index2D> Bounds { get; }
	}
}
