using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Data.Pact.Stores
{
	public interface ITransaction<TEntity> : IDisposed
		where TEntity : class
	{
		IEnumerable<TEntity> All();

		IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

		void Add(TEntity entity);

		void AddAll(IEnumerable<TEntity> entities);

		void AddOrIgnore(TEntity entity);

		void AddOrIgnoreAll(IEnumerable<TEntity> entities);

		void AddOrUpdate(TEntity entity);

		void AddOrUpdateAll(IEnumerable<TEntity> entities);

		void Update(TEntity entity);

		void UpdateAll(IEnumerable<TEntity> entities);

		void Remove(TEntity entity);

		void RemoveAll(IEnumerable<TEntity> entities);

		void RemoveAll();
	}

	/// <summary>
	/// Provides contracts for <see cref="ITransaction{TEntity}"/>.
	/// </summary>
	public static partial class ITransactionContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void All<TEntity>(ITransaction<TEntity> instance)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Where<TEntity>(ITransaction<TEntity> instance, Expression<Func<TEntity, bool>> predicate)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(predicate != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Add<TEntity>(ITransaction<TEntity> instance, TEntity entity)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entity != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddAll<TEntity>(ITransaction<TEntity> instance, IEnumerable<TEntity> entities)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entities.AllAndSelfNotNull());
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddOrIgnore<TEntity>(ITransaction<TEntity> instance, TEntity entity)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entity != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddOrIgnoreAll<TEntity>(ITransaction<TEntity> instance, IEnumerable<TEntity> entities)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entities.AllAndSelfNotNull());
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddOrUpdate<TEntity>(ITransaction<TEntity> instance, TEntity entity)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entity != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddOrUpdateAll<TEntity>(ITransaction<TEntity> instance, IEnumerable<TEntity> entities)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entities.AllAndSelfNotNull());
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Update<TEntity>(ITransaction<TEntity> instance, TEntity entity)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entity != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void UpdateAll<TEntity>(ITransaction<TEntity> instance, IEnumerable<TEntity> entities)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entities.AllAndSelfNotNull());
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Remove<TEntity>(ITransaction<TEntity> instance, TEntity entity)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entity != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void RemoveAll<TEntity>(ITransaction<TEntity> instance, IEnumerable<TEntity> entities)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entities.AllAndSelfNotNull());
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void RemoveAll<TEntity>(ITransaction<TEntity> instance)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
		}
	}
}
