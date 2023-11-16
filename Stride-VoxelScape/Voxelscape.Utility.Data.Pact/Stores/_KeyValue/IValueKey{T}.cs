using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Utility.Data.Pact.Stores
{
	public interface IValueKey<T> : IKeyed<string>
	{
		bool TryDeserialize(string value, out T result);

		string Serialize(T value);
	}
}
