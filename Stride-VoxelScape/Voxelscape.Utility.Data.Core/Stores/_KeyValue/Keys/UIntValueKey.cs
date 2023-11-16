using System;
using System.Globalization;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class UIntValueKey : AbstractNumberValueKey<uint>
	{
		public UIntValueKey(string key, NumberStyles style = NumberStyles.Integer | NumberStyles.AllowThousands)
			: base(key, style)
		{
		}

		public UIntValueKey(
			string key, IFormatProvider provider, NumberStyles style = NumberStyles.Integer | NumberStyles.AllowThousands)
			: base(key, provider, style)
		{
		}

		/// <inheritdoc />
		public override string Serialize(uint value) => value.ToString(this.Provider);

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out uint result) =>
			uint.TryParse(value, this.Style, this.Provider, out result);
	}
}
