using System.Collections.Generic;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class EnumerableSerializer<T> : AbstractEnumerableSerializer<IEnumerable<T>, T>
	{
		public EnumerableSerializer(ISerializerDeserializer<T> valueSerializer)
			: base(valueSerializer, Serializer.Int[valueSerializer.Endianness])
		{
		}

		public EnumerableSerializer(
			ISerializerDeserializer<T> valueSerializer, IConstantSerializerDeserializer<int> countSerializer)
			: base(valueSerializer, countSerializer)
		{
		}

		/// <inheritdoc />
		protected override IEnumerable<T> DeserializeValues(int countOfValues, IBufferedArray buffer) =>
			DeserializeArray.DeserializeValues(this.ValueSerializer, countOfValues, buffer);

		/// <inheritdoc />
		protected override IEnumerable<T> DeserializeValues(int countOfValues, byte[] buffer, ref int index) =>
			DeserializeArray.DeserializeValues(this.ValueSerializer, countOfValues, buffer, ref index);
	}
}
