using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Common.Indexing.Pact.Indices;

namespace Voxelscape.Common.Indexing.Pact.Bounds
{
	public interface IDynamicallyBoundedReadOnlyIndexable<TIndex, TValue> :
		IReadOnlyIndexable<TIndex, TValue>, IDynamicIndexingBounds<TIndex>
		where TIndex : IIndex
	{
	}
}
