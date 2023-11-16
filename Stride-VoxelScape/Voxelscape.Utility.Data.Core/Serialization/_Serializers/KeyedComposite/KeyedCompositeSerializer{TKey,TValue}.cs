using System;
using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class KeyedCompositeSerializer<TKey, TValue> : ISerializerDeserializer<TValue>
		where TValue : IKeyed<TKey>
	{
		private readonly ISerializerDeserializer<TKey> keySerializer;

		private readonly IReadOnlyDictionary<TKey, ISerializerDeserializer<TValue>> serializers;

		public KeyedCompositeSerializer(
			ISerializerDeserializer<TKey> keySerializer,
			IReadOnlyDictionary<TKey, ISerializerDeserializer<TValue>> serializers)
		{
			Contracts.Requires.That(keySerializer != null);
			Contracts.Requires.That(serializers != null);
			Contracts.Requires.That(serializers.Keys.All(key => serializers[key] != null));
			Contracts.Requires.That(
				serializers.Values.Select(value => value.Endianness).AllEqualTo(keySerializer.Endianness));

			this.keySerializer = keySerializer;
			this.serializers = serializers;
		}

		/// <inheritdoc />
		public Endian Endianness => this.keySerializer.Endianness;

		/// <inheritdoc />
		public int GetSerializedLength(TValue value)
		{
			ISerializerContracts.GetSerializedLength(value);

			return this.keySerializer.GetSerializedLength(value.Key)
				+ this.serializers[value.Key].GetSerializedLength(value);
		}

		/// <inheritdoc />
		public int Serialize(TValue value, byte[] buffer, ref int index)
		{
			ISerializerContracts.Serialize(this, value, buffer, index);

			return this.keySerializer.Serialize(value.Key, buffer, ref index)
				+ this.serializers[value.Key].Serialize(value, buffer, ref index);
		}

		/// <inheritdoc />
		public int Serialize(TValue value, Action<byte> writeByte)
		{
			ISerializerContracts.Serialize(value, writeByte);

			return this.keySerializer.Serialize(value.Key, writeByte)
				+ this.serializers[value.Key].Serialize(value, writeByte);
		}

		/// <inheritdoc />
		public TValue Deserialize(IBufferedArray buffer)
		{
			IDeserializerContracts.Deserialize(buffer);

			TKey key = this.keySerializer.Deserialize(buffer);
			return this.serializers[key].Deserialize(buffer);
		}

		/// <inheritdoc />
		public TValue Deserialize(byte[] buffer, ref int index)
		{
			IDeserializerContracts.Deserialize(buffer, index);

			TKey key = this.keySerializer.Deserialize(buffer, ref index);
			return this.serializers[key].Deserialize(buffer, ref index);
		}
	}
}
