using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public class MultidirectionalDynamicArray2DSerializer<T> :
		AbstractIndexable2DInlineSerializer<MultidirectionalDynamicArray2D<T>, T>
	{
		public MultidirectionalDynamicArray2DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer,
			IConstantSerializerDeserializer<Index2D> indexSerializer)
			: base(valueSerializer, new IndexableSerializerOptions<Index2D>(indexSerializer))
		{
		}

		/// <inheritdoc />
		protected override MultidirectionalDynamicArray2D<T> Create(Index2D dimensions) =>
			new MultidirectionalDynamicArray2D<T>(dimensions);

		/// <inheritdoc />
		protected override Index2D GetDimensions(MultidirectionalDynamicArray2D<T> value) => value.CurrentDimensions;

		/// <inheritdoc />
		protected override Index2D GetLowerBounds(MultidirectionalDynamicArray2D<T> value) => value.CurrentLowerBounds;
	}
}
