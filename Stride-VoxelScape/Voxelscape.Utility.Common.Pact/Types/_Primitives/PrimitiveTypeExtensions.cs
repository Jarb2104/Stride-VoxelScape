using System;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;
using Voxelscape.Utility.Common.Pact.Types;

/// <summary>
/// Provides extension methods for the <see cref="PrimitiveType"/> enum.
/// </summary>
public static class PrimitiveTypeExtensions
{
	/// <summary>
	/// The conversion table used to convert types to primitive type enum values.
	/// </summary>
	private static readonly IDictionary<Type, PrimitiveType> ConversionTable = CreateConversionTable();

	#region Number Of Bits/Bytes/Chars Methods

	/// <summary>
	/// Gets the number of bits needed to represent a primitive type.
	/// </summary>
	/// <param name="primitiveType">Type of the primitive.</param>
	/// <returns>The number of bits needed to represent the primitive type.</returns>
	public static int NumberOfBits(this PrimitiveType primitiveType)
	{
		switch (primitiveType)
		{
			case PrimitiveType.Byte:
				return PrimitiveTypeSizes.ByteNumberOfBits;

			case PrimitiveType.SByte:
				return PrimitiveTypeSizes.SByteNumberOfBits;

			case PrimitiveType.Short:
				return PrimitiveTypeSizes.ShortNumberOfBits;

			case PrimitiveType.UShort:
				return PrimitiveTypeSizes.UShortNumberOfBits;

			case PrimitiveType.Int:
				return PrimitiveTypeSizes.IntNumberOfBits;

			case PrimitiveType.UInt:
				return PrimitiveTypeSizes.UIntNumberOfBits;

			case PrimitiveType.Long:
				return PrimitiveTypeSizes.LongNumberOfBits;

			case PrimitiveType.ULong:
				return PrimitiveTypeSizes.ULongNumberOfBits;

			case PrimitiveType.Float:
				return PrimitiveTypeSizes.FloatNumberOfBits;

			case PrimitiveType.Double:
				return PrimitiveTypeSizes.DoubleNumberOfBits;

			case PrimitiveType.Decimal:
				return PrimitiveTypeSizes.DecimalNumberOfBits;

			case PrimitiveType.Char:
				return PrimitiveTypeSizes.CharNumberOfBits;

			case PrimitiveType.Bool:
				return PrimitiveTypeSizes.BoolNumberOfBits;

			default:
				throw InvalidEnumArgument.CreateException(nameof(primitiveType), primitiveType);
		}
	}

	/// <summary>
	/// Gets the number of bytes needed to represent a primitive type.
	/// </summary>
	/// <param name="primitiveType">Type of the primitive.</param>
	/// <returns>The number of bytes needed to represent the primitive type.</returns>
	public static int NumberOfBytes(this PrimitiveType primitiveType)
	{
		switch (primitiveType)
		{
			case PrimitiveType.Byte:
				return PrimitiveTypeSizes.ByteNumberOfBytes;

			case PrimitiveType.SByte:
				return PrimitiveTypeSizes.SByteNumberOfBytes;

			case PrimitiveType.Short:
				return PrimitiveTypeSizes.ShortNumberOfBytes;

			case PrimitiveType.UShort:
				return PrimitiveTypeSizes.UShortNumberOfBytes;

			case PrimitiveType.Int:
				return PrimitiveTypeSizes.IntNumberOfBytes;

			case PrimitiveType.UInt:
				return PrimitiveTypeSizes.UIntNumberOfBytes;

			case PrimitiveType.Long:
				return PrimitiveTypeSizes.LongNumberOfBytes;

			case PrimitiveType.ULong:
				return PrimitiveTypeSizes.ULongNumberOfBytes;

			case PrimitiveType.Float:
				return PrimitiveTypeSizes.FloatNumberOfBytes;

			case PrimitiveType.Double:
				return PrimitiveTypeSizes.DoubleNumberOfBytes;

			case PrimitiveType.Decimal:
				return PrimitiveTypeSizes.DecimalNumberOfBytes;

			case PrimitiveType.Char:
				return PrimitiveTypeSizes.CharNumberOfBytes;

			case PrimitiveType.Bool:
				return PrimitiveTypeSizes.BoolNumberOfBytes;

			default:
				throw InvalidEnumArgument.CreateException(nameof(primitiveType), primitiveType);
		}
	}

