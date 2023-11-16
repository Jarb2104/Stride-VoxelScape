using Voxelscape.Utility.Common.Pact.Hashing;

namespace Voxelscape.Utility.Common.Core.Hashing
{
	/// <summary>
	///
	/// </summary>
	/// <seealso href="http://www.eternallyconfuzzled.com/tuts/algorithms/jsw_tut_hashing.aspx"/>
	public struct FNVHash : IHash<FNVHash>
	{
		private readonly uint hash;

		public FNVHash(uint hash)
		{
			this.hash = hash;
		}

		public FNVHash(int hash)
			: this((uint)hash)
		{
		}

		/// <inheritdoc />
		public int Result => (int)this.hash;

		public static implicit operator int(FNVHash value) => value.Result;

		/// <inheritdoc />
		public FNVHash And<T>(T value)
		{
			unchecked
			{
				uint hash = this.hash;
				hash = (hash * 16777619) ^ (uint)value.GetHashCodeNullSafe();
				return new FNVHash(hash);
			}
		}
	}
}
