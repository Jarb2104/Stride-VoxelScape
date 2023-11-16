using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public class Array3DSerializer<T> : AbstractIndexable3DInlineSerializer<Array3D<T>, T>
	{
		public Array3DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer,
			IConstantSerializerDeserializer<Index3D> dimensionsSerializer)
			: base(valueSerializer, new IndexableSerializerOptions<Index3D>(Index3D.Zero, dimensionsSerializer))
		{
		}

		public Array3DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer, Index3D dimensions)
			: base(valueSerializer, new IndexableSerializerOptions<Index3D>(Index3D.Zero, dimensions))
		{
		}

		/// <inheritdoc />
		protected override Array3D<T> Create(Index3D dimensions) => new Array3D<T>(dimensions);

		/// <inheritdoc />
		protected override Index3D GetDimensions(Array3D<T> value) => value.Dimensions;

		/// <inheritdoc />
		protected override Index3D GetLowerBounds(Array3D<T> value) => value.LowerBounds;
	}
}
