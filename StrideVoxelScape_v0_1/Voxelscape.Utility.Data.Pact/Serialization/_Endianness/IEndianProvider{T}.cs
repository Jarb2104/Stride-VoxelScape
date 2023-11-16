namespace Voxelscape.Utility.Data.Pact.Serialization
{
	public interface IEndianProvider<T>
		where T : IEndianSpecific
	{
		T BigEndian { get; }

		T LittleEndian { get; }

		T this[Endian endianness] { get; }
	}
}
