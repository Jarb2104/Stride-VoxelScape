using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.SQLite.Stores
{
	/// <summary>
	///
	/// </summary>
	public class PartitionedSQLiteStoreFactory : IAsyncStoreFactory
	{
		private readonly bool addLocks;

		public PartitionedSQLiteStoreFactory(bool addLocks = true)
		{
			this.addLocks = addLocks;
		}

		/// <inheritdoc />
		public async Task<IAsyncStore<TEntity>> CreateStoreAsync<TEntity>(
			IPersistenceConfig config, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			IAsyncStoreFactoryContracts.CreateStoreAsync(config);

			cancellation.ThrowIfCancellationRequested();
			var migrator = new SQLiteStoreMigrator(config, typeof(TEntity));
			await migrator.MigrateAsync(cancellation).DontMarshallContext();

			cancellation.ThrowIfCancellationRequested();
			return this.addLocks ?
				SQLiteStore.Locked.ForEntity<TEntity>.NoMigration.New(config) :
				SQLiteStore.Lockless.ForEntity<TEntity>.NoMigration.New(config);
		}
	}
}
