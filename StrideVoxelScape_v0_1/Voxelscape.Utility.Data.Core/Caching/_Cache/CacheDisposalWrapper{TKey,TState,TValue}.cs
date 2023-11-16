using System;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Utility.Data.Core.Caching
{
	public class CacheDisposalWrapper<TKey, TState, TValue> :
		AbstractAsyncDisposedCompletable, IAsyncCache<TKey, TState, TValue>
	{
		private readonly IAsyncCache<TKey, TState, TValue> cache;

		private readonly IDisposable disposable;

		public CacheDisposalWrapper(IAsyncCache<TKey, TState, TValue> cache, IDisposable disposable)
		{
			Contracts.Requires.That(cache != null);
			Contracts.Requires.That(disposable != null);

			this.cache = cache;
			this.disposable = disposable;
		}

		/// <inheritdoc />
		public Task<IPinnedValue<TKey, TValue>> GetPinAsync(TKey key, TState state) =>
			this.cache.GetPinAsync(key, state);

		/// <inheritdoc />
		protected override async Task CompleteAsync()
		{
			try
			{
				await this.cache.CompleteAndAwaitAsync().DontMarshallContext();
			}
			finally
			{
				this.disposable.Dispose();
			}
		}
	}
}
