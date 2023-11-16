using System.Linq;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Pact.Indices
{
	/// <summary>
	///
	/// </summary>
	public static class IndexUtilities
	{
		/// <summary>
		/// Determines whether the specified index is within the specified range of indices.
		/// </summary>
		/// <typeparam name="TIndex">The type of the index.</typeparam>
		/// <param name="index">The index.</param>
		/// <param name="minimum">The minimum index.</param>
		/// <param name="maximum">The maximum index.</param>
		/// <param name="rangeOptions">The inclusive/exclusive range options.</param>
		/// <returns>True if the value is within range, false otherwise.</returns>
		public static bool IsIn<TIndex>(
			TIndex index,
			TIndex minimum,
			TIndex maximum,
			RangeClusivity rangeOptions = RangeClusivity.Inclusive)
			where TIndex : IIndex
		{
			Contracts.Requires.That(index != null);
			Contracts.Requires.That(minimum != null);
			Contracts.Requires.That(maximum != null);
			Contracts.Requires.That(index.Rank == minimum.Rank);
			Contracts.Requires.That(minimum.Rank == maximum.Rank);
			Contracts.Requires.That(
				Enumerable.Range(0, index.Rank).All(dimension => minimum[dimension] <= maximum[dimension]));

			for (int dimension = 0; dimension < index.Rank; dimension++)
			{
				if (!index[dimension].IsIn(Range.New(minimum[dimension], maximum[dimension], rangeOptions)))
				{
					return false;
				}
			}

			return true;
		}
	}
}
