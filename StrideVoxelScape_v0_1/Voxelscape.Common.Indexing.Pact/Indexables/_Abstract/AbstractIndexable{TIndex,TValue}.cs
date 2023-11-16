using System.Collections;
using System.Collections.Generic;
using Voxelscape.Common.Indexing.Pact.Indices;

namespace Voxelscape.Common.Indexing.Pact.Indexables
{
	public abstract class AbstractIndexable<TIndex, TValue> : IIndexable<TIndex, TValue>
		where TIndex : IIndex
	{
		/// <inheritdoc />
		public abstract int Rank { get; }

		/// <inheritdoc />
		public abstract TValue this[TIndex index] { get; set; }

		/// <inheritdoc />
		public abstract bool IsIndexValid(TIndex index);

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
