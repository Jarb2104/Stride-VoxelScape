using Voxelscape.Utility.Data.Core.Stores;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.SQLite.Stores
{
	/// <summary>
	/// An implementation of <see cref="ITransactionalStore"/> backed by an SQLite database.
	/// </summary>
	public class LockedSQLiteStore : LockedTransactionalStore
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LockedSQLiteStore"/> class.
		/// </summary>
		/// <param name="persistenceConfig">The persistence configuration.</param>
		public LockedSQLiteStore(IPersistenceConfig persistenceConfig)
			: base(new LocklessSQLiteStore(persistenceConfig))
		{
		}
	}
}
