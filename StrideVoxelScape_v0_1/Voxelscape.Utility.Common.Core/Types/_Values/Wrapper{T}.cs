namespace Voxelscape.Utility.Common.Core.Types
{
	/// <summary>
	/// Wraps any value in an object. This allows value types to be passed by reference without the possible confusion
	/// of auto boxing.
	/// </summary>
	/// <typeparam name="T">The type of value being wrapped in an object.</typeparam>
	public class Wrapper<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Wrapper{T}"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public Wrapper(T value)
		{
			this.Value = value;
		}

		/// <summary>
		/// Gets or sets the value being wrapped in an object.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public T Value { get; set; }

		#region Implicit Coversion Operators

		public static implicit operator T(Wrapper<T> obj) => obj.Value;

		public static implicit operator Wrapper<T>(T value) => new Wrapper<T>(value);

		#endregion

		#region Equality Operators

		public static bool operator ==(Wrapper<T> lhs, Wrapper<T> rhs) => lhs.EqualsNullSafe(rhs);

		public static bool operator !=(Wrapper<T> lhs, Wrapper<T> rhs) => !lhs.EqualsNullSafe(rhs);

		#endregion

		#region Object Overrides

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (this.EqualsByReferenceNullSafe(obj))
			{
				return true;
			}

			var other = obj as Wrapper<T>;
			if (other == null)
			{
				return false;
			}

			return this.Value.EqualsNullSafe(other.Value);
		}

		/// <inheritdoc />
		public override int GetHashCode() => this.Value.GetHashCodeNullSafe();

		/// <inheritdoc />
		public override string ToString() => this.Value.ToStringNullSafe();

		#endregion
	}
}
