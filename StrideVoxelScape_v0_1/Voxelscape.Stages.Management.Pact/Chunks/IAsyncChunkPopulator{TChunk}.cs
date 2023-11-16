using System.Diagnostics;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Pact.Chunks
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="TChunk">The type of the chunk.</typeparam>
	public interface IAsyncChunkPopulator<TChunk>
	{
		Task PopulateAsync(TChunk chunk);
	}

	public static class IAsyncChunkPopulatorContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void PopulateAsync<TChunk>(TChunk chunk)
		{
			Contracts.Requires.That(chunk != null);
		}
	}
}
