using Voxelscape.Common.Indexing.Core.Enums;

namespace Voxelscape.Common.Indexing.Core.Indices
{
	/// <summary>
	///
	/// </summary>
	internal static class InternalIndexUtilities
	{
		/// <summary>
		/// Gets the orthant of an axis. The result from this can be combined with the results for all other axes
		/// to produce the final enumeration value designating the specific orthant.
		/// </summary>
		/// <param name="thisCoordinate">The coordinate of the index whose orthant location is being determined.</param>
		/// <param name="originCoordinate">The origin coordinate to base the orthants off of.</param>
		/// <param name="axis">The axis. For X this is 0, Y is 1, and so on.</param>
		/// <returns>The bit flags that define this axis's portion of the final orthant enumeration value.</returns>
		internal static int GetOrthantOfAxis(int thisCoordinate, int originCoordinate, int axis)
		{
			// 3 bits are used by each axis in the Orthant enum
			axis *= 3;

			if (thisCoordinate < originCoordinate)
			{
				return (int)Orthant1D.LessX << axis;
			}
			else if (thisCoordinate > originCoordinate)
			{
				return (int)Orthant1D.GreaterX << axis;
			}
			else
			{
				return (int)Orthant1D.EqualX << axis;
			}
		}
	}
}
