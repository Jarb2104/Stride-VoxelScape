using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Utility.Data.Pact.Stores
{
	/// <summary>
	///
	/// </summary>
	public interface IKeyValueEntity : IKeyed<string>
	{
		new string Key { get; set; }

		string Value { get; set; }
	}
}
