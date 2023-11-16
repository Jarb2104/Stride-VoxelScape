using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class ShortSerializer : IConstantSerializerDeserializer<short>
	{
		private readonly IByteConverter converter;

		public ShortSerializer(IByteConverter converter)
		{
			Contracts.Requires.That(converter != null);

			this.converter = converter;
		}

		/// <inheritdoc />
		public Endian Endianness => this.converter.Endianness;

		/// <inheritdoc />
		public int SerializedLength => ByteLength.Short;

		/// <inheritdoc />
		public int GetSerializedLength(short value) => this.SerializedLength;

		/// <inheritdoc />
		public int Serialize(short value, byte[] buffer, ref int index)
		{
			ISerializerContracts.Serialize(this, value, buffer, index);

			this.converter.CopyBytes(value, buffer, ref index);
			return this.SerializedLength;
		}

		/// <inheritdoc />
		public int Serialize(short value, Action<byte> writeByte)
		{
			ISerializerContracts.Serialize(value, writeByte);

			this.converter.WriteBytes(value, writeByte);
			return this.SerializedLength;
		}

		/// <inheritdoc />
		public short Deserialize(byte[] buffer, ref int index)
		{
			IDeserializerContracts.Deserialize(buffer, index);

			return this.converter.ToShort(buffer, ref index);
		}

		/// <inheritdoc />
		public short Deserialize(IBufferedArray buffer)
		{
			IDeserializerContracts.Deserialize(buffer);

			return buffer.NextShort(this.converter);
		}
	}
}
