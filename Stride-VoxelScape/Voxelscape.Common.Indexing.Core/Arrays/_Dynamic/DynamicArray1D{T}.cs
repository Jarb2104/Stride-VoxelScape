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
	/// A generic one dimensional array that implements the <see cref="IDynamicallyBoundedIndexable{TIndex, TValue}"/>
	/// composite interface and that will dynamically grow to fit the slots indexed into it.
	/// </summary>
	/// <typeparam name="T">The type of the value stored in the array.</typeparam>
	/// <remarks>
	/// This implementation starts at zero, grows positively infinitely, and can't handle negative indices.
	/// </remarks>
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "Grouping by interface.")]
	public class DynamicArray1D<T> : AbstractDynamicallyBoundedIndexable1D<T>
	{
		/// <summary>
		/// The actual array being wrapped by this implementation of the IArray1D interface.
		/// </summary>
		private T[] array;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DynamicArray1D{TValue}"/> class.
		/// </summary>
		/// <param name="dimensions">The dimensions of the array.</param>
		public DynamicArray1D(Index1D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositive());

			this.array = new T[dimensions.X];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DynamicArray1D{TValue}"/> class.
		/// </summary>
		public DynamicArray1D()
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

				this.HandleArrayResizing(index);
				return this.array[index.X];
			}

			set
			{
				IIndexableContracts.IndexerSet(this, index);

				this.HandleArrayResizing(index);
				this.array[index.X] = value;
			}
		}

		/// <inheritdoc />
		public override bool IsIndexValid(Index1D index)
		{
			IReadOnlyIndexableContracts.IsIndexValid(this, index);

			return index.IsAllPositiveOrZero();
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

			return 0;
		}

		/// <inheritdoc />
		public override int GetCurrentUpperBound(int dimension)
		{
			IDynamicIndexingBoundsContracts.GetCurrentUpperBound(this, dimension);

			return this.array.GetUpperBound(dimension);
		}

		#endregion

		#region IEnumerable<KeyValuePair<Index1D, TValue>> Members

		/// <inheritdoc />
		public override IEnumerator<KeyValuePair<Index1D, T>> GetEnumerator()
		{
			return this.array.GetIndexValuePairs().GetEnumerator();
		}

		#endregion

		#region Indexer helpers

		/// <summary>
		/// Handles the array resizing if necessary when an index is accessed.
		/// </summary>
		/// <param name="index">The index being accessed.</param>
		private void HandleArrayResizing(Index1D index)
		{
			if (this.IsIndexInCurrentBounds(index))
			{
				return;
			}

			// determine size of new array
			int xNewSize = DynamicArrayUtilities.HandleAxis(this.GetCurrentLength(Axis1D.X), index.X);

			// copy old array into new array
			T[] newArray = new T[xNewSize];

			foreach (KeyValuePair<Index1D, T> entry in this)
			{
				newArray[entry.Key.X] =
					this.array[entry.Key.X];
			}

			this.array = newArray;
		}

		#endregion
	}
}
