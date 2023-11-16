using System;
using System.Globalization;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class DateTimeValueKey : AbstractDateTimeValueKey<DateTime>
	{
		public DateTimeValueKey(string key, DateTimeStyles style = DateTimeStyles.None)
			: base(key, style)
		{
		}

		public DateTimeValueKey(
			string key, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
			: base(key, provider, style)
		{
		}

		/// <inheritdoc />
		public override string Serialize(DateTime value) => value.ToString(this.Provider);

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out DateTime result) =>
			DateTime.TryParse(value, this.Provider, this.Style, out result);
	}
}
