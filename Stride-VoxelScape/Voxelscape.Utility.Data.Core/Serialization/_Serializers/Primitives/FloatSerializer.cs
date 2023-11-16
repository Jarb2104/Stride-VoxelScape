using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class FloatSerializer : IConstantSerializerDeserializer<float>
	{
		private readonly IByteConverter converter;

		public FloatSerializer(IByteConverter converter)
		{
			Contracts.Requires.That(converter != null);

			this.converter = converter;
		}

		/// <inheritdoc />
		public Endian Endianness => this.converter.Endianness;

		/// <inheritdoc />
		public int SerializedLength => ByteLength.Float;

		/// <inheritdoc />
		public int GetSerializedLength(float value) => this.SerializedLength;

		/// <inheritdoc />
		public int Serialize(float value, byte[] buffer, ref int index)
		{
			ISerializerContracts.Serialize(this, value, buffer, index);

			this.converter.CopyBytes(value, buffer, ref index);
			return this.SerializedLength;
		}

		/// <inheritdoc />
		public int Serialize(float value, Action<byte> writeByte)
		{
			ISerializerContracts.Serialize(value, writeByte);

			this.converter.WriteBytes(value, writeByte);
			return this.SerializedLength;
		}

		/// <inheritdoc />
		public float Deserialize(byte[] buffer, ref int index)
		{
			IDeserializerContracts.Deserialize(buffer, index);

			return this.converter.ToFloat(buffer, ref index);
		}

		/// <inheritdoc />
		public float Deserialize(IBufferedArray buffer)
		{
			IDeserializerContracts.Deserialize(buffer);

			return buffer.NextFloat(this.converter);
		}
	}
}
