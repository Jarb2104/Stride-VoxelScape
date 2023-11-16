namespace Voxelscape.Utility.Data.Core.Stores
{
	public class BoolValueKey : AbstractValueKey<bool>
	{
		public BoolValueKey(string key)
			: base(key)
		{
		}

		/// <inheritdoc />
		public override string Serialize(bool value) => value.ToString();

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out bool result) =>
			bool.TryParse(value, out result);
	}
}
