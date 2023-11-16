using System;
using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;
using Voxelscape.Utility.Common.Core.Disposables;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Stages.Voxels.Core.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public class VoxelGridChunkPoolingFactory : IAsyncFactory<ChunkKey, IDisposableValue<IVoxelGridChunk>>
	{
		private readonly IPool<VoxelGridChunkResources> pool;

		private readonly IAsyncChunkPopulator<IVoxelGridChunk> populator;

		public VoxelGridChunkPoolingFactory(
			IPool<VoxelGridChunkResources> pool, IAsyncChunkPopulator<IVoxelGridChunk> populator)
		{
			Contracts.Requires.That(pool != null);
			Contracts.Requires.That(populator != null);

			this.pool = pool;
			this.populator = populator;
		}

		/// <inheritdoc />
		public async Task<IDisposableValue<IVoxelGridChunk>> CreateAsync(ChunkKey key)
		{
			var resources = await this.pool.TakeLoanAsync().DontMarshallContext();
			try
			{
				var chunk = new VoxelGridChunk(key, resources.Value.Voxels);
				await this.populator.PopulateAsync(chunk).DontMarshallContext();
				return new CompositeDisposableWrapper<IVoxelGridChunk>(chunk, resources);
			}
			catch (Exception)
			{
				resources.Dispose();
				throw;
			}
		}
	}
}
