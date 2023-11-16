using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Voxelscape.Common.Indexing.Core.Bounds;
using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;

namespace Voxelscape.Common.Indexing.Core.Arrays
{
	/// <summary>
	/// A generic one dimensional array that implements the <see cref="IDynamicallyBoundedIndexable{TIndex, TValue}"/>
	/// composite interface and that will dynamically grow to fit the slots indexed into it.
	/// </summary>
	/// <typeparam name="T">The type of the value stored in the array.</typeparam>
	/// <remarks>
	/// This implementation grows infinitely in both positive and negative directions.
	/// </remarks>
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "Grouping by interface.")]
	public class MultidirectionalDynamicArray1D<T> : AbstractDynamicallyBoundedIndexable1D<T>
	{
		/// <summary>
		/// The actual array being wrapped by this implementation of the IArray1D interface.
		/// </summary>
		private T[] array;

		/// <summary>
		/// The offset to add to a client's index into the array to reach what appears to the client as the zeroed origin.
		/// </summary>
		private int xOrigin;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MultidirectionalDynamicArray1D{TValue}"/> class.
		/// </summary>
		/// <param name="dimensions">The dimensions of the array.</param>
		public MultidirectionalDynamicArray1D(Index1D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositive());

			this.array = new T[dimensions.X];
			this.xOrigin = dimensions.X / 2;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MultidirectionalDynamicArray1D{TValue}"/> class.
		/// </summary>
		public MultidirectionalDynamicArray1D()
			: this(DynamicArrayUtilities.DefaultSize1D)
		{
		}

		#endregion

		#region IIndexable<Index1D,TValue> Members

		/// <inheritdoc />
		public override T this[Index1D index]
		{
			get
			{
				IReadOnlyIndexableContracts.IndexerGet(this, index);

				index = this.HandleArrayResizing(index);
				return this.array[index.X];
			}

			set
			{
				IIndexableContracts.IndexerSet(this, index);

				index = this.HandleArrayResizing(index);
				this.array[index.X] = value;
			}
		}

		#endregion

		#region IDynamicIndexingBounds<Index1D>

		/// <inheritdoc />
		public override int GetCurrentLength(int dimension)
		{
			IDynamicIndexingBoundsContracts.GetCurrentLength(this, dimension);

			return this.array.GetLength(dimension);
		}

		/// <inheritdoc />
		public override int GetCurrentLowerBound(int dimension)
		{
			IDynamicIndexingBoundsContracts.GetCurrentLowerBound(this, dimension);

			return -this.GetOriginOffset(dimension);
		}

		/// <inheritdoc />
		public override int GetCurrentUpperBound(int dimension)
		{
			IDynamicIndexingBoundsContracts.GetCurrentUpperBound(this, dimension);

			return this.array.GetUpperBound(dimension) - this.GetOriginOffset(dimension);
		}

		#endregion

		#region IEnumerable<KeyValuePair<Index1D,TValue>> Members

		/// <inheritdoc />
		public override IEnumerator<KeyValuePair<Index1D, T>> GetEnumerator()
		{
			foreach (KeyValuePair<Index1D, T> pair in this.array.GetIndexValuePairs())
			{
				yield return new KeyValuePair<Index1D, T>(
					new Index1D(pair.Key.X - this.xOrigin),
					pair.Value);
			}
		}

		#endregion

		#region Private helpers

		/// <summary>
		/// Gets the origin offset for a specified dimension.
		/// </summary>
		/// <param name="dimension">The dimension.</param>
		/// <returns>The origin offset.</returns>
		private int GetOriginOffset(int dimension)
		{
			switch (dimension)
			{
				case (int)Axis1D.X: return this.xOrigin;
				default:
					throw new UnreachableCodeException();
			}
		}

		/// <summary>
		/// Handles the array resizing if necessary when an index is accessed.
		/// </summary>
		/// <param name="index">The index being accessed.</param>
		/// <returns>The adjusted index to be used for accessing the backing array.</returns>
		private Index1D HandleArrayResizing(Index1D index)
		{
			int x = index.X;

			if (this.IsIndexInCurrentBounds(index))
			{
				x += this.xOrigin;
				return new Index1D(x);
			}

			// determine size of new array
			int xOffset;
			int xNewSize = DynamicArrayUtilities.HandleAxis(
				this.GetCurrentLength(Axis1D.X), ref x, ref this.xOrigin, out xOffset);

			// copy old array into new array
			T[] newArray = new T[xNewSize];
			foreach (var pair in this.array.GetIndexValuePairs())
			{
				newArray[pair.Key.X + xOffset] = pair.Value;
			}

			this.array = newArray;
			return new Index1D(x);
		}

		#endregion
	}
}
