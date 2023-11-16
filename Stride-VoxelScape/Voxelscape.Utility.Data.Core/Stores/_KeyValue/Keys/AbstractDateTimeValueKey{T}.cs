using System;
using System.Globalization;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public abstract class AbstractDateTimeValueKey<T> : AbstractValueKey<T>
	{
		public AbstractDateTimeValueKey(string key, DateTimeStyles style)
			: this(key, CultureInfo.InvariantCulture, style)
		{
		}

		public AbstractDateTimeValueKey(string key, IFormatProvider provider, DateTimeStyles style)
			: base(key)
		{
			Contracts.Requires.That(provider != null);

			this.Provider = provider;
			this.Style = style;
		}

		protected IFormatProvider Provider { get; }

		protected DateTimeStyles Style { get; }
	}
}
