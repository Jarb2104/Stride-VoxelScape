using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	/// <summary>
	///
	/// </summary>
	public class InferCountSerializer : IConstantSerializerDeserializer<int>
	{
		private readonly IConstantSerializedLength enumerableValue;

		public InferCountSerializer(IConstantSerializedLength enumerableValueSerializer)
		{
			Contracts.Requires.That(enumerableValueSerializer != null);
			Contracts.Requires.That(enumerableValueSerializer.SerializedLength >= 1);

			this.enumerableValue = enumerableValueSerializer;
		}

		/// <inheritdoc />
		public Endian Endianness => this.enumerableValue.Endianness;

		/// <inheritdoc />
		public int SerializedLength => 0;

		/// <inheritdoc />
		public int GetSerializedLength(int value) => this.SerializedLength;

		/// <inheritdoc />
		public int Serialize(int value, Action<byte> writeByte)
		{
			ISerializerContracts.Serialize(value, writeByte);

			return this.SerializedLength;
		}

		/// <inheritdoc />
		public int Serialize(int value, byte[] buffer, ref int index)
		{
			ISerializerContracts.Serialize(this, value, buffer, index);

			return this.SerializedLength;
		}

		/// <inheritdoc />
		public int Deserialize(IBufferedArray buffer)
		{
			IDeserializerContracts.Deserialize(buffer);
			Contracts.Requires.That(buffer.TotalRemainingLength.HasValue);
			Contracts.Requires.That(buffer.TotalRemainingLength.Value.IsDivisibleBy(
				this.enumerableValue.SerializedLength));

			return buffer.TotalRemainingLength.Value / this.enumerableValue.SerializedLength;
		}

		/// <inheritdoc />
		public int Deserialize(byte[] buffer, ref int index)
		{
			IDeserializerContracts.Deserialize(buffer, index);
			Contracts.Requires.That((buffer.Length - index).IsDivisibleBy(this.enumerableValue.SerializedLength));

			return (buffer.Length - index) / this.enumerableValue.SerializedLength;
		}
	}
}
