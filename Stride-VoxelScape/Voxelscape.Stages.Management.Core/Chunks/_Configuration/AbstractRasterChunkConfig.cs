using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Mathematics;

namespace Voxelscape.Stages.Management.Core.Chunks
{
	/// <summary>
	///
	/// </summary>
	public abstract class AbstractRasterChunkConfig : IRasterChunkConfig
	{
		public AbstractRasterChunkConfig(int treeDepth)
		{
			Contracts.Requires.That(treeDepth >= 0);

			this.TreeDepth = treeDepth;
			this.SideLength = MathUtilities.IntegerPower(2, treeDepth);
		}

		/// <inheritdoc />
		public abstract int ApproximateSizeInBytes { get; }

		/// <inheritdoc />
		public int SideLength { get; }

		/// <inheritdoc />
		public int TreeDepth { get; }
	}
}
