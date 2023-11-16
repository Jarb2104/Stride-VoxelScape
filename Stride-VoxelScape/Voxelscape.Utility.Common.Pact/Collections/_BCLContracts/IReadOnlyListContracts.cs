using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Collections
{
	/// <summary>
	/// Provides contracts for <see cref="IReadOnlyList{T}"/> interface.
	/// </summary>
	public static class IReadOnlyListContracts
	{
		/// <summary>
		/// Contracts for <see cref="IReadOnlyList{T}"/>'s index getter.
		/// </summary>
		/// <typeparam name="T">The type of the values stored in the list.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="index">The index.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Indexer<T>(IReadOnlyList<T> instance, int index)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(index >= 0 && index < instance.Count);
		}
	}
}
