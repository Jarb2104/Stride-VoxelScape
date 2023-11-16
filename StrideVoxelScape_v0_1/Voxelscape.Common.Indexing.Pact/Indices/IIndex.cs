using System.Diagnostics;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Pact.Indices
{
	/// <summary>
	/// An integer based index into an array or grid based structure of an undefined
	/// number of dimensions. Implementations of this interface define how many
	/// dimensions the index can represent.
	/// </summary>
	public interface IIndex
	{
		/// <summary>
		/// Gets the rank (number of dimensions) of the IIndex. For example, a one-dimensional index returns 1,
		/// a two-dimensional index returns 2, and so on.
		/// </summary>
		/// <value>
		/// The number of dimensions.
		/// </value>
		int Rank { get; }

		/// <summary>
		/// Gets the coordinate value or index of a particular dimension of the IIndex.
		/// </summary>
		/// <param name="dimension">
		/// A zero-based dimension of the IIndex whose coordinate value needs to be determined.
		/// </param>
		/// <returns>
		/// The value of the dimensional coordinate.
		/// </returns>
		int this[int dimension] { get; }
	}

	public static class IIndexContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Indexer<TIndex>(TIndex instance, int dimension)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(dimension.IsIn(Range.FromLength(instance.Rank)));
		}
	}
}
