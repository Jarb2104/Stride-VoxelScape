using System;
using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for when working with enums.
/// </summary>
public static class PactEnumExtensions
{
	/// <summary>
	/// Tries to convert the string representation of the name or numeric value of one or more enumerated
	/// constants to an equivalent enumerated object.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The string representation of the enumeration name or underlying value to convert.</param>
	/// <param name="result">
	/// When this method returns, result contains an object of type TEnum whose value is represented by value if the parse
	/// operation succeeds. If the parse operation fails, result contains the default value of the underlying type of TEnum.
	/// Note that this value need not be a member of the TEnum enumeration. This parameter is passed uninitialized.
	/// </param>
	/// <returns><c>true</c> if the value parameter was converted successfully; otherwise, <c>false</c>.</returns>
	/// <seealso href="https://msdn.microsoft.com/en-us/library/dd783499(v=vs.110).aspx"/>
	public static bool TryParseEnum<TEnum>(this string value, out TEnum result)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		ParseEnumContracts<TEnum>(value);

		return Enum.TryParse(value, out result);
	}

	/// <summary>
	/// Tries to convert the string representation of the name or numeric value of one or more enumerated
	/// constants to an equivalent enumerated object.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The string representation of the enumeration name or underlying value to convert.</param>
	/// <param name="ignoreCase"><c>true</c> to ignore case, <c>false</c> to consider case.</param>
	/// <param name="result">
	/// When this method returns, result contains an object of type TEnum whose value is represented by value if the parse
	/// operation succeeds. If the parse operation fails, result contains the default value of the underlying type of TEnum.
	/// Note that this value need not be a member of the TEnum enumeration. This parameter is passed uninitialized.
	/// </param>
	/// <returns><c>true</c> if the value parameter was converted successfully; otherwise, <c>false</c>.</returns>
	/// <seealso href="https://msdn.microsoft.com/en-us/library/dd991317(v=vs.110).aspx"/>
	public static bool TryParseEnum<TEnum>(this string value, bool ignoreCase, out TEnum result)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		ParseEnumContracts<TEnum>(value);

		return Enum.TryParse(value, ignoreCase, out result);
	}

	/// <summary>
	/// Contains the contracts for the ParseEnum methods.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value.</param>
	[Conditional(Contracts.Requires.CompilationSymbol)]
	private static void ParseEnumContracts<TEnum>(string value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		Contracts.Requires.That(typeof(TEnum).IsEnum);
		Contracts.Requires.That(!value.IsNullOrWhiteSpace());
	}
}
