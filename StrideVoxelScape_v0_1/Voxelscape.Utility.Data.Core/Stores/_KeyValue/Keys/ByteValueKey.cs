using System;
using System.Globalization;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class ByteValueKey : AbstractNumberValueKey<byte>
	{
		public ByteValueKey(string key, NumberStyles style = NumberStyles.Integer)
			: base(key, style)
		{
		}

		public ByteValueKey(
			string key, IFormatProvider provider, NumberStyles style = NumberStyles.Integer)
			: base(key, provider, style)
		{
		}

		/// <inheritdoc />
		public override string Serialize(byte value) => value.ToString(this.Provider);

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out byte result) =>
			byte.TryParse(value, this.Style, this.Provider, out result);
	}
}
