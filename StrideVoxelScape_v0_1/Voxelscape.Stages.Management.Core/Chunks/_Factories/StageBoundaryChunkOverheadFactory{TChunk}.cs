using System.Threading.Tasks;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Stages.Management.Core.Chunks
{
	public class StageBoundaryChunkOverheadFactory<TChunk> : IAsyncFactory<ChunkOverheadKey, TChunk>
	{
		private readonly Index2D minKey;

		private readonly Index2D maxKey;

		private readonly IAsyncFactory<ChunkOverheadKey, TChunk> interiorFactory;

		private readonly IFactory<ChunkOverheadKey, TChunk> exteriorFactory;

		public StageBoundaryChunkOverheadFactory(
			IStageBounds bounds,
			IAsyncFactory<ChunkOverheadKey, TChunk> interiorFactory,
			IFactory<ChunkOverheadKey, TChunk> exteriorFactory)
		{
			Contracts.Requires.That(bounds != null);
			Contracts.Requires.That(interiorFactory != null);
			Contracts.Requires.That(exteriorFactory != null);

			this.minKey = bounds.InOverheadChunks.LowerBounds;
			this.maxKey = bounds.InOverheadChunks.UpperBounds;
			this.interiorFactory = interiorFactory;
			this.exteriorFactory = exteriorFactory;
		}

		/// <inheritdoc />
		public async Task<TChunk> CreateAsync(ChunkOverheadKey key)
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
