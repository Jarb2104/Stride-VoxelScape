namespace Voxelscape.Utility.Data.Pact.Serialization
{
	public interface IConstantSerializedLength : IEndianSpecific
	{
		int SerializedLength { get; }
	}
}
