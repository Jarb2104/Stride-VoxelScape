using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Indices;

namespace Voxelscape.Common.Indexing.Core.Utility
{
	/// <summary>
	///
	/// </summary>
	/// <seealso href="https://en.wikipedia.org/wiki/Z-order_curve"/>
	/// <seealso href="http://danpburke.blogspot.com/2011_01_01_archive.html"/>
	public class ZOrderCurveEqualityComparer3D : EqualityComparer<Index3D>
	{
		private ZOrderCurveEqualityComparer3D()
		{
		}

		public static ZOrderCurveEqualityComparer3D Instance { get; } = new ZOrderCurveEqualityComparer3D();

		/// <inheritdoc />
		public override bool Equals(Index3D lhs, Index3D rhs) => lhs == rhs;

		/// <inheritdoc />
		public override int GetHashCode(Index3D index) => new MortonCode3D(index).ToInt();
	}
}
