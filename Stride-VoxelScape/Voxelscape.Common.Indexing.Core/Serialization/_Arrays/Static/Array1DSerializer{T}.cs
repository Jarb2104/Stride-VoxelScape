using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public class Array1DSerializer<T> : AbstractIndexable1DInlineSerializer<Array1D<T>, T>
	{
		public Array1DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer,
			IConstantSerializerDeserializer<Index1D> dimensionsSerializer)
			: base(valueSerializer, new IndexableSerializerOptions<Index1D>(Index1D.Zero, dimensionsSerializer))
		{
		}

		public Array1DSerializer(
			IConstantSerializerDeserializer<T> valueSerializer, Index1D dimensions)
			: base(valueSerializer, new IndexableSerializerOptions<Index1D>(Index1D.Zero, dimensions))
		{
		}

		/// <inheritdoc />
		protected override Array1D<T> Create(Index1D dimensions) => new Array1D<T>(dimensions);

		/// <inheritdoc />
		protected override Index1D GetDimensions(Array1D<T> value) => value.Dimensions;

		/// <inheritdoc />
		protected override Index1D GetLowerBounds(Array1D<T> value) => value.LowerBounds;
	}
}
