using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public class MultidirectionalDynamicArray3DSerializer<T> :
		AbstractIndexable3DInlineSerializer<MultidirectionalDynamicArray3D<T>, T>
	{
		public MultidirectionalDynamicArray3DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer,
			IConstantSerializerDeserializer<Index3D> indexSerializer)
			: base(valueSerializer, new IndexableSerializerOptions<Index3D>(indexSerializer))
		{
		}

		/// <inheritdoc />
		protected override MultidirectionalDynamicArray3D<T> Create(Index3D dimensions) =>
			new MultidirectionalDynamicArray3D<T>(dimensions);

		/// <inheritdoc />
		protected override Index3D GetDimensions(MultidirectionalDynamicArray3D<T> value) => value.CurrentDimensions;

		/// <inheritdoc />
		protected override Index3D GetLowerBounds(MultidirectionalDynamicArray3D<T> value) => value.CurrentLowerBounds;
	}
}
