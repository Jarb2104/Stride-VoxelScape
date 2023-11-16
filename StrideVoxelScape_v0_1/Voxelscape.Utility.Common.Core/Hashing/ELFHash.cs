using Voxelscape.Utility.Common.Pact.Hashing;

namespace Voxelscape.Utility.Common.Core.Hashing
{
	/// <summary>
	///
	/// </summary>
	/// <seealso href="http://www.eternallyconfuzzled.com/tuts/algorithms/jsw_tut_hashing.aspx"/>
	public struct ELFHash : IHash<ELFHash>
	{
		private readonly uint hash;

		public ELFHash(uint hash)
		{
			this.hash = hash;
		}

		public ELFHash(int hash)
			: this((uint)hash)
		{
		}

		/// <inheritdoc />
		public int Result => (int)this.hash;

		public static implicit operator int(ELFHash value) => value.Result;

		/// <inheritdoc />
		public ELFHash And<T>(T value)
		{
			unchecked
			{
				uint hash = this.hash;

				hash = (hash << 4) + (uint)value.GetHashCodeNullSafe();
				uint g = hash & 0xf0000000;

				if (g != 0)
				{
					hash ^= g >> 24;
				}

				hash &= ~g;

				return new ELFHash(hash);
			}
		}
	}
}
