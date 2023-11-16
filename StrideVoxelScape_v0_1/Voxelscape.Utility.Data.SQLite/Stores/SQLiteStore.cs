using Voxelscape.Utility.Data.Core.Stores;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.SQLite.Stores
{
	/// <summary>
	///
	/// </summary>
	public static class SQLiteStore
	{
		public static class Locked
		{
			public static class AnyEntities
			{
				public static class SingleFile
				{
					public static ITransactionalStore New(IPersistenceConfig config) => new LockedSQLiteStore(config);
				}

				public static class Partitioned
				{
					public static IAsyncStore New(IPersistenceConfigFactory configFactory) =>
						new PartitionedAsyncStore(new AsyncStorePartitioner(
							configFactory, new PartitionedSQLiteStoreFactory(addLocks: true)));
				}
			}

			public static class ForEntity<TEntity>
				where TEntity : class
			{
				public static class WithMigration
				{
					public static MigratedTransactionalStore<TEntity> New(IPersistenceConfig config) =>
						new MigratedTransactionalStore<TEntity>(
							new SQLiteStoreMigrator(config, typeof(TEntity)),
							new LockedSQLiteStore(config));
				}

				public static class NoMigration
				{
					public static ITransactionalStore<TEntity> New(IPersistenceConfig config) =>
						new TypedTransactionalStore<TEntity>(new LockedSQLiteStore(config));
				}
			}
		}

		public static class Lockless
		{
			public static class SingleFile
			{
				public static ITransactionalStore New(IPersistenceConfig config) => new LocklessSQLiteStore(config);
			}

			public static class Partitioned
			{
				public static IAsyncStore New(IPersistenceConfigFactory configFactory) =>
					new PartitionedAsyncStore(new AsyncStorePartitioner(
						configFactory, new PartitionedSQLiteStoreFactory(addLocks: false)));
			}

			public static class ForEntity<TEntity>
				where TEntity : class
			{
				public static class WithMigration
				{
					public static MigratedTransactionalStore<TEntity> New(IPersistenceConfig config) =>
						new MigratedTransactionalStore<TEntity>(
							new SQLiteStoreMigrator(config, typeof(TEntity)),
							new LocklessSQLiteStore(config));
				}

				public static class NoMigration
				{
					public static ITransactionalStore<TEntity> New(IPersistenceConfig config) =>
						new TypedTransactionalStore<TEntity>(new LocklessSQLiteStore(config));
				}
			}
		}
	}
}
