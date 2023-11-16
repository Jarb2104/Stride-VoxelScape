using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public class DynamicArray3DSerializer<T> : AbstractIndexable3DInlineSerializer<DynamicArray3D<T>, T>
	{
		public DynamicArray3DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer,
			IConstantSerializerDeserializer<Index3D> dimensionsSerializer)
			: base(valueSerializer, new IndexableSerializerOptions<Index3D>(Index3D.Zero, dimensionsSerializer))
		{
		}

		/// <inheritdoc />
		protected override DynamicArray3D<T> Create(Index3D dimensions) => new DynamicArray3D<T>(dimensions);

		/// <inheritdoc />
		protected override Index3D GetDimensions(DynamicArray3D<T> value) => value.CurrentDimensions;

		/// <inheritdoc />
		protected override Index3D GetLowerBounds(DynamicArray3D<T> value) => value.CurrentLowerBounds;
	}
}
