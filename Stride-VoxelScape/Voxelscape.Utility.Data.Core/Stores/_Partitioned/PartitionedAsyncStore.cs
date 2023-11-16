using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class PartitionedAsyncStore : IAsyncStore
	{
		private readonly IAsyncStorePartitioner partitioner;

		public PartitionedAsyncStore(IAsyncStorePartitioner partitioner)
		{
			Contracts.Requires.That(partitioner != null);

			this.partitioner = partitioner;
		}

		#region IAsyncStore Members

		/// <inheritdoc />
		public async Task<IEnumerable<TEntity>> AllAsync<TEntity>(
			CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			var store = await this.GetStoreAsync<TEntity>(cancellation).DontMarshallContext();
			return await store.AllAsync(cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task<IEnumerable<TEntity>> WhereAsync<TEntity>(
			Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.WhereAsync(predicate);

			var store = await this.GetStoreAsync<TEntity>(cancellation).DontMarshallContext();
			return await store.WhereAsync(predicate, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task AddAsync<TEntity>(
			TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddAsync(entity);

			var store = await this.GetStoreAsync<TEntity>(cancellation).DontMarshallContext();
			await store.AddAsync(entity, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task AddAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddAllAsync(entities);

			var store = await this.GetStoreAsync<TEntity>(cancellation).DontMarshallContext();
			await store.AddAllAsync(entities, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task AddOrIgnoreAsync<TEntity>(
			TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddOrIgnoreAsync(entity);

			var store = await this.GetStoreAsync<TEntity>(cancellation).DontMarshallContext();
			await store.AddOrIgnoreAsync(entity, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task AddOrIgnoreAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddOrIgnoreAllAsync(entities);

			var store = await this.GetStoreAsync<TEntity>(cancellation).DontMarshallContext();
			await store.AddOrIgnoreAllAsync(entities, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task AddOrUpdateAsync<TEntity>(
			TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddOrUpdateAsync(entity);

			var store = await this.GetStoreAsync<TEntity>(cancellation).DontMarshallContext();
			await store.AddOrUpdateAsync(entity, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task AddOrUpdateAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.AddOrUpdateAllAsync(entities);

			var store = await this.GetStoreAsync<TEntity>(cancellation).DontMarshallContext();
			await store.AddOrUpdateAllAsync(entities, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task UpdateAsync<TEntity>(
			TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.UpdateAsync(entity);

			var store = await this.GetStoreAsync<TEntity>(cancellation).DontMarshallContext();
			await store.UpdateAsync(entity, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task UpdateAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.UpdateAllAsync(entities);

			var store = await this.GetStoreAsync<TEntity>(cancellation).DontMarshallContext();
			await store.UpdateAllAsync(entities, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task RemoveAsync<TEntity>(
			TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.RemoveAsync(entity);

			var store = await this.GetStoreAsync<TEntity>(cancellation).DontMarshallContext();
			await store.RemoveAsync(entity, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task RemoveAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreContracts.RemoveAllAsync(entities);

			var store = await this.GetStoreAsync<TEntity>(cancellation).DontMarshallContext();
			await store.RemoveAllAsync(entities, cancellation).DontMarshallContext();
		}

		/// <inheritdoc />
		public async Task RemoveAllAsync<TEntity>(CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			var store = await this.GetStoreAsync<TEntity>(cancellation).DontMarshallContext();
			await store.RemoveAllAsync<TEntity>(cancellation).DontMarshallContext();
		}

		#endregion

		private async Task<IAsyncStore<TEntity>> GetStoreAsync<TEntity>(CancellationToken cancellation)
			where TEntity : class
		{
			// check cancellation first in case partitioner doesn't actually check
			cancellation.ThrowIfCancellationRequested();
			var store = await this.partitioner.GetStoreAsync<TEntity>(cancellation).DontMarshallContext();

			// check cancellation again in case the returned store doesn't actually check cancellation
			// when its methods are invoked
			cancellation.ThrowIfCancellationRequested();
			return store;
		}
	}
}
