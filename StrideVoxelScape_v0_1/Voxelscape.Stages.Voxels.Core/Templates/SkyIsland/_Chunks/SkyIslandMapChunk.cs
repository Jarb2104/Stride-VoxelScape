using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapChunk : ISkyIslandMapChunk
	{
		public SkyIslandMapChunk(ChunkOverheadKey key, IBoundedIndexable<Index2D, SkyIslandMaps> values)
		{
			Contracts.Requires.That(values != null);

			this.Key = key;
			this.MapsLocalView = values;
			this.MapsStageView = new OffsetArray2D<SkyIslandMaps>(values, values.Dimensions * key.Index);
		}

		/// <inheritdoc />
		public ChunkOverheadKey Key { get; }

		/// <inheritdoc />
		public IBoundedIndexable<Index2D, SkyIslandMaps> MapsLocalView { get; }

		/// <inheritdoc />
		public IBoundedIndexable<Index2D, SkyIslandMaps> MapsStageView { get; }

		/// <inheritdoc />
		IBoundedReadOnlyIndexable<Index2D, SkyIslandMaps> IReadOnlySkyIslandMapChunk.MapsLocalView => this.MapsLocalView;

		/// <inheritdoc />
		IBoundedReadOnlyIndexable<Index2D, SkyIslandMaps> IReadOnlySkyIslandMapChunk.MapsStageView => this.MapsStageView;
	}
}
