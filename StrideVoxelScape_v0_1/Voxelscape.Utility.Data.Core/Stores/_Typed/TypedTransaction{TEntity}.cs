using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class TypedTransaction<TEntity> : ITransaction<TEntity>
		where TEntity : class
	{
		private readonly ITransaction transaction;

		public TypedTransaction(ITransaction transaction)
		{
			Contracts.Requires.That(transaction != null);

			this.transaction = transaction;
		}

		/// <inheritdoc />
		public bool IsDisposed => this.transaction.IsDisposed;

		/// <inheritdoc />
		public IEnumerable<TEntity> All()
		{
			ITransactionContracts.All(this);

			return this.transaction.All<TEntity>();
		}

		/// <inheritdoc />
		public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
		{
			ITransactionContracts.Where(this, predicate);

			return this.transaction.Where(predicate);
		}

		/// <inheritdoc />
		public void Add(TEntity entity)
		{
			ITransactionContracts.Add(this, entity);

			this.transaction.Add(entity);
		}

		/// <inheritdoc />
		public void AddAll(IEnumerable<TEntity> entities)
		{
			ITransactionContracts.AddAll(this, entities);

			this.transaction.AddAll(entities);
		}

		/// <inheritdoc />
		public void AddOrIgnore(TEntity entity)
		{
			ITransactionContracts.AddOrIgnore(this, entity);

			this.transaction.AddOrIgnore(entity);
		}

		/// <inheritdoc />
		public void AddOrIgnoreAll(IEnumerable<TEntity> entities)
		{
			ITransactionContracts.AddOrIgnoreAll(this, entities);

			this.transaction.AddOrIgnoreAll(entities);
		}

		/// <inheritdoc />
		public void AddOrUpdate(TEntity entity)
		{
			ITransactionContracts.AddOrUpdate(this, entity);

			this.transaction.AddOrUpdate(entity);
		}

		/// <inheritdoc />
		public void AddOrUpdateAll(IEnumerable<TEntity> entities)
		{
			ITransactionContracts.AddOrUpdateAll(this, entities);

			this.transaction.AddOrUpdateAll(entities);
		}

		/// <inheritdoc />
		public void Update(TEntity entity)
		{
			ITransactionContracts.Update(this, entity);

			this.transaction.Update(entity);
		}

		/// <inheritdoc />
		public void UpdateAll(IEnumerable<TEntity> entities)
		{
			ITransactionContracts.UpdateAll(this, entities);

			this.transaction.UpdateAll(entities);
		}

		/// <inheritdoc />
		public void Remove(TEntity entity)
		{
			ITransactionContracts.Remove(this, entity);

			this.transaction.Remove(entity);
		}

		/// <inheritdoc />
		public void RemoveAll(IEnumerable<TEntity> entities)
		{
			ITransactionContracts.RemoveAll(this, entities);

			this.transaction.RemoveAll(entities);
		}

		/// <inheritdoc />
		public void RemoveAll()
		{
			ITransactionContracts.RemoveAll(this);

			this.transaction.RemoveAll<TEntity>();
		}
	}
}
