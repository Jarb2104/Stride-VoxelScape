using System;
using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Rasterization;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Rasterization
{
	/// <summary>
	/// A spherical mask for rasterization.
	/// </summary>
	/// <typeparam name="T">The type of the mask values.</typeparam>
	public struct SphereMask<T> : IRasterizableMask<Index3D, T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SphereMask{T}"/> struct.
		/// </summary>
		/// <param name="diameter">The diameter of the sphere.</param>
		public SphereMask(float diameter)
		{
			Contracts.Requires.That(diameter > 0);

			this.Diameter = diameter;
		}

		/// <summary>
		/// Gets the radius of the sphere.
		/// </summary>
		/// <value>
		/// The radius of the sphere.
		/// </value>
		public float Radius => this.Diameter / 2;

		/// <summary>
		/// Gets the diameter of the sphere.
		/// </summary>
		/// <value>
		/// The diameter of the sphere.
		/// </value>
		public float Diameter { get; }

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

			T[,,] array = MaskArrayFactory.Create<T>(this.Diameter, this.Diameter, this.Diameter, cellLength);

			if (array.Length == 0)
			{
				return new Array3D<T>(array);
			}

			int max = array.GetLength(0) - 1;
			int mid = max / 2;
			float radius = array.GetLength(0) / 2f;
			float distance = radius * radius;

			for (int iX = 0; iX <= mid; iX++)
			{
				for (int iY = 0; iY <= mid; iY++)
				{
					for (int iZ = 0; iZ <= mid; iZ++)
					{
						int mX = max - iX;
						int mY = max - iY;
						int mZ = max - iZ;

						if (DistanceCheck(iX - mid, iY - mid, iZ - mid, distance))
						{
							array[iX, iY, iZ] = internalValueFactory(new Index3D(iX, iY, iZ));
							array[mX, iY, iZ] = internalValueFactory(new Index3D(mX, iY, iZ));
							array[iX, mY, iZ] = internalValueFactory(new Index3D(iX, mY, iZ));
							array[iX, iY, mZ] = internalValueFactory(new Index3D(iX, iY, mZ));
							array[mX, mY, iZ] = internalValueFactory(new Index3D(mX, mY, iZ));
							array[iX, mY, mZ] = internalValueFactory(new Index3D(iX, mY, mZ));
							array[mX, iY, mZ] = internalValueFactory(new Index3D(mX, iY, mZ));
							array[mX, mY, mZ] = internalValueFactory(new Index3D(mX, mY, mZ));
						}
						else
						{
							array[iX, iY, iZ] = externalValueFactory(new Index3D(iX, iY, iZ));
							array[mX, iY, iZ] = externalValueFactory(new Index3D(mX, iY, iZ));
							array[iX, mY, iZ] = externalValueFactory(new Index3D(iX, mY, iZ));
							array[iX, iY, mZ] = externalValueFactory(new Index3D(iX, iY, mZ));
							array[mX, mY, iZ] = externalValueFactory(new Index3D(mX, mY, iZ));
							array[iX, mY, mZ] = externalValueFactory(new Index3D(iX, mY, mZ));
							array[mX, iY, mZ] = externalValueFactory(new Index3D(mX, iY, mZ));
							array[mX, mY, mZ] = externalValueFactory(new Index3D(mX, mY, mZ));
						}
					}
				}
			}

			return new Array3D<T>(array);
		}

		#endregion

		/// <summary>
		/// Checks if the distance for the given difference is less than the specified distance.
		/// </summary>
		/// <param name="xDiff">The x difference.</param>
		/// <param name="yDiff">The y difference.</param>
		/// <param name="zDiff">The z difference.</param>
		/// <param name="distance">The distance to check against.</param>
		/// <returns>True if the difference distance is less than the specified distance; otherwise false.</returns>
		private static bool DistanceCheck(float xDiff, float yDiff, float zDiff, float distance) =>
			(xDiff * xDiff) + (yDiff * yDiff) + (zDiff * zDiff) < distance;
	}
}
