using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public class BoundedIndexable3DSerializer<T> :
		AbstractIndexable3DInlineSerializer<IBoundedIndexable<Index3D, T>, T>
	{
		public BoundedIndexable3DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer,
			IConstantSerializerDeserializer<Index3D> dimensionsSerializer)
			: base(valueSerializer, new IndexableSerializerOptions<Index3D>(Index3D.Zero, dimensionsSerializer))
		{
		}

		public BoundedIndexable3DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer, Index3D dimensions)
			: base(valueSerializer, new IndexableSerializerOptions<Index3D>(Index3D.Zero, dimensions))
		{
		}

		/// <inheritdoc />
		protected override IBoundedIndexable<Index3D, T> Create(Index3D dimensions) => new Array3D<T>(dimensions);

		/// <inheritdoc />
		protected override Index3D GetDimensions(IBoundedIndexable<Index3D, T> value) => value.Dimensions;

		/// <inheritdoc />
		protected override Index3D GetLowerBounds(IBoundedIndexable<Index3D, T> value) => value.LowerBounds;
	}
}
