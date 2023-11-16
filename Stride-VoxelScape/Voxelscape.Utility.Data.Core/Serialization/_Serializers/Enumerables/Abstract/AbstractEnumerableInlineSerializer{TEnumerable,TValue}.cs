using System.Collections.Generic;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public abstract class AbstractEnumerableInlineSerializer<TEnumerable, TValue> :
		AbstractEnumerableSerializer<TEnumerable, TValue>, IInlineSerializerDeserializer<TEnumerable>
		where TEnumerable : class, IEnumerable<TValue>
	{
		public AbstractEnumerableInlineSerializer(
			ISerializerDeserializer<TValue> valueSerializer,
			IConstantSerializerDeserializer<int> countSerializer)
			: base(valueSerializer, countSerializer)
		{
		}

		/// <inheritdoc />
		public void DeserializeInline(byte[] buffer, ref int index, TEnumerable result)
		{
			IInlineDeserializerContracts.DeserializeInline(buffer, index, result);

			this.DeserializeValuesInline(
				this.CountSerializer.Deserialize(buffer, ref index), buffer, ref index, result);
		}

		/// <inheritdoc />
		public void DeserializeInline(IBufferedArray buffer, TEnumerable result)
		{
			IInlineDeserializerContracts.DeserializeInline(buffer, result);

			this.DeserializeValuesInline(this.CountSerializer.Deserialize(buffer), buffer, result);
		}

		/// <inheritdoc />
		protected sealed override TEnumerable DeserializeValues(int countOfValues, byte[] buffer, ref int index)
		{
			TEnumerable result = this.Create(countOfValues);
			this.DeserializeValuesInline(countOfValues, buffer, ref index, result);
			return result;
		}

		/// <inheritdoc />
		protected sealed override TEnumerable DeserializeValues(int countOfValues, IBufferedArray buffer)
		{
			TEnumerable result = this.Create(countOfValues);
			this.DeserializeValuesInline(countOfValues, buffer, result);
			return result;
		}

		protected abstract TEnumerable Create(int countOfValues);

		protected abstract void DeserializeValuesInline(
			int countOfValues, byte[] buffer, ref int index, TEnumerable result);

		protected abstract void DeserializeValuesInline(
			int countOfValues, IBufferedArray buffer, TEnumerable result);
	}
}
