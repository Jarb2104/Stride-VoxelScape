using System;
using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Collections
{
	public interface ITypeSet<TBase> :
		ISet<TBase>, IDictionary<Type, TBase>, IReadOnlyDictionary<Type, TBase>, IEnumerable<TBase>
	{
		// avoid ambiguity
		new int Count { get; }

		// avoid ambiguity
		new bool IsReadOnly { get; }

		// additional contracts
		new TBase this[Type key] { get; set; }

		bool Add<T>(T value)
			where T : TBase;

		// additional return value
		new bool Add(KeyValuePair<Type, TBase> item);

		// additional return value
		new bool Add(Type key, TBase value);

		bool ContainsKey<T>()
			where T : TBase;

		bool TryGetValue<T>(out T value)
			where T : TBase;

		bool Remove<T>()
			where T : TBase;
	}

	public static class ITypeSetContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void IndexerGet<TBase>(ITypeSet<TBase> instance, Type key) =>
			IDictionaryContracts.IndexerGet(instance, key);

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void IndexerSet<TBase>(ITypeSet<TBase> instance, Type key, TBase value)
		{
			IDictionaryContracts.IndexerSet(instance, key);
			Contracts.Requires.That(value.GetType().IsImplementationOfType(key));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Add<TBase, T>(ITypeSet<TBase> instance, T value)
			where T : TBase
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsReadOnly);
			Contracts.Requires.That(value != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Add<TBase>(ITypeSet<TBase> instance, KeyValuePair<Type, TBase> item)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsReadOnly);
			Contracts.Requires.That(item.Key != null);
			Contracts.Requires.That(item.Value != null);
			Contracts.Requires.That(item.Value.GetType().IsImplementationOfType(item.Key));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Add<TBase>(ITypeSet<TBase> instance, Type key, TBase value)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsReadOnly);
			Contracts.Requires.That(key != null);
			Contracts.Requires.That(value != null);
			Contracts.Requires.That(value.GetType().IsImplementationOfType(key));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Remove<TBase>(ITypeSet<TBase> instance)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsReadOnly);
		}
	}
}
