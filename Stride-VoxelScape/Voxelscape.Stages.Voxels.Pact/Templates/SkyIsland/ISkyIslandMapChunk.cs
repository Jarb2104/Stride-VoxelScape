using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;

namespace Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public interface ISkyIslandMapChunk : IReadOnlySkyIslandMapChunk
	{
		new IBoundedIndexable<Index2D, SkyIslandMaps> MapsLocalView { get; }

		new IBoundedIndexable<Index2D, SkyIslandMaps> MapsStageView { get; }
	}
}
