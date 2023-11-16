using Voxelscape.Utility.Common.Pact.Types;

/// <summary>
/// Provides extension methods for the various forms of representational sizes for the primitive types.
/// </summary>
public static class PrimitiveTypeSizeExtensions
{
	#region Byte

	/// <summary>
	/// The numbers of bits needed to represent a byte as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bits.
	/// </returns>
	public static int NumberOfBits(this byte value)
	{
		return PrimitiveTypeSizes.ByteNumberOfBits;
	}

	/// <summary>
	/// The number of bytes needed to represent a byte as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bytes.
	/// </returns>
	public static int NumberOfBytes(this byte value)
	{
		return PrimitiveTypeSizes.ByteNumberOfBytes;
	}

	/// <summary>
	/// The maximum number of characters needed to represent a byte as a string.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The maximum number of chars.
	/// </returns>
	public static int NumberOfCharsMax(this byte value)
	{
		return PrimitiveTypeSizes.ByteMaxNumberOfChars;
	}

	#endregion

	#region SByte

	/// <summary>
	/// The numbers of bits needed to represent a sbyte as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bits.
	/// </returns>
	public static int NumberOfBits(this sbyte value)
	{
		return PrimitiveTypeSizes.SByteNumberOfBits;
	}

	/// <summary>
	/// The number of bytes needed to represent a sbyte as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bytes.
	/// </returns>
	public static int NumberOfBytes(this sbyte value)
	{
		return PrimitiveTypeSizes.SByteNumberOfBytes;
	}

	/// <summary>
	/// The maximum number of characters needed to represent a sbyte as a string.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The maximum number of chars.
	/// </returns>
	public static int NumberOfCharsMax(this sbyte value)
	{
		return PrimitiveTypeSizes.SByteMaxNumberOfChars;
	}

	#endregion

	#region Short

	/// <summary>
	/// The numbers of bits needed to represent a short as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bits.
	/// </returns>
	public static int NumberOfBits(this short value)
	{
		return PrimitiveTypeSizes.ShortNumberOfBits;
	}

	/// <summary>
	/// The number of bytes needed to represent a short as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bytes.
	/// </returns>
	public static int NumberOfBytes(this short value)
	{
		return PrimitiveTypeSizes.ShortNumberOfBytes;
	}

	/// <summary>
	/// The maximum number of characters needed to represent a short as a string.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The maximum number of chars.
	/// </returns>
	public static int NumberOfCharsMax(this short value)
	{
		return PrimitiveTypeSizes.ShortMaxNumberOfChars;
	}

	#endregion

	#region UShort

	/// <summary>
	/// The numbers of bits needed to represent a ushort as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bits.
	/// </returns>
	public static int NumberOfBits(this ushort value)
	{
		return PrimitiveTypeSizes.UShortNumberOfBits;
	}

	/// <summary>
	/// The number of bytes needed to represent a ushort as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bytes.
	/// </returns>
	public static int NumberOfBytes(this ushort value)
	{
		return PrimitiveTypeSizes.UShortNumberOfBytes;
	}

	/// <summary>
	/// The maximum number of characters needed to represent a ushort as a string.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The maximum number of chars.
	/// </returns>
	public static int NumberOfCharsMax(this ushort value)
	{
		return PrimitiveTypeSizes.UShortMaxNumberOfChars;
	}

	#endregion

	#region Int

	/// <summary>
	/// The numbers of bits needed to represent a int as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bits.
	/// </returns>
	public static int NumberOfBits(this int value)
	{
		return PrimitiveTypeSizes.IntNumberOfBits;
	}

	/// <summary>
	/// The number of bytes needed to represent a int as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bytes.
	/// </returns>
	public static int NumberOfBytes(this int value)
	{
		return PrimitiveTypeSizes.IntNumberOfBytes;
	}

	/// <summary>
	/// The maximum number of characters needed to represent a int as a string.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The maximum number of chars.
	/// </returns>
	public static int NumberOfCharsMax(this int value)
	{
		return PrimitiveTypeSizes.IntMaxNumberOfChars;
	}

	#endregion

	#region UInt

	/// <summary>
	/// The numbers of bits needed to represent a uint as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bits.
	/// </returns>
	public static int NumberOfBits(this uint value)
	{
		return PrimitiveTypeSizes.UIntNumberOfBits;
	}

	/// <summary>
	/// The number of bytes needed to represent a uint as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bytes.
	/// </returns>
	public static int NumberOfBytes(this uint value)
	{
		return PrimitiveTypeSizes.UIntNumberOfBytes;
	}

	/// <summary>
	/// The maximum number of characters needed to represent a uint as a string.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The maximum number of chars.
	/// </returns>
	public static int NumberOfCharsMax(this uint value)
	{
		return PrimitiveTypeSizes.UIntMaxNumberOfChars;
	}

	#endregion

	#region Long

	/// <summary>
	/// The numbers of bits needed to represent a long as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bits.
	/// </returns>
	public static int NumberOfBits(this long value)
	{
		return PrimitiveTypeSizes.LongNumberOfBits;
	}

