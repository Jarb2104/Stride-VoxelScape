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
	/// A generic two dimensional array that implements the <see cref="IDynamicallyBoundedIndexable{TIndex, TValue}"/>
	/// composite interface and that will dynamically grow to fit the slots indexed into it.
	/// </summary>
	/// <typeparam name="T">The type of the value stored in the array.</typeparam>
	/// <remarks>
	/// This implementation grows infinitely in both positive and negative directions.
	/// </remarks>
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "Grouping by interface.")]
	public class MultidirectionalDynamicArray2D<T> : AbstractDynamicallyBoundedIndexable2D<T>
	{
		/// <summary>
		/// The actual array being wrapped by this implementation of the IArray2D interface.
		/// </summary>
		private T[,] array;

		/// <summary>
		/// The x offset to add to a client's index into the array to reach what appears to the client as the zeroed origin.
		/// </summary>
		private int xOrigin;

		/// <summary>
		/// The y offset to add to a client's index into the array to reach what appears to the client as the zeroed origin.
		/// </summary>
		private int yOrigin;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MultidirectionalDynamicArray2D{TValue}"/> class.
		/// </summary>
		/// <param name="dimensions">The dimensions of the array.</param>
		public MultidirectionalDynamicArray2D(Index2D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositive());

			this.array = new T[dimensions.X, dimensions.Y];
			this.xOrigin = dimensions.X / 2;
			this.yOrigin = dimensions.Y / 2;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MultidirectionalDynamicArray2D{TValue}"/> class.
		/// </summary>
		public MultidirectionalDynamicArray2D()
			: this(DynamicArrayUtilities.DefaultSize2D)
		{
		}

		#endregion

		#region IIndexable<Index2D,TValue> Members

		/// <inheritdoc />
		public override T this[Index2D index]
		{
			get
			{
				IReadOnlyIndexableContracts.IndexerGet(this, index);

				index = this.HandleArrayResizing(index);
				return this.array[index.X, index.Y];
			}

			set
			{
				IIndexableContracts.IndexerSet(this, index);

				index = this.HandleArrayResizing(index);
				this.array[index.X, index.Y] = value;
			}
		}

		#endregion

		#region IDynamicIndexingBounds<Index2D>

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

		#region IEnumerable<KeyValuePair<Index2D,TValue>> Members

		/// <inheritdoc />
		public override IEnumerator<KeyValuePair<Index2D, T>> GetEnumerator()
		{
			foreach (KeyValuePair<Index2D, T> pair in this.array.GetIndexValuePairs())
			{
				yield return new KeyValuePair<Index2D, T>(
					new Index2D(
						pair.Key.X - this.xOrigin,
						pair.Key.Y - this.yOrigin),
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
				case (int)Axis2D.X: return this.xOrigin;
				case (int)Axis2D.Y: return this.yOrigin;
				default:
					throw new UnreachableCodeException();
			}
		}

		/// <summary>
		/// Handles the array resizing if necessary when an index is accessed.
		/// </summary>
		/// <param name="index">The index being accessed.</param>
		/// <returns>The adjusted index to be used for accessing the backing array.</returns>
		private Index2D HandleArrayResizing(Index2D index)
		{
			int x = index.X;
			int y = index.Y;

			if (this.IsIndexInCurrentBounds(index))
			{
				x += this.xOrigin;
				y += this.yOrigin;
				return new Index2D(x, y);
			}

			// determine size of new array
			int xOffset, yOffset;
			int xNewSize = DynamicArrayUtilities.HandleAxis(this.GetCurrentLength(Axis2D.X), ref x, ref this.xOrigin, out xOffset);
			int yNewSize = DynamicArrayUtilities.HandleAxis(this.GetCurrentLength(Axis2D.Y), ref y, ref this.yOrigin, out yOffset);

			// copy old array into new array
			T[,] newArray = new T[xNewSize, yNewSize];
			foreach (KeyValuePair<Index2D, T> pair in this.array.GetIndexValuePairs())
			{
				newArray[pair.Key.X + xOffset, pair.Key.Y + yOffset] = pair.Value;
			}

			this.array = newArray;
			return new Index2D(x, y);
		}

		#endregion
	}
}
