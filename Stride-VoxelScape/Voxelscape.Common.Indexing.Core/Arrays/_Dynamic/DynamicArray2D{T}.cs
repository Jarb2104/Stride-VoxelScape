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
	/// A generic two dimensional array that implements the <see cref="IDynamicallyBoundedIndexable{TIndex, TValue}"/>
	/// composite interface and that will dynamically grow to fit the slots indexed into it.
	/// </summary>
	/// <typeparam name="T">The type of the value stored in the array.</typeparam>
	/// <remarks>
	/// This implementation starts at zero, grows positively infinitely, and can't handle negative indices.
	/// </remarks>
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "Grouping by interface.")]
	public class DynamicArray2D<T> : AbstractDynamicallyBoundedIndexable2D<T>
	{
		/// <summary>
		/// The actual array being wrapped by this implementation of the IArray2D interface.
		/// </summary>
		private T[,] array;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DynamicArray2D{TValue}"/> class.
		/// </summary>
		/// <param name="dimensions">The dimensions of the array.</param>
		public DynamicArray2D(Index2D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositive());

			this.array = new T[dimensions.X, dimensions.Y];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DynamicArray2D{TValue}"/> class.
		/// </summary>
		public DynamicArray2D()
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

				this.HandleArrayResizing(index);
				return this.array[index.X, index.Y];
			}

			set
			{
				IIndexableContracts.IndexerSet(this, index);

				this.HandleArrayResizing(index);
				this.array[index.X, index.Y] = value;
			}
		}

		/// <inheritdoc />
		public override bool IsIndexValid(Index2D index)
		{
			IReadOnlyIndexableContracts.IsIndexValid(this, index);

			return index.IsAllPositiveOrZero();
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

			return 0;
		}

		/// <inheritdoc />
		public override int GetCurrentUpperBound(int dimension)
		{
			IDynamicIndexingBoundsContracts.GetCurrentUpperBound(this, dimension);

			return this.array.GetUpperBound(dimension);
		}

		#endregion

		#region IEnumerable<KeyValuePair<Index2D, TValue>> Members

		/// <inheritdoc />
		public override IEnumerator<KeyValuePair<Index2D, T>> GetEnumerator()
		{
			return this.array.GetIndexValuePairs().GetEnumerator();
		}

		#endregion

		#region Indexer helpers

		/// <summary>
		/// Handles the array resizing if necessary when an index is accessed.
		/// </summary>
		/// <param name="index">The index being accessed.</param>
		private void HandleArrayResizing(Index2D index)
		{
			if (this.IsIndexInCurrentBounds(index))
			{
				return;
			}

			// determine size of new array
			int xNewSize = DynamicArrayUtilities.HandleAxis(this.GetCurrentLength(Axis2D.X), index.X);
			int yNewSize = DynamicArrayUtilities.HandleAxis(this.GetCurrentLength(Axis2D.Y), index.Y);

			// copy old array into new array
			T[,] newArray = new T[xNewSize, yNewSize];

			foreach (KeyValuePair<Index2D, T> entry in this)
			{
				newArray[entry.Key.X, entry.Key.Y] =
					this.array[entry.Key.X, entry.Key.Y];
			}

			this.array = newArray;
		}

		#endregion
	}
}
