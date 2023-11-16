using System.Collections.Concurrent;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Utility.Data.Core.Caching
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <threadsafety static="true" instance="true" />
	public class QueueStash<TKey> : AbstractDisposable, IExpiryStash<TKey>
	{
		private readonly BlockingCollection<ExpiryToken<TKey>> tokens;

		public QueueStash(int boundedCapacity = Capacity.Unbounded)
		{
			Contracts.Requires.That(boundedCapacity > 0 || boundedCapacity == Capacity.Unbounded);

			this.tokens = boundedCapacity != Capacity.Unbounded ?
				new BlockingCollection<ExpiryToken<TKey>>(boundedCapacity) :
				new BlockingCollection<ExpiryToken<TKey>>();
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
			this.tokens.CompleteAdding();

			ExpiryToken<TKey> token;
			while (this.tokens.TryTake(out token))
			{
				token.TryDisposeCacheValue();
			}

			this.tokens.Dispose();
		}
	}
}
