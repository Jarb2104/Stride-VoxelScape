using Voxelscape.Stages.Voxels.Core.Templates.SkyIsland;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for working with <see cref="SkyIslandMapChunkResources"/>.
/// </summary>
public static class SkyIslandMapChunkResourcesExtensions
{
	public static SkyIslandMapChunkResources GetResources(this ISkyIslandMapChunk chunk)
	{
		Contracts.Requires.That(chunk != null);

		return new SkyIslandMapChunkResources(chunk.MapsLocalView);
	}
}
