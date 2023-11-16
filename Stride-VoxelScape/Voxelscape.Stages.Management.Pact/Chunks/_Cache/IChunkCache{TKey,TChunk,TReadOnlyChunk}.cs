namespace Voxelscape.Stages.Management.Pact.Chunks
{
	public interface IChunkCache<TKey, TChunk, TReadOnlyChunk> : IChunkCache<TKey, TChunk>
		where TChunk : TReadOnlyChunk
	{
		IChunkCache<TKey, TReadOnlyChunk> AsReadOnly { get; }
	}
}
