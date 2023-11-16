using System;
using System.Globalization;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class FloatValueKey : AbstractNumberValueKey<float>
	{
		public FloatValueKey(string key, NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands)
			: base(key, style)
		{
		}

		public FloatValueKey(
			string key, IFormatProvider provider, NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands)
			: base(key, provider, style)
		{
		}

		/// <inheritdoc />
		public override string Serialize(float value) => value.ToString(this.Provider);

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out float result) =>
			float.TryParse(value, this.Style, this.Provider, out result);
	}
}
