using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public abstract class AbstractCompositeConstantSerializer<TValue, T1, T2, T3> :
		AbstractCompositeSerializer<TValue, T1, T2, T3>, IConstantSerializerDeserializer<TValue>
	{
		public AbstractCompositeConstantSerializer(
			IConstantSerializerDeserializer<T1> serialzierT1,
			IConstantSerializerDeserializer<T2> serialzierT2,
			IConstantSerializerDeserializer<T3> serialzierT3)
			: base(serialzierT1, serialzierT2, serialzierT3)
		{
			Contracts.Requires.That(serialzierT1 != null);
			Contracts.Requires.That(serialzierT2 != null);
			Contracts.Requires.That(serialzierT3 != null);

			this.SerializedLength =
				serialzierT1.SerializedLength +
				serialzierT2.SerializedLength +
				serialzierT3.SerializedLength;
		}

		/// <inheritdoc />
		public int SerializedLength { get; }

		/// <inheritdoc />
		public sealed override int GetSerializedLength(TValue value) => this.SerializedLength;
	}
}
