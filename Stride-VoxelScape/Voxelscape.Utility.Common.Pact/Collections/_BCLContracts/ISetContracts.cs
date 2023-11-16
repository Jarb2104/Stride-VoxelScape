using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Collections
{
	/// <summary>
	/// Provides contracts for the <see cref="ISet{T}"/> interface.
	/// </summary>
	public static class ISetContracts
	{
		/// <summary>
		/// Contracts for <see cref="ISet{T}.ExceptWith(IEnumerable{T})" />.
		/// </summary>
		/// <typeparam name="T">The type of the values in the set.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="other">The collection to compare to the current set.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ExceptWith<T>(ISet<T> instance, IEnumerable<T> other)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsReadOnly);
			Contracts.Requires.That(other != null);
		}

		/// <summary>
		/// Contracts for <see cref="ISet{T}.IntersectWith(IEnumerable{T})" />.
		/// </summary>
		/// <typeparam name="T">The type of the values in the set.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="other">The collection to compare to the current set.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void IntersectWith<T>(ISet<T> instance, IEnumerable<T> other)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsReadOnly);
			Contracts.Requires.That(other != null);
		}

		/// <summary>
		/// Contracts for <see cref="ISet{T}.IsProperSubsetOf(IEnumerable{T})"/>.
		/// </summary>
		/// <typeparam name="T">The type of the values in the set.</typeparam>
		/// <param name="other">The collection to compare to the current set.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void IsProperSubsetOf<T>(IEnumerable<T> other)
		{
			Contracts.Requires.That(other != null);
		}

		/// <summary>
		/// Contracts for <see cref="ISet{T}.IsProperSupersetOf(IEnumerable{T})"/>.
		/// </summary>
		/// <typeparam name="T">The type of the values in the set.</typeparam>
		/// <param name="other">The collection to compare to the current set.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void IsProperSupersetOf<T>(IEnumerable<T> other)
		{
			Contracts.Requires.That(other != null);
		}

		/// <summary>
		/// Contracts for <see cref="ISet{T}.IsSubsetOf(IEnumerable{T})"/>.
		/// </summary>
		/// <typeparam name="T">The type of the values in the set.</typeparam>
		/// <param name="other">The collection to compare to the current set.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void IsSubsetOf<T>(IEnumerable<T> other)
		{
			Contracts.Requires.That(other != null);
		}

		/// <summary>
		/// Contracts for <see cref="ISet{T}.IsSupersetOf(IEnumerable{T})"/>.
		/// </summary>
		/// <typeparam name="T">The type of the values in the set.</typeparam>
		/// <param name="other">The collection to compare to the current set.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void IsSupersetOf<T>(IEnumerable<T> other)
		{
			Contracts.Requires.That(other != null);
		}

		/// <summary>
		/// Contracts for <see cref="ISet{T}.Overlaps(IEnumerable{T})"/>.
		/// </summary>
		/// <typeparam name="T">The type of the values in the set.</typeparam>
		/// <param name="other">The collection to compare to the current set.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Overlaps<T>(IEnumerable<T> other)
		{
			Contracts.Requires.That(other != null);
		}

		/// <summary>
		/// Contracts for <see cref="ISet{T}.SetEquals(IEnumerable{T})"/>.
		/// </summary>
		/// <typeparam name="T">The type of the values in the set.</typeparam>
		/// <param name="other">The collection to compare to the current set.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void SetEquals<T>(IEnumerable<T> other)
		{
			Contracts.Requires.That(other != null);
		}

		/// <summary>
		/// Contracts for <see cref="ISet{T}.SymmetricExceptWith(IEnumerable{T})" />.
		/// </summary>
		/// <typeparam name="T">The type of the values in the set.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="other">The collection to compare to the current set.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void SymmetricExceptWith<T>(ISet<T> instance, IEnumerable<T> other)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsReadOnly);
			Contracts.Requires.That(other != null);
		}

		/// <summary>
		/// Contracts for <see cref="ISet{T}.UnionWith(IEnumerable{T})" />.
		/// </summary>
		/// <typeparam name="T">The type of the values in the set.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="other">The collection to compare to the current set.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void UnionWith<T>(ISet<T> instance, IEnumerable<T> other)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsReadOnly);
			Contracts.Requires.That(other != null);
		}
	}
}
