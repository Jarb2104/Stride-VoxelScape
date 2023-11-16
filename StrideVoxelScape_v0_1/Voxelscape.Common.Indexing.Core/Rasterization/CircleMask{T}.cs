using System;
using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Rasterization;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Rasterization
{
	/// <summary>
	/// A circular mask for rasterization.
	/// </summary>
	/// <typeparam name="T">The type of the mask values.</typeparam>
	public struct CircleMask<T> : IRasterizableMask<Index2D, T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CircleMask{T}"/> struct.
		/// </summary>
		/// <param name="diameter">The diameter of the circle.</param>
		public CircleMask(float diameter)
		{
			Contracts.Requires.That(diameter > 0);

			this.Diameter = diameter;
		}

		/// <summary>
		/// Gets the radius of the circle.
		/// </summary>
		/// <value>
		/// The radius of the circle.
		/// </value>
		public float Radius => this.Diameter / 2f;

		/// <summary>
		/// Gets the diameter of the circle.
		/// </summary>
		/// <value>
		/// The diameter of the circle.
		/// </value>
		public float Diameter { get; }

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

			T[,] array = MaskArrayFactory.Create<T>(this.Diameter, this.Diameter, cellLength);
			int size = array.GetLength(0);

			// mask tracks what slots are inside and outside of the circle
			bool[,] mask = MaskArrayFactory.CreateCircleMask(size);

			// for each cell in the array, fill in according to the mask
			for (int iX = 0; iX < size; iX++)
			{
				for (int iY = 0; iY < size; iY++)
				{
					if (mask[iX, iY])
					{
						// inside the circle
						array[iX, iY] = internalValueFactory(new Index2D(iX, iY));
					}
					else
					{
						// outside the circle
						array[iX, iY] = externalValueFactory(new Index2D(iX, iY));
					}
				}
			}

			return new Array2D<T>(array);
		}

		#endregion
	}
}
