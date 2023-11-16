using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Common.Indexing.Pact.Indices;

namespace Voxelscape.Common.Indexing.Pact.Bounds
{
	public interface IBoundedReadOnlyIndexable<TIndex, TValue> : IReadOnlyIndexable<TIndex, TValue>, IIndexingBounds<TIndex>
		where TIndex : IIndex
	{
	}
}
