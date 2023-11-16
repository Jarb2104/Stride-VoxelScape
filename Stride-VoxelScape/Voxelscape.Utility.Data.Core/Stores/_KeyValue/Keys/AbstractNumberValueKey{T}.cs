using System;
using System.Globalization;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public abstract class AbstractNumberValueKey<T> : AbstractValueKey<T>
	{
		public AbstractNumberValueKey(string key, NumberStyles style)
			: this(key, CultureInfo.InvariantCulture, style)
		{
		}

		public AbstractNumberValueKey(string key, IFormatProvider provider, NumberStyles style)
			: base(key)
		{
			Contracts.Requires.That(provider != null);

			this.Provider = provider;
			this.Style = style;
		}

		protected IFormatProvider Provider { get; }

		protected NumberStyles Style { get; }
	}
}
