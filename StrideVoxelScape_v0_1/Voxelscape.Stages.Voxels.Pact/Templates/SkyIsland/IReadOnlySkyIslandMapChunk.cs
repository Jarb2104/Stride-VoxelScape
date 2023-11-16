using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public interface IReadOnlySkyIslandMapChunk : IKeyed<ChunkOverheadKey>
	{
		IBoundedReadOnlyIndexable<Index2D, SkyIslandMaps> MapsLocalView { get; }

		IBoundedReadOnlyIndexable<Index2D, SkyIslandMaps> MapsStageView { get; }
	}
}
