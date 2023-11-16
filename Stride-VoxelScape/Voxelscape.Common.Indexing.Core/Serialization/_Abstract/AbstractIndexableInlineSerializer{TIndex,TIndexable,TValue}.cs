using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public abstract class AbstractIndexableInlineSerializer<TIndex, TIndexable, TValue> :
		AbstractIndexableSerializer<TIndex, TIndexable, TValue>, IInlineSerializerDeserializer<TIndexable>
		where TIndex : IIndex
		where TIndexable : class, IIndexable<TIndex, TValue>
	{
		public AbstractIndexableInlineSerializer(
			IConstantSerializerDeserializer<TValue> valueSerializer, IndexableSerializerOptions<TIndex> options)
			: base(valueSerializer, options)
		{
		}

		/// <inheritdoc />
		public void DeserializeInline(byte[] buffer, ref int index, TIndexable result)
		{
			IInlineDeserializerContracts.DeserializeInline(buffer, index, result);

			TIndex lowerBounds = this.LowerBoundsSerializer.Deserialize(buffer, ref index);
			TIndex dimensions = this.DimensionsSerializer.Deserialize(buffer, ref index);
			this.DeserializeValues(lowerBounds, dimensions, buffer, ref index, result);
		}

		/// <inheritdoc />
		public void DeserializeInline(IBufferedArray buffer, TIndexable result)
		{
			IInlineDeserializerContracts.DeserializeInline(buffer, result);

			TIndex lowerBounds = this.LowerBoundsSerializer.Deserialize(buffer);
			TIndex dimensions = this.DimensionsSerializer.Deserialize(buffer);
			this.DeserializeValues(lowerBounds, dimensions, buffer, result);
		}

		/// <inheritdoc />
		protected sealed override TIndexable DeserializeValues(
			TIndex lowerBounds, TIndex dimensions, byte[] buffer, ref int index)
		{
			TIndexable result = this.Create(dimensions);
			this.DeserializeValues(lowerBounds, dimensions, buffer, ref index, result);
			return result;
		}

		/// <inheritdoc />
		protected sealed override TIndexable DeserializeValues(
			TIndex lowerBounds, TIndex dimensions, IBufferedArray buffer)
		{
			TIndexable result = this.Create(dimensions);
			this.DeserializeValues(lowerBounds, dimensions, buffer, result);
			return result;
		}

		protected abstract TIndexable Create(TIndex dimensions);

		protected abstract void DeserializeValues(
			TIndex lowerBounds, TIndex dimensions, byte[] buffer, ref int index, TIndexable result);

		protected abstract void DeserializeValues(
			TIndex lowerBounds, TIndex dimensions, IBufferedArray buffer, TIndexable result);
	}
}
