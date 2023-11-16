using System;
using Voxelscape.Common.Indexing.Core.Enumerables;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Utility.Data.Pact.Serialization;
using Index = Voxelscape.Common.Indexing.Core.Enumerables.Index;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public abstract class AbstractIndexable4DInlineSerializer<TIndexable, TValue> :
		AbstractIndexableInlineSerializer<Index4D, TIndexable, TValue>
		where TIndexable : class, IIndexable<Index4D, TValue>
	{
		public AbstractIndexable4DInlineSerializer(
			IConstantSerializerDeserializer<TValue> valueSerializer, IndexableSerializerOptions<Index4D> options)
			: base(valueSerializer, options)
		{
		}

		/// <inheritdoc />
		protected override int SerializeValues(
			TIndexable value, Index4D lowerBounds, Index4D dimensions, Action<byte> writeByte)
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
			TIndexable value, Index4D lowerBounds, Index4D dimensions, byte[] buffer, ref int index)
		{
			foreach (var valueIndex in Index.Range(lowerBounds, dimensions))
			{
				this.ValueSerializer.Serialize(value[valueIndex], buffer, ref index);
			}
		}

		/// <inheritdoc />
		protected override void DeserializeValues(
			Index4D lowerBounds, Index4D dimensions, IBufferedArray buffer, TIndexable result)
		{
			foreach (var resultIndex in Index.Range(lowerBounds, dimensions))
			{
				result[resultIndex] = this.ValueSerializer.Deserialize(buffer);
			}
		}

		/// <inheritdoc />
		protected override void DeserializeValues(
			Index4D lowerBounds, Index4D dimensions, byte[] buffer, ref int index, TIndexable result)
		{
			foreach (var resultIndex in Index.Range(lowerBounds, dimensions))
			{
				result[resultIndex] = this.ValueSerializer.Deserialize(buffer, ref index);
			}
		}
	}
}
