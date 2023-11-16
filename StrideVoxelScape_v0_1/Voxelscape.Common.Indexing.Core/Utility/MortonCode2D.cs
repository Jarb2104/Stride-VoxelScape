using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Indices;

namespace Voxelscape.Common.Indexing.Core.Utility
{
	/// <summary>
	///
	/// </summary>
	public struct MortonCode2D : IMortonCode<Index2D>
	{
		private readonly uint mortonCode;

		public MortonCode2D(uint mortonCode)
		{
			this.mortonCode = mortonCode;
		}

		public MortonCode2D(int mortonCode)
			: this(unchecked((uint)mortonCode))
		{
		}

		public MortonCode2D(Index2D index)
			: this(Split(index.X) | (Split(index.Y) << 1))
		{
		}

		/// <inheritdoc />
		public Index2D ToIndex() => new Index2D(Compact(this.mortonCode), Compact(this.mortonCode >> 1));

		/// <inheritdoc />
		public int ToInt() => unchecked((int)this.mortonCode);

		private static uint Split(int value)
		{
			unchecked
			{
				uint x = (uint)value;
				x &= 0x0000ffff;                    // x = ---- ---- ---- ---- fedc ba98 7654 3210
				x = (x | (x << 16)) & 0x00ff00ff;   // x = ---- ---- fedc ba98 ---- ---- 7654 3210
				x = (x | (x << 8)) & 0x0f0f0f0f;    // x = ---- fedc ---- ba98 ---- 7654 ---- 3210
				x = (x | (x << 4)) & 0x33333333;    // x = --fe --dc --ba --98 --76 --54 --32 --10
				x = (x | (x << 2)) & 0x55555555;    // x = -f-e -d-c -b-a -9-8 -7-6 -5-4 -3-2 -1-0
				return x;
			}
		}

		private static int Compact(uint x)
		{
			unchecked
			{
				x &= 0x55555555;                    // x = -f-e -d-c -b-a -9-8 -7-6 -5-4 -3-2 -1-0
				x = (x | (x >> 2)) & 0x33333333;    // x = --fe --dc --ba --98 --76 --54 --32 --10
				x = (x | (x >> 4)) & 0x0f0f0f0f;    // x = ---- fedc ---- ba98 ---- 7654 ---- 3210
				x = (x | (x >> 8)) & 0x00ff00ff;    // x = ---- ---- fedc ba98 ---- ---- 7654 3210
				x = (x | (x >> 16)) & 0x0000ffff;   // x = ---- ---- ---- ---- fedc ba98 7654 3210
				return (int)x;
			}
		}
	}
}
