using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Stages.Management.Pact.Chunks
{
	public interface IChunkCache<TKey, TChunk> :
		IAsyncCache<TKey, TChunk>, IAsyncFactory<TKey, IDisposableValue<TChunk>>
	{
	}
}
