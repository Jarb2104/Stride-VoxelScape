using System;
using System.Globalization;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class IntValueKey : AbstractNumberValueKey<int>
	{
		public IntValueKey(string key, NumberStyles style = NumberStyles.Integer | NumberStyles.AllowThousands)
			: base(key, style)
		{
		}

		public IntValueKey(
			string key, IFormatProvider provider, NumberStyles style = NumberStyles.Integer | NumberStyles.AllowThousands)
			: base(key, provider, style)
		{
		}

		/// <inheritdoc />
		public override string Serialize(int value) => value.ToString(this.Provider);

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out int result) =>
			int.TryParse(value, this.Style, this.Provider, out result);
	}
}
