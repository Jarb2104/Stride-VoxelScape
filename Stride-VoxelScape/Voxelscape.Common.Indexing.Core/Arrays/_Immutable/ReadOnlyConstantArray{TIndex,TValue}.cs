using System.Collections;
using System.Collections.Generic;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Arrays
{
	/// <summary>
	/// An immutable implementation of <see cref="IBoundedReadOnlyIndexable{TIndex, TValue}"/> that
	/// returns the same value for all indices.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	public abstract class ReadOnlyConstantArray<TIndex, TValue> : IBoundedReadOnlyIndexable<TIndex, TValue>
		where TIndex : IIndex
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReadOnlyConstantArray{TIndex, TValue}"/> class.
		/// </summary>
		/// <param name="dimensions">The dimensions of the indexable.</param>
		/// <param name="value">The value the indexable will always return.</param>
		public ReadOnlyConstantArray(TIndex dimensions, TValue value)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			this.Dimensions = dimensions;
			this.ConstantValue = value;
		}

		/// <inheritdoc />
		public TIndex Dimensions { get; }

		/// <inheritdoc />
		public int Length => this.Dimensions.MultiplyCoordinates();

		/// <inheritdoc />
		public long LongLength => this.Dimensions.MultiplyCoordinatesLong();

		/// <inheritdoc />
		public abstract TIndex LowerBounds { get; }

		/// <inheritdoc />
		public int Rank => this.Dimensions.Rank;

		/// <inheritdoc />
		public abstract TIndex UpperBounds { get; }

		/// <summary>
		/// Gets the value that this indexable will always return.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		protected TValue ConstantValue { get; }

		/// <inheritdoc />
		public TValue this[TIndex index]
		{
			get
			{
				IReadOnlyIndexableContracts.IndexerGet(this, index);

				return this.ConstantValue;
			}
		}

		/// <inheritdoc />
		public int GetLength(int dimension) => this.Dimensions[dimension];

		/// <inheritdoc />
		public int GetLowerBound(int dimension) => 0;

		/// <inheritdoc />
		public int GetUpperBound(int dimension) => this.Dimensions[dimension] - 1;

		/// <inheritdoc />
		public bool IsIndexValid(TIndex index) => IndexUtilities.IsIn(index, this.LowerBounds, this.UpperBounds);

		/// <inheritdoc />
		public bool TryGetValue(TIndex index, out TValue value) => ReadOnlyIndexableUtilities.TryGetValue(this, index, out value);

		/// <inheritdoc />
		public abstract IEnumerator<KeyValuePair<TIndex, TValue>> GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
