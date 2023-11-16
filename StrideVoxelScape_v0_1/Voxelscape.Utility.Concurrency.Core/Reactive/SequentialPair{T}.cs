using System;
using Voxelscape.Utility.Common.Core.Hashing;
using Voxelscape.Utility.Common.Core.Types;
using HashCode = Voxelscape.Utility.Common.Core.Hashing.HashCode;

namespace Voxelscape.Utility.Concurrency.Core.Reactive
{
	/// <summary>
	/// Represents a previous-next pair of values from a sequence.
	/// </summary>
	/// <typeparam name="T">The type of the values.</typeparam>
	public struct SequentialPair<T> : IEquatable<SequentialPair<T>>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SequentialPair{T}"/> struct.
		/// </summary>
		/// <param name="previous">The previous value.</param>
		/// <param name="next">The next value.</param>
		public SequentialPair(T previous, T next)
		{
			this.Previous = previous;
			this.Next = next;
		}

		#region Properties

		/// <summary>
		/// Gets the previous value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public T Previous { get; }

		/// <summary>
		/// Gets the next value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public T Next { get; }

		#endregion

		#region Equality Operators

		public static bool operator ==(SequentialPair<T> lhs, SequentialPair<T> rhs) => lhs.Equals(rhs);

		public static bool operator !=(SequentialPair<T> lhs, SequentialPair<T> rhs) => !lhs.Equals(rhs);

		#endregion

		#region IEquatable<SequentialPair<T>> Members

		/// <inheritdoc />
		public bool Equals(SequentialPair<T> other) =>
			this.Previous.EqualsNullSafe(other.Previous) && this.Next.EqualsNullSafe(other.Next);

		#endregion

		#region Object Overrides

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => HashCode.Start.And(this.Previous).And(this.Next);

		/// <inheritdoc />
		public override string ToString() => $"({this.Previous.ToStringNullSafe()}, {this.Next.ToStringNullSafe()})";

		#endregion
	}
}
