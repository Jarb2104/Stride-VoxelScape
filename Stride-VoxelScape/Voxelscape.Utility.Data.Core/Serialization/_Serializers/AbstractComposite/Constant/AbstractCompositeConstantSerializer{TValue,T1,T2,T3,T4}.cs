using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public abstract class AbstractCompositeConstantSerializer<TValue, T1, T2, T3, T4> :
		AbstractCompositeSerializer<TValue, T1, T2, T3, T4>, IConstantSerializerDeserializer<TValue>
	{
		public AbstractCompositeConstantSerializer(
			IConstantSerializerDeserializer<T1> serialzierT1,
			IConstantSerializerDeserializer<T2> serialzierT2,
			IConstantSerializerDeserializer<T3> serialzierT3,
			IConstantSerializerDeserializer<T4> serialzierT4)
			: base(serialzierT1, serialzierT2, serialzierT3, serialzierT4)
		{
			Contracts.Requires.That(serialzierT1 != null);
			Contracts.Requires.That(serialzierT2 != null);
			Contracts.Requires.That(serialzierT3 != null);
			Contracts.Requires.That(serialzierT4 != null);

			this.SerializedLength =
				serialzierT1.SerializedLength +
				serialzierT2.SerializedLength +
				serialzierT3.SerializedLength +
				serialzierT4.SerializedLength;
		}

		/// <inheritdoc />
		public int SerializedLength { get; }

		/// <inheritdoc />
		public sealed override int GetSerializedLength(TValue value) => this.SerializedLength;
	}
}
