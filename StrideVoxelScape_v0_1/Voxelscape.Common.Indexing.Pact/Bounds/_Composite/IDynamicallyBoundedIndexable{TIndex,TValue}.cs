using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Common.Indexing.Pact.Indices;

namespace Voxelscape.Common.Indexing.Pact.Bounds
{
	/// <summary>
	/// A composite interface that combines <see cref="IIndexable{TIndex,TValue}"/> and <see cref="IDynamicIndexingBounds{TIndex}"/>.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	public interface IDynamicallyBoundedIndexable<TIndex, TValue> :
		IDynamicallyBoundedReadOnlyIndexable<TIndex, TValue>, IIndexable<TIndex, TValue>
		where TIndex : IIndex
	{
	}
}
