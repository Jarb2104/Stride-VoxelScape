using Voxelscape.Utility.Common.Pact.Conversions;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class ConverterConstantSerializer<TValue, TSerialize> :
		ConverterSerializer<TValue, TSerialize>, IConstantSerializerDeserializer<TValue>
	{
		public ConverterConstantSerializer(
			ITwoWayConverter<TValue, TSerialize> converter, IConstantSerializerDeserializer<TSerialize> serializer)
			: base(converter, serializer)
		{
			Contracts.Requires.That(converter != null);
			Contracts.Requires.That(serializer != null);

			this.SerializedLength = serializer.SerializedLength;
		}

		/// <inheritdoc />
		public int SerializedLength { get; }
	}
}
