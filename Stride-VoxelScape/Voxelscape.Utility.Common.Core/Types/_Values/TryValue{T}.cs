using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Types
{
	/// <summary>
	/// Represents a value of any type that can be in an assigned or unassigned state.
	/// This can include reference types that use null as a valid assigned value.
	/// </summary>
	/// <typeparam name="T">The type of the value contained within the try operation result.</typeparam>
	/// <remarks>
	/// The 'try' method pattern is a method that returns a boolean success value and a result value through
	/// an output parameter. This wrapper can be used to represent such a method when the normal pattern cannot
	/// be applied. For example; when providing an asynchronous version of such a method you can use a task that
	/// returns a <see cref="TryValue{T}"/> as the return type and no output parameter because output parameters
	/// don't work with asynchronous methods.
	/// </remarks>
	public struct TryValue<T> : IEquatable<TryValue<T>>
	{
		/// <summary>
		/// The value.
		/// </summary>
		private readonly T value;

		/// <summary>
		/// Initializes a new instance of the <see cref="TryValue{T}"/> struct.
		/// </summary>
		/// <param name="value">The value.</param>
		public TryValue(T value)
		{
			this.value = value;
			this.HasValue = true;
		}

		/// <summary>
		/// Gets a value indicating whether this instance has a value assigned to it.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance has a value; otherwise, <c>false</c>.
		/// </value>
		public bool HasValue { get; }

		/// <summary>
		/// Gets the value if it has been assigned. This may only be accessed if there is a value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public T Value
		{
			get
			{
				Contracts.Requires.That(this.HasValue);

				return this.value;
			}
		}

		public static bool operator ==(TryValue<T> lhs, TryValue<T> rhs) => lhs.Equals(rhs);

		public static bool operator !=(TryValue<T> lhs, TryValue<T> rhs) => !lhs.Equals(rhs);

		/// <inheritdoc />
		public bool Equals(TryValue<T> other) =>
			(this.HasValue && other.HasValue && this.value.EqualsNullSafe(other.value)) ||
			(!this.HasValue && !other.HasValue);

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => this.value.GetHashCodeNullSafe();

		/// <inheritdoc />
		public override string ToString() => this.value.ToStringNullSafe();

		public bool UnpackToTryMethodPattern(out T value)
		{
			if (this.HasValue)
			{
				value = this.Value;
				return true;
			}
			else
			{
				value = default(T);
				return false;
			}
		}

		public TryValue<TResult> Select<TResult>(Func<T, TResult> selector)
		{
			Contracts.Requires.That(selector != null);

			if (this.HasValue)
			{
				return TryValue.New(selector(this.Value));
			}
			else
			{
				return TryValue.None<TResult>();
			}
		}
	}
}
