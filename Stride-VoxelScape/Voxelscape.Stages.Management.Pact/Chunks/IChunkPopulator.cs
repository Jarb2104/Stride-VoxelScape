using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Pact.Chunks
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="TChunk">The type of the chunk.</typeparam>
	public interface IChunkPopulator<TChunk>
	{
		void Populate(TChunk chunk);
	}

	public static class IChunkPopulatorContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Populate<TChunk>(TChunk chunk)
		{
			Contracts.Requires.That(chunk != null);
		}
	}
}
