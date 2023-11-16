using System;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Rasterization
{
	/// <summary>
	/// Provides factory methods for creating arrays to rasterize shapes to.
	/// </summary>
	internal static class MaskArrayFactory
	{
		/// <summary>
		/// Creates a circular boolean mask where true is in the circle and false is outside of the circle.
		/// </summary>
		/// <param name="diameter">The diameter of the circle mask to create. This is the size of the array returned.</param>
		/// <returns>The array containing the circle mask.</returns>
		public static bool[,] CreateCircleMask(int diameter)
		{
			Contracts.Requires.That(diameter > 0);

			int midIndex = diameter.IsOdd() ? diameter / 2 : (diameter / 2) - 1;
			bool[,] result = new bool[diameter, diameter];
			RasterCircle.FillCircle(result, new Index2D(midIndex, midIndex), diameter, _ => true);
			return result;
		}

		/// <summary>
		/// Calculates the length for each dimension of an array to create when rasterizing a shape.
		/// </summary>
		/// <param name="shapeLength">The floating point length of the shape to rasterize.</param>
		/// <param name="cellLength">
		/// The length of a cell to rasterize the type at. The lower the value the smaller the array
		/// cells will be and the greater the total dimensions of the resulting array will be.
		/// </param>
		/// <returns>The size of the array to create.</returns>
		public static int CalculateLength(float shapeLength, float cellLength)
		{
			Contracts.Requires.That(shapeLength > 0);
			Contracts.Requires.That(cellLength > 0);

			return (int)Math.Ceiling(shapeLength / cellLength);
		}

		/// <summary>
		/// Creates a two dimensional array to rasterize a shape to.
		/// </summary>
		/// <typeparam name="T">The type of the array to create.</typeparam>
		/// <param name="xLength">Length of the x-axis of the shape to rasterize.</param>
		/// <param name="yLength">Length of the y-axis of the shape to rasterize.</param>
		/// <param name="cellLength">
		/// The length of a cell to rasterize the type at. The lower the value the smaller the array
		/// cells will be and the greater the total dimensions of the resulting array will be.
		/// </param>
		/// <returns>The array created to store the rasterized result in.</returns>
		public static T[,] Create<T>(float xLength, float yLength, float cellLength)
		{
			Contracts.Requires.That(xLength > 0);
			Contracts.Requires.That(yLength > 0);
			Contracts.Requires.That(cellLength > 0);

			return new T[CalculateLength(xLength, cellLength), CalculateLength(yLength, cellLength)];
		}

		/// <summary>
		/// Creates a three dimensional array to rasterize a shape to.
		/// </summary>
		/// <typeparam name="T">The type of the array to create.</typeparam>
		/// <param name="xLength">Length of the x-axis of the shape to rasterize.</param>
		/// <param name="yLength">Length of the y-axis of the shape to rasterize.</param>
		/// <param name="zLength">Length of the z-axis of the shape to rasterize.</param>
		/// <param name="cellLength">
		/// The length of a cell to rasterize the type at. The lower the value the smaller the array
		/// cells will be and the greater the total dimensions of the resulting array will be.
		/// </param>
		/// <returns>The array created to store the rasterized result in.</returns>
		public static T[,,] Create<T>(float xLength, float yLength, float zLength, float cellLength)
		{
			Contracts.Requires.That(xLength > 0);
			Contracts.Requires.That(yLength > 0);
			Contracts.Requires.That(zLength > 0);
			Contracts.Requires.That(cellLength > 0);

			return new T[
				CalculateLength(xLength, cellLength),
				CalculateLength(yLength, cellLength),
				CalculateLength(zLength, cellLength)];
		}
	}
}
