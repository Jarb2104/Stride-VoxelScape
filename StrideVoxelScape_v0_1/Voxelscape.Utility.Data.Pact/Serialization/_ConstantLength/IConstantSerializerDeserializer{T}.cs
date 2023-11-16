namespace Voxelscape.Utility.Data.Pact.Serialization
{
	public interface IConstantSerializerDeserializer<T> :
		ISerializerDeserializer<T>, IConstantSerializer<T>, IConstantDeserializer<T>
	{
	}
}
