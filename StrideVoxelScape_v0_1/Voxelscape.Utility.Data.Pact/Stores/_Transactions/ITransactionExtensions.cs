using System.Diagnostics;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;
using Voxelscape.Utility.Data.Pact.Stores;

/// <summary>
/// Provides extension methods for <see cref="ITransaction"/> and <see cref="ITransaction{TEntity}"/>.
/// </summary>
public static class ITransactionExtensions
{
	#region ITransaction 'Get' methods

	public static TEntity Get<TKey, TEntity>(this ITransaction transaction, TKey key)
		where TEntity : class, IKeyed<TKey>
	{
		GetMethodContracts(transaction, key);

		return transaction.Where((TEntity entity) => entity.Key.Equals(key)).Single();
	}

	public static TEntity GetOrNull<TKey, TEntity>(this ITransaction transaction, TKey key)
		where TEntity : class, IKeyed<TKey>
	{
		GetMethodContracts(transaction, key);

		return transaction.Where((TEntity entity) => entity.Key.Equals(key)).SingleOrDefault();
	}

	public static bool TryGet<TKey, TEntity>(this ITransaction transaction, TKey key, out TEntity entity)
		where TEntity : class, IKeyed<TKey>
	{
		GetMethodContracts(transaction, key);

		return transaction.Where((TEntity checking) => checking.Key.Equals(key))
			.SingleOrNone().UnpackToTryMethodPattern(out entity);
	}

	#endregion

	#region ITransaction<TEntity> 'Get' methods

	public static TEntity Get<TKey, TEntity>(this ITransaction<TEntity> transaction, TKey key)
		where TEntity : class, IKeyed<TKey>
	{
		GetMethodContracts(transaction, key);

		return transaction.Where(entity => entity.Key.Equals(key)).Single();
	}

	public static TEntity GetOrNull<TKey, TEntity>(this ITransaction<TEntity> transaction, TKey key)
		where TEntity : class, IKeyed<TKey>
	{
		GetMethodContracts(transaction, key);

		return transaction.Where(entity => entity.Key.Equals(key)).SingleOrDefault();
	}

	public static bool TryGet<TKey, TEntity>(this ITransaction<TEntity> transaction, TKey key, out TEntity entity)
		where TEntity : class, IKeyed<TKey>
	{
		GetMethodContracts(transaction, key);

		return transaction.Where(checking => checking.Key.Equals(key))
			.SingleOrNone().UnpackToTryMethodPattern(out entity);
	}

	#endregion

	#region ITransaction params overloads of 'All' methods

	public static void AddAll<TEntity>(this ITransaction transaction, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(transaction, entities);

		transaction.AddAll(entities);
	}

	public static void AddOrIgnoreAll<TEntity>(this ITransaction transaction, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(transaction, entities);

		transaction.AddOrIgnoreAll(entities);
	}

	public static void AddOrUpdateAll<TEntity>(this ITransaction transaction, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(transaction, entities);

		transaction.AddOrUpdateAll(entities);
	}

	public static void UpdateAll<TEntity>(this ITransaction transaction, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(transaction, entities);

		transaction.UpdateAll(entities);
	}

	public static void RemoveAll<TEntity>(this ITransaction transaction, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(transaction, entities);

		transaction.RemoveAll(entities);
	}

	#endregion

	#region ITransaction<TEntity> params overloads of 'All' methods

	public static void AddAll<TEntity>(this ITransaction<TEntity> transaction, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(transaction, entities);

		transaction.AddAll(entities);
	}

	public static void AddOrIgnoreAll<TEntity>(this ITransaction<TEntity> transaction, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(transaction, entities);

		transaction.AddOrIgnoreAll(entities);
	}

	public static void AddOrUpdateAll<TEntity>(this ITransaction<TEntity> transaction, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(transaction, entities);

		transaction.AddOrUpdateAll(entities);
	}

	public static void UpdateAll<TEntity>(this ITransaction<TEntity> transaction, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(transaction, entities);

		transaction.UpdateAll(entities);
	}

	public static void RemoveAll<TEntity>(this ITransaction<TEntity> transaction, params TEntity[] entities)
		where TEntity : class
	{
		AllMethodContracts(transaction, entities);

		transaction.RemoveAll(entities);
	}

	#endregion

	#region Contracts

	[Conditional(Contracts.Requires.CompilationSymbol)]
	private static void GetMethodContracts<TKey>(this ITransaction transaction, TKey key)
	{
		Contracts.Requires.That(transaction != null);
		Contracts.Requires.That(!transaction.IsDisposed);
		Contracts.Requires.That(key != null);
	}

	[Conditional(Contracts.Requires.CompilationSymbol)]
	private static void GetMethodContracts<TKey, TEntity>(this ITransaction<TEntity> transaction, TKey key)
		where TEntity : class, IKeyed<TKey>
	{
		Contracts.Requires.That(transaction != null);
		Contracts.Requires.That(!transaction.IsDisposed);
		Contracts.Requires.That(key != null);
	}

	[Conditional(Contracts.Requires.CompilationSymbol)]
	private static void AllMethodContracts<TEntity>(this ITransaction transaction, TEntity[] entities)
	{
		Contracts.Requires.That(transaction != null);
		Contracts.Requires.That(!transaction.IsDisposed);
		Contracts.Requires.That(entities.AllAndSelfNotNull());
	}

	[Conditional(Contracts.Requires.CompilationSymbol)]
	private static void AllMethodContracts<TEntity>(this ITransaction<TEntity> transaction, TEntity[] entities)
		where TEntity : class
	{
		Contracts.Requires.That(transaction != null);
		Contracts.Requires.That(!transaction.IsDisposed);
		Contracts.Requires.That(entities.AllAndSelfNotNull());
	}

	#endregion
}
