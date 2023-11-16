using System;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Utility.Data.Core.Caching
{
	public class CacheDisposalWrapper<TKey, TValue> :
		AbstractAsyncDisposedCompletable, IAsyncCache<TKey, TValue>
	{
		private readonly IAsyncCache<TKey, TValue> cache;

		private readonly IDisposable disposable;

		public CacheDisposalWrapper(IAsyncCache<TKey, TValue> cache, IDisposable disposable)
		{
			Contracts.Requires.That(cache != null);
			Contracts.Requires.That(disposable != null);

			this.cache = cache;
			this.disposable = disposable;
		}

		/// <inheritdoc />
		public Task<IPinnedValue<TKey, TValue>> GetPinAsync(TKey key) => this.cache.GetPinAsync(key);

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
