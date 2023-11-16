using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Common.Pact.Mathematics;

namespace Voxelscape.Common.Indexing.Core.Utility
{
	/// <summary>
	///
	/// </summary>
	public static class MiddleIndex
	{
		public static Index1D Get(Index1D index1, Index1D index2, bool roundUp = false) =>
			new Index1D(MathUtilities.IntegerMidpoint(index1.X, index2.X, roundUp));

		public static Index2D Get(Index2D index1, Index2D index2, bool roundUp = false)
		{
			int x = MathUtilities.IntegerMidpoint(index1.X, index2.X, roundUp);
			int y = MathUtilities.IntegerMidpoint(index1.Y, index2.Y, roundUp);
			return new Index2D(x, y);
		}

		public static Index3D Get(Index3D index1, Index3D index2, bool roundUp = false)
		{
			int x = MathUtilities.IntegerMidpoint(index1.X, index2.X, roundUp);
			int y = MathUtilities.IntegerMidpoint(index1.Y, index2.Y, roundUp);
			int z = MathUtilities.IntegerMidpoint(index1.Z, index2.Z, roundUp);
			return new Index3D(x, y, z);
		}

		public static Index4D Get(Index4D index1, Index4D index2, bool roundUp = false)
		{
			int x = MathUtilities.IntegerMidpoint(index1.X, index2.X, roundUp);
			int y = MathUtilities.IntegerMidpoint(index1.Y, index2.Y, roundUp);
			int z = MathUtilities.IntegerMidpoint(index1.Z, index2.Z, roundUp);
			int w = MathUtilities.IntegerMidpoint(index1.W, index2.W, roundUp);
			return new Index4D(x, y, z, w);
		}
	}
}
