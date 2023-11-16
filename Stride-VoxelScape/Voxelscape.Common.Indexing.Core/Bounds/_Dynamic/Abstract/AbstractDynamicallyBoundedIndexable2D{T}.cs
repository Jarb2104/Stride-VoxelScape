using System.Collections;
using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indexables;

namespace Voxelscape.Common.Indexing.Core.Bounds
{
	/// <summary>
	/// An abstract base class for extending to implement an <see cref="IDynamicallyBoundedIndexable{Index2D,TValue}" />.
	/// This base class provides implementations of methods that should not need re-implementing for most implementations.
	/// </summary>
	/// <typeparam name="T">The type of the values stored.</typeparam>
	public abstract class AbstractDynamicallyBoundedIndexable2D<T> :
		AbstractDynamicIndexingBounds2D,
		IDynamicallyBoundedIndexable<Index2D, T>
	{
		/// <inheritdoc />
		public abstract T this[Index2D index]
		{
			get;
			set;
		}

		/// <inheritdoc />
		public virtual bool IsIndexValid(Index2D index)
		{
			IReadOnlyIndexableContracts.IsIndexValid(this, index);

			return true;
		}

		/// <inheritdoc />
		public virtual bool TryGetValue(Index2D index, out T value) => ReadOnlyIndexableUtilities.TryGetValue(this, index, out value);

		/// <inheritdoc />
		public virtual bool TrySetValue(Index2D index, T value) => IndexableUtilities.TrySetValue(this, index, value);

		/// <inheritdoc />
		public abstract IEnumerator<KeyValuePair<Index2D, T>> GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
