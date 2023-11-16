using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nito.AsyncEx;
using Voxelscape.Utility.Common.Core.Disposables;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Concurrency.Core.Synchronization;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Utility.Data.Core.Caching
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TState">The type of the state.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <threadsafety static="true" instance="true" />
	public class AsyncCache<TKey, TState, TValue> :
		AbstractAsyncDisposedCompletable, IAsyncCache<TKey, TState, TValue>
	{
		private readonly IAsyncFactory<TKey, TState, ICacheValue<TKey, TValue>> factory;

		// The delegate used to create the cache entry could possibly be called multiple times
		// even though only one instance of it will be used in the dictionary.
		// https://msdn.microsoft.com/en-us/library/ee378677(v=vs.110).aspx
		// This is why AsyncLazy is used to generate the CacheEntry, ensuring the factory
		// is only executed once per CacheEntry.
		private readonly ConcurrentDictionary<TKey, AsyncLazy<CacheEntry>> entries;

		public AsyncCache(
			IAsyncFactory<TKey, TState, ICacheValue<TKey, TValue>> factory)
			: this(factory, EqualityComparer<TKey>.Default)
		{
		}

		public AsyncCache(
			IAsyncFactory<TKey, TState, ICacheValue<TKey, TValue>> factory,
			IEqualityComparer<TKey> comparer)
		{
			Contracts.Requires.That(factory != null);
			Contracts.Requires.That(comparer != null);

			this.factory = factory;
			this.entries = new ConcurrentDictionary<TKey, AsyncLazy<CacheEntry>>(comparer);
		}

		/// <inheritdoc />
		public async Task<IPinnedValue<TKey, TValue>> GetPinAsync(TKey key, TState state)
		{
			IAsyncCacheContracts.GetPinAsync(this, key);

			while (true)
			{
				try
				{
					var entry = await this.entries.GetOrAddLazyAsync(
						key, pinKey => this.CreateCacheEntryAsync(pinKey, state)).DontMarshallContext();

					IPinnedValue<TKey, TValue> pin;
					if (entry.TryPin(out pin))
					{
						// if pin was successful return the pin, otherwise loop back and try again
						// (pinning can fail if the cache entry is being disposed by another thread)
						return pin;
					}
				}
				catch (Exception)
				{
					// if creating the cache value threw an exception remove it from the dictionary
					// otherwise exceptions could slowly fill in the cache, aka memory leak
					this.entries.TryRemove(key);
					throw;
				}
			}
		}

		/// <inheritdoc />
		protected override async Task CompleteAsync()
		{
			var stillPinnedKeys = new List<TKey>();
			var exceptions = new List<Exception>();
			try
			{
				foreach (var lazyEntry in this.entries.ValuesLockFree())
				{
					try
					{
						var entry = await lazyEntry;
						if (!entry.IsDisposed && !entry.TryDispose())
						{
							stillPinnedKeys.Add(entry.Key);
						}
					}
					catch (Exception thrown)
					{
						exceptions.Add(thrown);
					}
				}
			}
			finally
			{
				if (stillPinnedKeys.Any())
				{
					exceptions.Add(new CacheCompletionException<TKey>(
						stillPinnedKeys,
						$"Unable to release {stillPinnedKeys.Count} cache entries because they are still pinned."));
				}

				if (exceptions.Count == 1)
				{
					throw exceptions.First();
				}

				if (exceptions.Count > 1)
				{
					throw new AggregateException(
						$"{exceptions.Count} exceptions were thrown while completing the cache.", exceptions);
				}
			}
		}

		private async Task<CacheEntry> CreateCacheEntryAsync(TKey key, TState state) =>
			new CacheEntry(this, await this.factory.CreateAsync(key, state).DontMarshallContext());

		private class CacheEntry : ICacheEntry<TKey>, IDisposed
		{
			private readonly LockableCounter pins = new LockableCounter();

			private readonly AtomicInt tokenVersion = new AtomicInt(int.MinValue);

			private readonly AsyncCache<TKey, TState, TValue> cache;

			private readonly ICacheValue<TKey, TValue> value;

			public CacheEntry(AsyncCache<TKey, TState, TValue> cache, ICacheValue<TKey, TValue> value)
			{
				Contracts.Requires.That(cache != null);
				Contracts.Requires.That(value != null);

				this.cache = cache;
				this.value = value;
			}

			/// <inheritdoc />
			public TKey Key => this.value.Key;

			/// <inheritdoc />
			public bool IsDisposed => this.pins.IsLocked;

			/// <inheritdoc />
			public bool IsTokenExpired(ExpiryToken<TKey> expirationToken) =>
				expirationToken.TokenVersion != this.tokenVersion.Read();

			/// <inheritdoc />
			public bool TryDisposeAndRemove(ExpiryToken<TKey> expirationToken)
			{
				if (this.IsTokenExpired(expirationToken))
				{
					return false;
				}

				if (this.TryDispose())
				{
					this.cache.entries.TryRemove(this.Key);
					return true;
				}
				else
				{
					return false;
				}
			}

			public bool TryDispose()
			{
				if (this.pins.TryLock())
				{
					this.value.Dispose();
					return true;
				}
				else
				{
					return false;
				}
			}

			public bool TryPin(out IPinnedValue<TKey, TValue> pin)
			{
				if (this.pins.TryAddCount())
				{
					pin = new Pin(this.cache, this, this.value.Value);
					return true;
				}
				else
				{
					pin = null;
					return false;
				}
			}

			public void Unpin()
			{
				int newPinCount;
				if (this.pins.TryRemoveCount(out newPinCount) && newPinCount == 0)
				{
					// cache entry went from being pinned to no longer pinned
					// so create new cache token with incremented version
					int newTokenVersion = this.tokenVersion.Increment();
					this.value.AddExpiration(new ExpiryToken<TKey>(this, newTokenVersion));
				}
			}
		}

		private class Pin : LinkedDisposableWrapper<TValue>, IPinnedValue<TKey, TValue>
		{
			private readonly CacheEntry entry;

			public Pin(AsyncCache<TKey, TState, TValue> cache, CacheEntry entry, TValue value)
				: base(cache, value)
			{
				Contracts.Requires.That(entry != null);

				this.entry = entry;
			}

			/// <inheritdoc />
			public TKey Key => this.entry.Key;

			/// <inheritdoc />
			protected override void ManagedDisposal(TValue value) => this.entry.Unpin();
		}
	}
}
