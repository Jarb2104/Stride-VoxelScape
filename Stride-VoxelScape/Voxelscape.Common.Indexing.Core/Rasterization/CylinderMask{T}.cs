using System;
using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Rasterization;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Rasterization
{
	/// <summary>
	/// A cylindrical mask for rasterization.
	/// </summary>
	/// <typeparam name="T">The type of the mask values.</typeparam>
	public struct CylinderMask<T> : IRasterizableMask<Index3D, T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CylinderMask{T}"/> struct.
		/// </summary>
		/// <param name="diameter">The diameter of the cylinder.</param>
		/// <param name="height">The height of the cylinder.</param>
		public CylinderMask(float diameter, float height)
		{
			Contracts.Requires.That(diameter > 0);
			Contracts.Requires.That(height > 0);

			this.Diameter = diameter;
			this.Height = height;
		}

		/// <summary>
		/// Gets the radius of the cylinder.
		/// </summary>
		/// <value>
		/// The radius of the cylinder.
		/// </value>
		public float Radius => this.Diameter / 2f;

		/// <summary>
		/// Gets the diameter of the cylinder.
		/// </summary>
		/// <value>
		/// The diameter of the cylinder.
		/// </value>
		public float Diameter { get; }

		/// <summary>
		/// Gets the height of the cylinder.
		/// </summary>
		/// <value>
		/// The height of the cylinder.
		/// </value>
		public float Height { get; }

		#region IRasterizableMask<Index3D,T> Members

		/// <inheritdoc />
		public IBoundedIndexable<Index3D, T> Rasterize(float cellLength, T internalValue, T externalValue)
		{
			IRasterizableMaskContracts.Rasterize(cellLength, internalValue, externalValue);

			return this.Rasterize(cellLength, _ => internalValue, _ => externalValue);
		}

		/// <inheritdoc />
		public IBoundedIndexable<Index3D, T> Rasterize(
			float cellLength, Func<T> internalValueFactory, Func<T> externalValueFactory)
		{
			IRasterizableMaskContracts.Rasterize(cellLength, internalValueFactory, externalValueFactory);

			return this.Rasterize(cellLength, _ => internalValueFactory(), _ => externalValueFactory());
		}

		/// <inheritdoc />
		public IBoundedIndexable<Index3D, T> Rasterize(
			float cellLength, Func<Index3D, T> internalValueFactory, Func<Index3D, T> externalValueFactory)
		{
			IRasterizableMaskContracts.Rasterize(cellLength, internalValueFactory, externalValueFactory);

			T[,,] array = MaskArrayFactory.Create<T>(this.Diameter, this.Height, this.Diameter, cellLength);
			int sizeBase = array.GetLength(0);
			int sizeHeight = array.GetLength(1);

			// mask tracks what slots are inside and outside of the circle base in order to fill in the columns
			bool[,] mask = MaskArrayFactory.CreateCircleMask(sizeBase);

			// for each slot in the circular base
			for (int iX = 0; iX < sizeBase; iX++)
			{
				for (int iZ = 0; iZ < sizeBase; iZ++)
				{
					// choose fill function depending on if it's inside or outside the circle base
					Func<Index3D, T> value = mask[iX, iZ] ? internalValueFactory : externalValueFactory;

					// fill in the column with the chosen function
					for (int iY = 0; iY < sizeHeight; iY++)
					{
						array[iX, iY, iZ] = value(new Index3D(iX, iY, iZ));
					}
				}
			}

			return new Array3D<T>(array);
		}

		#endregion
	}
}
