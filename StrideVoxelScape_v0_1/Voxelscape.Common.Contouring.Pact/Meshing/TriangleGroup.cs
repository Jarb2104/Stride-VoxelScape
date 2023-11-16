using System;
using Voxelscape.Utility.Common.Core.Hashing;
using Voxelscape.Utility.Common.Core.Types;
using HashCode = Voxelscape.Utility.Common.Core.Hashing.HashCode;

namespace Voxelscape.Common.Contouring.Pact.Meshing
{
	/// <summary>
	///
	/// </summary>
	public struct TriangleGroup : IEquatable<TriangleGroup>
	{
		public TriangleGroup(byte triangles, byte vertices)
		{
			this.Triangles = triangles;
			this.Vertices = vertices;
		}

		public static TriangleGroup Empty => default(TriangleGroup);

		public byte Triangles { get; }

		public int Offsets => this.Triangles * MeshConstants.VerticesPerTriangle;

		public byte Vertices { get; }

		public static bool operator ==(TriangleGroup lhs, TriangleGroup rhs) => lhs.Equals(rhs);

		public static bool operator !=(TriangleGroup lhs, TriangleGroup rhs) => !lhs.Equals(rhs);

		/// <inheritdoc />
		public bool Equals(TriangleGroup other) =>
			this.Triangles == other.Triangles && this.Vertices == other.Vertices;

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => HashCode.Start.And(this.Triangles).And(this.Vertices);

		/// <inheritdoc />
		public override string ToString() => $"Triangles: {this.Triangles}, Vertices: {this.Vertices}";
	}
}
