using System.Collections;
using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indexables;

namespace Voxelscape.Common.Indexing.Core.Bounds
{
	/// <summary>
	/// An abstract base class for extending to implement an <see cref="IBoundedIndexable{Index1D,TValue}" />.
	/// This base class provides implementations of methods that should not need re-implementing for most implementations.
	/// </summary>
	/// <typeparam name="T">The type of the values stored.</typeparam>
	public abstract class AbstractBoundedIndexable1D<T> : AbstractIndexingBounds1D, IBoundedIndexable<Index1D, T>
	{
		/// <inheritdoc />
		public abstract T this[Index1D index] { get; set; }

		/// <inheritdoc />
		public virtual bool IsIndexValid(Index1D index)
		{
			IReadOnlyIndexableContracts.IsIndexValid(this, index);

			return this.IsIndexInBounds(index);
		}

		/// <inheritdoc />
		public virtual bool TryGetValue(Index1D index, out T value) =>
			ReadOnlyIndexableUtilities.TryGetValue(this, index, out value);

		/// <inheritdoc />
		public virtual bool TrySetValue(Index1D index, T value) =>
			IndexableUtilities.TrySetValue(this, index, value);

		/// <inheritdoc />
		public abstract IEnumerator<KeyValuePair<Index1D, T>> GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
