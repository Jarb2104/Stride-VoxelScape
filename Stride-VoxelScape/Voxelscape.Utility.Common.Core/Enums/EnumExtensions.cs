using System;
using System.Diagnostics;
using Voxelscape.Utility.Common.Core.Enums;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;

/// <summary>
/// Provides extension methods for working with <see cref="Enum"/> types.
/// </summary>
public static class CoreEnumExtensions
{
	/// <summary>
	/// Determines whether the primitive type enum represents a valid underlying type for an enum.
	/// </summary>
	/// <param name="primitiveType">The primitive type enumeration value.</param>
	/// <returns>
	/// True if the primitive type is valid for underlying an enum, otherwise false.
	/// </returns>
	public static bool IsValidUnderlyingTypeForEnum(this PrimitiveType primitiveType)
	{
		switch (primitiveType)
		{
			case PrimitiveType.Byte:
			case PrimitiveType.SByte:
			case PrimitiveType.Short:
			case PrimitiveType.UShort:
			case PrimitiveType.Int:
			case PrimitiveType.UInt:
			case PrimitiveType.Long:
			case PrimitiveType.ULong:
				return true;

			default:
				return false;
		}
	}

	/// <summary>
	/// Determines whether the type is a valid underlying type for an enum.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns>
	/// True if the type is valid for underlying an enum, otherwise false.
	/// </returns>
	public static bool IsValidUnderlyingTypeForEnum(this Type type)
	{
		Contracts.Requires.That(type != null);

		return type == typeof(byte)
			|| type == typeof(sbyte)
			|| type == typeof(short)
			|| type == typeof(ushort)
			|| type == typeof(int)
			|| type == typeof(uint)
			|| type == typeof(long)
			|| type == typeof(ulong);
	}

	#region Parsing

	/// <summary>
	/// Converts the string representation of the name or numeric value of one or more enumerated constants to an
	/// equivalent enumerated object, or throws an exception if parsing fails.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The string representation of the enumeration name or underlying value to convert.</param>
	/// <returns>An object of type enumType whose value is represented by value.</returns>
	/// <seealso href="https://msdn.microsoft.com/en-us/library/essfb559(v=vs.110).aspx"/>
	public static TEnum ParseEnum<TEnum>(this string value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		ParseEnumContracts<TEnum>(value);

		return (TEnum)Enum.Parse(typeof(TEnum), value);
	}

	/// <summary>
	/// Converts the string representation of the name or numeric value of one or more enumerated constants to an
	/// equivalent enumerated object, or throws an exception if parsing fails.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The string representation of the enumeration name or underlying value to convert.</param>
	/// <param name="ignoreCase"><c>true</c> to ignore case, <c>false</c> to consider case.</param>
	/// <returns>An object of type enumType whose value is represented by value.</returns>
	/// <seealso href="https://msdn.microsoft.com/en-us/library/kxydatf9(v=vs.110).aspx"/>
	public static TEnum ParseEnum<TEnum>(this string value, bool ignoreCase)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		ParseEnumContracts<TEnum>(value);

