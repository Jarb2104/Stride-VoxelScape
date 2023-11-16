using Voxelscape.Utility.Common.Pact.Conversions;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class SerializeInt
	{
		public static IEndianProvider<ConverterConstantSerializer<int, byte>> AsByte { get; } = EndianProvider.New(
			serializer => new ConverterConstantSerializer<int, byte>(new IntToByteConverter(), serializer));

		public static IEndianProvider<ConverterConstantSerializer<int, sbyte>> AsSByte { get; } = EndianProvider.New(
			serializer => new ConverterConstantSerializer<int, sbyte>(new IntToSByteConverter(), serializer));

		public static IEndianProvider<ConverterConstantSerializer<int, short>> AsShort { get; } = EndianProvider.New(
			serializer => new ConverterConstantSerializer<int, short>(new IntToShortConverter(), serializer));

		public static IEndianProvider<ConverterConstantSerializer<int, ushort>> AsUShort { get; } = EndianProvider.New(
			serializer => new ConverterConstantSerializer<int, ushort>(new IntToUShortConverter(), serializer));

		private class IntToByteConverter : ITwoWayConverter<int, byte>
		{
			/// <inheritdoc />
			public byte Convert(int value) => checked((byte)value);

			/// <inheritdoc />
			public int Convert(byte value) => value;
		}

		private class IntToSByteConverter : ITwoWayConverter<int, sbyte>
		{
			/// <inheritdoc />
			public sbyte Convert(int value) => checked((sbyte)value);

			/// <inheritdoc />
			public int Convert(sbyte value) => value;
		}

		private class IntToShortConverter : ITwoWayConverter<int, short>
		{
			/// <inheritdoc />
			public short Convert(int value) => checked((short)value);

			/// <inheritdoc />
			public int Convert(short value) => value;
		}

		private class IntToUShortConverter : ITwoWayConverter<int, ushort>
		{
			/// <inheritdoc />
			public ushort Convert(int value) => checked((ushort)value);

			/// <inheritdoc />
			public int Convert(ushort value) => value;
		}
	}
}
