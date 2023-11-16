using System;
using System.Globalization;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class ULongValueKey : AbstractNumberValueKey<ulong>
	{
		public ULongValueKey(string key, NumberStyles style = NumberStyles.Integer | NumberStyles.AllowThousands)
			: base(key, style)
		{
		}

		public ULongValueKey(
			string key, IFormatProvider provider, NumberStyles style = NumberStyles.Integer | NumberStyles.AllowThousands)
			: base(key, provider, style)
		{
		}

		/// <inheritdoc />
		public override string Serialize(ulong value) => value.ToString(this.Provider);

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out ulong result) =>
			ulong.TryParse(value, this.Style, this.Provider, out result);
	}
}
