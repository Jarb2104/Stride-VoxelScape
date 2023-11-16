using System;
using System.Globalization;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class LongValueKey : AbstractNumberValueKey<long>
	{
		public LongValueKey(string key, NumberStyles style = NumberStyles.Integer | NumberStyles.AllowThousands)
			: base(key, style)
		{
		}

		public LongValueKey(
			string key, IFormatProvider provider, NumberStyles style = NumberStyles.Integer | NumberStyles.AllowThousands)
			: base(key, provider, style)
		{
		}

		/// <inheritdoc />
		public override string Serialize(long value) => value.ToString(this.Provider);

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out long result) =>
			long.TryParse(value, this.Style, this.Provider, out result);
	}
}
