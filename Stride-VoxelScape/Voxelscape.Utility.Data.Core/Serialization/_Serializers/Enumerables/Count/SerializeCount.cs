using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class SerializeCount
	{
		public static IEndianProvider<IntSerializer> Serialize() => Serializer.Int;

		public static IEndianProvider<ConverterConstantSerializer<int, ushort>> SerializeAsUShort() =>
			SerializeInt.AsUShort;

		public static IEndianProvider<ConverterConstantSerializer<int, byte>> SerializeAsByte() =>
			SerializeInt.AsByte;

		public static IEndianProvider<InferCountSerializer> InferCount<T>(IEndianProvider<T> provider)
			where T : IConstantSerializedLength =>
			EndianProvider.New(value => new InferCountSerializer(value), provider);

		public static IEndianProvider<FixedConstantSerializer<int>> FixedCount(int count) =>
			FixedConstantSerializer.NewProvider(count);

		public static IConstantSerializerDeserializer<int> Serialize(Endian endianness) =>
			Serialize()[endianness];

		public static IConstantSerializerDeserializer<int> SerializeAsUShort(Endian endianness) =>
			SerializeAsUShort()[endianness];

		public static IConstantSerializerDeserializer<int> SerializeAsByte(Endian endianness) =>
			SerializeAsByte()[endianness];

		public static IConstantSerializerDeserializer<int> InferCount(IConstantSerializedLength value) =>
			new InferCountSerializer(value);

		public static IConstantSerializerDeserializer<int> FixedCount(int count, Endian endianness)
		{
			Contracts.Requires.That(count >= 0);

			return new FixedConstantSerializer<int>(count, endianness);
		}
	}
}
