using Voxelscape.Stages.Voxels.Core.Voxels.Grid;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for <see cref="VoxelGridChunkResources"/>.
/// </summary>
public static class VoxelGridChunkResourcesExtensions
{
	public static VoxelGridChunkResources GetResources(this IVoxelGridChunk chunk)
	{
		Contracts.Requires.That(chunk != null);

		return new VoxelGridChunkResources(chunk.VoxelsLocalView);
	}
}
