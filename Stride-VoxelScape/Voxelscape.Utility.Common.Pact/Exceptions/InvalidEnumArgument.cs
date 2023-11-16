using System;
using System.ComponentModel;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Exceptions
{
	/// <summary>
	/// Provides helper methods for creating instances of <see cref="InvalidEnumArgumentException"/>.
	/// </summary>
	public static class InvalidEnumArgument
	{
		public static InvalidEnumArgumentException CreateException<TEnum>(string argumentName, TEnum value)
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Contracts.Requires.That(!argumentName.IsNullOrEmpty());

			return new InvalidEnumArgumentException(argumentName, Convert.ToInt32(value), typeof(TEnum));
		}
	}
}
