namespace Voxelscape.Utility.Common.Pact.Types
{
	/// <summary>
	/// Provides the various forms of representational sizes for the primitive types.
	/// </summary>
	public static class PrimitiveTypeSizes
	{
		#region Byte

		/// <summary>
		/// Gets the number of bits needed to represent a byte as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bits.
		/// </value>
		public static int ByteNumberOfBits => 8;

		/// <summary>
		/// Gets the number of bytes needed to represent a byte as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bytes.
		/// </value>
		public static int ByteNumberOfBytes => 1;

		/// <summary>
		/// Gets the maximum number of characters needed to represent a byte as a string.
		/// </summary>
		/// <value>
		/// The maximum number of chars.
		/// </value>
		/// <remarks>
		/// <para>The number of characters needed to represent a particular value is often less than this size.</para>
		/// <para>The string is in cultural invariant English with no commas delimiting size.</para>
		/// </remarks>
		public static int ByteMaxNumberOfChars => 3;

		#endregion

		#region SByte

		/// <summary>
		/// Gets the number of bits needed to represent a sbyte as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bits.
		/// </value>
		public static int SByteNumberOfBits => 8;

		/// <summary>
		/// Gets the number of bytes needed to represent a sbyte as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bytes.
		/// </value>
		public static int SByteNumberOfBytes => 1;

		/// <summary>
		/// Gets the maximum number of characters needed to represent a sbyte as a string.
		/// </summary>
		/// <value>
		/// The maximum number of chars.
		/// </value>
		/// <remarks>
		/// <para>The number of characters needed to represent a particular value is often less than this size.</para>
		/// <para>The string is in cultural invariant English with no commas delimiting size.</para>
		/// </remarks>
		public static int SByteMaxNumberOfChars => 4;

		#endregion

		#region Short

		/// <summary>
		/// Gets the number of bits needed to represent a short as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bits.
		/// </value>
		public static int ShortNumberOfBits => 16;

		/// <summary>
		/// Gets the number of bytes needed to represent a short as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bytes.
		/// </value>
		public static int ShortNumberOfBytes => 2;

		/// <summary>
		/// Gets the maximum number of characters needed to represent a short as a string.
		/// </summary>
		/// <value>
		/// The maximum number of chars.
		/// </value>
		/// <remarks>
		/// <para>The number of characters needed to represent a particular value is often less than this size.</para>
		/// <para>The string is in cultural invariant English with no commas delimiting size.</para>
		/// </remarks>
		public static int ShortMaxNumberOfChars => 6;

		#endregion

		#region UShort

		/// <summary>
		/// Gets the number of bits needed to represent a ushort as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bits.
		/// </value>
		public static int UShortNumberOfBits => 16;

		/// <summary>
		/// Gets the number of bytes needed to represent a ushort as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bytes.
		/// </value>
		public static int UShortNumberOfBytes => 2;

		/// <summary>
		/// Gets the maximum number of characters needed to represent a ushort as a string.
		/// </summary>
		/// <value>
		/// The maximum number of chars.
		/// </value>
		/// <remarks>
		/// <para>The number of characters needed to represent a particular value is often less than this size.</para>
		/// <para>The string is in cultural invariant English with no commas delimiting size.</para>
		/// </remarks>
		public static int UShortMaxNumberOfChars => 5;

		#endregion

		#region Int

		/// <summary>
		/// Gets the number of bits needed to represent a int as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bits.
		/// </value>
		public static int IntNumberOfBits => 32;

		/// <summary>
		/// Gets the number of bytes needed to represent a int as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bytes.
		/// </value>
		public static int IntNumberOfBytes => 4;

		/// <summary>
		/// Gets the maximum number of characters needed to represent a int as a string.
		/// </summary>
		/// <value>
		/// The maximum number of chars.
		/// </value>
		/// <remarks>
		/// <para>The number of characters needed to represent a particular value is often less than this size.</para>
		/// <para>The string is in cultural invariant English with no commas delimiting size.</para>
		/// </remarks>
		public static int IntMaxNumberOfChars => 11;

		#endregion

		#region UInt

		/// <summary>
		/// Gets the number of bits needed to represent a uint as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bits.
		/// </value>
		public static int UIntNumberOfBits => 32;

		/// <summary>
		/// Gets the number of bytes needed to represent a uint as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bytes.
		/// </value>
		public static int UIntNumberOfBytes => 4;

		/// <summary>
		/// Gets the maximum number of characters needed to represent a uint as a string.
		/// </summary>
		/// <value>
		/// The maximum number of chars.
		/// </value>
		/// <remarks>
		/// <para>The number of characters needed to represent a particular value is often less than this size.</para>
		/// <para>The string is in cultural invariant English with no commas delimiting size.</para>
		/// </remarks>
		public static int UIntMaxNumberOfChars => 10;

		#endregion

		#region Long

		/// <summary>
		/// Gets the number of bits needed to represent a long as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bits.
		/// </value>
		public static int LongNumberOfBits => 64;

		/// <summary>
		/// Gets the number of bytes needed to represent a long as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bytes.
		/// </value>
		public static int LongNumberOfBytes => 8;

		/// <summary>
		/// Gets the maximum number of characters needed to represent a long as a string.
		/// </summary>
		/// <value>
		/// The maximum number of chars.
		/// </value>
		/// <remarks>
		/// <para>The number of characters needed to represent a particular value is often less than this size.</para>
		/// <para>The string is in cultural invariant English with no commas delimiting size.</para>
		/// </remarks>
		public static int LongMaxNumberOfChars => 20;

