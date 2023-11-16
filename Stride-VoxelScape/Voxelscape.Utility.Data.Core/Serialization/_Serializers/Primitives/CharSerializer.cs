using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class CharSerializer : IConstantSerializerDeserializer<char>
	{
		private readonly IByteConverter converter;

		public CharSerializer(IByteConverter converter)
		{
			Contracts.Requires.That(converter != null);

			this.converter = converter;
		}

		/// <inheritdoc />
		public Endian Endianness => this.converter.Endianness;

		/// <inheritdoc />
		public int SerializedLength => ByteLength.Char;

		/// <inheritdoc />
		public int GetSerializedLength(char value) => this.SerializedLength;

		/// <inheritdoc />
		public int Serialize(char value, byte[] buffer, ref int index)
		{
			ISerializerContracts.Serialize(this, value, buffer, index);

			this.converter.CopyBytes(value, buffer, ref index);
			return this.SerializedLength;
		}

		/// <inheritdoc />
		public int Serialize(char value, Action<byte> writeByte)
		{
			ISerializerContracts.Serialize(value, writeByte);

			this.converter.WriteBytes(value, writeByte);
			return this.SerializedLength;
		}

		/// <inheritdoc />
		public char Deserialize(byte[] buffer, ref int index)
		{
			IDeserializerContracts.Deserialize(buffer, index);

			return this.converter.ToChar(buffer, ref index);
		}

		/// <inheritdoc />
		public char Deserialize(IBufferedArray buffer)
		{
			IDeserializerContracts.Deserialize(buffer);

			return buffer.NextChar(this.converter);
		}
	}
}
