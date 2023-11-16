using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

/// <summary>
/// Provides extension methods for the <see cref="IDeserializer{T}"/> interface.
/// </summary>
public static class IDeserializerExtensions
{
	public static T Deserialize<T>(this IDeserializer<T> deserializer, byte[] buffer, int index)
	{
		Contracts.Requires.That(deserializer != null);

		return deserializer.Deserialize(buffer, ref index);
	}

	public static T Deserialize<T>(this IDeserializer<T> deserializer, byte[] buffer)
	{
		Contracts.Requires.That(deserializer != null);

		return deserializer.Deserialize(buffer, 0);
	}
}
