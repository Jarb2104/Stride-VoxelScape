using System.Collections.Generic;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class ReadOnlyCollectionConstantSerializer<T> :
		AbstractConstantEnumerableSerializer<IReadOnlyCollection<T>, T>
	{
		public ReadOnlyCollectionConstantSerializer(
			IConstantSerializerDeserializer<T> valueSerializer, IConstantSerializerDeserializer<int> countSerializer)
			: base(valueSerializer, countSerializer)
		{
		}

		/// <inheritdoc />
		protected override int GetCount(IReadOnlyCollection<T> value) => value.Count;

		/// <inheritdoc />
		protected override IReadOnlyCollection<T> DeserializeValues(int countOfValues, IBufferedArray buffer) =>
			DeserializeArray.DeserializeValues(this.ValueSerializer, countOfValues, buffer);

		/// <inheritdoc />
		protected override IReadOnlyCollection<T> DeserializeValues(int countOfValues, byte[] buffer, ref int index) =>
			DeserializeArray.DeserializeValues(this.ValueSerializer, countOfValues, buffer, ref index);
	}
}
