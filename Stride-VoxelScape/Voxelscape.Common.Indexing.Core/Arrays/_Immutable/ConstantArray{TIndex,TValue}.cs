using System;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Common.Indexing.Pact.Indices;

namespace Voxelscape.Common.Indexing.Core.Arrays
{
	/// <summary>
	/// An immutable implementation of <see cref="IBoundedIndexable{TIndex, TValue}"/> that returns the same value for all indices
	/// and ignores any attemps to modify its values.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	public abstract class ConstantArray<TIndex, TValue> :
		ReadOnlyConstantArray<TIndex, TValue>, IBoundedIndexable<TIndex, TValue>
		where TIndex : IIndex
	{
		private readonly bool setThrowsException;

		/// <summary>
		/// Initializes a new instance of the <see cref="ConstantArray{TIndex, TValue}"/> class.
		/// </summary>
		/// <param name="dimensions">The dimensions of the indexable.</param>
		/// <param name="value">The value the indexable will always return.</param>
		/// <param name="setThrowsException">
		/// True if attempting to set the value at an index should throw an exception,
		/// false to just ignore the attempt.
		/// </param>
		public ConstantArray(TIndex dimensions, TValue value, bool setThrowsException)
			: base(dimensions, value)
		{
			this.setThrowsException = setThrowsException;
		}

		/// <inheritdoc />
		public new TValue this[TIndex index]
		{
			get
			{
				return base[index];
			}

			set
			{
				IIndexableContracts.IndexerSet(this, index);

				if (this.setThrowsException)
				{
					throw new NotSupportedException("Modifying constant indexable is not allowed.");
				}
			}
		}

		/// <inheritdoc />
		public bool TrySetValue(TIndex index, TValue value) => IndexableUtilities.TrySetValue(this, index, value);
	}
}
