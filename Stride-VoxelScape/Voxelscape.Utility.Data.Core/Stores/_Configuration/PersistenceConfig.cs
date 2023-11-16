using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.Core.Stores
{
	/// <summary>
	/// Provides configuration information for the persistence library.
	/// </summary>
	public class PersistenceConfig : IPersistenceConfig
	{
		public PersistenceConfig(string databasePath)
		{
			Contracts.Requires.That(!databasePath.IsNullOrWhiteSpace());

			this.DatabasePath = databasePath;
		}

		/// <inheritdoc />
		public string DatabasePath { get; }
	}
}
