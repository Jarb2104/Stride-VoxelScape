using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;
using Voxelscape.Utility.Data.Pact.Stores;

/// <summary>
/// Provides extension methods for <see cref="IAsyncStore"/> and <see cref="IAsyncStore{TEntity}"/>.
/// </summary>
public static class IAsyncStoreExtensions
{
	#region IAsyncStore 'Get' methods

	public static Task<TEntity> GetAsync<TKey, TEntity>(
		this IAsyncStore store, TKey key, CancellationToken cancellation = default(CancellationToken))
		where TEntity : class, IKeyed<TKey>
	{
		GetMethodContracts(store, key);

		return store.WhereAsync((TEntity entity) => entity.Key.Equals(key)).SingleAsync();
	}

	public static Task<TEntity> GetOrNullAsync<TKey, TEntity>(
		this IAsyncStore store, TKey key, CancellationToken cancellation = default(CancellationToken))
		where TEntity : class, IKeyed<TKey>
	{
		GetMethodContracts(store, key);

		return store.WhereAsync((TEntity entity) => entity.Key.Equals(key)).SingleOrDefaultAsync();
	}

	public static Task<TryValue<TEntity>> TryGetAsync<TKey, TEntity>(
		this IAsyncStore store, TKey key, CancellationToken cancellation = default(CancellationToken))
		where TEntity : class, IKeyed<TKey>
	{
		GetMethodContracts(store, key);

		return store.WhereAsync((TEntity entity) => entity.Key.Equals(key)).SingleOrNoneAsync();
	}

	#endregion

	#region IAsyncStore<TEntity> 'Get' methods

	public static Task<TEntity> GetAsync<TKey, TEntity>(
		this IAsyncStore<TEntity> store, TKey key, CancellationToken cancellation = default(CancellationToken))
		where TEntity : class, IKeyed<TKey>
	{
		GetMethodContracts(store, key);

		return store.WhereAsync(entity => entity.Key.Equals(key)).SingleAsync();
	}

	public static Task<TEntity> GetOrNullAsync<TKey, TEntity>(
		this IAsyncStore<TEntity> store, TKey key, CancellationToken cancellation = default(CancellationToken))
		where TEntity : class, IKeyed<TKey>
	{
		GetMethodContracts(store, key);

		return store.WhereAsync(entity => entity.Key.Equals(key)).SingleOrDefaultAsync();
	}

	public static Task<TryValue<TEntity>> TryGetAsync<TKey, TEntity>(
		this IAsyncStore<TEntity> store, TKey key, CancellationToken cancellation = default(CancellationToken))
		where TEntity : class, IKeyed<TKey>
	{
		GetMethodContracts(store, key);

		return store.WhereAsync(entity => entity.Key.Equals(key)).SingleOrNoneAsync();
	}

	#endregion

	#region IAsyncStore params overloads of 'All' methods

	public static Task AddAllAsync<TEntity>(this IAsyncStore store, params TEntity[] entities)
		where TEntity : class => store.AddAllAsync(CancellationToken.None, entities);

	public static Task AddAllAsync<TEntity>(
		this IAsyncStore store, CancellationToken cancellation, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(store, entities);

		return store.AddAllAsync(entities, cancellation);
	}

	public static Task AddOrIgnoreAllAsync<TEntity>(this IAsyncStore store, params TEntity[] entities)
		where TEntity : class => store.AddOrIgnoreAllAsync(CancellationToken.None, entities);

	public static Task AddOrIgnoreAllAsync<TEntity>(
		this IAsyncStore store, CancellationToken cancellation, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(store, entities);

		return store.AddOrIgnoreAllAsync(entities, cancellation);
	}

	public static Task AddOrUpdateAllAsync<TEntity>(this IAsyncStore store, params TEntity[] entities)
		where TEntity : class => store.AddOrUpdateAllAsync(CancellationToken.None, entities);

	public static Task AddOrUpdateAllAsync<TEntity>(
		this IAsyncStore store, CancellationToken cancellation, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(store, entities);

		return store.AddOrUpdateAllAsync(entities, cancellation);
	}

	public static Task UpdateAllAsync<TEntity>(this IAsyncStore store, params TEntity[] entities)
		where TEntity : class => store.UpdateAllAsync(CancellationToken.None, entities);

