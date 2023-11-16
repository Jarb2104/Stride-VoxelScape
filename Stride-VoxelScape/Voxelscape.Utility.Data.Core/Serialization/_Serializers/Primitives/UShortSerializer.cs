using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class UShortSerializer : IConstantSerializerDeserializer<ushort>
	{
		private readonly IByteConverter converter;

		public UShortSerializer(IByteConverter converter)
		{
			Contracts.Requires.That(converter != null);

			this.converter = converter;
		}

		/// <inheritdoc />
		public Endian Endianness => this.converter.Endianness;

		/// <inheritdoc />
		public int SerializedLength => ByteLength.UShort;

		/// <inheritdoc />
		public int GetSerializedLength(ushort value) => this.SerializedLength;

		/// <inheritdoc />
		public int Serialize(ushort value, byte[] buffer, ref int index)
		{
			ISerializerContracts.Serialize(this, value, buffer, index);

			this.converter.CopyBytes(value, buffer, ref index);
			return this.SerializedLength;
		}

		/// <inheritdoc />
		public int Serialize(ushort value, Action<byte> writeByte)
		{
			ISerializerContracts.Serialize(value, writeByte);

			this.converter.WriteBytes(value, writeByte);
			return this.SerializedLength;
		}

		/// <inheritdoc />
		public ushort Deserialize(byte[] buffer, ref int index)
		{
			IDeserializerContracts.Deserialize(buffer, index);

			return this.converter.ToUShort(buffer, ref index);
		}

		/// <inheritdoc />
		public ushort Deserialize(IBufferedArray buffer)
		{
			IDeserializerContracts.Deserialize(buffer);

			return buffer.NextUShort(this.converter);
		}
	}
}