	/// <summary>
	/// The number of bytes needed to represent a long as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bytes.
	/// </returns>
	public static int NumberOfBytes(this long value)
	{
		return PrimitiveTypeSizes.LongNumberOfBytes;
	}

	/// <summary>
	/// The maximum number of characters needed to represent a long as a string.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The maximum number of chars.
	/// </returns>
	public static int NumberOfCharsMax(this long value)
	{
		return PrimitiveTypeSizes.LongMaxNumberOfChars;
	}

	#endregion

	#region ULong

	/// <summary>
	/// The numbers of bits needed to represent a ulong as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bits.
	/// </returns>
	public static int NumberOfBits(this ulong value)
	{
		return PrimitiveTypeSizes.ULongNumberOfBits;
	}

	/// <summary>
	/// The number of bytes needed to represent a ulong as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bytes.
	/// </returns>
	public static int NumberOfBytes(this ulong value)
	{
		return PrimitiveTypeSizes.ULongNumberOfBytes;
	}

	/// <summary>
	/// The maximum number of characters needed to represent a ulong as a string.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The maximum number of chars.
	/// </returns>
	public static int NumberOfCharsMax(this ulong value)
	{
		return PrimitiveTypeSizes.ULongMaxNumberOfChars;
	}

	#endregion

	#region Float

	/// <summary>
	/// The numbers of bits needed to represent a float as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bits.
	/// </returns>
	public static int NumberOfBits(this float value)
	{
		return PrimitiveTypeSizes.FloatNumberOfBits;
	}

	/// <summary>
	/// The number of bytes needed to represent a float as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bytes.
	/// </returns>
	public static int NumberOfBytes(this float value)
	{
		return PrimitiveTypeSizes.FloatNumberOfBytes;
	}

	/// <summary>
	/// The maximum number of characters needed to represent a float as a string.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The maximum number of chars.
	/// </returns>
	public static int NumberOfCharsMax(this float value)
	{
		return PrimitiveTypeSizes.FloatMaxNumberOfChars;
	}

	#endregion

	#region Double

	/// <summary>
	/// The numbers of bits needed to represent a double as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bits.
	/// </returns>
	public static int NumberOfBits(this double value)
	{
		return PrimitiveTypeSizes.DoubleNumberOfBits;
	}

	/// <summary>
	/// The number of bytes needed to represent a double as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bytes.
	/// </returns>
	public static int NumberOfBytes(this double value)
	{
		return PrimitiveTypeSizes.DoubleNumberOfBytes;
	}

	/// <summary>
	/// The maximum number of characters needed to represent a double as a string.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The maximum number of chars.
	/// </returns>
	public static int NumberOfCharsMax(this double value)
	{
		return PrimitiveTypeSizes.DoubleMaxNumberOfChars;
	}

	#endregion

	#region Decimal

	/// <summary>
	/// The numbers of bits needed to represent a decimal as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bits.
	/// </returns>
	public static int NumberOfBits(this decimal value)
	{
		return PrimitiveTypeSizes.DecimalNumberOfBits;
	}

	/// <summary>
	/// The number of bytes needed to represent a decimal as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bytes.
	/// </returns>
	public static int NumberOfBytes(this decimal value)
	{
		return PrimitiveTypeSizes.DecimalNumberOfBytes;
	}

	/// <summary>
	/// The maximum number of characters needed to represent a decimal as a string.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The maximum number of chars.
	/// </returns>
	public static int NumberOfCharsMax(this decimal value)
	{
		return PrimitiveTypeSizes.DecimalMaxNumberOfChars;
	}

	#endregion

	#region Char

	/// <summary>
	/// The numbers of bits needed to represent a char as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bits.
	/// </returns>
	public static int NumberOfBits(this char value)
	{
		return PrimitiveTypeSizes.CharNumberOfBits;
	}

	/// <summary>
	/// The number of bytes needed to represent a char as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bytes.
	/// </returns>
	public static int NumberOfBytes(this char value)
	{
		return PrimitiveTypeSizes.CharNumberOfBytes;
	}

	/// <summary>
	/// The maximum number of characters needed to represent a char as a string.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The maximum number of chars.
	/// </returns>
	public static int NumberOfCharsMax(this char value)
	{
		return PrimitiveTypeSizes.CharMaxNumberOfChars;
	}

	#endregion

	#region Bool

	/// <summary>
	/// The numbers of bits needed to represent a bool as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bits.
	/// </returns>
	public static int NumberOfBits(this bool value)
	{
		return PrimitiveTypeSizes.BoolNumberOfBits;
	}

	/// <summary>
	/// The number of bytes needed to represent a bool as a binary primitive type.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The number of bytes.
	/// </returns>
	public static int NumberOfBytes(this bool value)
	{
		return PrimitiveTypeSizes.BoolNumberOfBytes;
	}

	/// <summary>
	/// The maximum number of characters needed to represent a bool as a string.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	/// The maximum number of chars.
	/// </returns>
	public static int NumberOfCharsMax(this bool value)
	{
		return PrimitiveTypeSizes.BoolMaxNumberOfChars;
	}

	#endregion
}
