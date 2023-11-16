using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

/// <summary>
/// Provides extension methods for <see cref="IInlineDeserializer{T}"/>.
/// </summary>
public static class IInlineDeserializerExtensions
{
	public static void DeserializeInline<T>(
		this IInlineDeserializer<T> deserializer, byte[] buffer, int index, T result)
	{
		Contracts.Requires.That(deserializer != null);

		deserializer.DeserializeInline(buffer, ref index, result);
	}

	public static void DeserializeInline<T>(this IInlineDeserializer<T> deserializer, byte[] buffer, T result)
	{
		Contracts.Requires.That(deserializer != null);

		deserializer.DeserializeInline(buffer, 0, result);
	}
}
