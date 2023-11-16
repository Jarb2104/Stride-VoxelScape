using System.Runtime.InteropServices;
using Voxelscape.Stages.Management.Core.Chunks;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapChunkConfig : AbstractRasterChunkConfig2D
	{
		public SkyIslandMapChunkConfig(int treeDepth)
			: base(treeDepth)
		{
			this.ApproximateSizeInBytes = this.Bounds.Length * Marshal.SizeOf<SkyIslandMaps>();
		}

		/// <inheritdoc />
		public override int ApproximateSizeInBytes { get; }
	}
}
