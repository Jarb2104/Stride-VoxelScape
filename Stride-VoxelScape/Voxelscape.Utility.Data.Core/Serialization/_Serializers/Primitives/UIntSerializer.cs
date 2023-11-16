using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class UIntSerializer : IConstantSerializerDeserializer<uint>
	{
		private readonly IByteConverter converter;

		public UIntSerializer(IByteConverter converter)
		{
			Contracts.Requires.That(converter != null);

			this.converter = converter;
		}

		/// <inheritdoc />
		public Endian Endianness => this.converter.Endianness;

		/// <inheritdoc />
		public int SerializedLength => ByteLength.UInt;

		/// <inheritdoc />
		public int GetSerializedLength(uint value) => this.SerializedLength;

		/// <inheritdoc />
		public int Serialize(uint value, byte[] buffer, ref int index)
		{
			ISerializerContracts.Serialize(this, value, buffer, index);

			this.converter.CopyBytes(value, buffer, ref index);
			return this.SerializedLength;
		}

		/// <inheritdoc />
		public int Serialize(uint value, Action<byte> writeByte)
		{
			ISerializerContracts.Serialize(value, writeByte);

			this.converter.WriteBytes(value, writeByte);
			return this.SerializedLength;
		}

		/// <inheritdoc />
		public uint Deserialize(byte[] buffer, ref int index)
		{
			IDeserializerContracts.Deserialize(buffer, index);

			return this.converter.ToUInt(buffer, ref index);
		}

		/// <inheritdoc />
		public uint Deserialize(IBufferedArray buffer)
		{
			IDeserializerContracts.Deserialize(buffer);

			return buffer.NextUInt(this.converter);
		}
	}
}
