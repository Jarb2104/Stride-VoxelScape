using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Data.Pact.Stores
{
	/// <summary>
	///
	/// </summary>
	public static class IAsyncStoreContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void WhereAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
			where TEntity : class
		{
			Contracts.Requires.That(predicate != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddAsync<TEntity>(TEntity entity)
			where TEntity : class
		{
			Contracts.Requires.That(entity != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddAllAsync<TEntity>(IEnumerable<TEntity> entities)
			where TEntity : class
		{
			Contracts.Requires.That(entities.AllAndSelfNotNull());
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddOrIgnoreAsync<TEntity>(TEntity entity)
			where TEntity : class
		{
			Contracts.Requires.That(entity != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddOrIgnoreAllAsync<TEntity>(IEnumerable<TEntity> entities)
			where TEntity : class
		{
			Contracts.Requires.That(entities.AllAndSelfNotNull());
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddOrUpdateAsync<TEntity>(TEntity entity)
			where TEntity : class
		{
			Contracts.Requires.That(entity != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddOrUpdateAllAsync<TEntity>(IEnumerable<TEntity> entities)
			where TEntity : class
		{
			Contracts.Requires.That(entities.AllAndSelfNotNull());
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void UpdateAsync<TEntity>(TEntity entity)
			where TEntity : class
		{
			Contracts.Requires.That(entity != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void UpdateAllAsync<TEntity>(IEnumerable<TEntity> entities)
			where TEntity : class
		{
			Contracts.Requires.That(entities.AllAndSelfNotNull());
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void RemoveAsync<TEntity>(TEntity entity)
			where TEntity : class
		{
			Contracts.Requires.That(entity != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void RemoveAllAsync<TEntity>(IEnumerable<TEntity> entities)
			where TEntity : class
		{
			Contracts.Requires.That(entities.AllAndSelfNotNull());
		}
	}
}
