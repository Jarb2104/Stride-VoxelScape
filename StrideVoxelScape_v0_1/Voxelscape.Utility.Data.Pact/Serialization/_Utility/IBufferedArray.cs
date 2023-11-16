using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Data.Pact.Serialization
{
	public interface IBufferedArray
	{
		byte[] Buffer { get; }

		int? TotalRemainingLength { get; }

		bool HasNext { get; }

		/// <summary>
		/// Buffers the next <paramref name="count"/> values if possible.
		/// </summary>
		/// <param name="count">The count.</param>
		/// <returns>The number of values actually buffered.</returns>
		int BufferNext(int count);
	}

	public static class IBufferedArrayContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void BufferNext(int count)
		{
			Contracts.Requires.That(count >= 0);
		}
	}
}
