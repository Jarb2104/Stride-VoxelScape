using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Procedural.Core.Mathematics
{
	/// <summary>
	/// Provides methods for constructing new data points within the range of a discrete set of known data points.
	/// </summary>
	public static class Interpolation
	{
		/// <summary>
		/// Converts a value from an original range of values to a new range of values.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="originalStart">The start value of the original range.</param>
		/// <param name="originalEnd">The end value of the original range.</param>
		/// <param name="newStart">The start value of the new range to convert to.</param>
		/// <param name="newEnd">The end value of the new range to convert to.</param>
		/// <returns>The value converted to the new range of values.</returns>
		public static double ConvertRange(
			double value, double originalStart, double originalEnd, double newStart, double newEnd)
		{
			Contracts.Requires.That(originalStart != originalEnd);
			Contracts.Requires.That(newStart != newEnd);

			return (((value - originalStart) * (newEnd - newStart)) / (originalEnd - originalStart)) + newStart;
		}

		/// <summary>
		/// Calculates the weighted average between two values.
		/// </summary>
		/// <param name="value0">The value to return if the weight is 0.</param>
		/// <param name="value1">The value to return if the weight is 1.</param>
		/// <param name="weight">A weight between 0 and 1.</param>
		/// <returns>The weighted average.</returns>
		/// <remarks>
		/// The closer <paramref name="weight"/> is to 0 the closer the returned value will be to
		/// <paramref name="value0"/>. The closer <paramref name="weight"/> is to 1 the closer the
		/// returned value will be to <paramref name="value1"/>.
		/// </remarks>
		public static double WeightedAverage(double value0, double value1, double weight)
		{
			Contracts.Requires.That(weight.IsIn(Range.New(0.0, 1)));

			return (value0 * (1 - weight)) + (value1 * weight);
		}

		#region Dimensional Interpolations

		/// <summary>
		/// Defines a gradient between two values associated with two points on a line and returns the
		/// weighted average value of a third point along that gradient line.
		/// </summary>
		/// <param name="x">The x coordinate of the value to determine.</param>
		/// <param name="x0">The x coordinate of the first point defining the gradient.</param>
		/// <param name="x1">The x coordinate of the second point defining the gradient.</param>
		/// <param name="value0">The value of the gradient at the point <paramref name="x0"/>.</param>
		/// <param name="value1">The value of the gradient at the point <paramref name="x1"/>.</param>
		/// <returns>The value of the gradient at the point <paramref name="x"/>.</returns>
		/// <seealso href="https://en.wikipedia.org/wiki/Linear_interpolation"/>
		public static double Linear(
			double x,
			double x0,
			double x1,
			double value0,
			double value1)
		{
			double divisor = x1 - x0;

			if (divisor == 0)
			{
				return (value0 + value1) / 2d;
			}

			return value0 + ((value1 - value0) * ((x - x0) / divisor));
		}

		/// <summary>
		/// Defines a gradient between values associated with the bounds of two two dimensional points and
		/// returns the weighted average value of a third point along that gradient.
		/// </summary>
		/// <param name="x">The x coordinate of the value to determine.</param>
		/// <param name="y">The y coordinate of the value to determine.</param>
		/// <param name="x0">The x coordinate of the first point defining the gradient.</param>
		/// <param name="y0">The y coordinate of the first point defining the gradient.</param>
		/// <param name="x1">The x coordinate of the second point defining the gradient.</param>
		/// <param name="y1">The y coordinate of the second point defining the gradient.</param>
		/// <param name="value00">The value of the gradient at the point (<paramref name="x0"/>, <paramref name="y0"/>).</param>
		/// <param name="value10">The value of the gradient at the point (<paramref name="x1"/>, <paramref name="y0"/>).</param>
		/// <param name="value01">The value of the gradient at the point (<paramref name="x0"/>, <paramref name="y1"/>).</param>
		/// <param name="value11">The value of the gradient at the point (<paramref name="x1"/>, <paramref name="y1"/>).</param>
		/// <returns>The value of the gradient at the point (<paramref name="x"/>, <paramref name="y"/>).</returns>
		/// <seealso href="https://en.wikipedia.org/wiki/Bilinear_interpolation"/>
		public static double Bilinear(
			double x,
			double y,
			double x0,
			double y0,
			double x1,
			double y1,
			double value00,
			double value10,
			double value01,
			double value11)
		{
			double lower = Linear(x, x0, x1, value00, value10);
			double upper = Linear(x, x0, x1, value01, value11);

			return Linear(y, y0, y1, lower, upper);
		}

		/// <summary>
		/// Defines a gradient between values associated with the bounds of two three dimensional points and
		/// returns the weighted average value of a third point along that gradient.
		/// </summary>
		/// <param name="x">The x coordinate of the value to determine.</param>
		/// <param name="y">The y coordinate of the value to determine.</param>
		/// <param name="z">The z coordinate of the value to determine.</param>
		/// <param name="x0">The x coordinate of the first point defining the gradient.</param>
		/// <param name="y0">The y coordinate of the first point defining the gradient.</param>
		/// <param name="z0">The z coordinate of the first point defining the gradient.</param>
		/// <param name="x1">The x coordinate of the second point defining the gradient.</param>
		/// <param name="y1">The y coordinate of the second point defining the gradient.</param>
		/// <param name="z1">The z coordinate of the second point defining the gradient.</param>
		/// <param name="value000">
		/// The value of the gradient at the point (<paramref name="x0"/>, <paramref name="y0"/>, <paramref name="z0"/>).
		/// </param>
		/// <param name="value100">
		/// The value of the gradient at the point (<paramref name="x1"/>, <paramref name="y0"/>, <paramref name="z0"/>).
		/// </param>
		/// <param name="value010">
		/// The value of the gradient at the point (<paramref name="x0"/>, <paramref name="y1"/>, <paramref name="z0"/>).
		/// </param>
		/// <param name="value110">
		/// The value of the gradient at the point (<paramref name="x1"/>, <paramref name="y1"/>, <paramref name="z0"/>).
		/// </param>
		/// <param name="value001">
		/// The value of the gradient at the point (<paramref name="x0"/>, <paramref name="y0"/>, <paramref name="z1"/>).
		/// </param>
		/// <param name="value101">
		/// The value of the gradient at the point (<paramref name="x1"/>, <paramref name="y0"/>, <paramref name="z1"/>).
		/// </param>
		/// <param name="value011">
		/// The value of the gradient at the point (<paramref name="x0"/>, <paramref name="y1"/>, <paramref name="z1"/>).
		/// </param>
		/// <param name="value111">
		/// The value of the gradient at the point (<paramref name="x1"/>, <paramref name="y1"/>, <paramref name="z1"/>).
		/// </param>
		/// <returns>
		/// The value of the gradient at the point (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>).
		/// </returns>
		/// <seealso href="https://en.wikipedia.org/wiki/Trilinear_interpolation"/>
		public static double Trilinear(
			double x,
			double y,
			double z,
			double x0,
			double y0,
			double z0,
			double x1,
			double y1,
			double z1,
			double value000,
			double value100,
			double value010,
			double value110,
			double value001,
			double value101,
			double value011,
			double value111)
		{
			double frontLower = Linear(x, x0, x1, value000, value100);
			double frontUpper = Linear(x, x0, x1, value010, value110);
			double backLower = Linear(x, x0, x1, value001, value101);
			double backUpper = Linear(x, x0, x1, value011, value111);

			double front = Linear(y, y0, y1, frontLower, frontUpper);
			double back = Linear(y, y0, y1, backLower, backUpper);

			return Linear(z, z0, z1, front, back);
		}

		#endregion
	}
}
