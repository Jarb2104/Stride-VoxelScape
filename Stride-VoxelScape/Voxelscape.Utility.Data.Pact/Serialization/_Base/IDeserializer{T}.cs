using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Data.Pact.Serialization
{
	public interface IDeserializer<T> : IEndianSpecific
	{
		T Deserialize(byte[] buffer, ref int index);

		T Deserialize(IBufferedArray buffer);
	}

	public static class IDeserializerContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Deserialize(byte[] buffer, int index)
		{
			Contracts.Requires.That(buffer != null);

			// Index must either be a valid index into the buffer or be the very next index after the
			// end of the buffer, which must be interpreted as reaching the end of the buffer, otherwise
			// attempting to access that index will result in an ArgumentOutOfRangeException being thrown.
			// This extra fringe case is allowed to support 0 length serialized objects still being able
			// to be deserialized from the end of the buffer.
			Contracts.Requires.That(index.IsIn(buffer.GetIndexRange()) || index == buffer.Length);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Deserialize(IBufferedArray buffer)
		{
			Contracts.Requires.That(buffer != null);
		}
	}
}
