using System.Collections;
using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indexables;

namespace Voxelscape.Common.Indexing.Core.Bounds
{
	/// <summary>
	/// An abstract base class for extending to implement an <see cref="IBoundedIndexable{Index4D,TValue}" />.
	/// This base class provides implementations of methods that should not need re-implementing for most implementations.
	/// </summary>
	/// <typeparam name="T">The type of the values stored.</typeparam>
	public abstract class AbstractBoundedIndexable4D<T> : AbstractIndexingBounds4D, IBoundedIndexable<Index4D, T>
	{
		/// <inheritdoc />
		public abstract T this[Index4D index] { get; set; }

		/// <inheritdoc />
		public virtual bool IsIndexValid(Index4D index)
		{
			IReadOnlyIndexableContracts.IsIndexValid(this, index);

			return this.IsIndexInBounds(index);
		}

		/// <inheritdoc />
		public virtual bool TryGetValue(Index4D index, out T value) =>
			ReadOnlyIndexableUtilities.TryGetValue(this, index, out value);

		/// <inheritdoc />
		public virtual bool TrySetValue(Index4D index, T value) =>
			IndexableUtilities.TrySetValue(this, index, value);

		/// <inheritdoc />
		public abstract IEnumerator<KeyValuePair<Index4D, T>> GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
