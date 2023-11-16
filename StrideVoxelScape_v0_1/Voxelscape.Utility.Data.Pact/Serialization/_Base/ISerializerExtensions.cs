using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

/// <summary>
/// Provides extension methods for the <see cref="ISerializer{T}"/> class.
/// </summary>
public static class ISerializerExtensions
{
	public static int Serialize<T>(this ISerializer<T> serializer, T value, byte[] buffer, int index)
	{
		Contracts.Requires.That(serializer != null);

		return serializer.Serialize(value, buffer, ref index);
	}

	public static int Serialize<T>(this ISerializer<T> serializer, T value, byte[] buffer)
	{
		Contracts.Requires.That(serializer != null);

		return serializer.Serialize(value, buffer, 0);
	}

	public static byte[] Serialize<T>(this ISerializer<T> serializer, T value)
	{
		Contracts.Requires.That(serializer != null);

		byte[] result = new byte[serializer.GetSerializedLength(value)];
		serializer.Serialize(value, result, 0);
		return result;
	}
}
