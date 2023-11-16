using System.Diagnostics;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Data.Pact.Serialization
{
	/// <summary>
	///
	/// </summary>
	public interface IByteConverter : IEndianSpecific
	{
		byte GetByte(bool value);

		byte GetByte(sbyte value);

		#region GetBytes methods

		/// <summary>
		/// Returns the specified boolean value as an array of bytes.
		/// </summary>
		/// <param name="value">The boolean value to convert.</param>
		/// <returns>An array of bytes with length 1.</returns>
		byte[] GetBytes(bool value);

		/// <summary>
		/// Returns the specified unicode character value as an array of bytes.
		/// </summary>
		/// <param name="value">The character to convert.</param>
		/// <returns>An array of bytes with length 2.</returns>
		byte[] GetBytes(char value);

		/// <summary>
		/// Returns the specified single-precision floating point value as an array of bytes.
		/// </summary>
		/// <param name="value">The number to convert.</param>
		/// <returns>An array of bytes with length 4.</returns>
		byte[] GetBytes(float value);

		/// <summary>
		/// Returns the specified double-precision floating point value as an array of bytes.
		/// </summary>
		/// <param name="value">The number to convert.</param>
		/// <returns>An array of bytes with length 8.</returns>
		byte[] GetBytes(double value);

		byte[] GetBytes(decimal value);

		byte[] GetBytes(byte value);

		byte[] GetBytes(sbyte value);

		/// <summary>
		/// Returns the specified 16-bit signed integer value as an array of bytes.
		/// </summary>
		/// <param name="value">The number to convert.</param>
		/// <returns>An array of bytes with length 2.</returns>
		byte[] GetBytes(short value);

		/// <summary>
		/// Returns the specified 16-bit unsigned integer value as an array of bytes.
		/// </summary>
		/// <param name="value">The number to convert.</param>
		/// <returns>An array of bytes with length 2.</returns>
		byte[] GetBytes(ushort value);

		/// <summary>
		/// Returns the specified 32-bit signed integer value as an array of bytes.
		/// </summary>
		/// <param name="value">The number to convert.</param>
		/// <returns>An array of bytes with length 4.</returns>
		byte[] GetBytes(int value);

		/// <summary>
		/// Returns the specified 32-bit unsigned integer value as an array of bytes.
		/// </summary>
		/// <param name="value">The number to convert.</param>
		/// <returns>An array of bytes with length 4.</returns>
		byte[] GetBytes(uint value);

		/// <summary>
		/// Returns the specified 64-bit signed integer value as an array of bytes.
		/// </summary>
		/// <param name="value">The number to convert.</param>
		/// <returns>An array of bytes with length 8.</returns>
		byte[] GetBytes(long value);

		/// <summary>
		/// Returns the specified 64-bit unsigned integer value as an array of bytes.
		/// </summary>
		/// <param name="value">The number to convert.</param>
		/// <returns>An array of bytes with length 8.</returns>
		byte[] GetBytes(ulong value);

		#endregion

		#region CopyBytes methods

		void CopyBytes(bool value, byte[] buffer, ref int index);

		void CopyBytes(char value, byte[] buffer, ref int index);

		void CopyBytes(float value, byte[] buffer, ref int index);

		void CopyBytes(double value, byte[] buffer, ref int index);

		void CopyBytes(decimal value, byte[] buffer, ref int index);

		void CopyBytes(byte value, byte[] buffer, ref int index);

		void CopyBytes(sbyte value, byte[] buffer, ref int index);

		void CopyBytes(short value, byte[] buffer, ref int index);

		void CopyBytes(ushort value, byte[] buffer, ref int index);

		void CopyBytes(int value, byte[] buffer, ref int index);

		void CopyBytes(uint value, byte[] buffer, ref int index);

		void CopyBytes(long value, byte[] buffer, ref int index);

		void CopyBytes(ulong value, byte[] buffer, ref int index);

		#endregion

		#region To[Type] methods

		/// <summary>
		/// Returns a boolean value converted from 1 byte at a specified position in a byte array.
		/// </summary>
		/// <param name="buffer">An array of bytes.</param>
		/// <param name="index">
		/// The starting position within value. This will be updated by this method to be the index
		/// immediately after the last byte read by this method to produce the converted result.
		/// </param>
		/// <returns>True if the byte at <paramref name="index"/> in value is nonzero; otherwise, false.</returns>
		bool ToBool(byte[] buffer, ref int index);

		/// <summary>
		/// Returns a unicode character converted from 2 bytes at a specified position in a byte array.
		/// </summary>
		/// <param name="buffer">An array of bytes.</param>
		/// <param name="index">
		/// The starting position within value. This will be updated by this method to be the index
		/// immediately after the last byte read by this method to produce the converted result.
		/// </param>
		/// <returns>A character formed by 2 bytes beginning at <paramref name="index"/>.</returns>
		char ToChar(byte[] buffer, ref int index);

		/// <summary>
		/// Returns a single-precision floating point number converted from 4 bytes at a specified position
		/// in a byte array.
		/// </summary>
		/// <param name="buffer">An array of bytes.</param>
		/// <param name="index">
		/// The starting position within value. This will be updated by this method to be the index
		/// immediately after the last byte read by this method to produce the converted result.
		/// </param>
		/// <returns>
		/// A single-precision floating point number formed by 4 bytes beginning at <paramref name="index"/>.
		/// </returns>
		float ToFloat(byte[] buffer, ref int index);

		/// <summary>
		/// Returns a double-precision floating point number converted from 8 bytes at a specified position
		/// in a byte array.
		/// </summary>
		/// <param name="buffer">An array of bytes.</param>
		/// <param name="index">
		/// The starting position within value. This will be updated by this method to be the index
		/// immediately after the last byte read by this method to produce the converted result.
		/// </param>
		/// <returns>
		/// A double precision floating point number formed by 8 bytes beginning at <paramref name="index"/>.
		/// </returns>
		double ToDouble(byte[] buffer, ref int index);

		decimal ToDecimal(byte[] buffer, ref int index);

		byte ToByte(byte[] buffer, ref int index);

		sbyte ToSByte(byte[] buffer, ref int index);

		/// <summary>
		/// Returns a 16-bit signed integer converted from 2 bytes at a specified position in a byte array.
		/// </summary>
		/// <param name="buffer">An array of bytes.</param>
		/// <param name="index">
		/// The starting position within value. This will be updated by this method to be the index
		/// immediately after the last byte read by this method to produce the converted result.
		/// </param>
		/// <returns>A 16-bit signed integer formed by 2 bytes beginning at <paramref name="index"/>.</returns>
		short ToShort(byte[] buffer, ref int index);

		/// <summary>
		/// Returns a 16-bit unsigned integer converted from 2 bytes at a specified position in a byte array.
		/// </summary>
		/// <param name="buffer">An array of bytes.</param>
		/// <param name="index">
		/// The starting position within value. This will be updated by this method to be the index
		/// immediately after the last byte read by this method to produce the converted result.
		/// </param>
		/// <returns>A 16-bit unsigned integer formed by 2 bytes beginning at <paramref name="index"/>.</returns>
		ushort ToUShort(byte[] buffer, ref int index);

		/// <summary>
		/// Returns a 32-bit signed integer converted from 4 bytes at a specified position in a byte array.
		/// </summary>
		/// <param name="buffer">An array of bytes.</param>
		/// <param name="index">
		/// The starting position within value. This will be updated by this method to be the index
		/// immediately after the last byte read by this method to produce the converted result.
		/// </param>
		/// <returns>A 32-bit signed integer formed by 4 bytes beginning at <paramref name="index"/>.</returns>
		int ToInt(byte[] buffer, ref int index);

		/// <summary>
		/// Returns a 32-bit unsigned integer converted from 4 bytes at a specified position in a byte array.
		/// </summary>
		/// <param name="buffer">An array of bytes.</param>
		/// <param name="index">
		/// The starting position within value. This will be updated by this method to be the index
		/// immediately after the last byte read by this method to produce the converted result.
		/// </param>
		/// <returns>A 32-bit unsigned integer formed by 4 bytes beginning at <paramref name="index"/>.</returns>
		uint ToUInt(byte[] buffer, ref int index);

		/// <summary>
		/// Returns a 64-bit signed integer converted from 8 bytes at a specified position in a byte array.
		/// </summary>
		/// <param name="buffer">An array of bytes.</param>
		/// <param name="index">
		/// The starting position within value. This will be updated by this method to be the index
		/// immediately after the last byte read by this method to produce the converted result.
		/// </param>
		/// <returns>A 64-bit signed integer formed by 8 bytes beginning at <paramref name="index"/>.</returns>
		long ToLong(byte[] buffer, ref int index);

		/// <summary>
		/// Returns a 64-bit unsigned integer converted from 8 bytes at a specified position in a byte array.
		/// </summary>
		/// <param name="buffer">An array of bytes.</param>
		/// <param name="index">
		/// The starting position within value. This will be updated by this method to be the index
		/// immediately after the last byte read by this method to produce the converted result.
		/// </param>
		/// <returns>A 64-bit unsigned integer formed by the 8 bytes beginning at <paramref name="index"/>.</returns>
		ulong ToULong(byte[] buffer, ref int index);

		#endregion
	}

	public static class IByteConverterContracts
	{
		#region CopyBytes methods

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyBytes(bool value, byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Bool)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyBytes(char value, byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Char)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyBytes(float value, byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Float)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyBytes(double value, byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Double)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyBytes(decimal value, byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Decimal)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyBytes(byte value, byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Byte)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyBytes(sbyte value, byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.SByte)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyBytes(short value, byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Short)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyBytes(ushort value, byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.UShort)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyBytes(int value, byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Int)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyBytes(uint value, byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.UInt)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyBytes(long value, byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Long)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyBytes(ulong value, byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.ULong)));
		}

		#endregion

		#region To[Type] methods

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ToBool(byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Bool)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ToChar(byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Char)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ToFloat(byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Float)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ToDouble(byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Double)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ToDecimal(byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Decimal)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ToByte(byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Byte)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ToSByte(byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.SByte)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ToShort(byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Short)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ToUShort(byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.UShort)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ToInt(byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Int)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ToUInt(byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.UInt)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ToLong(byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.Long)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ToULong(byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(buffer.GetIndexRange().Contains(Range.FromLength(index, ByteLength.ULong)));
		}

		#endregion
	}
}
