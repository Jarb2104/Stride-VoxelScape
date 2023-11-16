using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class ArrayConstantSerializer<T> : AbstractConstantEnumerableInlineSerializer<T[], T>
	{
		public ArrayConstantSerializer(
			IConstantSerializerDeserializer<T> valueSerializer, IConstantSerializerDeserializer<int> countSerializer)
			: base(valueSerializer, countSerializer)
		{
		}

		/// <inheritdoc />
		protected override int GetCount(T[] value) => value.Length;

		/// <inheritdoc />
		protected override T[] Create(int countOfValues) => new T[countOfValues];

		/// <inheritdoc />
		protected override void DeserializeValuesInline(int countOfValues, IBufferedArray buffer, T[] result) =>
			DeserializeArray.DeserializeValuesInline(this.ValueSerializer, countOfValues, buffer, result);

		/// <inheritdoc />
		protected override void DeserializeValuesInline(int countOfValues, byte[] buffer, ref int index, T[] result) =>
			DeserializeArray.DeserializeValuesInline(
				this.ValueSerializer, countOfValues, buffer, ref index, result);
	}
}
