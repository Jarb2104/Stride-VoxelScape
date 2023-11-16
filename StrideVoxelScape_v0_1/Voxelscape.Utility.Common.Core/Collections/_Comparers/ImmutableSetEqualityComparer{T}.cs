using System.Collections.Generic;
using System.Collections.Immutable;
using Voxelscape.Utility.Common.Pact.Collections;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// Provides comparison and equality logic for <see cref="IImmutableSet{T}" />s.
	/// </summary>
	/// <typeparam name="T">The type of values contained in the sets.</typeparam>
	public class ImmutableSetEqualityComparer<T> : EqualityComparer<IImmutableSet<T>>
	{
		/// <summary>
		/// Prevents a default instance of the <see cref="ImmutableSetEqualityComparer{T}"/> class from being created.
		/// </summary>
		private ImmutableSetEqualityComparer()
		{
		}

		/// <summary>
		/// Gets the singleton instance of the <see cref="ImmutableSetEqualityComparer{T}"/> comparer.
		/// </summary>
		/// <value>
		/// The singleton comparer instance.
		/// </value>
		public static ImmutableSetEqualityComparer<T> Instance { get; } = new ImmutableSetEqualityComparer<T>();

		#region Overriding EqualityComparer<IImmutableSet<T>> Members

		/// <inheritdoc />
		public override bool Equals(IImmutableSet<T> a, IImmutableSet<T> b)
		{
			if (a == b)
			{
				return true;
			}

			return a.SetEquals(b);
		}

		/// <inheritdoc />
		public override int GetHashCode(IImmutableSet<T> obj)
		{
			IEqualityComparerContracts.GetHashCode(obj);

			int result = 0;

			foreach (T value in obj)
			{
				result ^= value.GetHashCodeNullSafe();
			}

			return result;
		}

		#endregion
	}
}
