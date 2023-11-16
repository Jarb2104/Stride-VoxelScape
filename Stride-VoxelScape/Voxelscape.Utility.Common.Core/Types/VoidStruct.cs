using System;

namespace Voxelscape.Utility.Common.Core.Types
{
	/// <summary>
	/// An empty struct to be used with generic classes that don't offer a non-generic version.
	/// For example, task completion source.
	/// </summary>
	public struct VoidStruct : IEquatable<VoidStruct>
	{
		public static bool operator ==(VoidStruct lhs, VoidStruct rhs) => lhs.Equals(rhs);

		public static bool operator !=(VoidStruct lhs, VoidStruct rhs) => !lhs.Equals(rhs);

		/// <inheritdoc />
		public bool Equals(VoidStruct other) => true;

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => 0;
	}
}
