using System;
using System.Globalization;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class DecimalValueKey : AbstractNumberValueKey<decimal>
	{
		public DecimalValueKey(string key, NumberStyles style = NumberStyles.Number | NumberStyles.AllowThousands)
			: base(key, style)
		{
		}

		public DecimalValueKey(
			string key, IFormatProvider provider, NumberStyles style = NumberStyles.Number | NumberStyles.AllowThousands)
			: base(key, provider, style)
		{
		}

		/// <inheritdoc />
		public override string Serialize(decimal value) => value.ToString(this.Provider);

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out decimal result) =>
			decimal.TryParse(value, this.Style, this.Provider, out result);
	}
}
