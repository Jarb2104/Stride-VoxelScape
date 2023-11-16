using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapChunkResourcesFactory : IFactory<SkyIslandMapChunkResources>
	{
		private readonly Index2D dimensions;

		public SkyIslandMapChunkResourcesFactory(IRasterChunkConfig<Index2D> config)
		{
			Contracts.Requires.That(config != null);

			this.dimensions = config.Bounds.Dimensions;
		}

		/// <inheritdoc />
		public SkyIslandMapChunkResources Create() =>
			new SkyIslandMapChunkResources(new Array2D<SkyIslandMaps>(this.dimensions));
	}
}
