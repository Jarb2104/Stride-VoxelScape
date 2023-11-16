using System;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Utility.Common.Core.Types
{
	/// <summary>
	/// A generic identifier to use for a simple key.
	/// </summary>
	/// <typeparam name="T">The type of the identifier.</typeparam>
	public struct KeyStruct<T> : IKeyed<T>, IEquatable<KeyStruct<T>>
		where T : struct, IEquatable<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="KeyStruct{T}"/> struct.
		/// </summary>
		/// <param name="key">The identifier.</param>
		public KeyStruct(T key)
		{
			this.Key = key;
		}

		#region IKey<T> Members

		/// <inheritdoc />
		public T Key { get; }

		#endregion

		#region Implicit Conversion Operators

		public static implicit operator KeyStruct<T>(T value) => new KeyStruct<T>(value);

		public static implicit operator T(KeyStruct<T> value) => value.Key;

		#endregion

		#region Equality Operators

		public static bool operator ==(KeyStruct<T> lhs, KeyStruct<T> rhs) => lhs.Equals(rhs);

		public static bool operator !=(KeyStruct<T> lhs, KeyStruct<T> rhs) => !lhs.Equals(rhs);

		#endregion

		#region IEquatable<ID<T>> Members

		/// <inheritdoc />
		public bool Equals(KeyStruct<T> other) => this.Key.Equals(other.Key);

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