	/// <summary>
	/// Gets the number of characters needed to represent a primitive type as a string.
	/// </summary>
	/// <param name="primitiveType">Type of the primitive.</param>
	/// <returns>The number of characters needed to represent the primitive type as a string.</returns>
	public static int MaxNumberOfChars(this PrimitiveType primitiveType)
	{
		switch (primitiveType)
		{
			case PrimitiveType.Byte:
				return PrimitiveTypeSizes.ByteMaxNumberOfChars;

			case PrimitiveType.SByte:
				return PrimitiveTypeSizes.SByteMaxNumberOfChars;

			case PrimitiveType.Short:
				return PrimitiveTypeSizes.ShortMaxNumberOfChars;

			case PrimitiveType.UShort:
				return PrimitiveTypeSizes.UShortMaxNumberOfChars;

			case PrimitiveType.Int:
				return PrimitiveTypeSizes.IntMaxNumberOfChars;

			case PrimitiveType.UInt:
				return PrimitiveTypeSizes.UIntMaxNumberOfChars;

			case PrimitiveType.Long:
				return PrimitiveTypeSizes.LongMaxNumberOfChars;

			case PrimitiveType.ULong:
				return PrimitiveTypeSizes.ULongMaxNumberOfChars;

			case PrimitiveType.Float:
				return PrimitiveTypeSizes.FloatMaxNumberOfChars;

			case PrimitiveType.Double:
				return PrimitiveTypeSizes.DoubleMaxNumberOfChars;

			case PrimitiveType.Decimal:
				return PrimitiveTypeSizes.DecimalMaxNumberOfChars;

			case PrimitiveType.Char:
				return PrimitiveTypeSizes.CharMaxNumberOfChars;

			case PrimitiveType.Bool:
				return PrimitiveTypeSizes.BoolMaxNumberOfChars;

			default:
				throw InvalidEnumArgument.CreateException(nameof(primitiveType), primitiveType);
		}
	}

	#endregion

	#region Numeric/Integral Methods

	/// <summary>
	/// Determines whether the specified primitive type is a numeric type.
	/// </summary>
	/// <param name="primitiveType">The primitive type.</param>
	/// <returns>True if the primitive type represents a numeric type, otherwise false.</returns>
	public static bool IsNumeric(this PrimitiveType primitiveType)
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
			case PrimitiveType.Float:
			case PrimitiveType.Double:
			case PrimitiveType.Decimal:
				return true;

