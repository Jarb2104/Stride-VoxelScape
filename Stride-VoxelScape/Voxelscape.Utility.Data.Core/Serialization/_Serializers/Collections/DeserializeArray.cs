using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class DeserializeArray
	{
		public static T[] DeserializeValues<T>(
			IDeserializer<T> deserializer, int count, byte[] buffer, ref int index)
		{
			Contracts.Requires.That(deserializer != null);
			Contracts.Requires.That(count >= 0);
			Contracts.Requires.That(buffer != null);

			var result = new T[count];
			DeserializeValuesInline(deserializer, count, buffer, ref index, result);
			return result;
		}

		public static T[] DeserializeValues<T>(
			IDeserializer<T> deserializer, int count, IBufferedArray buffer)
		{
			Contracts.Requires.That(deserializer != null);
			Contracts.Requires.That(count >= 0);
			Contracts.Requires.That(buffer != null);

			var result = new T[count];
			DeserializeValuesInline(deserializer, count, buffer, result);
			return result;
		}

		public static void DeserializeValuesInline<T>(
			IDeserializer<T> deserializer, int count, byte[] buffer, ref int index, T[] result)
		{
			Contracts.Requires.That(deserializer != null);
			Contracts.Requires.That(count >= 0);
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(result != null);

			for (int currentCount = 0; currentCount < count; currentCount++)
			{
				result[currentCount] = deserializer.Deserialize(buffer, ref index);
			}
		}

		public static void DeserializeValuesInline<T>(
			IDeserializer<T> deserializer, int count, IBufferedArray buffer, T[] result)
		{
			Contracts.Requires.That(deserializer != null);
			Contracts.Requires.That(count >= 0);
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(result != null);

			for (int currentCount = 0; currentCount < count; currentCount++)
			{
				result[currentCount] = deserializer.Deserialize(buffer);
			}
		}
	}
}
