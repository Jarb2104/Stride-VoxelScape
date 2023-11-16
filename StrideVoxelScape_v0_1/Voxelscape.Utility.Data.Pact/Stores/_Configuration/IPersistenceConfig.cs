namespace Voxelscape.Utility.Data.Pact.Stores
{
	/// <summary>
	/// Provides configuration information for the persistence library.
	/// </summary>
	public interface IPersistenceConfig
	{
		/// <summary>
		/// Gets the path to the database as a string.
		/// </summary>
		/// <value>
		/// The database path string.
		/// </value>
		string DatabasePath { get; }
	}
}
