using System;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Range = Voxelscape.Utility.Common.Core.Mathematics.Range;

namespace Voxelscape.Common.Indexing.Core.Rasterization
{
	/// <summary>
	/// Provides methods for drawing circles in arrays.
	/// </summary>
	public static class RasterCircle
	{
		/// <summary>
		/// Draws the outline of a circle.
		/// </summary>
		/// <typeparam name="T">The type of the array.</typeparam>
		/// <param name="array">The array to draw the circle in.</param>
		/// <param name="midPoint">
		/// The mid point of the circle, or the top left mid point if the circle has an even diameter.
		/// </param>
		/// <param name="diameter">The diameter of the circle.</param>
		/// <param name="valueFunction">
		/// The value function used to create values to draw the circle with. The index of the array slot being filled
		/// is passed to the function.
		/// </param>
		public static void OutlineCircle<T>(T[,] array, Index2D midPoint, int diameter, Func<Index2D, T> valueFunction)
		{
			CircleContracts<T>(array, diameter, valueFunction);

			int radius = diameter / 2;
			int shift = diameter.IsOdd() ? 0 : 1;
			Circle<T> circle = new Circle<T>(array, midPoint.X, midPoint.Y, valueFunction, shift);

			// handle special cases that the algorithm doesn't handle properly
			switch (diameter)
			{
				case 0:
					return;

				case 1:
					circle.AssignValueAtIndexOffset(0, 0);
					return;
			}

			// This is an implementation of the Midpoint circle algorithm taken from
			// http://en.wikipedia.org/wiki/Midpoint_circle_algorithm
			int x = radius;
			int y = 0;
			int radiusError = 1 - x;

			while (x >= y)
			{
				// this shift is used in the following assignments to handle even sized circles according to this suggestion;
				// http://stackoverflow.com/questions/13583683/midpoint-algorithm-for-diameter-with-an-even-number-of-pixels
				circle.AssignValueAtIndexOffset(x, y);
				circle.AssignValueAtIndexOffset(y, x);
				circle.AssignValueAtIndexOffset(-x + shift, y);
				circle.AssignValueAtIndexOffset(-y + shift, x);
				circle.AssignValueAtIndexOffset(-x + shift, -y + shift);
				circle.AssignValueAtIndexOffset(-y + shift, -x + shift);
				circle.AssignValueAtIndexOffset(x, -y + shift);
				circle.AssignValueAtIndexOffset(y, -x + shift);

				y++;
				if (radiusError < 0)
				{
					radiusError += (2 * y) + 1;
				}
				else
				{
					x--;
					radiusError += 2 * (y - x + 1);
				}
			}
		}

		/// <summary>
		/// Draws and fills in a circle.
		/// </summary>
		/// <typeparam name="T">The type of the array.</typeparam>
		/// <param name="array">The array to draw the circle in.</param>
		/// <param name="midPoint">
		/// The mid point of the circle, or the top left mid point if the circle has an even diameter.
		/// </param>
		/// <param name="diameter">The diameter of the circle.</param>
		/// <param name="valueFunction">
		/// The value function used to create values to draw the circle with. The index of the array slot being filled
		/// is passed to the function.
		/// </param>
		public static void FillCircle<T>(T[,] array, Index2D midPoint, int diameter, Func<Index2D, T> valueFunction)
		{
			CircleContracts<T>(array, diameter, valueFunction);

			int radius = diameter / 2;
			int shift = diameter.IsOdd() ? 0 : 1;
			Circle<T> circle = new Circle<T>(array, midPoint.X, midPoint.Y, valueFunction, shift);

			// handle special cases that the algorithm doesn't handle properly
			switch (diameter)
			{
				case 0:
					return;

				case 2:
					circle.AssignValueAtIndexOffset(0, 0);
					circle.AssignValueAtIndexOffset(1, 0);
					circle.AssignValueAtIndexOffset(0, 1);
					circle.AssignValueAtIndexOffset(1, 1);
					return;
			}

			// This is an implementation of the Midpoint circle algorithm taken from
			// http://en.wikipedia.org/wiki/Midpoint_circle_algorithm
			// and modified to fill the circle based off of the algoritm discussed here
			// http://stackoverflow.com/questions/10878209/midpoint-circle-algorithm-for-filled-circles
			int error = -radius;
			int x = radius;
			int y = 0;

			while (x >= y)
			{
				int lastY = y;

				error += y;
				++y;
				error += y;

				circle.PlotPoints(x, lastY);

				if (error >= 0)
				{
					if (x != lastY)
					{
						circle.PlotPoints(lastY, x);
					}

					error -= x;
					--x;
					error -= x;
				}
			}
		}

