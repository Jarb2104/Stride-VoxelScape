using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class KeyValueStore<TEntity> : IKeyValueStore
		where TEntity : class, IKeyValueEntity, new()
	{
		private readonly IAsyncStore<TEntity> store;

		public KeyValueStore(IAsyncStore<TEntity> store)
		{
			Contracts.Requires.That(store != null);

			this.store = store;
		}

		/// <inheritdoc />
		public async Task<TryValue<T>> TryGetAsync<T>(
			IValueKey<T> key, CancellationToken cancellation = default(CancellationToken))
		{
			IKeyValueStoreContracts.TryGetAsync(key);

			cancellation.ThrowIfCancellationRequested();
			var result = await this.store.TryGetAsync(key.Key, cancellation).DontMarshallContext();
			return result.HasValue ? key.TryDeserialize(result.Value.Value) : TryValue.None<T>();
		}

		/// <inheritdoc />
		public Task AddAsync<T>(
			IValueKey<T> key, T value, CancellationToken cancellation = default(CancellationToken))
		{
			IKeyValueStoreContracts.AddAsync(key);

			cancellation.ThrowIfCancellationRequested();

			var entity = new TEntity()
			{
				Key = key.Key,
				Value = key.Serialize(value),
			};

			return this.store.AddAsync(entity, cancellation);
		}

		/// <inheritdoc />
		public Task AddOrIgnoreAsync<T>(
			IValueKey<T> key, T value, CancellationToken cancellation = default(CancellationToken))
		{
			IKeyValueStoreContracts.AddOrIgnoreAsync(key);

			cancellation.ThrowIfCancellationRequested();

			var entity = new TEntity()
			{
				Key = key.Key,
				Value = key.Serialize(value),
			};

			return this.store.AddOrIgnoreAsync(entity, cancellation);
		}

		/// <inheritdoc />
		public Task AddOrUpdateAsync<T>(
			IValueKey<T> key, T value, CancellationToken cancellation = default(CancellationToken))
		{
			IKeyValueStoreContracts.AddOrUpdateAsync(key);

			cancellation.ThrowIfCancellationRequested();

			var entity = new TEntity()
			{
				Key = key.Key,
				Value = key.Serialize(value),
			};

			return this.store.AddOrUpdateAsync(entity, cancellation);
		}

		/// <inheritdoc />
		public Task UpdateAsync<T>(
			IValueKey<T> key, T value, CancellationToken cancellation = default(CancellationToken))
		{
			IKeyValueStoreContracts.UpdateAsync(key);

			cancellation.ThrowIfCancellationRequested();

			var entity = new TEntity()
			{
				Key = key.Key,
				Value = key.Serialize(value),
			};

			return this.store.UpdateAsync(entity, cancellation);
		}

		/// <inheritdoc />
		public Task RemoveAsync<T>(IValueKey<T> key, CancellationToken cancellation = default(CancellationToken))
		{
			IKeyValueStoreContracts.RemoveAsync(key);

			cancellation.ThrowIfCancellationRequested();

			var entity = new TEntity()
			{
				Key = key.Key,
			};

			return this.store.RemoveAsync(entity);
		}
	}
}
