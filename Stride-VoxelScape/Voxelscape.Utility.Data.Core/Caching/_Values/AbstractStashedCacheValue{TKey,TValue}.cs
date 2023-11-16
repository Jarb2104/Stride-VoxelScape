using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Utility.Data.Core.Caching
{
	public abstract class AbstractStashedCacheValue<TKey, TValue> : AbstractDisposable, ICacheValue<TKey, TValue>
	{
		private readonly IExpiryStash<TKey> stash;

		public AbstractStashedCacheValue(TKey key, IExpiryStash<TKey> stash)
		{
			Contracts.Requires.That(key != null);
			Contracts.Requires.That(stash != null);
			Contracts.Requires.That(!stash.IsDisposed);

			this.Key = key;
			this.stash = stash;
		}

		/// <inheritdoc />
		public TKey Key { get; }

		/// <inheritdoc />
		public TValue Value
		{
			get
			{
				ITemporaryValueContracts.Value(this);

				return this.GetValue;
			}
		}

		protected abstract TValue GetValue { get; }

		/// <inheritdoc />
		public void AddExpiration(ExpiryToken<TKey> expiration)
		{
			ICacheValueContracts.AddExpiration(this);

			this.stash.AddExpiration(expiration);
		}
	}
}