		/// <summary>
		/// The contracts for the circle methods.
		/// </summary>
		/// <typeparam name="T">The type of the array.</typeparam>
		/// <param name="array">The array to draw the circle in.</param>
		/// <param name="diameter">The diameter of the circle.</param>
		/// <param name="valueFunction">The value function used to create values to fill the circle with.</param>
		private static void CircleContracts<T>(T[,] array, int diameter, Func<Index2D, T> valueFunction)
		{
			Contracts.Requires.That(array != null);
			Contracts.Requires.That(diameter >= 0);
			Contracts.Requires.That(valueFunction != null);
		}

		/// <summary>
		/// A helper struct to contain the constants necessary when drawing a circle.
		/// </summary>
		/// <typeparam name="T">The type of the array.</typeparam>
		private struct Circle<T>
		{
			/// <summary>
			/// The array to draw the circle in.
			/// </summary>
			private readonly T[,] array;

			/// <summary>
			/// The x mid point index of the circle in the array.
			/// </summary>
			private readonly int xMid;

			/// <summary>
			/// The y mid point index of the circle in the array.
			/// </summary>
			private readonly int yMid;

			/// <summary>
			/// The value function used to generate the values to draw the circle with.
			/// </summary>
			private readonly Func<Index2D, T> valueFunction;

			/// <summary>
			/// The shift value. This should be 0 for circles with an odd diameter and 1 for even diameters.
			/// </summary>
			/// <remarks>
			/// This is used to shift values being drawn when creating a circle with an even diameter. This is necessary
			/// because the algorithm is normally meant for drawing circles with an odd diameter. The offset shifts the
			/// quadrants being filled in according to this suggestion;
			/// <see href="http://stackoverflow.com/questions/13583683/midpoint-algorithm-for-diameter-with-an-even-number-of-pixels"/>.
			/// </remarks>
			private readonly int shift;

			/// <summary>
			/// Initializes a new instance of the <see cref="Circle{T}"/> struct.
			/// </summary>
			/// <param name="array">The array to draw the circle in.</param>
			/// <param name="xMid">The x mid point of the circle.</param>
			/// <param name="yMid">The y mid point of the circle.</param>
			/// <param name="valueFunction">The value function used to generate the values to draw the circle with.</param>
			/// <param name="shift">
			/// The shift value. This should be 0 for circles with an odd diameter and 1 for even diameters.
			/// </param>
			public Circle(T[,] array, int xMid, int yMid, Func<Index2D, T> valueFunction, int shift)
			{
				Contracts.Requires.That(array != null);
				Contracts.Requires.That(valueFunction != null);
				Contracts.Requires.That(shift == 0 || shift == 1);

				this.array = array;
				this.xMid = xMid;
				this.yMid = yMid;
				this.valueFunction = valueFunction;
				this.shift = shift;
			}

			/// <summary>
			/// Generates and assigns a value to a slot in the array given
			/// the specified offset from the midpoint of the circle.
			/// </summary>
			/// <param name="x">The x index offset from the midpoint to assign a value to.</param>
			/// <param name="y">The y index offset from the midpoint to assign a value to.</param>
			public void AssignValueAtIndexOffset(int x, int y)
			{
				this.AssignValueAtIndex(this.xMid + x, this.yMid + y);
			}

			/// <summary>
			/// Handles plotting 4 points around the circumference of the circle, drawing horizontal lines connecting them
			/// to fill in the circle with.
			/// </summary>
			/// <param name="x">The x index offset from the midpoint.</param>
			/// <param name="y">The y index offset from the midpoint.</param>
			public void PlotPoints(int x, int y)
			{
				this.HorizontalLine(this.xMid - x + this.shift, this.xMid + x, this.yMid + y);

				if (y != 0)
				{
					this.HorizontalLine(this.xMid - x + this.shift, this.xMid + x, this.yMid - y + this.shift);
				}
			}

			/// <summary>
			/// Draws a horizontal line.
			/// </summary>
			/// <param name="xStart">The starting x index of the line.</param>
			/// <param name="xEnd">The ending x index of the line.</param>
			/// <param name="y">The y index of the horizontal line.</param>
			private void HorizontalLine(int xStart, int xEnd, int y)
			{
				for (int x = xStart; x <= xEnd; ++x)
				{
					this.AssignValueAtIndex(x, y);
				}
			}

			/// <summary>
			/// Generates and assigns a value at the specified index.
			/// </summary>
			/// <param name="x">The x index to assign a value to.</param>
			/// <param name="y">The y index to assign a value to.</param>
			private void AssignValueAtIndex(int x, int y)
			{
				if (!x.IsIn(Range.New(this.array.GetLowerBound(0), this.array.GetUpperBound(0))) ||
					!y.IsIn(Range.New(this.array.GetLowerBound(1), this.array.GetUpperBound(1))))
				{
					return;
				}

				this.array[x, y] = this.valueFunction(new Index2D(x, y));
			}
		}
	}
}
