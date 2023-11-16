using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class LockedAsyncStore<TEntity> : IAsyncStore<TEntity>
		where TEntity : class
	{
		private readonly IAsyncStore<TEntity> store;

		public LockedAsyncStore(IAsyncStore<TEntity> store)
		{
			Contracts.Requires.That(store != null);

			this.store = store;
		}

		protected AsyncReaderWriterLock Lock { get; } = new AsyncReaderWriterLock();

		/// <inheritdoc />
		public async Task<IEnumerable<TEntity>> AllAsync(CancellationToken cancellation = default(CancellationToken))
		{
			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.ReaderLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				return await this.store.AllAsync(cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task<IEnumerable<TEntity>> WhereAsync(
			Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.WhereAsync(predicate);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.ReaderLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				return await this.store.WhereAsync(predicate, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task AddAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.AddAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.AddAsync(entity, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task AddAllAsync(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.AddAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.AddAllAsync(entities, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task AddOrIgnoreAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.AddOrIgnoreAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.AddOrIgnoreAsync(entity, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task AddOrIgnoreAllAsync(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.AddOrIgnoreAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.AddOrIgnoreAllAsync(entities, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.AddOrUpdateAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.AddOrUpdateAsync(entity, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task AddOrUpdateAllAsync(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.AddOrUpdateAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.AddOrUpdateAllAsync(entities, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task UpdateAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.UpdateAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.UpdateAsync(entity, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task UpdateAllAsync(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.UpdateAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.UpdateAllAsync(entities, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task RemoveAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.RemoveAsync(entity);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.RemoveAsync(entity, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task RemoveAllAsync(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
		{
			IAsyncStoreContracts.RemoveAllAsync(entities);

			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.RemoveAllAsync(entities, cancellation).DontMarshallContext();
			}
		}

		/// <inheritdoc />
		public async Task RemoveAllAsync(CancellationToken cancellation = default(CancellationToken))
		{
			cancellation.ThrowIfCancellationRequested();
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.RemoveAllAsync(cancellation).DontMarshallContext();
			}
		}
	}
}
