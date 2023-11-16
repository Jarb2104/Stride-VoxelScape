using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Contouring;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Stages.Management.Core.Contouring
{
	public class ContourChunkView<TChunk> : IContourChunkView<TChunk>
		where TChunk : IKeyed<ChunkKey>
	{
		public ContourChunkView(ChunkKey key, IBoundedReadOnlyIndexable<Index3D, TChunk> chunks)
		{
			IContourChunkViewContracts.Constructor(key, chunks);

			this.Key = key;
			this.Chunks = chunks;
		}

		/// <inheritdoc />
		public ChunkKey Key { get; }

		/// <inheritdoc />
		public IBoundedReadOnlyIndexable<Index3D, TChunk> Chunks { get; }
	}
}
