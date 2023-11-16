using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SQLite;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.SQLite.Stores
{
    /// <summary>
    /// An implementation of <see cref="ITransactionalStore"/> backed by an SQLite database.
    /// </summary>
    public class LocklessSQLiteStore : ITransactionalStore
    {
        /// <summary>
        /// The connection to the SQLite database.
        /// </summary>
        private readonly SQLiteAsyncConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocklessSQLiteStore"/> class.
        /// </summary>
        /// <param name="persistenceConfig">The persistence configuration.</param>
        public LocklessSQLiteStore(IPersistenceConfig persistenceConfig)
        {
            Contracts.Requires.That(persistenceConfig != null);
            connection = new SQLiteAsyncConnection(persistenceConfig.DatabasePath);
        }

        #region IAsyncStore Members

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> AllAsync<TEntity>()
            where TEntity : new ()
        {
            return await connection.Table<TEntity>().ToListAsync().DontMarshallContext();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> WhereAsync<TEntity>(
            Expression<Func<TEntity, bool>> predicate)
            where TEntity : new ()
        {
            return await connection.Table<TEntity>()
                .Where(predicate)
                .ToListAsync()
                .DontMarshallContext();
        }

        /// <inheritdoc />
        public Task AddAsync<TEntity>(
            TEntity entity, CancellationToken cancellation = default)
            where TEntity : class
        {
            IAsyncStoreContracts.AddAsync(entity);

            cancellation.ThrowIfCancellationRequested();
            return connection.InsertAsync(entity);
        }

        /// <inheritdoc />
        public Task AddAllAsync<TEntity>(
            IEnumerable<TEntity> entities, CancellationToken cancellation = default)
            where TEntity : class
        {
            IAsyncStoreContracts.AddAllAsync(entities);

            cancellation.ThrowIfCancellationRequested();
            return connection.InsertAllAsync(entities);
        }

        /// <inheritdoc />
        public Task AddOrIgnoreAsync<TEntity>(
            TEntity entity, CancellationToken cancellation = default)
            where TEntity : class
        {
            IAsyncStoreContracts.AddOrIgnoreAsync(entity);

            // for some reason there is no overload that accepts a CancellationToken
            cancellation.ThrowIfCancellationRequested();
            return connection.InsertAsync(entity);
        }

        /// <inheritdoc />
        public Task AddOrIgnoreAllAsync<TEntity>(
            IEnumerable<TEntity> entities, CancellationToken cancellation = default)
            where TEntity : class
        {
            IAsyncStoreContracts.AddOrIgnoreAllAsync(entities);

            cancellation.ThrowIfCancellationRequested();
            return connection.InsertAllAsync(entities);
        }

        /// <inheritdoc />
        public Task AddOrUpdateAsync<TEntity>(
            TEntity entity, CancellationToken cancellation = default)
            where TEntity : class
        {
            IAsyncStoreContracts.AddOrUpdateAsync(entity);

            cancellation.ThrowIfCancellationRequested();
            return connection.InsertOrReplaceAsync(entity);
        }

        /// <inheritdoc />
        public Task AddOrUpdateAllAsync<TEntity>(
            IEnumerable<TEntity> entities, CancellationToken cancellation = default)
            where TEntity : class
        {
            IAsyncStoreContracts.AddOrUpdateAllAsync(entities);

            cancellation.ThrowIfCancellationRequested();
            return connection.InsertOrReplaceAsync(entities);
        }

        /// <inheritdoc />
        public Task UpdateAsync<TEntity>(
            TEntity entity, CancellationToken cancellation = default)
            where TEntity : class
        {
            IAsyncStoreContracts.UpdateAsync(entity);

            cancellation.ThrowIfCancellationRequested();
            return connection.UpdateAsync(entity);
        }

        /// <inheritdoc />
        public Task UpdateAllAsync<TEntity>(
            IEnumerable<TEntity> entities, CancellationToken cancellation = default)
            where TEntity : class
        {
            IAsyncStoreContracts.UpdateAllAsync(entities);

            cancellation.ThrowIfCancellationRequested();
            return connection.UpdateAllAsync(entities);
        }

        /// <inheritdoc />
        public Task RemoveAsync<TEntity>(
            TEntity entity, CancellationToken cancellation = default)
            where TEntity : class
        {
            IAsyncStoreContracts.RemoveAsync(entity);

            cancellation.ThrowIfCancellationRequested();
            return connection.DeleteAsync(entity);
        }

        /// <inheritdoc />
        public Task RemoveAllAsync<TEntity>(
            IEnumerable<TEntity> entities, CancellationToken cancellation = default)
            where TEntity : class
        {
            IAsyncStoreContracts.RemoveAllAsync(entities);

            cancellation.ThrowIfCancellationRequested();
            return RunInTransactionAsync(transaction => transaction.RemoveAll(entities), cancellation);
        }

        /// <inheritdoc />
        public Task RemoveAllAsync<TEntity>(CancellationToken cancellation = default)
            where TEntity : class
        {
            cancellation.ThrowIfCancellationRequested();
            return connection.DeleteAllAsync<TEntity>();
        }

        #endregion

        #region ITransactionalStore Members

        /// <inheritdoc />
        public Task RunInTransactionAsync(
            Action<ITransaction> action, CancellationToken cancellation = default)
        {
            ITransactionalStoreContracts.RunInTransactionAsync(action);

            cancellation.ThrowIfCancellationRequested();
            return connection.RunInTransactionAsync(
                (SQLiteConnection conn) =>
                {
                    using var transaction = new Transaction(conn);
                    action(transaction);
                });
        }

        Task<IEnumerable<TEntity>> IAsyncStore.AllAsync<TEntity>(CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TEntity>> IAsyncStore.WhereAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Classes

        private class Transaction : AbstractDisposable, ITransaction
        {
            /// <summary>
            /// The connection to the SQLite database.
            /// </summary>
            private readonly SQLiteConnection connection;

            public Transaction(SQLiteConnection connection)
            {
                Contracts.Requires.That(connection != null);

                // SQLiteAsyncConnection.RunInTransactionAsync(Action<SQLiteConnection> action)
                // handles managing the connection passed in so DO NOT start a transaction here
                this.connection = connection;
            }

            /// <inheritdoc />
            public void Add<TEntity>(TEntity entity)
                where TEntity : class
            {
                ITransactionContracts.Add(this, entity);

                connection.Insert(entity);
            }

            /// <inheritdoc />
            public void AddAll<TEntity>(IEnumerable<TEntity> entities)
                where TEntity : class
            {
                ITransactionContracts.AddAll(this, entities);

                connection.InsertAll(entities);
            }

            /// <inheritdoc />
            public void AddOrIgnore<TEntity>(TEntity entity)
                where TEntity : class
            {
                ITransactionContracts.AddOrIgnore(this, entity);

                connection.Insert(entity);
            }

            /// <inheritdoc />
            public void AddOrIgnoreAll<TEntity>(IEnumerable<TEntity> entities)
                where TEntity : class
            {
                ITransactionContracts.AddOrIgnoreAll(this, entities);

                connection.Insert(entities);
            }

            /// <inheritdoc />
            public void AddOrUpdate<TEntity>(TEntity entity)
                where TEntity : class
            {
                ITransactionContracts.AddOrUpdate(this, entity);

                connection.InsertOrReplace(entity);
            }

            /// <inheritdoc />
            public void AddOrUpdateAll<TEntity>(IEnumerable<TEntity> entities)
                where TEntity : class
            {
                ITransactionContracts.AddOrUpdateAll(this, entities);

                connection.InsertAll(entities);
            }

            /// <inheritdoc />
            public void Update<TEntity>(TEntity entity)
                where TEntity : class
            {
                ITransactionContracts.Update(this, entity);

                connection.Update(entity);
            }

            /// <inheritdoc />
            public void UpdateAll<TEntity>(IEnumerable<TEntity> entities)
                where TEntity : class
            {
                ITransactionContracts.UpdateAll(this, entities);

                connection.UpdateAll(entities);
            }

            /// <inheritdoc />
            public void Remove<TEntity>(TEntity entity)
                where TEntity : class
            {
                ITransactionContracts.Remove(this, entity);

                connection.Delete(entity);
            }

            /// <inheritdoc />
            public void RemoveAll<TEntity>(IEnumerable<TEntity> entities)
                where TEntity : class
            {
                ITransactionContracts.RemoveAll(this, entities);

                foreach (var entity in entities)
                {
                    connection.Delete(entity);
                }
            }

            /// <inheritdoc />
            public void RemoveAll<TEntity>()
                where TEntity : class
            {
                ITransactionContracts.RemoveAll<TEntity>(this);

                connection.DeleteAll<TEntity>();
            }

            /// <inheritdoc />
            protected override void ManagedDisposal()
            {
                // SQLiteAsyncConnection.RunInTransactionAsync(Action<SQLiteConnection> action)
                // handles managing the connection passed in so DO NOT commit or dispose it here
            }

            IEnumerable<TEntity> ITransaction.All<TEntity>()
            {
                throw new NotImplementedException();
            }

            IEnumerable<TEntity> ITransaction.Where<TEntity>(Expression<Func<TEntity, bool>> predicate)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
