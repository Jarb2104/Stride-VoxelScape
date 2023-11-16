using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Bounds;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Arrays
{
	/// <summary>
	/// A generic three dimensional array that implements the <see cref="IBoundedIndexable{TIndex, TValue}"/>
	/// composite interface and allows for an indexing origin offset to be set.
	/// </summary>
	/// <typeparam name="T">The type of the value stored in the array.</typeparam>
	public class OffsetArray2D<T> : AbstractBoundedIndexable2D<T>
	{
		/// <summary>
		/// The actual array being wrapped by this implementation.
		/// </summary>
		private readonly IBoundedIndexable<Index2D, T> array;

		private readonly Index2D offset;

		/// <summary>
		/// Initializes a new instance of the <see cref="OffsetArray2D{TValue}"/> class.
		/// </summary>
		/// <param name="array">The array to wrap.</param>
		/// <param name="offset">The offset of the zero origin.</param>
		public OffsetArray2D(IBoundedIndexable<Index2D, T> array, Index2D offset)
		{
			Contracts.Requires.That(array != null);

			this.array = array;
			this.offset = offset;
		}

		/// <inheritdoc />
		public override T this[Index2D index]
		{
			get
			{
				IReadOnlyIndexableContracts.IndexerGet(this, index);

				return this.array[index - this.offset];
			}

			set
			{
				IIndexableContracts.IndexerSet(this, index);

				this.array[index - this.offset] = value;
			}
		}

		/// <inheritdoc />
		public override int GetLength(int dimension)
		{
			IIndexingBoundsContracts.GetLength(this, dimension);

			return this.array.GetLength(dimension);
		}

		/// <inheritdoc />
		public override int GetLowerBound(int dimension)
		{
			IIndexingBoundsContracts.GetLowerBound(this, dimension);

			return this.array.GetLowerBound(dimension) + this.offset[dimension];
		}

		/// <inheritdoc />
		public override int GetUpperBound(int dimension)
		{
			IIndexingBoundsContracts.GetUpperBound(this, dimension);

			return this.array.GetUpperBound(dimension) + this.offset[dimension];
		}

		/// <inheritdoc />
		public override IEnumerator<KeyValuePair<Index2D, T>> GetEnumerator()
		{
			foreach (var pair in this.array)
			{
				yield return new KeyValuePair<Index2D, T>(pair.Key + this.offset, pair.Value);
			}
		}
	}
}
