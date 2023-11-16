using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class MigratedAsyncStore<TEntity> : IAsyncStore<TEntity>, IAsyncInitializable
		where TEntity : class
	{
		private readonly IAsyncStore store;

		public MigratedAsyncStore(IAsyncStoreMigrator migrator, IAsyncStore store)
		{
			Contracts.Requires.That(migrator != null);
			Contracts.Requires.That(store != null);

			this.store = store;
			this.Initialization = migrator.MigrateAsync();
		}

		/// <inheritdoc />
		public Task Initialization { get; }

		/// <inheritdoc />
		public async Task<IEnumerable<TEntity>> AllAsync(CancellationToken cancellation = default(CancellationToken))
		{
			cancellation.ThrowIfCancellationRequested();
			await this.Initialization.DontMarshallContext();
			return await this.store.AllAsync<TEntity>(cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task<IEnumerable<TEntity>> WhereAsync(
			Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.WhereAsync(predicate);

			cancellation.ThrowIfCancellationRequested();
			await this.Initialization.DontMarshallContext();
			return await this.store.WhereAsync(predicate, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task AddAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.AddAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			await this.Initialization.DontMarshallContext();
			await this.store.AddAsync(entity, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task AddAllAsync(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.AddAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			await this.Initialization.DontMarshallContext();
			await this.store.AddAllAsync(entities, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task AddOrIgnoreAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.AddOrIgnoreAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			await this.Initialization.DontMarshallContext();
			await this.store.AddOrIgnoreAsync(entity, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task AddOrIgnoreAllAsync(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.AddOrIgnoreAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			await this.Initialization.DontMarshallContext();
			await this.store.AddOrIgnoreAllAsync(entities, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.AddOrUpdateAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			await this.Initialization.DontMarshallContext();
			await this.store.AddOrUpdateAsync(entity, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task AddOrUpdateAllAsync(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.AddOrUpdateAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			await this.Initialization.DontMarshallContext();
			await this.store.AddOrUpdateAllAsync(entities, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task UpdateAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.UpdateAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			await this.Initialization.DontMarshallContext();
			await this.store.UpdateAsync(entity, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task UpdateAllAsync(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.UpdateAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			await this.Initialization.DontMarshallContext();
			await this.store.UpdateAllAsync(entities, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task RemoveAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.RemoveAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			await this.Initialization.DontMarshallContext();
			await this.store.RemoveAsync(entity, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task RemoveAllAsync(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.RemoveAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			await this.Initialization.DontMarshallContext();
			await this.store.RemoveAllAsync(entities, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task RemoveAllAsync(CancellationToken cancellation = default(CancellationToken))
		{
			cancellation.ThrowIfCancellationRequested();
			await this.Initialization.DontMarshallContext();
			await this.store.RemoveAllAsync<TEntity>(cancellation).DontMarshallContext();
		}
	}
}
