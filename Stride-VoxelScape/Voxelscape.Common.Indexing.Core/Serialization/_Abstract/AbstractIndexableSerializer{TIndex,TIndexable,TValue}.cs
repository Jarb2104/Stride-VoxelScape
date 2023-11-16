using System;
using System.Diagnostics;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	public abstract class AbstractIndexableSerializer<TIndex, TIndexable, TValue> :
		ISerializerDeserializer<TIndexable>
		where TIndex : IIndex
		where TIndexable : IIndexable<TIndex, TValue>
	{
		private readonly IndexableSerializerOptions<TIndex> options;

		public AbstractIndexableSerializer(
			IConstantSerializerDeserializer<TValue> valueSerializer, IndexableSerializerOptions<TIndex> options)
		{
			Contracts.Requires.That(valueSerializer != null);
			Contracts.Requires.That(options != null);
			Contracts.Requires.That(options.EndiannessMatches(valueSerializer.Endianness));

			this.ValueSerializer = valueSerializer;
			this.options = options;
		}

		/// <inheritdoc />
		public Endian Endianness => this.ValueSerializer.Endianness;

		protected IConstantSerializerDeserializer<TIndex> LowerBoundsSerializer => this.options.LowerBoundsSerializer;

		protected IConstantSerializerDeserializer<TIndex> DimensionsSerializer => this.options.DimensionsSerializer;

		protected IConstantSerializerDeserializer<TValue> ValueSerializer { get; }

		/// <inheritdoc />
		public int GetSerializedLength(TIndexable value)
		{
			ISerializerContracts.GetSerializedLength(value);
			this.AdditionalSerializeContracts(value);

			int result = this.LowerBoundsSerializer.SerializedLength;
			result += this.DimensionsSerializer.SerializedLength;
			result += this.GetDimensions(value).MultiplyCoordinates() * this.ValueSerializer.SerializedLength;
			return result;
		}

		/// <inheritdoc />
		public int Serialize(TIndexable value, byte[] buffer, ref int index)
		{
			ISerializerContracts.Serialize(this, value, buffer, index);
			this.AdditionalSerializeContracts(value);

			int originalArrayIndex = index;
			TIndex lowerBounds = this.GetLowerBounds(value);
			TIndex dimensions = this.GetDimensions(value);

			this.LowerBoundsSerializer.Serialize(lowerBounds, buffer, ref index);
			this.DimensionsSerializer.Serialize(dimensions, buffer, ref index);
			this.SerializeValues(value, lowerBounds, dimensions, buffer, ref index);
			return index - originalArrayIndex;
		}

		/// <inheritdoc />
		public int Serialize(TIndexable value, Action<byte> writeByte)
		{
			ISerializerContracts.Serialize(value, writeByte);
			this.AdditionalSerializeContracts(value);

			int serializedLength = 0;
			TIndex lowerBounds = this.GetLowerBounds(value);
			TIndex dimensions = this.GetDimensions(value);

			serializedLength += this.LowerBoundsSerializer.Serialize(lowerBounds, writeByte);
			serializedLength += this.DimensionsSerializer.Serialize(dimensions, writeByte);
			serializedLength += this.SerializeValues(value, lowerBounds, dimensions, writeByte);
			return serializedLength;
		}

		/// <inheritdoc />
		public TIndexable Deserialize(byte[] buffer, ref int index)
		{
			IDeserializerContracts.Deserialize(buffer, index);

			TIndex lowerBounds = this.LowerBoundsSerializer.Deserialize(buffer, ref index);
			TIndex dimensions = this.DimensionsSerializer.Deserialize(buffer, ref index);
			return this.DeserializeValues(lowerBounds, dimensions, buffer, ref index);
		}

		/// <inheritdoc />
		public TIndexable Deserialize(IBufferedArray buffer)
		{
			IDeserializerContracts.Deserialize(buffer);

			TIndex lowerBounds = this.LowerBoundsSerializer.Deserialize(buffer);
			TIndex dimensions = this.DimensionsSerializer.Deserialize(buffer);
			return this.DeserializeValues(lowerBounds, dimensions, buffer);
		}

		protected abstract TIndex GetLowerBounds(TIndexable value);

		protected abstract TIndex GetDimensions(TIndexable value);

		protected abstract void SerializeValues(
			TIndexable value, TIndex lowerBounds, TIndex dimensions, byte[] buffer, ref int index);

		protected abstract int SerializeValues(
			TIndexable value, TIndex lowerBounds, TIndex dimensions, Action<byte> writeByte);

		protected abstract TIndexable DeserializeValues(
			TIndex lowerBounds, TIndex dimensions, byte[] buffer, ref int index);

		protected abstract TIndexable DeserializeValues(
			TIndex lowerBounds, TIndex dimensions, IBufferedArray buffer);

		[Conditional(Contracts.Requires.CompilationSymbol)]
		private void AdditionalSerializeContracts(TIndexable value)
		{
			Contracts.Requires.That(value != null);

			var lowerBounds = this.options.ConstantLowerBounds;
			Contracts.Requires.That(
				lowerBounds.HasValue ?
				this.GetLowerBounds(value).Equals<TIndex>(lowerBounds.Value) : true,
				() => $"Lower bounds must be {lowerBounds.Value} because constant lower bounds have been set.");

			var dimensions = this.options.ConstantDimensions;
			Contracts.Requires.That(
				dimensions.HasValue ?
				this.GetDimensions(value).Equals<TIndex>(dimensions.Value) : true,
				() => $"Dimensions must be {dimensions.Value} because constant dimensions have been set.");
		}
	}
}
