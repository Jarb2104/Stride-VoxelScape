using System;
using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Concurrency.Core.Reactive;

namespace Voxelscape.Stages.Management.Pact.Interests
{
	public interface IInterestMap<TKey, TInterest> : IVisiblyDisposable
	{
		IEqualityComparer<TInterest> Comparer { get; }

		TInterest None { get; }

		IObservable<KeyValuePair<TKey, SequentialPair<TInterest>>> InterestsChanged { get; }

		TInterest this[TKey key] { get; }

		void AddInterest(TKey key, TInterest interest);

		void RemoveInterest(TKey key, TInterest interest);

		void UpdateInterests(TInterest interest, Utility.Common.Pact.Collections.IReadOnlySet<TKey> addTo, Utility.Common.Pact.Collections.IReadOnlySet<TKey> removeFrom);
	}

	public static class IInterestMapContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Indexer<TKey, TInterest>(IInterestMap<TKey, TInterest> instance, TKey key)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(key != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddInterest<TKey, TInterest>(IInterestMap<TKey, TInterest> instance, TKey key)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(key != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void RemoveInterest<TKey, TInterest>(IInterestMap<TKey, TInterest> instance, TKey key)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(key != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void UpdateInterests<TKey, TInterest>(
			IInterestMap<TKey, TInterest> instance, Utility.Common.Pact.Collections.IReadOnlySet<TKey> addTo, Utility.Common.Pact.Collections.IReadOnlySet<TKey> removeFrom)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(addTo.AllAndSelfNotNull());
			Contracts.Requires.That(removeFrom.AllAndSelfNotNull());
		}
	}
}
