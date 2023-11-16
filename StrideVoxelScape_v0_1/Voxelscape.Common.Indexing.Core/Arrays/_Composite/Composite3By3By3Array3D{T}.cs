using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Voxelscape.Common.Indexing.Core.Bounds;
using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Arrays
{
	/// <summary>
	/// An indexable array made up of a 3 by 3 by 3 grid of identically sized indexable arrays.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <remarks>
	/// The (0, 0, 0,) origin of the composite is automatically aligned to the (0, 0, 0) origin of the center
	/// most indexable in the composite.
	/// </remarks>
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "Grouping by interface.")]
	internal class Composite3By3By3Array3D<T> : AbstractBoundedIndexable3D<T>
	{
		/// <summary>
		/// The indexable arrays to make a composite indexable out of.
		/// </summary>
		private readonly IBoundedIndexable<Index3D, T>[,,] arrays;

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
		/// The indexing limits of the center array in the composite.
		/// </summary>
		private readonly int lowerLimitX;

		/// <summary>
		/// The indexing limits of the center array in the composite.
		/// </summary>
		private readonly int lowerLimitY;

		/// <summary>
		/// The indexing limits of the center array in the composite.
		/// </summary>
		private readonly int lowerLimitZ;

		/// <summary>
		/// The indexing limits of the center array in the composite.
		/// </summary>
		private readonly int upperLimitX;

		/// <summary>
		/// The indexing limits of the center array in the composite.
		/// </summary>
		private readonly int upperLimitY;

		/// <summary>
		/// The indexing limits of the center array in the composite.
		/// </summary>
		private readonly int upperLimitZ;

		/// <summary>
		/// Initializes a new instance of the <see cref="Composite3By3By3Array3D{TValue}"/> class.
		/// </summary>
		/// <param name="arrays">The indexable arrays to make the composite indexable out of.</param>
		public Composite3By3By3Array3D(IBoundedIndexable<Index3D, T>[,,] arrays)
		{
			Contracts.Requires.That(arrays != null);
			Contracts.Requires.That(arrays.GetLength(0) == 3);
			Contracts.Requires.That(arrays.GetLength(1) == 3);
			Contracts.Requires.That(arrays.GetLength(2) == 3);
			Contracts.Requires.That(CompositeArray.AreAllSameDimensionsAndZeroBounded(arrays));

			this.arrays = arrays;
			IBoundedIndexable<Index3D, T> array = this.arrays[1, 1, 1];

			this.singleArrayLengthX = array.GetLength(Axis3D.X);
			this.singleArrayLengthY = array.GetLength(Axis3D.Y);
			this.singleArrayLengthZ = array.GetLength(Axis3D.Z);
			Index3D indexingOffset = new Index3D(this.singleArrayLengthX, this.singleArrayLengthY, this.singleArrayLengthZ);

			this.dimensions = array.Dimensions + (indexingOffset * 2);
			this.lowerBounds = array.LowerBounds - indexingOffset;
			this.upperBounds = array.UpperBounds + indexingOffset;

			this.lowerLimitX = array.LowerBounds.X;
			this.lowerLimitY = array.LowerBounds.Y;
			this.lowerLimitZ = array.LowerBounds.Z;
			this.upperLimitX = array.UpperBounds.X;
			this.upperLimitY = array.UpperBounds.Y;
			this.upperLimitZ = array.UpperBounds.Z;
		}

		#region Base Class Overrides

		/// <inheritdoc />
		public override T this[Index3D index]
		{
			get
			{
				IReadOnlyIndexableContracts.IndexerGet(this, index);

				return this.GetIndexable(index.X, index.Y, index.Z)[new Index3D(
					(index.X + this.singleArrayLengthX) % this.singleArrayLengthX,
					(index.Y + this.singleArrayLengthY) % this.singleArrayLengthY,
					(index.Z + this.singleArrayLengthZ) % this.singleArrayLengthZ)];
			}

			set
			{
				IIndexableContracts.IndexerSet(this, index);

				this.GetIndexable(index.X, index.Y, index.Z)[new Index3D(
					(index.X + this.singleArrayLengthX) % this.singleArrayLengthX,
					(index.Y + this.singleArrayLengthY) % this.singleArrayLengthY,
					(index.Z + this.singleArrayLengthZ) % this.singleArrayLengthZ)] = value;
			}
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
		public override IEnumerator<KeyValuePair<Index3D, T>> GetEnumerator()
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

		#endregion

		/// <summary>
		/// Gets the specific indexable array from the composite that the specified indices would end up indexing into.
		/// </summary>
		/// <param name="x">The x index.</param>
		/// <param name="y">The y index.</param>
		/// <param name="z">The z index.</param>
		/// <returns>The indexable to access.</returns>
		private IBoundedIndexable<Index3D, T> GetIndexable(int x, int y, int z)
		{
			if (x < this.lowerLimitX)
			{
				x = 0;
			}
			else if (x > this.upperLimitX)
			{
				x = 2;
			}
			else
			{
				x = 1;
			}

			if (y < this.lowerLimitY)
			{
				y = 0;
			}
			else if (y > this.upperLimitY)
			{
				y = 2;
			}
			else
			{
				y = 1;
			}

			if (z < this.lowerLimitZ)
			{
				z = 0;
			}
			else if (z > this.upperLimitZ)
			{
				z = 2;
			}
			else
			{
				z = 1;
			}

			return this.arrays[x, y, z];
		}
	}
}
