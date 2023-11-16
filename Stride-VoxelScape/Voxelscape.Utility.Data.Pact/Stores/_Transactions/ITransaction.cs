using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Data.Pact.Stores
{
	public interface ITransaction : IDisposed
	{
		IEnumerable<TEntity> All<TEntity>()
			where TEntity : class;

		IEnumerable<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> predicate)
			where TEntity : class;

		void Add<TEntity>(TEntity entity)
			where TEntity : class;

		void AddAll<TEntity>(IEnumerable<TEntity> entities)
			where TEntity : class;

		void AddOrIgnore<TEntity>(TEntity entity)
			where TEntity : class;

		void AddOrIgnoreAll<TEntity>(IEnumerable<TEntity> entities)
			where TEntity : class;

		void AddOrUpdate<TEntity>(TEntity entity)
			where TEntity : class;

		void AddOrUpdateAll<TEntity>(IEnumerable<TEntity> entities)
			where TEntity : class;

		void Update<TEntity>(TEntity entity)
			where TEntity : class;

		void UpdateAll<TEntity>(IEnumerable<TEntity> entities)
			where TEntity : class;

		void Remove<TEntity>(TEntity entity)
			where TEntity : class;

		void RemoveAll<TEntity>(IEnumerable<TEntity> entities)
			where TEntity : class;

		void RemoveAll<TEntity>()
			where TEntity : class;
	}

	/// <summary>
	/// Provides contracts for <see cref="ITransaction"/>.
	/// </summary>
	public static partial class ITransactionContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void All<TEntity>(ITransaction instance)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Where<TEntity>(ITransaction instance, Expression<Func<TEntity, bool>> predicate)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(predicate != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Add<TEntity>(ITransaction instance, TEntity entity)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entity != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddAll<TEntity>(ITransaction instance, IEnumerable<TEntity> entities)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entities.AllAndSelfNotNull());
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddOrIgnore<TEntity>(ITransaction instance, TEntity entity)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entity != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddOrIgnoreAll<TEntity>(ITransaction instance, IEnumerable<TEntity> entities)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entities.AllAndSelfNotNull());
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddOrUpdate<TEntity>(ITransaction instance, TEntity entity)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entity != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddOrUpdateAll<TEntity>(ITransaction instance, IEnumerable<TEntity> entities)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entities.AllAndSelfNotNull());
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Update<TEntity>(ITransaction instance, TEntity entity)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entity != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void UpdateAll<TEntity>(ITransaction instance, IEnumerable<TEntity> entities)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entities.AllAndSelfNotNull());
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Remove<TEntity>(ITransaction instance, TEntity entity)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entity != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void RemoveAll<TEntity>(ITransaction instance, IEnumerable<TEntity> entities)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(entities.AllAndSelfNotNull());
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void RemoveAll<TEntity>(ITransaction instance)
			where TEntity : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
		}
	}
}
