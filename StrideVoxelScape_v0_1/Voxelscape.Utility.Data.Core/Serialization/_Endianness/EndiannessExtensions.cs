using MiscUtil.Conversion;
using Voxelscape.Utility.Common.Pact.Exceptions;
using Voxelscape.Utility.Data.Pact.Serialization;

/// <summary>
/// Provides extension methods for <see cref="Endianness"/>.
/// </summary>
public static class EndiannessExtensions
{
	public static Endian ToEndian(this Endianness endianness)
	{
		switch (endianness)
		{
			case Endianness.BigEndian: return Endian.Big;
			case Endianness.LittleEndian: return Endian.Little;
			default: throw InvalidEnumArgument.CreateException(nameof(endianness), endianness);
		}
	}
}