			default:
				return false;
		}
	}

	/// <summary>
	/// Determines whether the specified primitive type is an integral type.
	/// </summary>
	/// <param name="primitiveType">The primitive type.</param>
	/// <returns>True if the primitive type represents an integral type, otherwise false.</returns>
	public static bool IsIntegral(this PrimitiveType primitiveType)
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
	/// Gets the minimum value for the primitive type.
	/// </summary>
	/// <param name="primitiveType">The primitive type.</param>
	/// <returns>The minimum value of the primitive type.</returns>
	/// <remarks>
	/// The return type of decimal is used because it is the only type larger enough to encompass the values of all the primitive
	/// types accurately, with the exception of float and double. The range of values represented by a float or double can not be
	/// accurately represented as a decimal, nor can a float or double accurately represent the range of values of the other
	/// integral types. Therefor, this method is incompatible with floats and doubles.
	/// </remarks>
	public static decimal MinValue(this PrimitiveType primitiveType)
	{
		Contracts.Requires.That(primitiveType.IsIntegral() || primitiveType == PrimitiveType.Decimal);

		switch (primitiveType)
		{
			case PrimitiveType.Byte:
				return byte.MinValue;

			case PrimitiveType.SByte:
				return sbyte.MinValue;

			case PrimitiveType.Short:
				return short.MinValue;

			case PrimitiveType.UShort:
				return ushort.MinValue;

			case PrimitiveType.Int:
				return int.MinValue;

			case PrimitiveType.UInt:
				return uint.MinValue;

			case PrimitiveType.Long:
				return long.MinValue;

			case PrimitiveType.ULong:
				return ulong.MinValue;

			case PrimitiveType.Decimal:
				return decimal.MinValue;

			default:
				throw InvalidEnumArgument.CreateException(nameof(primitiveType), primitiveType);
		}
	}

	/// <summary>
	/// Gets the maximum value for the primitive type.
	/// </summary>
	/// <param name="primitiveType">The primitive type.</param>
	/// <returns>The maximum value of the primitive type.</returns>
	/// <remarks>
	/// The return type of decimal is used because it is the only type larger enough to encompass the values of all the primitive
	/// types accurately, with the exception of float and double. The range of values represented by a float or double can not be
	/// accurately represented as a decimal, nor can a float or double accurately represent the range of values of the other
	/// integral types. Therefor, this method is incompatible with floats and doubles.
	/// </remarks>
	public static decimal MaxValue(this PrimitiveType primitiveType)
	{
		Contracts.Requires.That(primitiveType.IsIntegral() || primitiveType == PrimitiveType.Decimal);

		switch (primitiveType)
		{
			case PrimitiveType.Byte:
				return byte.MaxValue;

			case PrimitiveType.SByte:
				return sbyte.MaxValue;

			case PrimitiveType.Short:
				return short.MaxValue;

			case PrimitiveType.UShort:
				return ushort.MaxValue;

			case PrimitiveType.Int:
				return int.MaxValue;

			case PrimitiveType.UInt:
				return uint.MaxValue;

			case PrimitiveType.Long:
				return long.MaxValue;

			case PrimitiveType.ULong:
				return ulong.MaxValue;

			case PrimitiveType.Decimal:
				return decimal.MaxValue;

			default:
				throw InvalidEnumArgument.CreateException(nameof(primitiveType), primitiveType);
		}
	}

	#endregion

	#region Conversion Methods

	/// <summary>
	/// Gets the <see cref="Type"/> the primitive type enumeration represents.
	/// </summary>
	/// <param name="primitiveType">Type of the primitive.</param>
	/// <returns>The type.</returns>
	public static Type ToType(this PrimitiveType primitiveType)
	{
		switch (primitiveType)
		{
			case PrimitiveType.Byte:
				return typeof(byte);

			case PrimitiveType.SByte:
				return typeof(sbyte);

			case PrimitiveType.Short:
				return typeof(short);

			case PrimitiveType.UShort:
				return typeof(ushort);

			case PrimitiveType.Int:
				return typeof(int);

			case PrimitiveType.UInt:
				return typeof(uint);

			case PrimitiveType.Long:
				return typeof(long);

			case PrimitiveType.ULong:
				return typeof(ulong);

			case PrimitiveType.Float:
				return typeof(float);

			case PrimitiveType.Double:
				return typeof(double);

			case PrimitiveType.Decimal:
				return typeof(decimal);

			case PrimitiveType.Char:
				return typeof(char);

			case PrimitiveType.Bool:
				return typeof(bool);

			default:
				throw InvalidEnumArgument.CreateException(nameof(primitiveType), primitiveType);
		}
	}

	/// <summary>
	/// Gets the <see cref="PrimitiveType" /> enumeration value that represents this type.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns>The primitive type enumeration.</returns>
	public static PrimitiveType ToPrimitiveType(this Type type)
	{
		Contracts.Requires.That(type != null);
		Contracts.Requires.That(type.IsConvertibleToPrimitiveType());

		return ConversionTable[type];
	}

	/// <summary>
	/// Determines whether the specified type can be represented by a <see cref="PrimitiveType"/> enumeration value.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns>True if the type matches a primitive type enum, otherwise false.</returns>
	public static bool IsConvertibleToPrimitiveType(this Type type)
	{
		Contracts.Requires.That(type != null);

		return ConversionTable.ContainsKey(type);
	}

	#endregion

	/// <summary>
	/// Creates the conversion table.
	/// </summary>
	/// <returns>The conversion table.</returns>
	private static IDictionary<Type, PrimitiveType> CreateConversionTable()
	{
		IDictionary<Type, PrimitiveType> result = new Dictionary<Type, PrimitiveType>();

		result[typeof(byte)] = PrimitiveType.Byte;
		result[typeof(sbyte)] = PrimitiveType.SByte;
		result[typeof(short)] = PrimitiveType.Short;
		result[typeof(ushort)] = PrimitiveType.UShort;
		result[typeof(int)] = PrimitiveType.Int;
		result[typeof(uint)] = PrimitiveType.UInt;
		result[typeof(long)] = PrimitiveType.Long;
		result[typeof(ulong)] = PrimitiveType.ULong;
		result[typeof(float)] = PrimitiveType.Float;
		result[typeof(double)] = PrimitiveType.Double;
		result[typeof(decimal)] = PrimitiveType.Decimal;
		result[typeof(char)] = PrimitiveType.Char;
		result[typeof(bool)] = PrimitiveType.Bool;

		return result;
	}
}
