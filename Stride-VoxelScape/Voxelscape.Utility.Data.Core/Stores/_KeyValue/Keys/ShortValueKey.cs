using System;
using System.Globalization;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class ShortValueKey : AbstractNumberValueKey<short>
	{
		public ShortValueKey(string key, NumberStyles style = NumberStyles.Integer | NumberStyles.AllowThousands)
			: base(key, style)
		{
		}

		public ShortValueKey(
			string key, IFormatProvider provider, NumberStyles style = NumberStyles.Integer | NumberStyles.AllowThousands)
			: base(key, provider, style)
		{
		}

		/// <inheritdoc />
		public override string Serialize(short value) => value.ToString(this.Provider);

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out short result) =>
			short.TryParse(value, this.Style, this.Provider, out result);
	}
}
