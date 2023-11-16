using System.Collections.Generic;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public abstract class AbstractConstantEnumerableInlineSerializer<TEnumerable, TValue> :
		AbstractEnumerableInlineSerializer<TEnumerable, TValue>, IInlineSerializerDeserializer<TEnumerable>
		where TEnumerable : class, IEnumerable<TValue>
	{
		public AbstractConstantEnumerableInlineSerializer(
			IConstantSerializerDeserializer<TValue> valueSerializer,
			IConstantSerializerDeserializer<int> countSerializer)
			: base(valueSerializer, countSerializer)
		{
		}

		protected new IConstantSerializerDeserializer<TValue> ValueSerializer =>
			(IConstantSerializerDeserializer<TValue>)base.ValueSerializer;

		/// <inheritdoc />
		public override int GetSerializedLength(TEnumerable value)
		{
			ISerializerContracts.GetSerializedLength(value);

			return this.CountSerializer.SerializedLength
				+ (this.GetCount(value) * this.ValueSerializer.SerializedLength);
		}
	}
}
