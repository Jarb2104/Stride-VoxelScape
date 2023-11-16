using System;
using Voxelscape.Utility.Common.Core.Types;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// Defines a key/value pair that can be set or retrieved whose equality if based on only the key.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	public struct KeyEqualityValuePair<TKey, TValue> : IEquatable<KeyEqualityValuePair<TKey, TValue>>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="KeyEqualityValuePair{TKey, TValue}"/> struct.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public KeyEqualityValuePair(TKey key, TValue value)
		{
			this.Key = key;
			this.Value = value;
		}

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>
		/// The key.
		/// </value>
		public TKey Key { get; }

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public TValue Value { get; }

		#region Equality Operators

		public static bool operator ==(KeyEqualityValuePair<TKey, TValue> lhs, KeyEqualityValuePair<TKey, TValue> rhs) =>
			lhs.Equals(rhs);

		public static bool operator !=(KeyEqualityValuePair<TKey, TValue> lhs, KeyEqualityValuePair<TKey, TValue> rhs) =>
			!lhs.Equals(rhs);

		#endregion

		#region IEquatable<KeyEqualityValuePair<TKey,TValue>> Members

		/// <inheritdoc />
		public bool Equals(KeyEqualityValuePair<TKey, TValue> other) => this.Key.EqualsNullSafe(other.Key);

		#endregion

		#region Object Overrides

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => this.Key.GetHashCodeNullSafe();

		/// <inheritdoc />
		public override string ToString() => $"[{this.Key}, {this.Value}]";

		#endregion
	}
}
