using System.Collections;
using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Bounds;
using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Arrays
{
	internal class ReadOnlyCompositeArray3D<T> : AbstractIndexingBounds3D, IBoundedReadOnlyIndexable<Index3D, T>
	{
		/// <summary>
		/// The indexable arrays to make a composite indexable out of.
		/// </summary>
		private readonly IBoundedReadOnlyIndexable<Index3D, T>[,,] arrays;

		/// <summary>
		/// The dimensions of the total composite indexable.
		/// </summary>
		private readonly Index3D dimensions;

		/// <summary>
		/// The lower bounds of the total composite indexable.
		/// </summary>
		private readonly Index3D lowerBounds;

		/// <summary>
		/// The upper bounds of the total composite indexable.
		/// </summary>
		private readonly Index3D upperBounds;

		private readonly Index3D originOffset;

		/// <summary>
		/// The x dimension length of a single indexable array that is part of the composite.
		/// </summary>
		private readonly int singleArrayLengthX;

		/// <summary>
		/// The y dimension length of a single indexable array that is part of the composite.
		/// </summary>
		private readonly int singleArrayLengthY;

		/// <summary>
		/// The z dimension length of a single indexable array that is part of the composite.
		/// </summary>
		private readonly int singleArrayLengthZ;

		/// <summary>
		/// Initializes a new instance of the <see cref="ReadOnlyCompositeArray3D{TValue}" /> class.
		/// </summary>
		/// <param name="arrays">The indexable arrays to make the composite indexable out of.</param>
		/// <param name="originOffset">The origin offset for indexing into the composite.</param>
		public ReadOnlyCompositeArray3D(IBoundedReadOnlyIndexable<Index3D, T>[,,] arrays, Index3D originOffset)
		{
			Contracts.Requires.That(arrays != null);
			Contracts.Requires.That(CompositeArray.AreAllSameDimensionsAndZeroBounded(arrays));

			this.arrays = arrays;
			IBoundedReadOnlyIndexable<Index3D, T> singleArray = this.arrays[0, 0, 0];

			this.singleArrayLengthX = singleArray.GetLength(Axis3D.X);
			this.singleArrayLengthY = singleArray.GetLength(Axis3D.Y);
			this.singleArrayLengthZ = singleArray.GetLength(Axis3D.Z);
			this.originOffset = originOffset;

			this.dimensions = singleArray.Dimensions * this.arrays.GetDimensions();
			this.lowerBounds = -this.originOffset;
			this.upperBounds = this.lowerBounds + this.dimensions;
		}

		/// <inheritdoc />
		public override Index3D Dimensions
		{
			get { return this.dimensions; }
		}

		/// <inheritdoc />
		public override Index3D LowerBounds
		{
			get { return this.lowerBounds; }
		}

		/// <inheritdoc />
		public override Index3D UpperBounds
		{
			get { return this.upperBounds; }
		}

		/// <inheritdoc />
		public T this[Index3D index]
		{
			get
			{
				IReadOnlyIndexableContracts.IndexerGet(this, index);

				Index3D subIndex;
				return this.GetIndexable(index + this.originOffset, out subIndex)[subIndex];
			}
		}

		/// <inheritdoc />
		public override int GetLength(int dimension)
		{
			IIndexingBoundsContracts.GetLength(this, dimension);

			return this.dimensions[dimension];
		}

		/// <inheritdoc />
		public override int GetLowerBound(int dimension)
		{
			IIndexingBoundsContracts.GetLowerBound(this, dimension);

			return this.lowerBounds[dimension];
		}

		/// <inheritdoc />
		public override int GetUpperBound(int dimension)
		{
			IIndexingBoundsContracts.GetUpperBound(this, dimension);

			return this.upperBounds[dimension];
		}

		/// <inheritdoc />
		public bool IsIndexValid(Index3D index)
		{
			IReadOnlyIndexableContracts.IsIndexValid(this, index);

			return this.IsIndexInBounds(index);
		}

		/// <inheritdoc />
		public bool TryGetValue(Index3D index, out T value) => ReadOnlyIndexableUtilities.TryGetValue(this, index, out value);

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<Index3D, T>> GetEnumerator()
		{
			for (int iZ = this.lowerBounds.Z; iZ <= this.upperBounds.Z; iZ++)
			{
				for (int iY = this.lowerBounds.Y; iY <= this.upperBounds.Y; iY++)
				{
					for (int iX = this.lowerBounds.X; iX <= this.upperBounds.X; iX++)
					{
						yield return new KeyValuePair<Index3D, T>(new Index3D(iX, iY, iZ), this[new Index3D(iX, iY, iZ)]);
					}
				}
			}
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		/// <summary>
		/// Gets the specific indexable array from the composite that the specified indices would end up indexing into.
		/// </summary>
		/// <param name="index">The index passed into the composite indexable.</param>
		/// <param name="subIndexableIndex">The index to use to access the indexable returned by this method.</param>
		/// <returns>The indexable to access.</returns>
		private IBoundedReadOnlyIndexable<Index3D, T> GetIndexable(Index3D index, out Index3D subIndexableIndex)
		{
			int x = index.X / this.singleArrayLengthX;
			int y = index.Y / this.singleArrayLengthY;
			int z = index.Z / this.singleArrayLengthZ;

			// this calculates the remainder of the division in a slightly more efficient way
			// http://stackoverflow.com/questions/6988414/difference-between-math-divrem-and-operator
			int xRemainder = index.X - (this.singleArrayLengthX * x);
			int yRemainder = index.Y - (this.singleArrayLengthY * y);
			int zRemainder = index.Z - (this.singleArrayLengthZ * z);

			subIndexableIndex = new Index3D(xRemainder, yRemainder, zRemainder);
			return this.arrays[x, y, z];
		}
	}
}
