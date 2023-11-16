using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public class Array4DSerializer<T> : AbstractIndexable4DInlineSerializer<Array4D<T>, T>
	{
		public Array4DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer,
			IConstantSerializerDeserializer<Index4D> dimensionsSerializer)
			: base(valueSerializer, new IndexableSerializerOptions<Index4D>(Index4D.Zero, dimensionsSerializer))
		{
		}

		public Array4DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer, Index4D dimensions)
			: base(valueSerializer, new IndexableSerializerOptions<Index4D>(Index4D.Zero, dimensions))
		{
		}

		/// <inheritdoc />
		protected override Array4D<T> Create(Index4D dimensions) => new Array4D<T>(dimensions);

		/// <inheritdoc />
		protected override Index4D GetDimensions(Array4D<T> value) => value.Dimensions;

		/// <inheritdoc />
		protected override Index4D GetLowerBounds(Array4D<T> value) => value.LowerBounds;
	}
}
