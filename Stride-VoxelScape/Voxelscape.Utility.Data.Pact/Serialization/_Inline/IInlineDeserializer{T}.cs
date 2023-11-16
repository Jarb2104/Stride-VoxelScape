using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Data.Pact.Serialization
{
	public interface IInlineDeserializer<T> : IEndianSpecific
	{
		void DeserializeInline(byte[] buffer, ref int index, T result);

		void DeserializeInline(IBufferedArray buffer, T result);
	}

	public static class IInlineDeserializerContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void DeserializeInline<T>(byte[] buffer, int index, T result)
		{
			IDeserializerContracts.Deserialize(buffer, index);
			Contracts.Requires.That(result != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void DeserializeInline<T>(IBufferedArray buffer, T result)
		{
			IDeserializerContracts.Deserialize(buffer);
			Contracts.Requires.That(result != null);
		}
	}
}
