using System;
using System.Globalization;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class DateTimeFormatValueKey : AbstractDateTimeValueKey<DateTime>
	{
		private readonly string format;

		public DateTimeFormatValueKey(string key, string format, DateTimeStyles style = DateTimeStyles.None)
			: base(key, style)
		{
			this.format = format;
		}

		public DateTimeFormatValueKey(
			string key, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
			: base(key, provider, style)
		{
			this.format = format;
		}

		/// <inheritdoc />
		public override string Serialize(DateTime value) => value.ToString(this.format, this.Provider);

		/// <inheritdoc />
		public override bool TryDeserialize(string value, out DateTime result) =>
			DateTime.TryParseExact(value, this.format, this.Provider, this.Style, out result);
	}
}
