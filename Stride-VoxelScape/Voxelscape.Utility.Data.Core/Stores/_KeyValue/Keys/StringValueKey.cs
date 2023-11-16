namespace Voxelscape.Utility.Data.Core.Stores
{
	public class StringValueKey : AbstractValueKey<string>
	{
		public StringValueKey(string key)
			: base(key)
		{
		}

		/// <inheritdoc />
		public override string Serialize(string value) => value;

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out string result)
		{
			result = value;
			return true;
		}
	}
}
