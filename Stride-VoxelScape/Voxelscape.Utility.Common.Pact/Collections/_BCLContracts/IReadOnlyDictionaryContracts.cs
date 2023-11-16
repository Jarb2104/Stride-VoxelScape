using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Collections
{
	/// <summary>
	/// Provides contracts for <see cref="IReadOnlyDictionary{TKey, TValue}"/> interface.
	/// </summary>
	public static class IReadOnlyDictionaryContracts
	{
		/// <summary>
		/// Contracts for <see cref="IReadOnlyDictionary{TKey, TValue}" />'s index getter.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="key">The key.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Indexer<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> instance, TKey key)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(key != null);
			Contracts.Requires.That(instance.ContainsKey(key));
		}

		/// <summary>
		/// Contracts for <see cref="IReadOnlyDictionary{TKey, TValue}.ContainsKey(TKey)" />.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <param name="key">The key.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ContainsKey<TKey>(TKey key)
		{
			Contracts.Requires.That(key != null);
		}

		/// <summary>
		/// Contracts for <see cref="IReadOnlyDictionary{TKey, TValue}.TryGetValue(TKey, out TValue)" />.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <param name="key">The key.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void TryGetValue<TKey>(TKey key)
		{
			Contracts.Requires.That(key != null);
		}
	}
}
