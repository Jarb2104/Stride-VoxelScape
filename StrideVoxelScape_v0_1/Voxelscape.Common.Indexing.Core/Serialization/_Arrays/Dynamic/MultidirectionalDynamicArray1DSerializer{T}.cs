using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public class MultidirectionalDynamicArray1DSerializer<T> :
		AbstractIndexable1DInlineSerializer<MultidirectionalDynamicArray1D<T>, T>
	{
		public MultidirectionalDynamicArray1DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer,
			IConstantSerializerDeserializer<Index1D> indexSerializer)
			: base(valueSerializer, new IndexableSerializerOptions<Index1D>(indexSerializer))
		{
		}

		/// <inheritdoc />
		protected override MultidirectionalDynamicArray1D<T> Create(Index1D dimensions) =>
			new MultidirectionalDynamicArray1D<T>(dimensions);

		/// <inheritdoc />
		protected override Index1D GetDimensions(MultidirectionalDynamicArray1D<T> value) => value.CurrentDimensions;

		/// <inheritdoc />
		protected override Index1D GetLowerBounds(MultidirectionalDynamicArray1D<T> value) => value.CurrentLowerBounds;
	}
}
