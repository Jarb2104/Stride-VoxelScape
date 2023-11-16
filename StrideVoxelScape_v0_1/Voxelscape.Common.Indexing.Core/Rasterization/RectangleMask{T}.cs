using System;
using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Rasterization;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Rasterization
{
	/// <summary>
	/// A rectangular mask for rasterization.
	/// </summary>
	/// <typeparam name="T">The type of the mask values.</typeparam>
	public struct RectangleMask<T> : IRasterizableMask<Index2D, T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RectangleMask{T}"/> struct.
		/// </summary>
		/// <param name="length">The length of the rectangle. This constructor creates a square.</param>
		public RectangleMask(float length)
			: this(length, length)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RectangleMask{T}"/> struct.
		/// </summary>
		/// <param name="xLength">Length of the x-axis of the rectangle.</param>
		/// <param name="yLength">Length of the y-axis of the rectangle.</param>
		public RectangleMask(float xLength, float yLength)
		{
			Contracts.Requires.That(xLength > 0);
			Contracts.Requires.That(yLength > 0);

			this.LengthX = xLength;
			this.LengthY = yLength;
		}

		/// <summary>
		/// Gets the length of the x-axis of the rectangle.
		/// </summary>
		/// <value>
		/// The length of the x-axis of the rectangle.
		/// </value>
		public float LengthX { get; }

		/// <summary>
		/// Gets the length of the y-axis of the rectangle.
		/// </summary>
		/// <value>
		/// The length of the y-axis of the rectangle.
		/// </value>
		public float LengthY { get; }

		#region IRasterizableMask<Index2D,T> Members

		/// <inheritdoc />
		public IBoundedIndexable<Index2D, T> Rasterize(float cellLength, T internalValue, T externalValue)
		{
			IRasterizableMaskContracts.Rasterize(cellLength, internalValue, externalValue);

			return this.Rasterize(cellLength, _ => internalValue, _ => externalValue);
		}

		/// <inheritdoc />
		public IBoundedIndexable<Index2D, T> Rasterize(
			float cellLength, Func<T> internalValueFactory, Func<T> externalValueFactory)
		{
			IRasterizableMaskContracts.Rasterize(cellLength, internalValueFactory, externalValueFactory);

			return this.Rasterize(cellLength, _ => internalValueFactory(), _ => externalValueFactory());
		}

		/// <inheritdoc />
		public IBoundedIndexable<Index2D, T> Rasterize(
			float cellLength, Func<Index2D, T> internalValueFactory, Func<Index2D, T> externalValueFactory)
		{
			IRasterizableMaskContracts.Rasterize(cellLength, internalValueFactory, externalValueFactory);

			T[,] array = MaskArrayFactory.Create<T>(this.LengthX, this.LengthY, cellLength);
			int xSize = array.GetLength(0);
			int ySize = array.GetLength(1);

			// populate the array with internal values
			for (int iX = 0; iX < xSize; iX++)
			{
				for (int iY = 0; iY < ySize; iY++)
				{
					array[iX, iY] = internalValueFactory(new Index2D(iX, iY));
				}
			}

			return new Array2D<T>(array);
		}

		#endregion
	}
}