		#endregion

		#region ULong

		/// <summary>
		/// Gets the number of bits needed to represent a ulong as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bits.
		/// </value>
		public static int ULongNumberOfBits => 64;

		/// <summary>
		/// Gets the number of bytes needed to represent a ulong as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bytes.
		/// </value>
		public static int ULongNumberOfBytes => 8;

		/// <summary>
		/// Gets the maximum number of characters needed to represent a ulong as a string.
		/// </summary>
		/// <value>
		/// The maximum number of chars.
		/// </value>
		/// <remarks>
		/// <para>The number of characters needed to represent a particular value is often less than this size.</para>
		/// <para>The string is in cultural invariant English with no commas delimiting size.</para>
		/// </remarks>
		public static int ULongMaxNumberOfChars => 20;

		#endregion

		#region Float

		/// <summary>
		/// Gets the number of bits needed to represent a float as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bits.
		/// </value>
		public static int FloatNumberOfBits => 32;

		/// <summary>
		/// Gets the number of bytes needed to represent a float as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bytes.
		/// </value>
		public static int FloatNumberOfBytes => 4;

		/// <summary>
		/// Gets the maximum number of characters needed to represent a float as a string.
		/// </summary>
		/// <value>
		/// The maximum number of chars.
		/// </value>
		/// <remarks>
		/// <para>The number of characters needed to represent a particular value is often less than this size.</para>
		/// <para>The string is in cultural invariant English with no commas delimiting size.</para>
		/// </remarks>
		public static int FloatMaxNumberOfChars => 12;

		#endregion

		#region Double

		/// <summary>
		/// Gets the number of bits needed to represent a double as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bits.
		/// </value>
		public static int DoubleNumberOfBits => 64;

		/// <summary>
		/// Gets the number of bytes needed to represent a double as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bytes.
		/// </value>
		public static int DoubleNumberOfBytes => 8;

		/// <summary>
		/// Gets the maximum number of characters needed to represent a double as a string.
		/// </summary>
		/// <value>
		/// The maximum number of chars.
		/// </value>
		/// <remarks>
		/// <para>The number of characters needed to represent a particular value is often less than this size.</para>
		/// <para>The string is in cultural invariant English with no commas delimiting size.</para>
		/// </remarks>
		public static int DoubleMaxNumberOfChars => 21;

		#endregion

		#region Decimal

		/// <summary>
		/// Gets the number of bits needed to represent a byte as a decimal primitive type.
		/// </summary>
		/// <value>
		/// The number of bits.
		/// </value>
		public static int DecimalNumberOfBits => 128;

		/// <summary>
		/// Gets the number of bytes needed to represent a byte as a decimal primitive type.
		/// </summary>
		/// <value>
		/// The number of bytes.
		/// </value>
		public static int DecimalNumberOfBytes => 16;

		/// <summary>
		/// Gets the maximum number of characters needed to represent a decimal as a string.
		/// </summary>
		/// <value>
		/// The maximum number of chars.
		/// </value>
		/// <remarks>
		/// <para>The number of characters needed to represent a particular value is often less than this size.</para>
		/// <para>The string is in cultural invariant English with no commas delimiting size.</para>
		/// </remarks>
		public static int DecimalMaxNumberOfChars => 30;

		#endregion

		#region Char

		/// <summary>
		/// Gets the number of bits needed to represent a char as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bits.
		/// </value>
		public static int CharNumberOfBits => 16;

		/// <summary>
		/// Gets the number of bytes needed to represent a char as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bytes.
		/// </value>
		public static int CharNumberOfBytes => 2;

		/// <summary>
		/// Gets the maximum number of characters needed to represent a char as a string.
		/// </summary>
		/// <value>
		/// The maximum number of chars.
		/// </value>
		/// <remarks>
		/// <para>The number of characters needed to represent a particular value is often less than this size.</para>
		/// <para>The string is in cultural invariant English with no commas delimiting size.</para>
		/// </remarks>
		public static int CharMaxNumberOfChars => 1;

		#endregion

		#region Bool

		/// <summary>
		/// Gets the number of bits needed to represent a bool as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bits.
		/// </value>
		/// <remarks>
		/// Although a bool only requires 1 bit to represent it, it will take at least a byte
		/// of memory to represent unless further optimization efforts are taken.
		/// </remarks>
		public static int BoolNumberOfBits => 8;

		/// <summary>
		/// Gets the number of bytes needed to represent a bool as a binary primitive type.
		/// </summary>
		/// <value>
		/// The number of bytes.
		/// </value>
		/// <remarks>
		/// Although a bool only requires 1 bit to represent it, it will take at least a byte
		/// of memory to represent unless further optimization efforts are taken.
		/// </remarks>
		public static int BoolNumberOfBytes => 1;

		/// <summary>
		/// Gets the maximum number of characters needed to represent a bool as a string.
		/// </summary>
		/// <value>
		/// The maximum number of chars.
		/// </value>
		/// <remarks>
		/// <para>The number of characters needed to represent a particular value is often less than this size.</para>
		/// <para>The string is in cultural invariant English with no commas delimiting size.</para>
		/// </remarks>
		public static int BoolMaxNumberOfChars => 5;

		#endregion
	}
}
