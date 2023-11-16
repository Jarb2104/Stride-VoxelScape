namespace Voxelscape.Utility.Data.Core.Stores
{
	public class CharValueKey : AbstractValueKey<char>
	{
		public CharValueKey(string key)
			: base(key)
		{
		}

		/// <inheritdoc />
		public override string Serialize(char value) => value.ToString();

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out char result) =>
			char.TryParse(value, out result);
	}
}
