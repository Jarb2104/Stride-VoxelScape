using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Collections;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// Provides identity comparison and equality logic for any reference type.
	/// </summary>
	/// <typeparam name="T">The type of the values.</typeparam>
	public class IdentityEqualityComparer<T> : EqualityComparer<T>
		where T : class
	{
		/// <summary>
		/// Prevents a default instance of the <see cref="IdentityEqualityComparer{T}"/> class from being created.
		/// </summary>
		private IdentityEqualityComparer()
		{
		}

		/// <summary>
		/// Gets the singleton instance of the <see cref="IdentityEqualityComparer{T}"/> comparer.
		/// </summary>
		/// <value>
		/// The singleton comparer instance.
		/// </value>
		public static IdentityEqualityComparer<T> Instance { get; } = new IdentityEqualityComparer<T>();

		#region Overriding EqualityComparer<T> Members

		/// <inheritdoc />
		public override bool Equals(T a, T b) => a.EqualsByReferenceNullSafe(b);

		/// <inheritdoc />
		public override int GetHashCode(T obj)
		{
			IEqualityComparerContracts.GetHashCode(obj);

			return obj.GetHashCodeByReferenceNullSafe();
		}

		#endregion
	}
}
