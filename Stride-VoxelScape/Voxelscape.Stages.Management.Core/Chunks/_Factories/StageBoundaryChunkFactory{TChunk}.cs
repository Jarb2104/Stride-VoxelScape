using System.Threading.Tasks;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Stages.Management.Core.Chunks
{
	public class StageBoundaryChunkFactory<TChunk> : IAsyncFactory<ChunkKey, TChunk>
	{
		private readonly Index3D minKey;

		private readonly Index3D maxKey;

		private readonly IAsyncFactory<ChunkKey, TChunk> interiorFactory;

		private readonly IFactory<ChunkKey, TChunk> exteriorFactory;

		public StageBoundaryChunkFactory(
			IStageBounds bounds,
			IAsyncFactory<ChunkKey, TChunk> interiorFactory,
			IFactory<ChunkKey, TChunk> exteriorFactory)
		{
			Contracts.Requires.That(bounds != null);
			Contracts.Requires.That(interiorFactory != null);
			Contracts.Requires.That(exteriorFactory != null);

			this.minKey = bounds.InChunks.LowerBounds;
			this.maxKey = bounds.InChunks.UpperBounds;
			this.interiorFactory = interiorFactory;
			this.exteriorFactory = exteriorFactory;
		}

		/// <inheritdoc />
		public async Task<TChunk> CreateAsync(ChunkKey key)
		{
			if (key.Index.IsIn(this.minKey, this.maxKey))
			{
				return await this.interiorFactory.CreateAsync(key).DontMarshallContext();
			}
			else
			{
				return this.exteriorFactory.Create(key);
			}
		}
	}
}
