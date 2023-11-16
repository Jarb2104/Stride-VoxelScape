using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Concurrency.Core.Collections;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Utility.Data.Core.Caching
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <threadsafety static="true" instance="true" />
	public class CompactingQueueStash<TKey> : AbstractDisposable, IExpiryStash<TKey>
	{
		private readonly CompactingBlockingQueue<ExpiryToken<TKey>> tokens;

		public CompactingQueueStash(int boundedCapacity)
		{
			Contracts.Requires.That(boundedCapacity > 0);

			this.tokens = new CompactingBlockingQueue<ExpiryToken<TKey>>(
				token => token.IsTokenExpired, boundedCapacity);
		}

		/// <inheritdoc />
		public void AddExpiration(ExpiryToken<TKey> expiration)
		{
			IExpiryStashContracts.AddExpiration(this);

			while (!this.tokens.TryAdd(expiration))
			{
				ExpiryToken<TKey> overflow;
				if (this.tokens.TryTake(out overflow))
				{
					overflow.TryDisposeCacheValue();
				}
			}
		}

		/// <inheritdoc />
		public bool TryExpireToken()
		{
			IExpiryStashContracts.TryExpireToken(this);

			ExpiryToken<TKey> token;
			while (this.tokens.TryTake(out token))
			{
				if (token.TryDisposeCacheValue())
				{
					return true;
				}
			}

			return false;
		}

		/// <inheritdoc />
		protected override void ManagedDisposal()
		{
			ExpiryToken<TKey> token;
			while (this.tokens.TryTake(out token))
			{
				token.TryDisposeCacheValue();
			}
		}
	}
}