		return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
	}

	#endregion

	#region IsValid(Flags)EnumValue extensions on Enum class

	/// <summary>
	/// Determines whether the specified value would convert to a valid enum value.
	/// </summary>
	/// <param name="value">The value to check.</param>
	/// <returns>True if the value would convert to a valid enum value; otherwise false.</returns>
	/// <remarks>
	/// <para>
	/// This method does not consider combinations of flag enum values.
	/// To include flag enum values use <see cref="IsValidFlagsEnumValue"/> instead.
	/// </para><para>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </para>
	/// </remarks>
	public static bool IsValidEnumValue(this Enum value)
	{
		Contracts.Requires.That(value != null);

		// Both passing by class Enum and Enum.IsDefined causes boxing anyway
		// so share implementation by passing as object to internal helper method.
		// Because boxing is already going to happen, I made this take in Enum class
		// instead of TEnum so it can be a proper extension method.
		return EnumUtilities.IsValidEnumValueInternalMethod(value);
	}

	/// <summary>
	/// Determines whether the specified value would convert to a valid enum value or a valid combination
	/// of flag enum values. This is slower than just <see cref="IsValidEnumValue"/> so only use if checking
	/// a flags enum.
	/// </summary>
	/// <param name="value">The value to check.</param>
	/// <returns>True if the value would convert to a valid enum value; otherwise false.</returns>
	/// <remarks>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </remarks>
	public static bool IsValidFlagsEnumValue(this Enum value)
	{
		Contracts.Requires.That(value != null);

		// Both passing by class Enum and calling ToString on an enum woulbe cause boxing
		// so share implementation by passing as object to internal helper method.
		// Because boxing is already going to happen, I made this take in Enum class
		// instead of TEnum so it can be a proper extension method.
		return EnumUtilities.IsValidFlagsEnumValueInternalMethod(value);
	}

	#endregion

	#region IsValidEnumValue extensions on primitive types

	/// <summary>
	/// Determines whether the specified value would convert to a valid enum value.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to check.</param>
	/// <returns>True if the value would convert to a valid enum value; otherwise false.</returns>
	/// <remarks>
	/// <para>
	/// This method does not consider combinations of flag enum values.
	/// To include flag enum values use <see cref="IsValidFlagsEnumValue"/> instead.
	/// </para><para>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </para>
	/// </remarks>
	public static bool IsValidEnumValue<TEnum>(this byte value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		Contracts.Requires.That(typeof(TEnum).IsEnum);

		// both Enum.IsDefined and Enum.ToObject operate on object (results in boxing)
		return Enum.IsDefined(typeof(TEnum), Enum.ToObject(typeof(TEnum), value));
	}

	/// <summary>
	/// Determines whether the specified value would convert to a valid enum value.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to check.</param>
	/// <returns>True if the value would convert to a valid enum value; otherwise false.</returns>
	/// <remarks>
	/// <para>
	/// This method does not consider combinations of flag enum values.
	/// To include flag enum values use <see cref="IsValidFlagsEnumValue"/> instead.
	/// </para><para>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </para>
	/// </remarks>
	public static bool IsValidEnumValue<TEnum>(this short value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		Contracts.Requires.That(typeof(TEnum).IsEnum);

		// both Enum.IsDefined and Enum.ToObject operate on object (results in boxing)
		return Enum.IsDefined(typeof(TEnum), Enum.ToObject(typeof(TEnum), value));
	}

	/// <summary>
	/// Determines whether the specified value would convert to a valid enum value.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to check.</param>
	/// <returns>True if the value would convert to a valid enum value; otherwise false.</returns>
	/// <remarks>
	/// <para>
	/// This method does not consider combinations of flag enum values.
	/// To include flag enum values use <see cref="IsValidFlagsEnumValue"/> instead.
	/// </para><para>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </para>
	/// </remarks>
	public static bool IsValidEnumValue<TEnum>(this int value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		Contracts.Requires.That(typeof(TEnum).IsEnum);

		// both Enum.IsDefined and Enum.ToObject operate on object (results in boxing)
		return Enum.IsDefined(typeof(TEnum), Enum.ToObject(typeof(TEnum), value));
	}

	/// <summary>
	/// Determines whether the specified value would convert to a valid enum value.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to check.</param>
	/// <returns>True if the value would convert to a valid enum value; otherwise false.</returns>
	/// <remarks>
	/// <para>
	/// This method does not consider combinations of flag enum values.
	/// To include flag enum values use <see cref="IsValidFlagsEnumValue"/> instead.
	/// </para><para>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </para>
	/// </remarks>
	public static bool IsValidEnumValue<TEnum>(this long value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		Contracts.Requires.That(typeof(TEnum).IsEnum);

		// both Enum.IsDefined and Enum.ToObject operate on object (results in boxing)
		return Enum.IsDefined(typeof(TEnum), Enum.ToObject(typeof(TEnum), value));
	}

	/// <summary>
	/// Determines whether the specified value would convert to a valid enum value.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to check.</param>
	/// <returns>True if the value would convert to a valid enum value; otherwise false.</returns>
	/// <remarks>
	/// <para>
	/// This method does not consider combinations of flag enum values.
	/// To include flag enum values use <see cref="IsValidFlagsEnumValue"/> instead.
	/// </para><para>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </para>
	/// </remarks>
	public static bool IsValidEnumValue<TEnum>(this sbyte value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		Contracts.Requires.That(typeof(TEnum).IsEnum);

		// both Enum.IsDefined and Enum.ToObject operate on object (results in boxing)
		return Enum.IsDefined(typeof(TEnum), Enum.ToObject(typeof(TEnum), value));
	}

	/// <summary>
	/// Determines whether the specified value would convert to a valid enum value.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to check.</param>
	/// <returns>True if the value would convert to a valid enum value; otherwise false.</returns>
	/// <remarks>
	/// <para>
	/// This method does not consider combinations of flag enum values.
	/// To include flag enum values use <see cref="IsValidFlagsEnumValue"/> instead.
	/// </para><para>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </para>
	/// </remarks>
	public static bool IsValidEnumValue<TEnum>(this ushort value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		Contracts.Requires.That(typeof(TEnum).IsEnum);

		// both Enum.IsDefined and Enum.ToObject operate on object (results in boxing)
		return Enum.IsDefined(typeof(TEnum), Enum.ToObject(typeof(TEnum), value));
	}

	/// <summary>
	/// Determines whether the specified value would convert to a valid enum value.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to check.</param>
	/// <returns>True if the value would convert to a valid enum value; otherwise false.</returns>
	/// <remarks>
	/// <para>
	/// This method does not consider combinations of flag enum values.
	/// To include flag enum values use <see cref="IsValidFlagsEnumValue"/> instead.
	/// </para><para>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </para>
	/// </remarks>
	public static bool IsValidEnumValue<TEnum>(this uint value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		Contracts.Requires.That(typeof(TEnum).IsEnum);

		// both Enum.IsDefined and Enum.ToObject operate on object (results in boxing)
		return Enum.IsDefined(typeof(TEnum), Enum.ToObject(typeof(TEnum), value));
	}

	/// <summary>
	/// Determines whether the specified value would convert to a valid enum value.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to check.</param>
	/// <returns>True if the value would convert to a valid enum value; otherwise false.</returns>
	/// <remarks>
	/// <para>
	/// This method does not consider combinations of flag enum values.
	/// To include flag enum values use <see cref="IsValidFlagsEnumValue"/> instead.
	/// </para><para>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </para>
	/// </remarks>
	public static bool IsValidEnumValue<TEnum>(this ulong value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		Contracts.Requires.That(typeof(TEnum).IsEnum);

		// both Enum.IsDefined and Enum.ToObject operate on object (results in boxing)
		return Enum.IsDefined(typeof(TEnum), Enum.ToObject(typeof(TEnum), value));
	}

	#endregion

	#region ToEnum extensions on primitive types

	/// <summary>
	/// Converts a value to its corresponding enum value representation.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to convert.</param>
	/// <returns>The value as an enum.</returns>
	/// <remarks>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </remarks>
	public static TEnum ToEnum<TEnum>(this byte value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		ToEnumContracts<TEnum>(value);

		// Enum.ToObject returns an object (casting results in unboxing)
		return (TEnum)Enum.ToObject(typeof(TEnum), value);
	}

	/// <summary>
	/// Converts a value to its corresponding enum value representation.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to convert.</param>
	/// <returns>The value as an enum.</returns>
	/// <remarks>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </remarks>
	public static TEnum ToEnum<TEnum>(this short value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		ToEnumContracts<TEnum>(value);

		// Enum.ToObject returns an object (casting results in unboxing)
		return (TEnum)Enum.ToObject(typeof(TEnum), value);
	}

	/// <summary>
	/// Converts a value to its corresponding enum value representation.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to convert.</param>
	/// <returns>The value as an enum.</returns>
	/// <remarks>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </remarks>
	public static TEnum ToEnum<TEnum>(this int value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		ToEnumContracts<TEnum>(value);

		// Enum.ToObject returns an object (casting results in unboxing)
		return (TEnum)Enum.ToObject(typeof(TEnum), value);
	}

	/// <summary>
	/// Converts a value to its corresponding enum value representation.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to convert.</param>
	/// <returns>The value as an enum.</returns>
	/// <remarks>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </remarks>
	public static TEnum ToEnum<TEnum>(this long value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		ToEnumContracts<TEnum>(value);

		// Enum.ToObject returns an object (casting results in unboxing)
		return (TEnum)Enum.ToObject(typeof(TEnum), value);
	}

	/// <summary>
	/// Converts a value to its corresponding enum value representation.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to convert.</param>
	/// <returns>The value as an enum.</returns>
	/// <remarks>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </remarks>
	public static TEnum ToEnum<TEnum>(this sbyte value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		ToEnumContracts<TEnum>(value);

		// Enum.ToObject returns an object (casting results in unboxing)
		return (TEnum)Enum.ToObject(typeof(TEnum), value);
	}

	/// <summary>
	/// Converts a value to its corresponding enum value representation.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to convert.</param>
	/// <returns>The value as an enum.</returns>
	/// <remarks>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </remarks>
	public static TEnum ToEnum<TEnum>(this ushort value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		ToEnumContracts<TEnum>(value);

		// Enum.ToObject returns an object (casting results in unboxing)
		return (TEnum)Enum.ToObject(typeof(TEnum), value);
	}

	/// <summary>
	/// Converts a value to its corresponding enum value representation.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to convert.</param>
	/// <returns>The value as an enum.</returns>
	/// <remarks>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </remarks>
	public static TEnum ToEnum<TEnum>(this uint value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		ToEnumContracts<TEnum>(value);

		// Enum.ToObject returns an object (casting results in unboxing)
		return (TEnum)Enum.ToObject(typeof(TEnum), value);
	}

	/// <summary>
	/// Converts a value to its corresponding enum value representation.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <param name="value">The value to convert.</param>
	/// <returns>The value as an enum.</returns>
	/// <remarks>
	/// This method results in boxing and may negatively impact performance critical loops.
	/// </remarks>
	public static TEnum ToEnum<TEnum>(this ulong value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		Contracts.Requires.That(typeof(TEnum).IsEnum);
		Contracts.Requires.That(IsValidEnumValue<TEnum>(value));

		// Enum.ToObject returns an object (casting results in unboxing)
		return (TEnum)Enum.ToObject(typeof(TEnum), value);
	}

	#endregion

	#region Contracts

	[Conditional(Contracts.Requires.CompilationSymbol)]
	private static void ToEnumContracts<TEnum>(long value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		Contracts.Requires.That(typeof(TEnum).IsEnum);
		Contracts.Requires.That(IsValidEnumValue<TEnum>(value));
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
		Contracts.Requires.That(!value.IsNullOrEmpty());
	}

	#endregion
}
