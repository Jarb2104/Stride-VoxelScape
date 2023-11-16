using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Bounds;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Arrays
{
	/// <summary>
	/// A generic three dimensional array that implements the <see cref="IBoundedIndexable{TIndex, TValue}"/> composite interface.
	/// </summary>
	/// <typeparam name="T">The type of the value stored in the array.</typeparam>
	public class Array3D<T> : AbstractBoundedIndexable3D<T>
	{
		/// <summary>
		/// The actual array being wrapped by this implementation.
		/// </summary>
		private readonly T[,,] array;

		/// <summary>
		/// Initializes a new instance of the <see cref="Array3D{TValue}"/> class.
		/// </summary>
		/// <param name="array">The array to wrap.</param>
		public Array3D(T[,,] array)
		{
			Contracts.Requires.That(array != null);

			this.array = array;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Array3D{TValue}"/> class.
		/// </summary>
		/// <param name="dimensions">The dimensions of the array.</param>
		public Array3D(Index3D dimensions)
			: this(new T[dimensions.X, dimensions.Y, dimensions.Z])
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());
		}

		/// <inheritdoc />
		public override T this[Index3D index]
		{
			get
			{
				IReadOnlyIndexableContracts.IndexerGet(this, index);

				return this.array[index.X, index.Y, index.Z];
			}

			set
			{
				IIndexableContracts.IndexerSet(this, index);

				this.array[index.X, index.Y, index.Z] = value;
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

			return this.array.GetLowerBound(dimension);
		}

		/// <inheritdoc />
		public override int GetUpperBound(int dimension)
		{
			IIndexingBoundsContracts.GetUpperBound(this, dimension);

			return this.array.GetUpperBound(dimension);
		}

		/// <inheritdoc />
		public override IEnumerator<KeyValuePair<Index3D, T>> GetEnumerator() =>
			this.array.GetIndexValuePairs().GetEnumerator();
	}
}
