using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public struct SkyIslandMapChunkResources
	{
		public SkyIslandMapChunkResources(IBoundedIndexable<Index2D, SkyIslandMaps> maps)
		{
			Contracts.Requires.That(maps != null);

			this.Maps = maps;
		}

		public IBoundedIndexable<Index2D, SkyIslandMaps> Maps { get; }
	}
}
