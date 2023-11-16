using System.Runtime.InteropServices;
using MiscUtil.Conversion;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	/// <summary>
	/// Converts base data types to an array of bytes, and an array of bytes to base data types, in a
	/// platform/architecture independent way. Little and big endian architectures will not change the
	/// results of these methods.
	/// </summary>
	public class ByteConverter : IByteConverter
	{
		private readonly EndianBitConverter converter;

		public ByteConverter(EndianBitConverter converter)
		{
			Contracts.Requires.That(converter != null);

			this.converter = converter;
			this.Endianness = converter.Endianness.ToEndian();
		}

		public static IEndianProvider<ByteConverter> Get { get; } = EndianProvider.New(
			new ByteConverter(EndianBitConverter.Big),
			new ByteConverter(EndianBitConverter.Little));

		/// <inheritdoc />
		public Endian Endianness { get; }

		/// <inheritdoc />
		public byte GetByte(bool value) => value ? (byte)1 : (byte)0;

		/// <inheritdoc />
		public byte GetByte(sbyte value) => new ByteUnion(value).AsByte;

		#region GetBytes methods

		/// <inheritdoc />
		public byte[] GetBytes(bool value) => new byte[] { this.GetByte(value) };

		/// <inheritdoc />
		public byte[] GetBytes(char value) => this.converter.GetBytes(value);

		/// <inheritdoc />
		public byte[] GetBytes(float value) => this.converter.GetBytes(value);

		/// <inheritdoc />
		public byte[] GetBytes(double value) => this.converter.GetBytes(value);

		/// <inheritdoc />
		public byte[] GetBytes(decimal value) => this.converter.GetBytes(value);

		/// <inheritdoc />
		public byte[] GetBytes(byte value) => new byte[] { value };

		/// <inheritdoc />
		public byte[] GetBytes(sbyte value) => new byte[] { this.GetByte(value) };

		/// <inheritdoc />
		public byte[] GetBytes(short value) => this.converter.GetBytes(value);

		/// <inheritdoc />
		public byte[] GetBytes(ushort value) => this.converter.GetBytes(value);

		/// <inheritdoc />
		public byte[] GetBytes(int value) => this.converter.GetBytes(value);

		/// <inheritdoc />
		public byte[] GetBytes(uint value) => this.converter.GetBytes(value);

		/// <inheritdoc />
		public byte[] GetBytes(long value) => this.converter.GetBytes(value);

		/// <inheritdoc />
		public byte[] GetBytes(ulong value) => this.converter.GetBytes(value);

		#endregion

		#region CopyBytes methods

		/// <inheritdoc />
		public void CopyBytes(bool value, byte[] buffer, ref int index)
		{
			IByteConverterContracts.CopyBytes(value, buffer, index);

			buffer[index] = this.GetByte(value);
			index += ByteLength.Bool;
		}

		/// <inheritdoc />
		public void CopyBytes(char value, byte[] buffer, ref int index)
		{
			IByteConverterContracts.CopyBytes(value, buffer, index);

			this.converter.CopyBytes(value, buffer, index);
			index += ByteLength.Char;
		}

		/// <inheritdoc />
		public void CopyBytes(float value, byte[] buffer, ref int index)
		{
			IByteConverterContracts.CopyBytes(value, buffer, index);

			this.converter.CopyBytes(value, buffer, index);
			index += ByteLength.Float;
		}

		/// <inheritdoc />
		public void CopyBytes(double value, byte[] buffer, ref int index)
		{
			IByteConverterContracts.CopyBytes(value, buffer, index);

			this.converter.CopyBytes(value, buffer, index);
			index += ByteLength.Double;
		}

		/// <inheritdoc />
		public void CopyBytes(decimal value, byte[] buffer, ref int index)
		{
			IByteConverterContracts.CopyBytes(value, buffer, index);

			this.converter.CopyBytes(value, buffer, index);
			index += ByteLength.Decimal;
		}

		/// <inheritdoc />
		public void CopyBytes(byte value, byte[] buffer, ref int index)
		{
			IByteConverterContracts.CopyBytes(value, buffer, index);

			buffer[index] = value;
			index += ByteLength.Byte;
		}

		/// <inheritdoc />
		public void CopyBytes(sbyte value, byte[] buffer, ref int index)
		{
			IByteConverterContracts.CopyBytes(value, buffer, index);

			buffer[index] = this.GetByte(value);
			index += ByteLength.SByte;
		}

		/// <inheritdoc />
		public void CopyBytes(short value, byte[] buffer, ref int index)
		{
			IByteConverterContracts.CopyBytes(value, buffer, index);

			this.converter.CopyBytes(value, buffer, index);
			index += ByteLength.Short;
		}

		/// <inheritdoc />
		public void CopyBytes(ushort value, byte[] buffer, ref int index)
		{
			IByteConverterContracts.CopyBytes(value, buffer, index);

			this.converter.CopyBytes(value, buffer, index);
			index += ByteLength.UShort;
		}

		/// <inheritdoc />
		public void CopyBytes(int value, byte[] buffer, ref int index)
		{
			IByteConverterContracts.CopyBytes(value, buffer, index);

			this.converter.CopyBytes(value, buffer, index);
			index += ByteLength.Int;
		}

		/// <inheritdoc />
		public void CopyBytes(uint value, byte[] buffer, ref int index)
		{
			IByteConverterContracts.CopyBytes(value, buffer, index);

			this.converter.CopyBytes(value, buffer, index);
			index += ByteLength.UInt;
		}

		/// <inheritdoc />
		public void CopyBytes(long value, byte[] buffer, ref int index)
		{
			IByteConverterContracts.CopyBytes(value, buffer, index);

			this.converter.CopyBytes(value, buffer, index);
			index += ByteLength.Long;
		}

		/// <inheritdoc />
		public void CopyBytes(ulong value, byte[] buffer, ref int index)
		{
			IByteConverterContracts.CopyBytes(value, buffer, index);

			this.converter.CopyBytes(value, buffer, index);
			index += ByteLength.ULong;
		}

		#endregion

		#region To[Type] methods

		/// <inheritdoc />
		public bool ToBool(byte[] buffer, ref int index)
		{
			IByteConverterContracts.ToBool(buffer, index);

			var result = buffer[index] == 1;
			index += ByteLength.Bool;
			return result;
		}

		/// <inheritdoc />
		public char ToChar(byte[] buffer, ref int index)
		{
			IByteConverterContracts.ToChar(buffer, index);

			var result = this.converter.ToChar(buffer, index);
			index += ByteLength.Char;
			return result;
		}

		/// <inheritdoc />
		public float ToFloat(byte[] buffer, ref int index)
		{
			IByteConverterContracts.ToFloat(buffer, index);

			var result = this.converter.ToSingle(buffer, index);
			index += ByteLength.Float;
			return result;
		}

		/// <inheritdoc />
		public double ToDouble(byte[] buffer, ref int index)
		{
			IByteConverterContracts.ToDouble(buffer, index);

			var result = this.converter.ToDouble(buffer, index);
			index += ByteLength.Double;
			return result;
		}

		/// <inheritdoc />
		public decimal ToDecimal(byte[] buffer, ref int index)
		{
			IByteConverterContracts.ToDecimal(buffer, index);

			var result = this.converter.ToDecimal(buffer, index);
			index += ByteLength.Decimal;
			return result;
		}

		/// <inheritdoc />
		public byte ToByte(byte[] buffer, ref int index)
		{
			IByteConverterContracts.ToByte(buffer, index);

			var result = buffer[index];
			index += ByteLength.Byte;
			return result;
		}

		/// <inheritdoc />
		public sbyte ToSByte(byte[] buffer, ref int index)
		{
			IByteConverterContracts.ToSByte(buffer, index);

			var result = new ByteUnion(buffer[index]).AsSByte;
			index += ByteLength.SByte;
			return result;
		}

		/// <inheritdoc />
		public short ToShort(byte[] buffer, ref int index)
		{
			IByteConverterContracts.ToShort(buffer, index);

			var result = this.converter.ToInt16(buffer, index);
			index += ByteLength.Short;
			return result;
		}

		/// <inheritdoc />
		public ushort ToUShort(byte[] buffer, ref int index)
		{
			IByteConverterContracts.ToUShort(buffer, index);

			var result = this.converter.ToUInt16(buffer, index);
			index += ByteLength.UShort;
			return result;
		}

		/// <inheritdoc />
		public int ToInt(byte[] buffer, ref int index)
		{
			IByteConverterContracts.ToInt(buffer, index);

			var result = this.converter.ToInt32(buffer, index);
			index += ByteLength.Int;
			return result;
		}

		/// <inheritdoc />
		public uint ToUInt(byte[] buffer, ref int index)
		{
			IByteConverterContracts.ToUInt(buffer, index);

			var result = this.converter.ToUInt32(buffer, index);
			index += ByteLength.UInt;
			return result;
		}

		/// <inheritdoc />
		public long ToLong(byte[] buffer, ref int index)
		{
			IByteConverterContracts.ToLong(buffer, index);

			var result = this.converter.ToInt64(buffer, index);
			index += ByteLength.Long;
			return result;
		}

		/// <inheritdoc />
		public ulong ToULong(byte[] buffer, ref int index)
		{
			IByteConverterContracts.ToULong(buffer, index);

			var result = this.converter.ToUInt64(buffer, index);
			index += ByteLength.ULong;
			return result;
		}

		#endregion

		[StructLayout(LayoutKind.Explicit)]
		private struct ByteUnion
		{
			[FieldOffset(0)]
			private readonly byte byteField;

			[FieldOffset(0)]
			private readonly sbyte sbyteField;

			public ByteUnion(byte byteField)
			{
				this.sbyteField = 0; // compiler requires this, must come first not to overwrite real value
				this.byteField = byteField;
			}

			public ByteUnion(sbyte sbyteField)
			{
				this.byteField = 0; // compiler requires this, must come first not to overwrite real value
				this.sbyteField = sbyteField;
			}

			public byte AsByte => this.byteField;

			public sbyte AsSByte => this.sbyteField;
		}
	}
}
