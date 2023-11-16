using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	/// <summary>
	/// A serializer that doesn't actually serialize a value.
	/// Instead it always returns the same constant value when deserializing.
	/// </summary>
	/// <typeparam name="T">The type of value to serialize.</typeparam>
	public class FixedConstantSerializer<T> : IConstantSerializerDeserializer<T>
	{
		public FixedConstantSerializer(T constantValue, Endian endianness)
		{
			this.ConstantValue = constantValue;
			this.Endianness = endianness;
		}

		public T ConstantValue { get; }

		/// <inheritdoc />
		public Endian Endianness { get; }

		/// <inheritdoc />
		public int SerializedLength => 0;

		/// <inheritdoc />
		public int GetSerializedLength(T value)
		{
			ISerializerContracts.GetSerializedLength(value);

			return this.SerializedLength;
		}

		/// <inheritdoc />
		public int Serialize(T value, Action<byte> writeByte)
		{
			ISerializerContracts.Serialize(value, writeByte);
			Contracts.Requires.That(value.Equals(this.ConstantValue));

			return this.SerializedLength;
		}

		/// <inheritdoc />
		public int Serialize(T value, byte[] buffer, ref int index)
		{
			ISerializerContracts.Serialize(this, value, buffer, index);
			Contracts.Requires.That(value.Equals(this.ConstantValue));

			return this.SerializedLength;
		}

		/// <inheritdoc />
		public T Deserialize(IBufferedArray buffer)
		{
			IDeserializerContracts.Deserialize(buffer);

			return this.ConstantValue;
		}

		/// <inheritdoc />
		public T Deserialize(byte[] buffer, ref int index)
		{
			IDeserializerContracts.Deserialize(buffer, index);

			return this.ConstantValue;
		}
	}
}
