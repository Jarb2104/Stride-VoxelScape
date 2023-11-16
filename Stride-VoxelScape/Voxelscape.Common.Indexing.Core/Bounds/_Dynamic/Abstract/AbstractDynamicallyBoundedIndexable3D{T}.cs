using System.Collections;
using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indexables;

namespace Voxelscape.Common.Indexing.Core.Bounds
{
	/// <summary>
	/// An abstract base class for extending to implement an <see cref="IDynamicallyBoundedIndexable{Index3D,TValue}" />.
	/// This base class provides implementations of methods that should not need re-implementing for most implementations.
	/// </summary>
	/// <typeparam name="T">The type of the values stored.</typeparam>
	public abstract class AbstractDynamicallyBoundedIndexable3D<T> :
		AbstractDynamicIndexingBounds3D,
		IDynamicallyBoundedIndexable<Index3D, T>
	{
		/// <inheritdoc />
		public abstract T this[Index3D index]
		{
			get;
			set;
		}

		/// <inheritdoc />
		public virtual bool IsIndexValid(Index3D index)
		{
			IReadOnlyIndexableContracts.IsIndexValid(this, index);

			return true;
		}

		/// <inheritdoc />
		public virtual bool TryGetValue(Index3D index, out T value) => ReadOnlyIndexableUtilities.TryGetValue(this, index, out value);

		/// <inheritdoc />
		public virtual bool TrySetValue(Index3D index, T value) => IndexableUtilities.TrySetValue(this, index, value);

		/// <inheritdoc />
		public abstract IEnumerator<KeyValuePair<Index3D, T>> GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
