using System;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class SerializeEnumerable
	{
		public static int SerializeValues<T>(
			ISerializer<T> serializer, IEnumerable<T> values, byte[] buffer, ref int index)
		{
			Contracts.Requires.That(serializer != null);
			Contracts.Requires.That(values != null);
			Contracts.Requires.That(buffer != null);

			int startIndex = index;
			foreach (T value in values)
			{
				serializer.Serialize(value, buffer, ref index);
			}

			return index - startIndex;
		}

		public static int SerializeValues<T>(
			ISerializer<T> serializer, IEnumerable<T> values, Action<byte> writeByte)
		{
			Contracts.Requires.That(serializer != null);
			Contracts.Requires.That(values != null);
			Contracts.Requires.That(writeByte != null);

			int serializedLength = 0;
			foreach (T value in values)
			{
				serializedLength += serializer.Serialize(value, writeByte);
			}

			return serializedLength;
		}
	}
}
