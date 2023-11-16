using SQLite;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.SQLite.Entities
{
	/// <summary>
	///
	/// </summary>
	[Table(nameof(KeyValueEntity))]
	public class KeyValueEntity : IKeyValueEntity
	{
		[PrimaryKey]
		public string Key { get; set; }

		public string Value { get; set; }
	}
}
