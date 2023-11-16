using System;
using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Core.Disposables;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapChunkPoolingFactory :
		IAsyncFactory<ChunkOverheadKey, IDisposableValue<ISkyIslandMapChunk>>
	{
		private readonly IPool<SkyIslandMapChunkResources> pool;

		private readonly IAsyncChunkPopulator<ISkyIslandMapChunk> populator;

		public SkyIslandMapChunkPoolingFactory(
			IPool<SkyIslandMapChunkResources> pool, IAsyncChunkPopulator<ISkyIslandMapChunk> populator)
		{
			Contracts.Requires.That(pool != null);
			Contracts.Requires.That(populator != null);

			this.pool = pool;
			this.populator = populator;
		}

		/// <inheritdoc />
		public async Task<IDisposableValue<ISkyIslandMapChunk>> CreateAsync(ChunkOverheadKey key)
		{
			var resources = await this.pool.TakeLoanAsync().DontMarshallContext();
			try
			{
				var chunk = new SkyIslandMapChunk(key, resources.Value.Maps);
				await this.populator.PopulateAsync(chunk).DontMarshallContext();
				return new CompositeDisposableWrapper<ISkyIslandMapChunk>(chunk, resources);
			}
			catch (Exception)
			{
				resources.Dispose();
				throw;
			}
		}
	}
}
