using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public abstract class AbstractCompositeSerializer<TValue, T> : ISerializerDeserializer<TValue>
	{
		private readonly ISerializerDeserializer<T> serialzier;

		public AbstractCompositeSerializer(ISerializerDeserializer<T> serialzier)
		{
			Contracts.Requires.That(serialzier != null);

			this.serialzier = serialzier;
		}

		/// <inheritdoc />
		public Endian Endianness => this.serialzier.Endianness;

		/// <inheritdoc />
		public virtual int GetSerializedLength(TValue value)
		{
			ISerializerContracts.GetSerializedLength(value);

			T part = this.DecomposeValue(value);

			return this.serialzier.GetSerializedLength(part);
		}

		/// <inheritdoc />
		public int Serialize(TValue value, Action<byte> writeByte)
		{
			ISerializerContracts.Serialize(value, writeByte);

			T part = this.DecomposeValue(value);

			return this.serialzier.Serialize(part, writeByte);
		}

		/// <inheritdoc />
		public int Serialize(TValue value, byte[] buffer, ref int index)
		{
			ISerializerContracts.Serialize(this, value, buffer, index);

			T part = this.DecomposeValue(value);

			return this.serialzier.Serialize(part, buffer, ref index);
		}

		/// <inheritdoc />
		public TValue Deserialize(IBufferedArray buffer)
		{
			IDeserializerContracts.Deserialize(buffer);

			T part = this.serialzier.Deserialize(buffer);

			return this.ComposeValue(part);
		}

		/// <inheritdoc />
		public TValue Deserialize(byte[] buffer, ref int index)
		{
			IDeserializerContracts.Deserialize(buffer, index);

			T part = this.serialzier.Deserialize(buffer, ref index);

			return this.ComposeValue(part);
		}

		protected abstract TValue ComposeValue(T part);

		protected abstract T DecomposeValue(TValue value);
	}
}
