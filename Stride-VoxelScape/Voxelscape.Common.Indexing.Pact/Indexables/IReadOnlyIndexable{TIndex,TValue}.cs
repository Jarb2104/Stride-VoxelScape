using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Pact.Indexables
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="TIndex">
	/// The type of the index used to access values.
	/// This determines the number of dimensions that can be indexed.
	/// </typeparam>
	/// <typeparam name="TValue">The type of the stored values.</typeparam>
	public interface IReadOnlyIndexable<TIndex, TValue> : IEnumerable<KeyValuePair<TIndex, TValue>>
		where TIndex : IIndex
	{
		/// <summary>
		/// Gets the rank (number of dimensions) this implementation supports.
		/// For example, a one-dimensional indexable returns 1, a two-dimensional indexable returns 2, and so on.
		/// </summary>
		/// <value>
		/// The number of dimensions.
		/// </value>
		int Rank { get; }

		/// <summary>
		/// Gets the <typeparamref name="TValue"/> at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>
		/// The <typeparamref name="TValue"/> at the specified index.
		/// </returns>
		TValue this[TIndex index] { get; }

		/// <summary>
		/// Determines whether an index is valid to use with this instance's indexer.
		/// </summary>
		/// <param name="index">The index to check.</param>
		/// <returns>True if the index is valid to use, otherwise false.</returns>
		/// <remarks>
		/// An index could be invalid for a variety of reasons. For example, the implementation may not accept negative indices,
		/// or it could be a small bounded array and the index is too large. It is entirely up to the implementer of this interface
		/// to determine what is considered valid or invalid. It is for this reason that this interface provides this method to test
		/// validity but exposes no further assumptions or dependencies about size. Whether an index is valid or not could even
		/// change over time as the underlying collection changes. For example, it could be a dynamic array that grows in response
		/// to how full it is. This interface also makes no guarantees about continuous ranges of indices being valid. An implementer
		/// could choose to have pockets of indices that are considered invalid surrounded by valid indices.
		/// </remarks>
		bool IsIndexValid(TIndex index);

		/// <summary>
		/// Gets value at the specified index if the index is valid to use.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">
		/// When this method returns, the value associated with the specified index, if the index is valid;
		/// otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
		/// <returns>True if the specified index is valid, otherwise false.</returns>
		bool TryGetValue(TIndex index, out TValue value);
	}

	public static class IReadOnlyIndexableContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void IndexerGet<TIndex, TValue>(IReadOnlyIndexable<TIndex, TValue> instance, TIndex index)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(instance.IsIndexValid(index));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void IsIndexValid<TIndex, TValue>(IReadOnlyIndexable<TIndex, TValue> instance, TIndex index)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(index != null);
			Contracts.Requires.That(instance.Rank == index.Rank);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void TryGetValue<TIndex, TValue>(IReadOnlyIndexable<TIndex, TValue> instance, TIndex index)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(index != null);
			Contracts.Requires.That(instance.Rank == index.Rank);
		}
	}
}
