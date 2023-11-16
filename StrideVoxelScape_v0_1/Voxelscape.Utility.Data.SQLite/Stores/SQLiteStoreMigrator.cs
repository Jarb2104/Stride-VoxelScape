using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SQLite;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.SQLite.Stores
{
	public class SQLiteStoreMigrator : IAsyncStoreMigrator
	{
		private readonly string _databasePath;

		private readonly Type[] _entityTypes;

		public SQLiteStoreMigrator(IPersistenceConfig persistenceConfig, IEnumerable<Type> entityTypes)
			: this(persistenceConfig, entityTypes?.ToArray())
		{
		}

		public SQLiteStoreMigrator(IPersistenceConfig persistenceConfig, params Type[] entityTypes)
		{
			Contracts.Requires.That(persistenceConfig != null);
			Contracts.Requires.That(entityTypes.AllAndSelfNotNull());

			_databasePath = persistenceConfig.DatabasePath;
			_entityTypes = entityTypes;
		}

		/// <inheritdoc />
		public async Task MigrateAsync(CancellationToken cancellation = default)
		{
			cancellation.ThrowIfCancellationRequested();
			var connection = new SQLiteAsyncConnection(_databasePath);

			cancellation.ThrowIfCancellationRequested();
			await connection.CreateTablesAsync(CreateFlags.None, _entityTypes).DontMarshallContext();
		}
	}
}
