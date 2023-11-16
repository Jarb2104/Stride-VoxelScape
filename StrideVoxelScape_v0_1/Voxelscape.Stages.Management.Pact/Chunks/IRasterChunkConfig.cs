namespace Voxelscape.Stages.Management.Pact.Chunks
{
	/// <summary>
	///
	/// </summary>
	public interface IRasterChunkConfig
	{
		int TreeDepth { get; }

		int SideLength { get; }

		int ApproximateSizeInBytes { get; }
	}
}
