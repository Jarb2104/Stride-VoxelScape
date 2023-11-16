using System.Collections.Generic;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class ReadOnlyListConstantSerializer<T> : AbstractConstantEnumerableSerializer<IReadOnlyList<T>, T>
	{
		public ReadOnlyListConstantSerializer(
			IConstantSerializerDeserializer<T> valueSerializer, IConstantSerializerDeserializer<int> countSerializer)
			: base(valueSerializer, countSerializer)
		{
		}

		/// <inheritdoc />
		protected override int GetCount(IReadOnlyList<T> value) => value.Count;

		/// <inheritdoc />
		protected override IReadOnlyList<T> DeserializeValues(int countOfValues, IBufferedArray buffer) =>
			DeserializeArray.DeserializeValues(this.ValueSerializer, countOfValues, buffer);

		/// <inheritdoc />
		protected override IReadOnlyList<T> DeserializeValues(int countOfValues, byte[] buffer, ref int index) =>
			DeserializeArray.DeserializeValues(this.ValueSerializer, countOfValues, buffer, ref index);
	}
}
