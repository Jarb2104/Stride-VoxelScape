using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public class IndexableSerializerOptions<TIndex>
		where TIndex : IIndex
	{
		public IndexableSerializerOptions(IConstantSerializerDeserializer<TIndex> indexSerializer)
		{
			Contracts.Requires.That(indexSerializer != null);

			this.LowerBoundsSerializer = indexSerializer;
			this.DimensionsSerializer = indexSerializer;
			this.ConstantLowerBounds = TryValue.None<TIndex>();
			this.ConstantDimensions = TryValue.None<TIndex>();
		}

		public IndexableSerializerOptions(
			IConstantSerializerDeserializer<TIndex> lowerBoundsSerializer, TIndex dimensions)
		{
			Contracts.Requires.That(lowerBoundsSerializer != null);
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			this.LowerBoundsSerializer = lowerBoundsSerializer;
			this.DimensionsSerializer = new FixedConstantSerializer<TIndex>(
				dimensions, lowerBoundsSerializer.Endianness);
			this.ConstantLowerBounds = TryValue.None<TIndex>();
			this.ConstantDimensions = TryValue.New(dimensions);
		}

		public IndexableSerializerOptions(
			TIndex lowerBounds, IConstantSerializerDeserializer<TIndex> dimensionsSerializer)
		{
			Contracts.Requires.That(dimensionsSerializer != null);

			this.LowerBoundsSerializer = new FixedConstantSerializer<TIndex>(
				lowerBounds, dimensionsSerializer.Endianness);
			this.DimensionsSerializer = dimensionsSerializer;
			this.ConstantLowerBounds = TryValue.New(lowerBounds);
			this.ConstantDimensions = TryValue.None<TIndex>();
		}

		public IndexableSerializerOptions(TIndex lowerBounds, TIndex dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			this.LowerBoundsSerializer = new FixedConstantSerializer<TIndex>(lowerBounds, Endian.Little);
			this.DimensionsSerializer = new FixedConstantSerializer<TIndex>(dimensions, Endian.Little);
			this.ConstantLowerBounds = TryValue.New(lowerBounds);
			this.ConstantDimensions = TryValue.New(dimensions);
		}

		public IConstantSerializerDeserializer<TIndex> LowerBoundsSerializer { get; }

		public IConstantSerializerDeserializer<TIndex> DimensionsSerializer { get; }

		public TryValue<TIndex> ConstantLowerBounds { get; }

		public TryValue<TIndex> ConstantDimensions { get; }

		public bool EndiannessMatches(Endian endianness)
		{
			if (!this.ConstantLowerBounds.HasValue && this.LowerBoundsSerializer.Endianness != endianness)
			{
				return false;
			}

			if (!this.ConstantDimensions.HasValue && this.DimensionsSerializer.Endianness != endianness)
			{
				return false;
			}

			return true;
		}
	}
}
