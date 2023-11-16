using Voxelscape.Utility.Common.Core.Disposables;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Utility.Data.Core.Caching
{
	public class InstantExpiryCacheValue<TKey, TValue> : DisposableWrapper<TValue>, ICacheValue<TKey, TValue>
	{
		public InstantExpiryCacheValue(TKey key, TValue value)
			: base(value)
		{
			Contracts.Requires.That(key != null);

			this.Key = key;
		}

		/// <inheritdoc />
		public TKey Key { get; }

		/// <inheritdoc />
		public void AddExpiration(ExpiryToken<TKey> expiration)
		{
			ICacheValueContracts.AddExpiration(this);

			expiration.TryDisposeCacheValue();
		}
	}
}
