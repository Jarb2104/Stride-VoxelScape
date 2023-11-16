using System;
using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public abstract class AbstractEnumerableSerializer<TEnumerable, TValue> :
		ISerializerDeserializer<TEnumerable>
		where TEnumerable : IEnumerable<TValue>
	{
		public AbstractEnumerableSerializer(
			ISerializerDeserializer<TValue> valueSerializer,
			IConstantSerializerDeserializer<int> countSerializer)
		{
			Contracts.Requires.That(valueSerializer != null);
			Contracts.Requires.That(countSerializer != null);
			Contracts.Requires.That(EndianUtilities.AllSameEndianness(valueSerializer, countSerializer));

			this.ValueSerializer = valueSerializer;
			this.CountSerializer = countSerializer;
		}

		/// <inheritdoc />
		public Endian Endianness => this.ValueSerializer.Endianness;

		protected IConstantSerializerDeserializer<int> CountSerializer { get; }

		protected ISerializerDeserializer<TValue> ValueSerializer { get; }

		/// <inheritdoc />
		public virtual int GetSerializedLength(TEnumerable value)
		{
			ISerializerContracts.GetSerializedLength(value);

			int serializedLength = this.CountSerializer.SerializedLength;
			foreach (var enumeratedValue in value)
			{
				serializedLength += this.ValueSerializer.GetSerializedLength(enumeratedValue);
			}

			return serializedLength;
		}

		/// <inheritdoc />
		public int Serialize(TEnumerable value, byte[] buffer, ref int index)
		{
			ISerializerContracts.Serialize(this, value, buffer, index);

			int originalArrayIndex = index;
			this.CountSerializer.Serialize(this.GetCount(value), buffer, ref index);

			SerializeEnumerable.SerializeValues(this.ValueSerializer, value, buffer, ref index);
			return index - originalArrayIndex;
		}

		/// <inheritdoc />
		public int Serialize(TEnumerable value, Action<byte> writeByte)
		{
			ISerializerContracts.Serialize(value, writeByte);

			int serializedLength = this.CountSerializer.Serialize(this.GetCount(value), writeByte);

			return SerializeEnumerable.SerializeValues(this.ValueSerializer, value, writeByte)
				+ serializedLength;
		}

		/// <inheritdoc />
		public TEnumerable Deserialize(byte[] buffer, ref int index)
		{
			IDeserializerContracts.Deserialize(buffer, index);

			return this.DeserializeValues(this.CountSerializer.Deserialize(buffer, ref index), buffer, ref index);
		}

		/// <inheritdoc />
		public TEnumerable Deserialize(IBufferedArray buffer)
		{
			IDeserializerContracts.Deserialize(buffer);

			return this.DeserializeValues(this.CountSerializer.Deserialize(buffer), buffer);
		}

		protected virtual int GetCount(TEnumerable value) => value.Count();

		protected abstract TEnumerable DeserializeValues(int countOfValues, byte[] buffer, ref int index);

		protected abstract TEnumerable DeserializeValues(int countOfValues, IBufferedArray buffer);
	}
}
