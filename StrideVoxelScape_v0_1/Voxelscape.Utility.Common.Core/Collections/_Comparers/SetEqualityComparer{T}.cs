using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Collections;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// Provides comparison and equality logic for <see cref="ISet{T}" />s.
	/// </summary>
	/// <typeparam name="T">The type of values contained in the sets.</typeparam>
	public class SetEqualityComparer<T> : EqualityComparer<ISet<T>>
	{
		/// <summary>
		/// Prevents a default instance of the <see cref="SetEqualityComparer{T}"/> class from being created.
		/// </summary>
		private SetEqualityComparer()
		{
		}

		/// <summary>
		/// Gets the singleton instance of the <see cref="SetEqualityComparer{T}"/> comparer.
		/// </summary>
		/// <value>
		/// The singleton comparer instance.
		/// </value>
		public static SetEqualityComparer<T> Instance { get; } = new SetEqualityComparer<T>();

		#region Overriding EqualityComparer<ISet<T>> Members

		/// <inheritdoc />
		public override bool Equals(ISet<T> a, ISet<T> b)
		{
			if (a == b)
			{
				return true;
			}

			return a.SetEquals(b);
		}

		/// <inheritdoc />
		public override int GetHashCode(ISet<T> obj)
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
