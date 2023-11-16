using System;
using System.Diagnostics;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Pact.Rasterization
{
	/// <summary>
	/// Represents a type that can be used as a mask during rasterization to create an indexable.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index. This determines the dimensions of the mask.</typeparam>
	/// <typeparam name="TValue">The type of the mask values.</typeparam>
	public interface IRasterizableMask<TIndex, TValue>
		where TIndex : IIndex
	{
		/// <summary>
		/// Converts the instance to an indexable representation given the specified fidelity.
		/// </summary>
		/// <param name="cellLength">
		/// The length of a cell to rasterize the type at. The lower the value the smaller the indexable
		/// cells will be and the greater the total dimensions of the resulting indexable will be.
		/// </param>
		/// <param name="internalValue">The value to assign inside of the mask.</param>
		/// <param name="externalValue">The value to assign outside of the mask.</param>
		/// <returns>The rasterized indexable version of this instance.</returns>
		IBoundedIndexable<TIndex, TValue> Rasterize(
			float cellLength, TValue internalValue, TValue externalValue);

		/// <summary>
		/// Converts the instance to an indexable representation given the specified fidelity.
		/// </summary>
		/// <param name="cellLength">
		/// The length of a cell to rasterize the type at. The lower the value the smaller the indexable
		/// cells will be and the greater the total dimensions of the resulting indexable will be.
		/// </param>
		/// <param name="internalValueFactory">The value factory used to assign values inside of the mask.</param>
		/// <param name="externalValueFactory">The value factory used to assign values outside of the mask.</param>
		/// <returns>The rasterized indexable version of this instance.</returns>
		IBoundedIndexable<TIndex, TValue> Rasterize(
			float cellLength, Func<TValue> internalValueFactory, Func<TValue> externalValueFactory);

		/// <summary>
		/// Converts the instance to an indexable representation given the specified fidelity.
		/// </summary>
		/// <param name="cellLength">
		/// The length of a cell to rasterize the type at. The lower the value the smaller the indexable
		/// cells will be and the greater the total dimensions of the resulting indexable will be.
		/// </param>
		/// <param name="internalValueFactory">The value factory used to assign values inside of the mask.</param>
		/// <param name="externalValueFactory">The value factory used to assign values outside of the mask.</param>
		/// <returns>The rasterized indexable version of this instance.</returns>
		IBoundedIndexable<TIndex, TValue> Rasterize(
			float cellLength, Func<TIndex, TValue> internalValueFactory, Func<TIndex, TValue> externalValueFactory);
	}

	public static class IRasterizableMaskContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Rasterize<TValue>(float cellLength, TValue internalValue, TValue externalValue)
		{
			Contracts.Requires.That(cellLength > 0);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Rasterize<TValue>(
			float cellLength, Func<TValue> internalValueFactory, Func<TValue> externalValueFactory)
		{
			Contracts.Requires.That(cellLength > 0);
			Contracts.Requires.That(internalValueFactory != null);
			Contracts.Requires.That(externalValueFactory != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Rasterize<TIndex, TValue>(
			float cellLength, Func<TIndex, TValue> internalValueFactory, Func<TIndex, TValue> externalValueFactory)
		{
			Contracts.Requires.That(cellLength > 0);
			Contracts.Requires.That(internalValueFactory != null);
			Contracts.Requires.That(externalValueFactory != null);
		}
	}
}
