using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public class MultidirectionalDynamicArray4DSerializer<T> :
		AbstractIndexable4DInlineSerializer<MultidirectionalDynamicArray4D<T>, T>
	{
		public MultidirectionalDynamicArray4DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer,
			IConstantSerializerDeserializer<Index4D> indexSerializer)
			: base(valueSerializer, new IndexableSerializerOptions<Index4D>(indexSerializer))
		{
		}

		/// <inheritdoc />
		protected override MultidirectionalDynamicArray4D<T> Create(Index4D dimensions) =>
			new MultidirectionalDynamicArray4D<T>(dimensions);

		/// <inheritdoc />
		protected override Index4D GetDimensions(MultidirectionalDynamicArray4D<T> value) => value.CurrentDimensions;

		/// <inheritdoc />
		protected override Index4D GetLowerBounds(MultidirectionalDynamicArray4D<T> value) => value.CurrentLowerBounds;
	}
}
