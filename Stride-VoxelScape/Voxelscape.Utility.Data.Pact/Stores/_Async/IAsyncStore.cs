using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Voxelscape.Utility.Data.Pact.Stores
{
	/// <summary>
	/// Defines an asynchronous store for persisting entities.
	/// </summary>
	public interface IAsyncStore
	{
		Task<IEnumerable<TEntity>> AllAsync<TEntity>(CancellationToken cancellation = default(CancellationToken))
			where TEntity : class;

		Task<IEnumerable<TEntity>> WhereAsync<TEntity>(
			Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class;

		Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class;

		Task AddAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class;

		Task AddOrIgnoreAsync<TEntity>(TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class;

		Task AddOrIgnoreAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class;

		Task AddOrUpdateAsync<TEntity>(TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class;

		Task AddOrUpdateAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class;

		Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class;

		Task UpdateAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class;

		Task RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class;

		Task RemoveAllAsync<TEntity>(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class;

		Task RemoveAllAsync<TEntity>(CancellationToken cancellation = default(CancellationToken))
			where TEntity : class;
	}
}
