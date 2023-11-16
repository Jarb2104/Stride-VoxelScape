using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public class DynamicArray4DSerializer<T> : AbstractIndexable4DInlineSerializer<DynamicArray4D<T>, T>
	{
		public DynamicArray4DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer,
			IConstantSerializerDeserializer<Index4D> dimensionsSerializer)
			: base(valueSerializer, new IndexableSerializerOptions<Index4D>(Index4D.Zero, dimensionsSerializer))
		{
		}

		/// <inheritdoc />
		protected override DynamicArray4D<T> Create(Index4D dimensions) => new DynamicArray4D<T>(dimensions);

		/// <inheritdoc />
		protected override Index4D GetDimensions(DynamicArray4D<T> value) => value.CurrentDimensions;

		/// <inheritdoc />
		protected override Index4D GetLowerBounds(DynamicArray4D<T> value) => value.CurrentLowerBounds;
	}
}
