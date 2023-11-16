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
	/// A generic four dimensional array that implements the <see cref="IDynamicallyBoundedIndexable{TIndex, TValue}"/>
	/// composite interface and that will dynamically grow to fit the slots indexed into it.
	/// </summary>
	/// <typeparam name="T">The type of the value stored in the array.</typeparam>
	/// <remarks>
	/// This implementation grows infinitely in both positive and negative directions.
	/// </remarks>
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "Grouping by interface.")]
	public class MultidirectionalDynamicArray4D<T> : AbstractDynamicallyBoundedIndexable4D<T>
	{
		/// <summary>
		/// The actual array being wrapped by this implementation of the IArray4D interface.
		/// </summary>
		private T[,,,] array;

		/// <summary>
		/// The x offset to add to a client's index into the array to reach what appears to the client as the zeroed origin.
		/// </summary>
		private int xOrigin;

		/// <summary>
		/// The y offset to add to a client's index into the array to reach what appears to the client as the zeroed origin.
		/// </summary>
		private int yOrigin;

		/// <summary>
		/// The z offset to add to a client's index into the array to reach what appears to the client as the zeroed origin.
		/// </summary>
		private int zOrigin;

		/// <summary>
		/// The w offset to add to a client's index into the array to reach what appears to the client as the zeroed origin.
		/// </summary>
		private int wOrigin;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MultidirectionalDynamicArray4D{TValue}"/> class.
		/// </summary>
		/// <param name="dimensions">The dimensions of the array.</param>
		public MultidirectionalDynamicArray4D(Index4D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositive());

			this.array = new T[dimensions.X, dimensions.Y, dimensions.Z, dimensions.W];
			this.xOrigin = dimensions.X / 2;
			this.yOrigin = dimensions.Y / 2;
			this.zOrigin = dimensions.Z / 2;
			this.wOrigin = dimensions.W / 2;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MultidirectionalDynamicArray4D{TValue}"/> class.
		/// </summary>
		public MultidirectionalDynamicArray4D()
			: this(DynamicArrayUtilities.DefaultSize4D)
		{
		}

		#endregion

		#region IIndexable<Index4D,TValue> Members

		/// <inheritdoc />
		public override T this[Index4D index]
		{
			get
			{
				IReadOnlyIndexableContracts.IndexerGet(this, index);

				index = this.HandleArrayResizing(index);
				return this.array[index.X, index.Y, index.Z, index.W];
			}

			set
			{
				IIndexableContracts.IndexerSet(this, index);

				index = this.HandleArrayResizing(index);
				this.array[index.X, index.Y, index.Z, index.W] = value;
			}
		}

		#endregion

		#region IDynamicIndexingBounds<Index4D>

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

		#region IEnumerable<KeyValuePair<Index4D,TValue>> Members

		/// <inheritdoc />
		public override IEnumerator<KeyValuePair<Index4D, T>> GetEnumerator()
		{
			foreach (KeyValuePair<Index4D, T> pair in this.array.GetIndexValuePairs())
			{
				yield return new KeyValuePair<Index4D, T>(
					new Index4D(
						pair.Key.X - this.xOrigin,
						pair.Key.Y - this.yOrigin,
						pair.Key.Z - this.zOrigin,
						pair.Key.W - this.wOrigin),
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
				case (int)Axis4D.X: return this.xOrigin;
				case (int)Axis4D.Y: return this.yOrigin;
				case (int)Axis4D.Z: return this.zOrigin;
				case (int)Axis4D.W: return this.wOrigin;
				default:
					throw new UnreachableCodeException();
			}
		}

		/// <summary>
		/// Handles the array resizing if necessary when an index is accessed.
		/// </summary>
		/// <param name="index">The index being accessed.</param>
		/// <returns>The adjusted index to be used for accessing the backing array.</returns>
		private Index4D HandleArrayResizing(Index4D index)
		{
			int x = index.X;
			int y = index.Y;
			int z = index.Z;
			int w = index.W;

			if (this.IsIndexInCurrentBounds(index))
			{
				x += this.xOrigin;
				y += this.yOrigin;
				z += this.zOrigin;
				w += this.wOrigin;
				return new Index4D(x, y, z, w);
			}

			// determine size of new array
			int xOffset, yOffset, zOffset, wOffset;
			int xNewSize = DynamicArrayUtilities.HandleAxis(this.GetCurrentLength(Axis4D.X), ref x, ref this.xOrigin, out xOffset);
			int yNewSize = DynamicArrayUtilities.HandleAxis(this.GetCurrentLength(Axis4D.Y), ref y, ref this.yOrigin, out yOffset);
			int zNewSize = DynamicArrayUtilities.HandleAxis(this.GetCurrentLength(Axis4D.Z), ref z, ref this.zOrigin, out zOffset);
			int wNewSize = DynamicArrayUtilities.HandleAxis(this.GetCurrentLength(Axis4D.W), ref w, ref this.wOrigin, out wOffset);

			// copy old array into new array
			T[,,,] newArray = new T[xNewSize, yNewSize, zNewSize, wNewSize];
			foreach (KeyValuePair<Index4D, T> pair in this.array.GetIndexValuePairs())
			{
				newArray[pair.Key.X + xOffset, pair.Key.Y + yOffset, pair.Key.Z + zOffset, pair.Key.W + wOffset] = pair.Value;
			}

			this.array = newArray;
			return new Index4D(x, y, z, w);
		}

		#endregion
	}
}
