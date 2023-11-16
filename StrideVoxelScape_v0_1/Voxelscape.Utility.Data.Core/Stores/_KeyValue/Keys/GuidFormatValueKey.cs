using System;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class GuidFormatValueKey : AbstractValueKey<Guid>
	{
		private readonly string format;

		public GuidFormatValueKey(string key, string format)
			: base(key)
		{
			this.format = format;
		}

		/// <inheritdoc />
		public override string Serialize(Guid value) => value.ToString(this.format);

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out Guid result) =>
			Guid.TryParseExact(value, this.format, out result);
	}
}
