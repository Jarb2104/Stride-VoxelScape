using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Collections
{
	/// <summary>
	/// Provides contracts for <see cref="IList{T}"/> interface.
	/// </summary>
	public static class IListContracts
	{
		/// <summary>
		/// Contracts for <see cref="IList{T}"/>'s index getter.
		/// </summary>
		/// <typeparam name="T">The type of the values stored in the list.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="index">The index.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void IndexerGet<T>(IList<T> instance, int index)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(index >= 0 && index < instance.Count);
		}

		/// <summary>
		/// Contracts for <see cref="IList{T}"/>'s index setter.
		/// </summary>
		/// <typeparam name="T">The type of the values stored in the list.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="index">The index.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void IndexerSet<T>(IList<T> instance, int index)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsReadOnly);
			Contracts.Requires.That(index >= 0 && index < instance.Count);
		}

		/// <summary>
		/// Contracts for <see cref="IList{T}.Insert(int, T)"/>.
		/// </summary>
		/// <typeparam name="T">The type of the values stored in the list.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="index">The index.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Insert<T>(IList<T> instance, int index)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsReadOnly);
			Contracts.Requires.That(index >= 0 && index <= instance.Count);
		}

		/// <summary>
		/// Contracts for <see cref="IList{T}.RemoveAt(int)"/>.
		/// </summary>
		/// <typeparam name="T">The type of the values stored in the list.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="index">The index.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void RemoveAt<T>(IList<T> instance, int index)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsReadOnly);
			Contracts.Requires.That(index >= 0 && index < instance.Count);
		}
	}
}
