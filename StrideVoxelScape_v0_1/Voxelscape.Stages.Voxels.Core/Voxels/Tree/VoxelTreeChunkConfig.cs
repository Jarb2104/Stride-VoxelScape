using System.Runtime.InteropServices;
using Voxelscape.Stages.Management.Core.Chunks;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Voxels.Tree
{
	/// <summary>
	///
	/// </summary>
	public class VoxelTreeChunkConfig : AbstractRasterChunkConfig3D
	{
		public VoxelTreeChunkConfig(int treeDepth)
			: base(treeDepth)
		{
			this.ApproximateSizeInBytes = CalculateSizeInBytes(this.TreeDepth);
		}

		/// <inheritdoc />
		public override int ApproximateSizeInBytes { get; }

		private static int CalculateSizeInBytes(int treeDepth)
		{
			Contracts.Requires.That(treeDepth >= 0);

			int currentLayerSize = 1;
			int totalSize = 1;
			for (int depth = 1; depth <= treeDepth; depth++)
			{
				// each layer down doubles in size along all 3 dimensions (so * 8 in total)
				currentLayerSize *= 8;
				totalSize += currentLayerSize;
			}

			return totalSize * Marshal.SizeOf<TerrainVoxel>();
		}
	}
}
