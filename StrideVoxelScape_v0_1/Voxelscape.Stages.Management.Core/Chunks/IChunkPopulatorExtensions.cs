using Voxelscape.Stages.Management.Core.Chunks;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for <see cref="IChunkPopulator{TChunk}"/>.
/// </summary>
public static class IChunkPopulatorExtensions
{
	public static IAsyncChunkPopulator<TChunk> WrapWithAsync<TChunk>(this IChunkPopulator<TChunk> populator)
	{
		Contracts.Requires.That(populator != null);

		return new AsyncChunkPopulator<TChunk>(populator);
	}
}
