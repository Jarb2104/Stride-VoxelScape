using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class Serializer
	{
		public static IEndianProvider<BoolSerializer> Bool { get; } =
			EndianProvider.New(converter => new BoolSerializer(converter));

		public static IEndianProvider<CharSerializer> Char { get; } =
			EndianProvider.New(converter => new CharSerializer(converter));

		public static IEndianProvider<FloatSerializer> Float { get; } =
			EndianProvider.New(converter => new FloatSerializer(converter));

		public static IEndianProvider<DoubleSerializer> Double { get; } =
			EndianProvider.New(converter => new DoubleSerializer(converter));

		public static IEndianProvider<DecimalSerializer> Decimal { get; } =
			EndianProvider.New(converter => new DecimalSerializer(converter));

		public static IEndianProvider<ByteSerializer> Byte { get; } =
			EndianProvider.New(converter => new ByteSerializer(converter));

		public static IEndianProvider<SByteSerializer> SByte { get; } =
			EndianProvider.New(converter => new SByteSerializer(converter));

		public static IEndianProvider<ShortSerializer> Short { get; } =
			EndianProvider.New(converter => new ShortSerializer(converter));

		public static IEndianProvider<UShortSerializer> UShort { get; } =
			EndianProvider.New(converter => new UShortSerializer(converter));

		public static IEndianProvider<IntSerializer> Int { get; } =
			EndianProvider.New(converter => new IntSerializer(converter));

		public static IEndianProvider<UIntSerializer> UInt { get; } =
			EndianProvider.New(converter => new UIntSerializer(converter));

		public static IEndianProvider<LongSerializer> Long { get; } =
			EndianProvider.New(converter => new LongSerializer(converter));

		public static IEndianProvider<ULongSerializer> ULong { get; } =
			EndianProvider.New(converter => new ULongSerializer(converter));
	}
}
