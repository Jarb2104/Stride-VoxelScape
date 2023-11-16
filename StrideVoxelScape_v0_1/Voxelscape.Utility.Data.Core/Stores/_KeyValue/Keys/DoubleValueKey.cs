using System;
using System.Globalization;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class DoubleValueKey : AbstractNumberValueKey<double>
	{
		public DoubleValueKey(string key, NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands)
			: base(key, style)
		{
		}

		public DoubleValueKey(
			string key, IFormatProvider provider, NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands)
			: base(key, provider, style)
		{
		}

		/// <inheritdoc />
		public override string Serialize(double value) => value.ToString(this.Provider);

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out double result) =>
			double.TryParse(value, this.Style, this.Provider, out result);
	}
}
