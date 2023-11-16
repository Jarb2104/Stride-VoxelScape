using System.Runtime.InteropServices;
using Voxelscape.Stages.Management.Core.Chunks;
using Voxelscape.Stages.Voxels.Pact.Voxels;

namespace Voxelscape.Stages.Voxels.Core.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public class VoxelGridChunkConfig : AbstractRasterChunkConfig3D
	{
		public VoxelGridChunkConfig(int treeDepth)
			: base(treeDepth)
		{
			this.ApproximateSizeInBytes = this.Bounds.Length * Marshal.SizeOf<TerrainVoxel>();
		}

		/// <inheritdoc />
		public override int ApproximateSizeInBytes { get; }
	}
}
