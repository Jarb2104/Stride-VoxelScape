using System;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Utility.Data.Core.Caching
{
	/// <summary>
	///
	/// </summary>
	public static class CacheValue
	{
		public static ICacheValue<TKey, TValue> CreateInstantExpiry<TKey, TValue>(TKey key, TValue value) =>
			new InstantExpiryCacheValue<TKey, TValue>(key, value);

		public static ICacheValue<TKey, TValue> CreateStashed<TKey, TValue>(
			TKey key, TValue value, IExpiryStash<TKey> stash) =>
			new StashedCacheValue<TKey, TValue>(key, value, stash);

		public static ICacheValue<TKey, TValue> CreateStashed<TKey, TValue>(
			TKey key, TValue value, IDisposable disposable, IExpiryStash<TKey> stash) =>
			new StashedCompositeCacheValue<TKey, TValue>(key, value, disposable, stash);

		public static ICacheValue<TKey, TValue> CreateStashed<TKey, TValue>(
			TKey key, IDisposableValue<TValue> value, IExpiryStash<TKey> stash) =>
			new StashedDisposableCacheValue<TKey, TValue>(key, value, stash);
	}
}
