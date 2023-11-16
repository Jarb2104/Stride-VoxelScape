using System.Collections;
using System.Collections.Generic;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indices;

namespace Voxelscape.Common.Indexing.Pact.Indexables
{
	/// <summary>
	/// An abstract base class for extending to implement an <see cref="IDynamicallyBoundedIndexable{TIndex,TValue}" />.
	/// This base class provides implementations of methods that should not need re-implementing for most implementations.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index used to access values.</typeparam>
	/// <typeparam name="TValue">The type of the values stored.</typeparam>
	public abstract class AbstractDynamicallyBoundedIndexable<TIndex, TValue> :
		AbstractDynamicIndexingBounds<TIndex>,
		IDynamicallyBoundedIndexable<TIndex, TValue>
		where TIndex : IIndex
	{
		/// <inheritdoc />
		public abstract TValue this[TIndex index]
		{
			get;
			set;
		}

		/// <inheritdoc />
		public virtual bool IsIndexValid(TIndex index)
		{
			IReadOnlyIndexableContracts.IsIndexValid(this, index);

			return true;
		}

		/// <inheritdoc />
		public virtual bool TryGetValue(TIndex index, out TValue value) => ReadOnlyIndexableUtilities.TryGetValue(this, index, out value);

		/// <inheritdoc />
		public virtual bool TrySetValue(TIndex index, TValue value) => IndexableUtilities.TrySetValue(this, index, value);

		/// <inheritdoc />
		public abstract IEnumerator<KeyValuePair<TIndex, TValue>> GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
