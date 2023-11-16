using System;
using System.Globalization;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class SByteValueKey : AbstractNumberValueKey<sbyte>
	{
		public SByteValueKey(string key, NumberStyles style = NumberStyles.Integer)
			: base(key, style)
		{
		}

		public SByteValueKey(
			string key, IFormatProvider provider, NumberStyles style = NumberStyles.Integer)
			: base(key, provider, style)
		{
		}

		/// <inheritdoc />
		public override string Serialize(sbyte value) => value.ToString(this.Provider);

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out sbyte result) =>
			sbyte.TryParse(value, this.Style, this.Provider, out result);
	}
}
