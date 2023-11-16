using System;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Utility.Common.Core.Types
{
	/// <summary>
	/// A generic identifier to use for a simple key.
	/// </summary>
	/// <typeparam name="T">The type of the identifier.</typeparam>
	public struct OrderedKeyStruct<T> : IKeyed<T>, IComparable<OrderedKeyStruct<T>>, IEquatable<OrderedKeyStruct<T>>
		where T : struct, IComparable<T>, IEquatable<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OrderedKeyStruct{T}"/> struct.
		/// </summary>
		/// <param name="key">The identifier.</param>
		public OrderedKeyStruct(T key)
		{
			this.Key = key;
		}

		#region IKey<T> Members

		/// <inheritdoc />
		public T Key { get; }

		#endregion

		#region Implicit Conversion Operators

		public static implicit operator OrderedKeyStruct<T>(T value) => new OrderedKeyStruct<T>(value);

		public static implicit operator T(OrderedKeyStruct<T> value) => value.Key;

		#endregion

		#region Equality Operators

		public static bool operator ==(OrderedKeyStruct<T> lhs, OrderedKeyStruct<T> rhs) => lhs.Equals(rhs);

		public static bool operator !=(OrderedKeyStruct<T> lhs, OrderedKeyStruct<T> rhs) => !lhs.Equals(rhs);

		#endregion

		#region Comparison Operators

		public static bool operator >(OrderedKeyStruct<T> lhs, OrderedKeyStruct<T> rhs) => lhs.IsGreaterThan(rhs);

		public static bool operator <(OrderedKeyStruct<T> lhs, OrderedKeyStruct<T> rhs) => lhs.IsLessThan(rhs);

		public static bool operator >=(OrderedKeyStruct<T> lhs, OrderedKeyStruct<T> rhs) => lhs.IsGreaterThanOrEqual(rhs);

		public static bool operator <=(OrderedKeyStruct<T> lhs, OrderedKeyStruct<T> rhs) => lhs.IsLessThanOrEqual(rhs);

		#endregion

		#region IComparable<OrderedID<T>> Members

		/// <inheritdoc />
		public int CompareTo(OrderedKeyStruct<T> other) => this.Key.CompareTo(other.Key);

		#endregion

		#region IEquatable<OrderedID<T>> Members

		/// <inheritdoc />
		public bool Equals(OrderedKeyStruct<T> other) => this.Key.Equals(other.Key);

		#endregion

		#region Object Overrides

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => this.Key.GetHashCode();

		/// <inheritdoc />
		public override string ToString() => this.Key.ToString();

		#endregion
	}
}
