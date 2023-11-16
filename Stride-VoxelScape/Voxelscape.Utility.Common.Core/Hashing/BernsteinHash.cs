using Voxelscape.Utility.Common.Pact.Hashing;

namespace Voxelscape.Utility.Common.Core.Hashing
{
	/// <summary>
	/// Modified version of the Bernstein Hash.
	/// </summary>
	/// <seealso href="http://www.eternallyconfuzzled.com/tuts/algorithms/jsw_tut_hashing.aspx"/>
	public struct BernsteinHash : IHash<BernsteinHash>
	{
		private readonly uint hash;

		public BernsteinHash(uint hash)
		{
			this.hash = hash;
		}

		public BernsteinHash(int hash)
			: this((uint)hash)
		{
		}

		/// <inheritdoc />
		public int Result => (int)this.hash;

		public static implicit operator int(BernsteinHash value) => value.Result;

		/// <inheritdoc />
		public BernsteinHash And<T>(T value)
		{
			unchecked
			{
				uint hash = this.hash;

				// optimized way to perform currentHash *= 31
				hash <<= 5;
				hash--;

				hash ^= (uint)value.GetHashCodeNullSafe();
				return new BernsteinHash(hash);
			}
		}
	}
}
