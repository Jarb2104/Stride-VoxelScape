using System;
using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Rasterization;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Rasterization
{
	/// <summary>
	/// A cuboid mask for rasterization.
	/// </summary>
	/// <typeparam name="T">The type of the mask values.</typeparam>
	public struct CuboidMask<T> : IRasterizableMask<Index3D, T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CuboidMask{T}"/> struct.
		/// </summary>
		/// <param name="length">The length of the cuboid. This constructor creates a cube.</param>
		public CuboidMask(float length)
			: this(length, length, length)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CuboidMask{T}"/> struct.
		/// </summary>
		/// <param name="xLength">Length of the x-axis of the cuboid.</param>
		/// <param name="yLength">Length of the y-axis of the cuboid.</param>
		/// <param name="zLength">Length of the z-axis of the cuboid.</param>
		public CuboidMask(float xLength, float yLength, float zLength)
		{
			Contracts.Requires.That(xLength > 0);
			Contracts.Requires.That(yLength > 0);
			Contracts.Requires.That(zLength > 0);

			this.LengthX = xLength;
			this.LengthY = yLength;
			this.LengthZ = zLength;
		}

		/// <summary>
		/// Gets the length of the x-axis of the cuboid.
		/// </summary>
		/// <value>
		/// The length of the x-axis of the cuboid.
		/// </value>
		public float LengthX { get; }

		/// <summary>
		/// Gets the length of the y-axis of the cuboid.
		/// </summary>
		/// <value>
		/// The length of the y-axis of the cuboid.
		/// </value>
		public float LengthY { get; }

		/// <summary>
		/// Gets the length of the z-axis of the cuboid.
		/// </summary>
		/// <value>
		/// The length of the z-axis of the cuboid.
		/// </value>
		public float LengthZ { get; }

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

			T[,,] array = MaskArrayFactory.Create<T>(this.LengthX, this.LengthY, this.LengthZ, cellLength);
			int xSize = array.GetLength(0);
			int ySize = array.GetLength(1);
			int zSize = array.GetLength(2);

			// populate the array with internal values
			for (int iX = 0; iX < xSize; iX++)
			{
				for (int iY = 0; iY < ySize; iY++)
				{
					for (int iZ = 0; iZ < zSize; iZ++)
					{
						array[iX, iY, iZ] = internalValueFactory(new Index3D(iX, iY, iZ));
					}
				}
			}

			return new Array3D<T>(array);
		}

		#endregion
	}
}
