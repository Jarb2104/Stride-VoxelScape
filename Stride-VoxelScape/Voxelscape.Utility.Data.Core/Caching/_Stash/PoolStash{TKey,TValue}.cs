using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Concurrency.Core.Synchronization;
using Voxelscape.Utility.Data.Pact.Caching;
using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Utility.Data.Core.Caching
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <threadsafety static="true" instance="true" />
	public class PoolStash<TKey, TValue> : AbstractDisposable, IExpiryStashPool<TKey, TValue>
	{
		private readonly AtomicInt awaitingTokensCount = new AtomicInt(0);

		private readonly IPool<TValue> pool;

		private readonly IExpiryStash<TKey> stash;

		public PoolStash(IPool<TValue> pool, IExpiryStash<TKey> stash)
		{
			Contracts.Requires.That(pool != null);
			Contracts.Requires.That(stash != null);

			this.pool = pool;
			this.stash = stash;
		}

		#region IPool<TValue> Members

		/// <inheritdoc />
		public int AvailableCount => this.pool.AvailableCount;

		/// <inheritdoc />
		public int BoundedCapacity => this.pool.BoundedCapacity;

		/// <inheritdoc />
		public TValue Take()
		{
			IPoolContracts.Take(this);

			TValue value;
			if (this.TryTake(out value))
			{
				return value;
			}
			else
			{
				this.awaitingTokensCount.Increment();
				try
				{
					return this.pool.Take();
				}
				finally
				{
					this.awaitingTokensCount.Decrement();
				}
			}
		}

		/// <inheritdoc />
		public async Task<TValue> TakeAsync()
		{
			IPoolContracts.TakeAsync(this);

			TValue value;
			if (this.TryTake(out value))
			{
				return value;
			}
			else
			{
				this.awaitingTokensCount.Increment();
				try
				{
					return await this.pool.TakeAsync().DontMarshallContext();
				}
				finally
				{
					this.awaitingTokensCount.Decrement();
				}
			}
		}

		/// <inheritdoc />
		public bool TryTake(out TValue value)
		{
			IPoolContracts.TryTake(this);

			while (true)
			{
				if (this.pool.TryTake(out value))
				{
					return true;
				}
				else
				{
					// unable to TryTake from the pool so try disposing a token
					// to release a value back to the pool
					if (!this.stash.TryExpireToken())
					{
						// unable to release any more values back to the pool
						// so TryTake can't succeed
						return false;
					}
				}
			}
		}

		/// <inheritdoc />
		public void Give(TValue value)
		{
			IPoolContracts.Give(this);

			this.pool.Give(value);
		}

		#endregion

		#region IExpiryStash<TKey> Members

		/// <inheritdoc />
		public void AddExpiration(ExpiryToken<TKey> expiration)
		{
			IExpiryStashContracts.AddExpiration(this);

			if (this.awaitingTokensCount.Read() <= 0)
			{
				// not awaiting a token so store it for disposal later
				this.stash.AddExpiration(expiration);
			}
			else
			{
				// pool is empty and Take/TakeAsync is awaiting a value so dispose token immediately
				expiration.TryDisposeCacheValue();
			}
		}

		/// <inheritdoc />
		public bool TryExpireToken()
		{
			IExpiryStashContracts.TryExpireToken(this);

			return this.stash.TryExpireToken();
		}

		#endregion

		/// <inheritdoc />
		protected override void ManagedDisposal()
		{
			// dispose expiry stash first because it may try to give values back to the pool
			this.stash.Dispose();
			this.pool.Dispose();
		}
	}
}
