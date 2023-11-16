using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Collections
{
	/// <summary>
	/// Provides contracts for the <see cref="ICollection{T}"/> interface.
	/// </summary>
	public static class ICollectionContracts
	{
		/// <summary>
		/// Contracts for <see cref="ICollection{T}.Add(T)"/>.
		/// </summary>
		/// <typeparam name="T">The type of value stored in the collection.</typeparam>
		/// <param name="instance">The instance.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Add<T>(ICollection<T> instance)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsReadOnly);
		}

		/// <summary>
		/// Contracts for <see cref="ICollection{T}.Add(T)"/>.
		/// </summary>
		/// <typeparam name="T">The type of value stored in the collection.</typeparam>
		/// <param name="instance">The instance.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Remove<T>(ICollection<T> instance)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsReadOnly);
		}

		/// <summary>
		/// Contracts for <see cref="ICollection{T}.Clear"/>.
		/// </summary>
		/// <typeparam name="T">The type of value stored in the collection.</typeparam>
		/// <param name="instance">The instance.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Clear<T>(ICollection<T> instance)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsReadOnly);
		}

		/// <summary>
		/// Contracts for <see cref="ICollection{T}.CopyTo(T[], int)"/>.
		/// </summary>
		/// <typeparam name="T">The type of value stored in the collection.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="array">The array.</param>
		/// <param name="index">Index of the array.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyTo<T>(ICollection<T> instance, T[] array, int index)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(array != null);
			Contracts.Requires.That(index >= 0);
			Contracts.Requires.That(index + instance.Count <= array.Length);
		}
	}
}
