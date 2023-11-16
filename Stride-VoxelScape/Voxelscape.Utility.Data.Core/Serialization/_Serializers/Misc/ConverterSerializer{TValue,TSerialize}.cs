using System;
using Voxelscape.Utility.Common.Pact.Conversions;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class ConverterSerializer<TValue, TSerialize> : ISerializerDeserializer<TValue>
	{
		private readonly ITwoWayConverter<TValue, TSerialize> converter;

		private readonly ISerializerDeserializer<TSerialize> serializer;

		public ConverterSerializer(
			ITwoWayConverter<TValue, TSerialize> converter, ISerializerDeserializer<TSerialize> serializer)
		{
			Contracts.Requires.That(converter != null);
			Contracts.Requires.That(serializer != null);

			this.converter = converter;
			this.serializer = serializer;
		}

		/// <inheritdoc />
		public Endian Endianness => this.serializer.Endianness;

		/// <inheritdoc />
		public int GetSerializedLength(TValue value)
		{
			ISerializerContracts.GetSerializedLength(value);

			return this.serializer.GetSerializedLength(this.converter.Convert(value));
		}

		/// <inheritdoc />
		public int Serialize(TValue value, Action<byte> writeByte)
		{
			ISerializerContracts.Serialize(value, writeByte);

			return this.serializer.Serialize(this.converter.Convert(value), writeByte);
		}

		/// <inheritdoc />
		public int Serialize(TValue value, byte[] buffer, ref int index)
		{
			ISerializerContracts.Serialize(this, value, buffer, index);

			return this.serializer.Serialize(this.converter.Convert(value), buffer, ref index);
		}

		/// <inheritdoc />
		public TValue Deserialize(IBufferedArray buffer)
		{
			IDeserializerContracts.Deserialize(buffer);

			return this.converter.Convert(this.serializer.Deserialize(buffer));
		}

		/// <inheritdoc />
		public TValue Deserialize(byte[] buffer, ref int index)
		{
			IDeserializerContracts.Deserialize(buffer, index);

			return this.converter.Convert(this.serializer.Deserialize(buffer, ref index));
		}
	}
}
