using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public abstract class AbstractCompositeConstantSerializer<TValue, T> :
		AbstractCompositeSerializer<TValue, T>, IConstantSerializerDeserializer<TValue>
	{
		public AbstractCompositeConstantSerializer(IConstantSerializerDeserializer<T> serialzier)
			: base(serialzier)
		{
			Contracts.Requires.That(serialzier != null);

			this.SerializedLength = serialzier.SerializedLength;
		}

		/// <inheritdoc />
		public int SerializedLength { get; }

		/// <inheritdoc />
		public sealed override int GetSerializedLength(TValue value) => this.SerializedLength;
	}
}
