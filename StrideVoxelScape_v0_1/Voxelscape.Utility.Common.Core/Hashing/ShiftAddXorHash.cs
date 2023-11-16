using Voxelscape.Utility.Common.Pact.Hashing;

namespace Voxelscape.Utility.Common.Core.Hashing
{
	/// <summary>
	///
	/// </summary>
	/// <seealso href="http://www.eternallyconfuzzled.com/tuts/algorithms/jsw_tut_hashing.aspx"/>
	public struct ShiftAddXorHash : IHash<ShiftAddXorHash>
	{
		private readonly uint hash;

		public ShiftAddXorHash(uint hash)
		{
			this.hash = hash;
		}

		public ShiftAddXorHash(int hash)
			: this((uint)hash)
		{
		}

		/// <inheritdoc />
		public int Result => (int)this.hash;

		public static implicit operator int(ShiftAddXorHash value) => value.Result;

		/// <inheritdoc />
		public ShiftAddXorHash And<T>(T value)
		{
			unchecked
			{
				uint hash = this.hash;
				hash ^= (hash << 5) + (hash >> 2) + (uint)value.GetHashCodeNullSafe();
				return new ShiftAddXorHash(hash);
			}
		}
	}
}
