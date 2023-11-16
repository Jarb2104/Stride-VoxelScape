using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;
using KeyValuePair = Voxelscape.Utility.Common.Core.Collections.KeyValuePair;

namespace Voxelscape.Stages.Management.Core.Stages
{
	public class Stage<TKey, TChunk> : AbstractDisposable, IStage<TKey, TChunk>
	{
		private readonly Subject<KeyValuePair<TKey, TChunk>> activated = new Subject<KeyValuePair<TKey, TChunk>>();

		private readonly Subject<KeyValuePair<TKey, TChunk>> deactivated = new Subject<KeyValuePair<TKey, TChunk>>();

		private readonly IDisposable chunkActivitySubscription;

		private readonly IFactory<TKey, TChunk> factory;

		private readonly IDictionary<TKey, TChunk> stage;

		public Stage(IObservable<KeyValuePair<TKey, bool>> chunkActivity, IFactory<TKey, TChunk> factory)
			: this(chunkActivity, factory, new Dictionary<TKey, TChunk>())
		{
		}

		public Stage(
			IObservable<KeyValuePair<TKey, bool>> chunkActivity,
			IFactory<TKey, TChunk> factory,
			IEqualityComparer<TKey> comparer)
			: this(chunkActivity, factory, new Dictionary<TKey, TChunk>(comparer))
		{
		}

		public Stage(
			IObservable<KeyValuePair<TKey, bool>> chunkActivity,
			IFactory<TKey, TChunk> factory,
			IDictionary<TKey, TChunk> stage)
		{
			Contracts.Requires.That(chunkActivity != null);
			Contracts.Requires.That(factory != null);
			Contracts.Requires.That(stage != null);

			this.factory = factory;
			this.stage = stage;

			this.chunkActivitySubscription = chunkActivity.Subscribe(this.OnNext, () => this.Dispose());

			this.Activated = this.activated.AsObservable();
			this.Deactivated = this.deactivated.AsObservable();
		}

		/// <inheritdoc />
		public IObservable<KeyValuePair<TKey, TChunk>> Activated { get; }

		/// <inheritdoc />
		public IObservable<KeyValuePair<TKey, TChunk>> Deactivated { get; }

		/// <inheritdoc />
		public int Count => this.stage.Count;

		/// <inheritdoc />
		public IEnumerable<TKey> Keys => this.stage.Keys;

		/// <inheritdoc />
		public IEnumerable<TChunk> Values => this.stage.Values;

		/// <inheritdoc />
		public TChunk this[TKey key]
		{
			get
			{
				IReadOnlyDictionaryContracts.Indexer(this, key);

				return this.stage[key];
			}
		}

		/// <inheritdoc />
		public bool ContainsKey(TKey key)
		{
			IReadOnlyDictionaryContracts.ContainsKey(key);

			return this.stage.ContainsKey(key);
		}

		/// <inheritdoc />
		public bool TryGetValue(TKey key, out TChunk chunk)
		{
			IReadOnlyDictionaryContracts.TryGetValue(key);

			return this.stage.TryGetValue(key, out chunk);
		}

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<TKey, TChunk>> GetEnumerator() => this.stage.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		/// <inheritdoc />
		protected override void ManagedDisposal()
		{
			this.chunkActivitySubscription.Dispose();
			this.activated.OnCompletedAndDispose();

			foreach (var pair in this.stage)
			{
				this.deactivated.OnNext(pair);
			}

			this.deactivated.OnCompletedAndDispose();
		}

		private void OnNext(KeyValuePair<TKey, bool> pair)
		{
			if (pair.Value)
			{
				// chunk activated
				if (!this.stage.ContainsKey(pair.Key))
				{
					var chunk = this.factory.Create(pair.Key);
					this.stage[pair.Key] = chunk;
					this.activated.OnNext(KeyValuePair.New(pair.Key, chunk));
				}
			}
			else
			{
				// chunk deactivated
				TChunk chunk;
				if (this.stage.TryGetValue(pair.Key, out chunk))
				{
					this.stage.Remove(pair.Key);
					this.deactivated.OnNext(KeyValuePair.New(pair.Key, chunk));
				}
			}
		}
	}
}
