using Voxelscape.Utility.Common.Pact.Hashing;

namespace Voxelscape.Utility.Common.Core.Hashing
{
	/// <summary>
	///
	/// </summary>
	/// <seealso href="http://www.eternallyconfuzzled.com/tuts/algorithms/jsw_tut_hashing.aspx"/>
	public struct OneAtATimeHash : IHash<OneAtATimeHash>
	{
		private readonly uint hash;

		public OneAtATimeHash(uint hash)
		{
			this.hash = hash;
		}

		public OneAtATimeHash(int hash)
			: this((uint)hash)
		{
		}

		/// <inheritdoc />
		public int Result
		{
			get
			{
				unchecked
				{
					uint hash = this.hash;
					hash += hash << 3;
					hash ^= hash >> 11;
					hash += hash << 15;
					return (int)hash;
				}
			}
		}

		public static implicit operator int(OneAtATimeHash value) => value.Result;

		/// <inheritdoc />
		public OneAtATimeHash And<T>(T value)
		{
			unchecked
			{
				uint hash = this.hash;
				hash += (uint)value.GetHashCodeNullSafe();
				hash += hash << 10;
				hash ^= hash >> 6;
				return new OneAtATimeHash(hash);
			}
		}
	}
}
