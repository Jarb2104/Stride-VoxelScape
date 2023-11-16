using System.Collections.Generic;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Core.Factories;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Utility.Data.Core.Caching
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <threadsafety static="true" instance="true" />
	public class AsyncCache<TKey, TValue> : IAsyncCache<TKey, TValue>
	{
		private readonly IAsyncCache<TKey, VoidStruct, TValue> cache;

		public AsyncCache(IAsyncFactory<TKey, ICacheValue<TKey, TValue>> factory)
			: this(factory, EqualityComparer<TKey>.Default)
		{
		}

		public AsyncCache(
			IAsyncFactory<TKey, ICacheValue<TKey, TValue>> factory,
			IEqualityComparer<TKey> comparer)
		{
			Contracts.Requires.That(factory != null);
			Contracts.Requires.That(comparer != null);

			this.cache = new AsyncCache<TKey, VoidStruct, TValue>(
				Factory.FromAsync((TKey key, VoidStruct unused) => factory.CreateAsync(key)),
				comparer);
		}

		/// <inheritdoc />
		public Task Completion => this.cache.Completion;

		/// <inheritdoc />
		public bool IsDisposed => this.cache.IsDisposed;

		/// <inheritdoc />
		public Task<IPinnedValue<TKey, TValue>> GetPinAsync(TKey key)
		{
			IAsyncCacheContracts.GetPinAsync(this, key);

			return this.cache.GetPinAsync(key, default(VoidStruct));
		}

		/// <inheritdoc />
		public void Complete() => this.cache.Complete();
	}
}
