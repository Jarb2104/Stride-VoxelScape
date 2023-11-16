using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Arrays
{
	/// <summary>
	///
	/// </summary>
	public static class OffsetArray
	{
		public static IBoundedIndexable<Index2D, T> CenterOnZero<T>(
			IBoundedIndexable<Index2D, T> array, bool roundUp = false)
		{
			Contracts.Requires.That(array != null);

			var offset = -array.GetMiddleIndex(roundUp);
			return offset != Index2D.Zero ? new OffsetArray2D<T>(array, offset) : array;
		}

		public static IBoundedIndexable<Index3D, T> CenterOnZero<T>(
			IBoundedIndexable<Index3D, T> array, bool roundUp = false)
		{
			Contracts.Requires.That(array != null);

			var offset = -array.GetMiddleIndex(roundUp);
			return offset != Index3D.Zero ? new OffsetArray3D<T>(array, offset) : array;
		}
	}
}
