using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Types
{
	public struct FaultableValue<T> : IEquatable<FaultableValue<T>>
	{
		/// <summary>
		/// The value if successful.
		/// </summary>
		private readonly T value;

		/// <summary>
		/// Initializes a new instance of the <see cref="FaultableValue{T}"/> struct.
		/// </summary>
		/// <param name="value">The value.</param>
		public FaultableValue(T value)
		{
			this.value = value;
			this.Exception = null;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FaultableValue{T}"/> struct.
		/// </summary>
		/// <param name="exception">The exception.</param>
		public FaultableValue(Exception exception)
		{
			Contracts.Requires.That(exception != null);

			this.value = default(T);
			this.Exception = exception;
		}

		/// <summary>
		/// Gets a value indicating whether this instance has a value assigned to it.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance has a value; otherwise, <c>false</c>.
		/// </value>
		public bool HasValue => this.Exception == null;

		public bool HasException => this.Exception != null;

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
				if (this.Exception != null)
				{
					throw this.Exception;
				}

				return this.value;
			}
		}

		public Exception Exception { get; }

		public static bool operator ==(FaultableValue<T> lhs, FaultableValue<T> rhs) => lhs.Equals(rhs);

		public static bool operator !=(FaultableValue<T> lhs, FaultableValue<T> rhs) => !lhs.Equals(rhs);

		/// <inheritdoc />
		public bool Equals(FaultableValue<T> other) =>
			(this.HasValue && other.HasValue && this.value.EqualsNullSafe(other.value)) ||
			(this.HasException && other.HasException && this.Exception.Equals(other.Exception));

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() =>
			this.HasValue ? this.value.GetHashCodeNullSafe() : this.Exception.GetHashCode();

		/// <inheritdoc />
		public override string ToString() =>
			this.HasValue ? this.value.ToStringNullSafe() : this.Exception.ToString();
	}
}
