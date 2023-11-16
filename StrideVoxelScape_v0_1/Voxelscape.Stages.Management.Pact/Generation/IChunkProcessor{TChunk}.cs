using System.Diagnostics;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Pact.Generation
{
	// TODO I am not happy with this abstraction. Should it be IAsyncCompletable? Should ProcessChunk be async?
	public interface IChunkProcessor<TChunk>
	{
		Task InitializeAsync();

		void ProcessChunk(TChunk chunk);

		Task CompleteAsync();
	}

	public static class IChunkProcessorContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ProcessChunk<TChunk>(TChunk chunk)
		{
			Contracts.Requires.That(chunk != null);
		}
	}
}
