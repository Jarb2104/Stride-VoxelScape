using System.Diagnostics;
using System.Linq;
using Voxelscape.Common.Indexing.Core.Enumerables;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Stages.Management.Pact.Contouring
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="TChunk">The type of the voxel chunk.</typeparam>
	public interface IContourChunkView<TChunk> : IKeyed<ChunkKey>
		where TChunk : IKeyed<ChunkKey>
	{
		IBoundedReadOnlyIndexable<Index3D, TChunk> Chunks { get; }
	}

	public static class IContourChunkViewContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Constructor<TChunk>(
			ChunkKey key, IBoundedReadOnlyIndexable<Index3D, TChunk> chunks)
			where TChunk : IKeyed<ChunkKey>
		{
			Contracts.Requires.That(chunks.AllAndSelfNotNull());
			Contracts.Requires.That(chunks.LowerBounds == new Index3D(-1));
			Contracts.Requires.That(chunks.UpperBounds == new Index3D(1));
			Contracts.Requires.That(chunks.Dimensions == new Index3D(3));
			Contracts.Requires.That(
				Index.Range(new Index3D(-1), new Index3D(3)).All(
					index => chunks[index].Key == new ChunkKey(key.Index + index)),
				$"Each chunk's offset in {nameof(chunks)} must match its Key's offset to {nameof(key)}.");
		}
	}
}
