using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Voxelscape.Stages.Management.Pact.Interests;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Concurrency.Core.Reactive;

namespace Voxelscape.Stages.Management.Core.Interests
{
	public class InterestMap<TKey, TInterest> : AbstractDisposable, IInterestMap<TKey, TInterest>
	{
		private readonly Subject<KeyValuePair<TKey, SequentialPair<TInterest>>> interestsChanged =
			new Subject<KeyValuePair<TKey, SequentialPair<TInterest>>>();

		private readonly IInterestMerger<TInterest> merger;

		private readonly IDictionary<TKey, TInterest> map;

		public InterestMap(IInterestMerger<TInterest> merger)
			: this(merger, new Dictionary<TKey, TInterest>())
		{
		}

		public InterestMap(IInterestMerger<TInterest> merger, IEqualityComparer<TKey> comparer)
			: this(merger, new Dictionary<TKey, TInterest>(comparer))
		{
		}

		public InterestMap(IInterestMerger<TInterest> merger, IDictionary<TKey, TInterest> map)
		{
			Contracts.Requires.That(merger != null);
			Contracts.Requires.That(map != null);

			this.merger = merger;
			this.map = map;
			this.InterestsChanged = this.interestsChanged.AsObservable();
		}

		/// <inheritdoc />
		public IEqualityComparer<TInterest> Comparer => this.merger.Comparer;

		/// <inheritdoc />
		public TInterest None => this.merger.None;

		/// <inheritdoc />
		public IObservable<KeyValuePair<TKey, SequentialPair<TInterest>>> InterestsChanged { get; }

		/// <inheritdoc />
		public TInterest this[TKey key]
		{
			get
			{
				IInterestMapContracts.Indexer(this, key);

				TInterest result;
				return this.map.TryGetValue(key, out result) ? result : this.merger.None;
			}
		}

		/// <inheritdoc />
		public void AddInterest(TKey key, TInterest interest)
		{
			IInterestMapContracts.AddInterest(this, key);

			TInterest previous = this[key];
			TInterest next = this.merger.GetInterestByAdding(previous, interest);

			if (!this.merger.Comparer.Equals(previous, next))
			{
				this.map[key] = next;
				this.interestsChanged.OnNext(Utility.Common.Core.Collections.KeyValuePair.New(key, SequentialPair.New(previous, next)));
			}
		}

		/// <inheritdoc />
		public void RemoveInterest(TKey key, TInterest interest)
		{
			IInterestMapContracts.RemoveInterest(this, key);

			TInterest previous = this[key];
			TInterest next = this.merger.GetInterestByRemoving(previous, interest);

			if (!this.merger.Comparer.Equals(previous, next))
			{
				if (this.merger.Comparer.Equals(next, this.merger.None))
				{
					this.map.Remove(key);
				}
				else
				{
					this.map[key] = next;
				}

				this.interestsChanged.OnNext(Utility.Common.Core.Collections.KeyValuePair.New(key, SequentialPair.New(previous, next)));
			}
		}

		/// <inheritdoc />
		public void UpdateInterests(TInterest interest, Utility.Common.Pact.Collections.IReadOnlySet<TKey> addTo, Utility.Common.Pact.Collections.IReadOnlySet<TKey> removeFrom)
		{
			IInterestMapContracts.UpdateInterests(this, addTo, removeFrom);

			foreach (var key in addTo)
			{
				if (!removeFrom.Contains(key))
				{
					this.AddInterest(key, interest);
				}
			}

			foreach (var key in removeFrom)
			{
				if (!addTo.Contains(key))
				{
					this.RemoveInterest(key, interest);
				}
			}
		}

		/// <inheritdoc />
		protected override void ManagedDisposal() => this.interestsChanged.OnCompletedAndDispose();
	}
}
