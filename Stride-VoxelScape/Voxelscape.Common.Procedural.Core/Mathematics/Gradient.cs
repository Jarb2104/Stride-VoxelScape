using System;

namespace Voxelscape.Common.Procedural.Core.Mathematics
{
	/// <summary>
	/// Provides methods for calculating values that lie somewhere within a gradient of values.
	/// </summary>
	public static class Gradient
	{
		/// <summary>
		/// Defines a gradient between two values associated with two points on a line and returns
		/// the gradient value of a third point along that gradient line.
		/// </summary>
		/// <param name="x">The x coordinate of the value along the gradient to determine.</param>
		/// <param name="x0">The x coordinate of the first point defining the gradient.</param>
		/// <param name="x1">The x coordinate of the second point defining the gradient.</param>
		/// <param name="value0">The value of the gradient at the point <paramref name="x0"/>.</param>
		/// <param name="value1">The value of the gradient at the point <paramref name="x1"/>.</param>
		/// <returns>The value of the gradient at the point <paramref name="x"/>.</returns>
		public static double Linear(
			double x,
			double x0,
			double x1,
			double value0,
			double value1)
		{
			if (x0 == x1)
			{
				return x0;
			}

			double floor, ceiling;

			if (x0 < x1)
			{
				floor = x0;
				ceiling = x1;
			}
			else
			{
				floor = x1;
				ceiling = x0;
			}

			if (x >= floor)
			{
				if (x <= ceiling)
				{
					double multiplier = (x - floor) / (ceiling - floor);
					return (value1 * multiplier) + (value0 * (1 - multiplier));
				}
				else
				{
					return value1;
				}
			}
			else
			{
				return value0;
			}
		}

		/// <summary>
		/// Defines a spherical gradient centered at a point and returns the gradient value of a point
		/// within that gradient.
		/// </summary>
		/// <param name="x">The x coordinate of the value to determine.</param>
		/// <param name="y">The y coordinate of the value to determine.</param>
		/// <param name="z">The z coordinate of the value to determine.</param>
		/// <param name="centerX">The x coordinate of the center of the gradient sphere.</param>
		/// <param name="centerY">The y coordinate of the center of the gradient sphere.</param>
		/// <param name="centerZ">The z coordinate of the center of the gradient sphere.</param>
		/// <param name="radiusStart">The radius at which the gradient begins.</param>
		/// <param name="radiusEnd">The radius at which the gradient ends.</param>
		/// <param name="valueStart">The value of the gradient at the starting radius.</param>
		/// <param name="valueEnd">The value of the gradient at the ending radius.</param>
		/// <returns>
		/// The value of the gradient at the point (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).
		/// </returns>
		public static double Radial(
			double x,
			double y,
			double z,
			double centerX,
			double centerY,
			double centerZ,
			double radiusStart,
			double radiusEnd,
			double valueStart,
			double valueEnd)
		{
			double distance = Math.Sqrt(
				Math.Pow(Math.Abs(x - centerX), 2) +
				Math.Pow(Math.Abs(y - centerY), 2) +
				Math.Pow(Math.Abs(z - centerZ), 2));

			return Linear(distance, radiusStart, radiusEnd, valueStart, valueEnd);
		}

		/// <summary>
		/// Defines a circular gradient centered at a point and returns the gradient value of a point
		/// within that gradient.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <param name="centerX">The x coordinate of the center of the gradient circle.</param>
		/// <param name="centerY">The y coordinate of the center of the gradient circle.</param>
		/// <param name="radiusStart">The radius at which the gradient begins.</param>
		/// <param name="radiusEnd">The radius at which the gradient ends.</param>
		/// <param name="valueStart">The value of the gradient at the starting radius.</param>
		/// <param name="valueEnd">The value of the gradient at the ending radius.</param>
		/// <returns>The value of the gradient at the point (<paramref name="x"/>, <paramref name="y"/>).</returns>
		public static double Radial(
			double x,
			double y,
			double centerX,
			double centerY,
			double radiusStart,
			double radiusEnd,
			double valueStart,
			double valueEnd)
		{
			double distance = Math.Sqrt(
				Math.Pow(Math.Abs(x - centerX), 2) +
				Math.Pow(Math.Abs(y - centerY), 2));

			return Linear(distance, radiusStart, radiusEnd, valueStart, valueEnd);
		}
	}
}
