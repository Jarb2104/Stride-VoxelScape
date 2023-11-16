using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public class BoundedIndexable2DSerializer<T> :
		AbstractIndexable2DInlineSerializer<IBoundedIndexable<Index2D, T>, T>
	{
		public BoundedIndexable2DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer,
			IConstantSerializerDeserializer<Index2D> dimensionsSerializer)
			: base(valueSerializer, new IndexableSerializerOptions<Index2D>(Index2D.Zero, dimensionsSerializer))
		{
		}

		public BoundedIndexable2DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer, Index2D dimensions)
			: base(valueSerializer, new IndexableSerializerOptions<Index2D>(Index2D.Zero, dimensions))
		{
		}

		/// <inheritdoc />
		protected override IBoundedIndexable<Index2D, T> Create(Index2D dimensions) => new Array2D<T>(dimensions);

		/// <inheritdoc />
		protected override Index2D GetDimensions(IBoundedIndexable<Index2D, T> value) => value.Dimensions;

		/// <inheritdoc />
		protected override Index2D GetLowerBounds(IBoundedIndexable<Index2D, T> value) => value.LowerBounds;
	}
}
