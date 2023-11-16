using System;
using Voxelscape.Common.Indexing.Core.Enumerables;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Utility.Data.Pact.Serialization;
using Index = Voxelscape.Common.Indexing.Core.Enumerables.Index;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public abstract class AbstractIndexable2DInlineSerializer<TIndexable, TValue> :
		AbstractIndexableInlineSerializer<Index2D, TIndexable, TValue>
		where TIndexable : class, IIndexable<Index2D, TValue>
	{
		public AbstractIndexable2DInlineSerializer(
			IConstantSerializerDeserializer<TValue> valueSerializer, IndexableSerializerOptions<Index2D> options)
			: base(valueSerializer, options)
		{
		}

		/// <inheritdoc />
		protected override int SerializeValues(
			TIndexable value, Index2D lowerBounds, Index2D dimensions, Action<byte> writeByte)
		{
			int serializedLength = 0;
			foreach (var valueIndex in Index.Range(lowerBounds, dimensions))
			{
				serializedLength += this.ValueSerializer.Serialize(value[valueIndex], writeByte);
			}

			return serializedLength;
		}

		/// <inheritdoc />
		protected override void SerializeValues(
			TIndexable value, Index2D lowerBounds, Index2D dimensions, byte[] buffer, ref int index)
		{
			foreach (var valueIndex in Index.Range(lowerBounds, dimensions))
			{
				this.ValueSerializer.Serialize(value[valueIndex], buffer, ref index);
			}
		}

		/// <inheritdoc />
		protected override void DeserializeValues(
			Index2D lowerBounds, Index2D dimensions, IBufferedArray buffer, TIndexable result)
		{
			foreach (var resultIndex in Index.Range(lowerBounds, dimensions))
			{
				result[resultIndex] = this.ValueSerializer.Deserialize(buffer);
			}
		}

		/// <inheritdoc />
		protected override void DeserializeValues(
			Index2D lowerBounds, Index2D dimensions, byte[] buffer, ref int index, TIndexable result)
		{
			foreach (var resultIndex in Index.Range(lowerBounds, dimensions))
			{
				result[resultIndex] = this.ValueSerializer.Deserialize(buffer, ref index);
			}
		}
	}
}
