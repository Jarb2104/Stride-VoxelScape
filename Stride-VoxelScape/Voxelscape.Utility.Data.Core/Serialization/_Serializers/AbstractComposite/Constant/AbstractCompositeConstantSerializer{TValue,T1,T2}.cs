using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public abstract class AbstractCompositeConstantSerializer<TValue, T1, T2> :
		AbstractCompositeSerializer<TValue, T1, T2>, IConstantSerializerDeserializer<TValue>
	{
		public AbstractCompositeConstantSerializer(
			IConstantSerializerDeserializer<T1> serialzierT1, IConstantSerializerDeserializer<T2> serialzierT2)
			: base(serialzierT1, serialzierT2)
		{
			Contracts.Requires.That(serialzierT1 != null);
			Contracts.Requires.That(serialzierT2 != null);

			this.SerializedLength = serialzierT1.SerializedLength + serialzierT2.SerializedLength;
		}

		/// <inheritdoc />
		public int SerializedLength { get; }

		/// <inheritdoc />
		public sealed override int GetSerializedLength(TValue value) => this.SerializedLength;
	}
}
