using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public class DynamicArray2DSerializer<T> : AbstractIndexable2DInlineSerializer<DynamicArray2D<T>, T>
	{
		public DynamicArray2DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer,
			IConstantSerializerDeserializer<Index2D> dimensionsSerializer)
			: base(valueSerializer, new IndexableSerializerOptions<Index2D>(Index2D.Zero, dimensionsSerializer))
		{
		}

		/// <inheritdoc />
		protected override DynamicArray2D<T> Create(Index2D dimensions) => new DynamicArray2D<T>(dimensions);

		/// <inheritdoc />
		protected override Index2D GetDimensions(DynamicArray2D<T> value) => value.CurrentDimensions;

		/// <inheritdoc />
		protected override Index2D GetLowerBounds(DynamicArray2D<T> value) => value.CurrentLowerBounds;
	}
}
