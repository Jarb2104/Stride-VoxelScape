using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Indices;

namespace Voxelscape.Common.Indexing.Core.Utility
{
	/// <summary>
	///
	/// </summary>
	public struct MortonCode3D : IMortonCode<Index3D>
	{
		private readonly uint mortonCode;

		public MortonCode3D(uint mortonCode)
		{
			this.mortonCode = mortonCode;
		}

		public MortonCode3D(int mortonCode)
			: this(unchecked((uint)mortonCode))
		{
		}

		public MortonCode3D(Index3D index)
			: this(Split(index.X) | (Split(index.Y) << 1) | (Split(index.Z) << 2))
		{
		}

		/// <inheritdoc />
		public Index3D ToIndex() => new Index3D(
			Compact(this.mortonCode), Compact(this.mortonCode >> 1), Compact(this.mortonCode >> 2));

		/// <inheritdoc />
		public int ToInt() => unchecked((int)this.mortonCode);

		private static uint Split(int value)
		{
			unchecked
			{
				uint x = (uint)value;
				x &= 0x000003ff;                    // x = ---- ---- ---- ---- ---- --98 7654 3210
				x = (x | (x << 16)) & 0x030000FF;   // x = ---- --98 ---- ---- ---- ---- 7654 3210
				x = (x | (x << 8)) & 0x0300F00F;    // x = ---- --98 ---- ---- 7654 ---- ---- 3210
				x = (x | (x << 4)) & 0x030C30C3;    // x = ---- --98 ---- 76-- --54 ---- 32-- --10
				x = (x | (x << 2)) & 0x09249249;    // x = ---- 9--8 --7- -6-- 5--4 --3- -2-- 1--0
				return x;
			}
		}

		private static int Compact(uint x)
		{
			unchecked
			{
				x &= 0x09249249;                    // x = ---- 9--8 --7- -6-- 5--4 --3- -2-- 1--0
				x = (x | (x >> 2)) & 0x030c30c3;    // x = ---- --98 ---- 76-- --54 ---- 32-- --10
				x = (x | (x >> 4)) & 0x0300f00f;    // x = ---- --98 ---- ---- 7654 ---- ---- 3210
				x = (x | (x >> 8)) & 0x030000FF;    // x = ---- --98 ---- ---- ---- ---- 7654 3210
				x = (x | (x >> 16)) & 0x000003ff;   // x = ---- ---- ---- ---- ---- --98 7654 3210
				return (int)x;
			}
		}
	}
}
