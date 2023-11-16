using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class ExteriorSkyIslandMapChunkPopulator : IChunkPopulator<ISkyIslandMapChunk>
	{
		private readonly SkyIslandMaps outOfBoundsValue;

		public ExteriorSkyIslandMapChunkPopulator(SkyIslandMaps outOfBoundsValue)
		{
			this.outOfBoundsValue = outOfBoundsValue;
		}

		/// <inheritdoc />
		public void Populate(ISkyIslandMapChunk chunk)
		{
			IChunkPopulatorContracts.Populate(chunk);

			foreach (var pair in chunk.MapsLocalView)
			{
				chunk.MapsLocalView[pair.Key] = this.outOfBoundsValue;
			}
		}
	}
}
