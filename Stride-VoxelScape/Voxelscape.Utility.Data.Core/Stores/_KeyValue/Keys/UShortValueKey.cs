using System;
using System.Globalization;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class UShortValueKey : AbstractNumberValueKey<ushort>
	{
		public UShortValueKey(string key, NumberStyles style = NumberStyles.Integer | NumberStyles.AllowThousands)
			: base(key, style)
		{
		}

		public UShortValueKey(
			string key, IFormatProvider provider, NumberStyles style = NumberStyles.Integer | NumberStyles.AllowThousands)
			: base(key, provider, style)
		{
		}

		/// <inheritdoc />
		public override string Serialize(ushort value) => value.ToString(this.Provider);

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out ushort result) =>
			ushort.TryParse(value, this.Style, this.Provider, out result);
	}
}
