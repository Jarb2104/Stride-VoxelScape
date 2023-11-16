using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Stages.Voxels.Core.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public class VoxelGridChunkResourcesFactory : IFactory<VoxelGridChunkResources>
	{
		private readonly Index3D dimensions;

		public VoxelGridChunkResourcesFactory(IRasterChunkConfig<Index3D> config)
		{
			Contracts.Requires.That(config != null);

			this.dimensions = config.Bounds.Dimensions;
		}

		/// <inheritdoc />
		public VoxelGridChunkResources Create() =>
			new VoxelGridChunkResources(new Array3D<TerrainVoxel>(this.dimensions));
	}
}
