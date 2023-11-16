using System;
using Voxelscape.Utility.Common.Core.Hashing;
using Voxelscape.Utility.Common.Core.Types;
using HashCode = Voxelscape.Utility.Common.Core.Hashing.HashCode;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// Represents an 8-tuple, or octuple.
	/// </summary>
	/// <typeparam name="T1">The type of the first value.</typeparam>
	public struct TupleStruct<T1> : IEquatable<TupleStruct<T1>>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TupleStruct{T1}"/> struct.
		/// </summary>
		/// <param name="item1">The first value.</param>
		public TupleStruct(T1 item1)
		{
			this.Item1 = item1;
		}

		#region Properties

		/// <summary>
		/// Gets the first value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public T1 Item1 { get; }

		#endregion

		#region Equality Operators

		public static bool operator ==(TupleStruct<T1> lhs, TupleStruct<T1> rhs) => lhs.Equals(rhs);

		public static bool operator !=(TupleStruct<T1> lhs, TupleStruct<T1> rhs) => !lhs.Equals(rhs);

		#endregion

		#region IEquatable<TupleStruct<T1>> Members

		/// <inheritdoc />
		public bool Equals(TupleStruct<T1> other) => this.Item1.EqualsNullSafe(other.Item1);

		#endregion

		#region Object Overrides

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => HashCode.Start.And(this.Item1);

		/// <inheritdoc />
		public override string ToString() => string.Format("({0})", this.Item1.ToStringNullSafe());

		#endregion
	}
}
