using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;

namespace Voxelscape.Stages.Management.Pact.Stages
{
	/// <summary>
	///
	/// </summary>
	public interface IStageBounds
	{
		IIndexingBounds<Index3D> InChunks { get; }

		IIndexingBounds<Index2D> InOverheadChunks { get; }
	}
}
