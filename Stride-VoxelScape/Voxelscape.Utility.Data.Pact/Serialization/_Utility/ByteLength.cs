using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Utility.Data.Pact.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class ByteLength
	{
		public static int Bool => PrimitiveTypeSizes.BoolNumberOfBytes;

		public static int Char => PrimitiveTypeSizes.CharNumberOfBytes;

		public static int Float => PrimitiveTypeSizes.FloatNumberOfBytes;

		public static int Double => PrimitiveTypeSizes.DoubleNumberOfBytes;

		public static int Decimal => PrimitiveTypeSizes.DecimalNumberOfBytes;

		public static int Byte => PrimitiveTypeSizes.ByteNumberOfBytes;

		public static int SByte => PrimitiveTypeSizes.SByteNumberOfBytes;

		public static int Short => PrimitiveTypeSizes.ShortNumberOfBytes;

		public static int UShort => PrimitiveTypeSizes.UShortNumberOfBytes;

		public static int Int => PrimitiveTypeSizes.IntNumberOfBytes;

		public static int UInt => PrimitiveTypeSizes.UIntNumberOfBytes;

		public static int Long => PrimitiveTypeSizes.LongNumberOfBytes;

		public static int ULong => PrimitiveTypeSizes.ULongNumberOfBytes;
	}
}
