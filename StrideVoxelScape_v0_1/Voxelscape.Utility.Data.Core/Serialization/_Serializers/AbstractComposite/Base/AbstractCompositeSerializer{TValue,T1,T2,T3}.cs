using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public abstract class AbstractCompositeSerializer<TValue, T1, T2, T3> : ISerializerDeserializer<TValue>
	{
		private readonly ISerializerDeserializer<T1> serialzierT1;

		private readonly ISerializerDeserializer<T2> serialzierT2;

		private readonly ISerializerDeserializer<T3> serialzierT3;

		public AbstractCompositeSerializer(
			ISerializerDeserializer<T1> serialzierT1,
			ISerializerDeserializer<T2> serialzierT2,
			ISerializerDeserializer<T3> serialzierT3)
		{
			Contracts.Requires.That(serialzierT1 != null);
			Contracts.Requires.That(serialzierT2 != null);
			Contracts.Requires.That(serialzierT3 != null);
			Contracts.Requires.That(EndianUtilities.AllSameEndianness(serialzierT1, serialzierT2, serialzierT3));

			this.serialzierT1 = serialzierT1;
			this.serialzierT2 = serialzierT2;
			this.serialzierT3 = serialzierT3;
		}

		/// <inheritdoc />
		public Endian Endianness => this.serialzierT1.Endianness;

		/// <inheritdoc />
		public virtual int GetSerializedLength(TValue value)
		{
			ISerializerContracts.GetSerializedLength(value);

			T1 part1;
			T2 part2;
			T3 part3;
			this.DecomposeValue(value, out part1, out part2, out part3);

			return this.serialzierT1.GetSerializedLength(part1)
				+ this.serialzierT2.GetSerializedLength(part2)
				+ this.serialzierT3.GetSerializedLength(part3);
		}

		/// <inheritdoc />
		public int Serialize(TValue value, Action<byte> writeByte)
		{
			ISerializerContracts.Serialize(value, writeByte);

			T1 part1;
			T2 part2;
			T3 part3;
			this.DecomposeValue(value, out part1, out part2, out part3);

			return this.serialzierT1.Serialize(part1, writeByte)
				+ this.serialzierT2.Serialize(part2, writeByte)
				+ this.serialzierT3.Serialize(part3, writeByte);
		}

		/// <inheritdoc />
		public int Serialize(TValue value, byte[] buffer, ref int index)
		{
			ISerializerContracts.Serialize(this, value, buffer, index);

			T1 part1;
			T2 part2;
			T3 part3;
			this.DecomposeValue(value, out part1, out part2, out part3);

			return this.serialzierT1.Serialize(part1, buffer, ref index)
				+ this.serialzierT2.Serialize(part2, buffer, ref index)
				+ this.serialzierT3.Serialize(part3, buffer, ref index);
		}

		/// <inheritdoc />
		public TValue Deserialize(IBufferedArray buffer)
		{
			IDeserializerContracts.Deserialize(buffer);

			T1 part1 = this.serialzierT1.Deserialize(buffer);
			T2 part2 = this.serialzierT2.Deserialize(buffer);
			T3 part3 = this.serialzierT3.Deserialize(buffer);

			return this.ComposeValue(part1, part2, part3);
		}

		/// <inheritdoc />
		public TValue Deserialize(byte[] buffer, ref int index)
		{
			IDeserializerContracts.Deserialize(buffer, index);

			T1 part1 = this.serialzierT1.Deserialize(buffer, ref index);
			T2 part2 = this.serialzierT2.Deserialize(buffer, ref index);
			T3 part3 = this.serialzierT3.Deserialize(buffer, ref index);

			return this.ComposeValue(part1, part2, part3);
		}

		protected abstract TValue ComposeValue(T1 part1, T2 part2, T3 part3);

		protected abstract void DecomposeValue(TValue value, out T1 part1, out T2 part2, out T3 part3);
	}
}
