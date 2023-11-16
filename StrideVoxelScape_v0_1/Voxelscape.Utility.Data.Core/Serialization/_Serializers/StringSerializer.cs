using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class StringSerializer : AbstractConstantEnumerableSerializer<string, char>
	{
		public StringSerializer(
			IConstantSerializerDeserializer<char> charSerializer,
			IConstantSerializerDeserializer<int> countSerializer)
			: base(charSerializer, countSerializer)
		{
		}

		public static IEndianProvider<StringSerializer> IncludeLength { get; } =
			EndianProvider.New(New, SerializeCount.Serialize());

		public static IEndianProvider<StringSerializer> InferLength { get; } =
			EndianProvider.New(New, SerializeCount.InferCount(Serializer.Char));

		public static IEndianProvider<StringSerializer> FixedLength(int length) =>
			EndianProvider.New(New, SerializeCount.FixedCount(length));

		/// <inheritdoc />
		protected override int GetCount(string value) => value.Length;

		/// <inheritdoc />
		protected override string DeserializeValues(int countOfValues, byte[] buffer, ref int index)
		{
			char[] chars = new char[countOfValues];
			for (int count = 0; count < countOfValues; count++)
			{
				chars[count] = this.ValueSerializer.Deserialize(buffer, ref index);
			}

			return new string(chars);
		}

		/// <inheritdoc />
		protected override string DeserializeValues(int countOfValues, IBufferedArray buffer)
		{
			char[] chars = new char[countOfValues];
			for (int count = 0; count < countOfValues; count++)
			{
				chars[count] = this.ValueSerializer.Deserialize(buffer);
			}

			return new string(chars);
		}

		private static StringSerializer New(IConstantSerializerDeserializer<int> countSerializer) =>
			new StringSerializer(Serializer.Char[countSerializer.Endianness], countSerializer);
	}
}
