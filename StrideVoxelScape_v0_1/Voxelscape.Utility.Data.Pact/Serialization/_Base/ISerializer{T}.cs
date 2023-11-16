using System;
using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Data.Pact.Serialization
{
	public interface ISerializer<T> : IEndianSpecific
	{
		int GetSerializedLength(T value);

		/// <summary>
		/// Serializes the specified object, storing the result in the specified byte array.
		/// </summary>
		/// <param name="value">The value to serialize.</param>
		/// <param name="buffer">The byte array to store the serialized object in.</param>
		/// <param name="index">Index of the array to begin storing the object at.</param>
		/// <returns>
		/// The length in bytes of the serialized object. This is how many elements of the
		/// <paramref name="buffer"/> are used to store the serialized object.
		/// </returns>
		int Serialize(T value, byte[] buffer, ref int index);

		int Serialize(T value, Action<byte> writeByte);
	}

	public static class ISerializerContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void GetSerializedLength<T>(T value)
		{
			Contracts.Requires.That(value != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Serialize<T>(ISerializer<T> instance, T value, byte[] buffer, int index)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(value != null);
			Contracts.Requires.That(buffer != null);
			Contracts.Requires.That(index >= 0);
			Contracts.Requires.That(instance.GetSerializedLength(value) + index <= buffer.Length);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Serialize<T>(T value, Action<byte> writeByte)
		{
			Contracts.Requires.That(value != null);
			Contracts.Requires.That(writeByte != null);
		}
	}
}
