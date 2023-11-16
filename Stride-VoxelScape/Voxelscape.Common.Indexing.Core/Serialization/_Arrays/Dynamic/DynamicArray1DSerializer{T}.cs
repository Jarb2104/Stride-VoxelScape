using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public class DynamicArray1DSerializer<T> : AbstractIndexable1DInlineSerializer<DynamicArray1D<T>, T>
	{
		public DynamicArray1DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer,
			IConstantSerializerDeserializer<Index1D> dimensionsSerializer)
			: base(valueSerializer, new IndexableSerializerOptions<Index1D>(Index1D.Zero, dimensionsSerializer))
		{
		}

		/// <inheritdoc />
		protected override DynamicArray1D<T> Create(Index1D dimensions) => new DynamicArray1D<T>(dimensions);

		/// <inheritdoc />
		protected override Index1D GetDimensions(DynamicArray1D<T> value) => value.CurrentDimensions;

		/// <inheritdoc />
		protected override Index1D GetLowerBounds(DynamicArray1D<T> value) => value.CurrentLowerBounds;
	}
}
