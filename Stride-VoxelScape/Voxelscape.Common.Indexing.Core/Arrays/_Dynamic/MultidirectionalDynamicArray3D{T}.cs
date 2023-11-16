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
	/// A generic three dimensional array that implements the <see cref="IDynamicallyBoundedIndexable{TIndex, TValue}"/>
	/// composite interface and that will dynamically grow to fit the slots indexed into it.
	/// </summary>
	/// <typeparam name="T">The type of the value stored in the array.</typeparam>
	/// <remarks>
	/// This implementation grows infinitely in both positive and negative directions.
	/// </remarks>
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "Grouping by interface.")]
	public class MultidirectionalDynamicArray3D<T> : AbstractDynamicallyBoundedIndexable3D<T>
	{
		/// <summary>
		/// The actual array being wrapped by this implementation of the IArray3D interface.
		/// </summary>
		private T[,,] array;

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

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MultidirectionalDynamicArray3D{TValue}"/> class.
		/// </summary>
		/// <param name="dimensions">The dimensions of the array.</param>
		public MultidirectionalDynamicArray3D(Index3D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositive());

			this.array = new T[dimensions.X, dimensions.Y, dimensions.Z];
			this.xOrigin = dimensions.X / 2;
			this.yOrigin = dimensions.Y / 2;
			this.zOrigin = dimensions.Z / 2;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MultidirectionalDynamicArray3D{TValue}"/> class.
		/// </summary>
		public MultidirectionalDynamicArray3D()
			: this(DynamicArrayUtilities.DefaultSize3D)
		{
		}

		#endregion

		#region IIndexable<Index3D,TValue> Members

		/// <inheritdoc />
		public override T this[Index3D index]
		{
			get
			{
				IReadOnlyIndexableContracts.IndexerGet(this, index);

				index = this.HandleArrayResizing(index);
				return this.array[index.X, index.Y, index.Z];
			}

			set
			{
				IIndexableContracts.IndexerSet(this, index);

				index = this.HandleArrayResizing(index);
				this.array[index.X, index.Y, index.Z] = value;
			}
		}

		#endregion

		#region IDynamicIndexingBounds<Index3D>

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

		#region IEnumerable<KeyValuePair<Index3D,TValue>> Members

		/// <inheritdoc />
		public override IEnumerator<KeyValuePair<Index3D, T>> GetEnumerator()
		{
			foreach (KeyValuePair<Index3D, T> pair in this.array.GetIndexValuePairs())
			{
				yield return new KeyValuePair<Index3D, T>(
					new Index3D(
						pair.Key.X - this.xOrigin,
						pair.Key.Y - this.yOrigin,
						pair.Key.Z - this.zOrigin),
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
				case (int)Axis3D.X: return this.xOrigin;
				case (int)Axis3D.Y: return this.yOrigin;
				case (int)Axis3D.Z: return this.zOrigin;
				default:
					throw new UnreachableCodeException();
			}
		}

		/// <summary>
		/// Handles the array resizing if necessary when an index is accessed.
		/// </summary>
		/// <param name="index">The index being accessed.</param>
		/// <returns>The adjusted index to be used for accessing the backing array.</returns>
		private Index3D HandleArrayResizing(Index3D index)
		{
			int x = index.X;
			int y = index.Y;
			int z = index.Z;

			if (this.IsIndexInCurrentBounds(index))
			{
				x += this.xOrigin;
				y += this.yOrigin;
				z += this.zOrigin;
				return new Index3D(x, y, z);
			}

			// determine size of new array
			int xOffset, yOffset, zOffset;
			int xNewSize = DynamicArrayUtilities.HandleAxis(this.GetCurrentLength(Axis3D.X), ref x, ref this.xOrigin, out xOffset);
			int yNewSize = DynamicArrayUtilities.HandleAxis(this.GetCurrentLength(Axis3D.Y), ref y, ref this.yOrigin, out yOffset);
			int zNewSize = DynamicArrayUtilities.HandleAxis(this.GetCurrentLength(Axis3D.Z), ref z, ref this.zOrigin, out zOffset);

			// copy old array into new array
			T[,,] newArray = new T[xNewSize, yNewSize, zNewSize];
			foreach (KeyValuePair<Index3D, T> pair in this.array.GetIndexValuePairs())
			{
				newArray[pair.Key.X + xOffset, pair.Key.Y + yOffset, pair.Key.Z + zOffset] = pair.Value;
			}

			this.array = newArray;
			return new Index3D(x, y, z);
		}

		#endregion
	}
}
