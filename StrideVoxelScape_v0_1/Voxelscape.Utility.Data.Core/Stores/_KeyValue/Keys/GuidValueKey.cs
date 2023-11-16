using System;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class GuidValueKey : AbstractValueKey<Guid>
	{
		public GuidValueKey(string key)
			: base(key)
		{
		}

		/// <inheritdoc />
		public override string Serialize(Guid value) => value.ToString();

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out Guid result) =>
			Guid.TryParse(value, out result);
	}
}