	public static Task UpdateAllAsync<TEntity>(
		this IAsyncStore store, CancellationToken cancellation, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(store, entities);

		return store.UpdateAllAsync(entities, cancellation);
	}

	public static Task RemoveAllAsync<TEntity>(this IAsyncStore store, params TEntity[] entities)
		where TEntity : class => store.RemoveAllAsync(CancellationToken.None, entities);

	public static Task RemoveAllAsync<TEntity>(
		this IAsyncStore store, CancellationToken cancellation, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(store, entities);

		return store.RemoveAllAsync(entities, cancellation);
	}

	#endregion

	#region IAsyncStore<TEntity> params overloads of 'All' methods

	public static Task AddAllAsync<TEntity>(
		this IAsyncStore<TEntity> store, params TEntity[] entities)
		where TEntity : class => store.AddAllAsync(CancellationToken.None, entities);

	public static Task AddAllAsync<TEntity>(
		this IAsyncStore<TEntity> store, CancellationToken cancellation, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(store, entities);

		return store.AddAllAsync(entities, cancellation);
	}

	public static Task AddOrIgnoreAllAsync<TEntity>(
		this IAsyncStore<TEntity> store, params TEntity[] entities)
		where TEntity : class => store.AddOrIgnoreAllAsync(CancellationToken.None, entities);

	public static Task AddOrIgnoreAllAsync<TEntity>(
		this IAsyncStore<TEntity> store, CancellationToken cancellation, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(store, entities);

		return store.AddOrIgnoreAllAsync(entities, cancellation);
	}

	public static Task AddOrUpdateAllAsync<TEntity>(
		this IAsyncStore<TEntity> store, params TEntity[] entities)
		where TEntity : class => store.AddOrUpdateAllAsync(CancellationToken.None, entities);

	public static Task AddOrUpdateAllAsync<TEntity>(
		this IAsyncStore<TEntity> store, CancellationToken cancellation, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(store, entities);

		return store.AddOrUpdateAllAsync(entities, cancellation);
	}

	public static Task UpdateAllAsync<TEntity>(
		this IAsyncStore<TEntity> store, params TEntity[] entities)
		where TEntity : class => store.UpdateAllAsync(CancellationToken.None, entities);

	public static Task UpdateAllAsync<TEntity>(
		this IAsyncStore<TEntity> store, CancellationToken cancellation, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(store, entities);

		return store.UpdateAllAsync(entities, cancellation);
	}

	public static Task RemoveAllAsync<TEntity>(
		this IAsyncStore<TEntity> store, params TEntity[] entities)
		where TEntity : class => store.RemoveAllAsync(CancellationToken.None, entities);

	public static Task RemoveAllAsync<TEntity>(
		this IAsyncStore<TEntity> store, CancellationToken cancellation, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(store, entities);

		return store.RemoveAllAsync(entities, cancellation);
	}

	#endregion

	#region Contracts

	[Conditional(Contracts.Requires.CompilationSymbol)]
	private static void GetMethodContracts<TKey>(this IAsyncStore store, TKey key)
	{
		Contracts.Requires.That(store != null);
		Contracts.Requires.That(key != null);
	}

	[Conditional(Contracts.Requires.CompilationSymbol)]
	private static void GetMethodContracts<TKey, TEntity>(this IAsyncStore<TEntity> store, TKey key)
		where TEntity : class, IKeyed<TKey>
	{
		Contracts.Requires.That(store != null);
		Contracts.Requires.That(key != null);
	}

	[Conditional(Contracts.Requires.CompilationSymbol)]
	private static void AllMethodContracts<TEntity>(this IAsyncStore store, TEntity[] entities)
		where TEntity : class
	{
		Contracts.Requires.That(store != null);
		Contracts.Requires.That(entities.AllAndSelfNotNull());
	}

	[Conditional(Contracts.Requires.CompilationSymbol)]
	private static void AllMethodContracts<TEntity>(this IAsyncStore<TEntity> store, TEntity[] entities)
		where TEntity : class
	{
		Contracts.Requires.That(store != null);
		Contracts.Requires.That(entities.AllAndSelfNotNull());
	}

	#endregion
}
