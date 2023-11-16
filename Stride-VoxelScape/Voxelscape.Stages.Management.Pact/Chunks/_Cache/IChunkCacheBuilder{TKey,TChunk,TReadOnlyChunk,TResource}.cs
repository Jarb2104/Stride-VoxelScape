using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Pact.Chunks
{
	public interface IChunkCacheBuilder<TKey, TChunk, TReadOnlyChunk, TResource> :
		IChunkCacheBuilder<TKey, TResource>
		where TChunk : TReadOnlyChunk
	{
		IChunkCache<TKey, TChunk, TReadOnlyChunk> Build();
	}

	public static class IChunkCacheBuilderContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Build<TKey, TChunk, TReadOnlyChunk, TResource>(
			IChunkCacheBuilder<TKey, TChunk, TReadOnlyChunk, TResource> instance)
			where TChunk : TReadOnlyChunk
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(instance.ResourceStash != null);
		}
	}
}
