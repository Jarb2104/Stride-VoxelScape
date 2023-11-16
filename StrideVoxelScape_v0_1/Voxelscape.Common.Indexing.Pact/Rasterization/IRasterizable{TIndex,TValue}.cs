using System.Diagnostics;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Pact.Rasterization
{
	/// <summary>
	/// Represents a type that can be rasterized into an indexable version.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index. This determines the dimensions of the mask.</typeparam>
	/// <typeparam name="TValue">The type of the value of the mask.</typeparam>
	public interface IRasterizable<TIndex, TValue>
		where TIndex : IIndex
	{
		/// <summary>
		/// Converts the instance to an indexable representation given the specified fidelity.
		/// </summary>
		/// <param name="cellLength">
		/// The length of a cell to rasterize the type at. The lower the value the smaller the indexable
		/// cells will be and the greater the total dimensions of the resulting indexable will be.
		/// </param>
		/// <returns>The rasterized indexable version of this instance.</returns>
		IBoundedIndexable<TIndex, TValue> Rasterize(float cellLength);
	}

	public static class IRasterizableContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Rasterize(float cellLength)
		{
			Contracts.Requires.That(cellLength > 0);
		}
	}
}
