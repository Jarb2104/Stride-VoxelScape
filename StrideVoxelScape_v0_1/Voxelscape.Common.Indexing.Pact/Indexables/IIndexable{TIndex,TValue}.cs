using System.Diagnostics;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Pact.Indexables
{
	/// <summary>
	/// Interface to define a grouping of values accessed by an <see cref="IIndex"/> of an undefined number of dimensions.
	/// Implementations of this interface may define how many dimensions they support.
	/// </summary>
	/// <typeparam name="TIndex">
	/// The type of the index used to access values.
	/// This determines the number of dimensions that can be indexed.
	/// </typeparam>
	/// <typeparam name="TValue">The type of the stored values.</typeparam>
	public interface IIndexable<TIndex, TValue> : IReadOnlyIndexable<TIndex, TValue>
		where TIndex : IIndex
	{
		/// <summary>
		/// Gets or sets the <typeparamref name="TValue"/> at the specified index.
		/// </summary>
		/// <value>
		/// The <typeparamref name="TValue"/> to assign at the specified index.
		/// </value>
		/// <param name="index">The index.</param>
		/// <returns>
		/// The <typeparamref name="TValue"/> at the specified index.
		/// </returns>
		new TValue this[TIndex index] { get; set; }

		/// <summary>
		/// Sets value at the specified index if the index is valid to use.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value to assign at the specified index.</param>
		/// <returns>True if the specified index is valid, otherwise false.</returns>
		bool TrySetValue(TIndex index, TValue value);
	}

	public static class IIndexableContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void IndexerSet<TIndex, TValue>(IIndexable<TIndex, TValue> instance, TIndex index)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(instance.IsIndexValid(index));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void TrySetValue<TIndex, TValue>(IIndexable<TIndex, TValue> instance, TIndex index)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(index != null);
			Contracts.Requires.That(instance.Rank == index.Rank);
		}
	}
}
