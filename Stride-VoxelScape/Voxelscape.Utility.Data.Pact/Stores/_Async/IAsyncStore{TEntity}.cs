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
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	public interface IAsyncStore<TEntity>
		where TEntity : class
	{
		Task<IEnumerable<TEntity>> AllAsync(CancellationToken cancellation = default(CancellationToken));

		Task<IEnumerable<TEntity>> WhereAsync(
			Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation = default(CancellationToken));

		Task AddAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken));

		Task AddAllAsync(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken));

		Task AddOrIgnoreAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken));

		Task AddOrIgnoreAllAsync(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken));

		Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken));

		Task AddOrUpdateAllAsync(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken));

		Task UpdateAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken));

		Task UpdateAllAsync(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken));

		Task RemoveAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken));

		Task RemoveAllAsync(
			IEnumerable<TEntity> entities, CancellationToken cancellation = default(CancellationToken));

		Task RemoveAllAsync(CancellationToken cancellation = default(CancellationToken));
	}
}
