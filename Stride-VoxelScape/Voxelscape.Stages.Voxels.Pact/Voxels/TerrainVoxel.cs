using System;
using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Utility.Common.Core.Hashing;
using Voxelscape.Utility.Common.Core.Types;
using HashCode = Voxelscape.Utility.Common.Core.Hashing.HashCode;

namespace Voxelscape.Stages.Voxels.Pact.Voxels
{
	public struct TerrainVoxel : IEquatable<TerrainVoxel>
	{
		public TerrainVoxel(TerrainMaterial material, byte density)
		{
			this.Material = material;
			this.Density = density;
		}

		public TerrainMaterial Material { get; }

		public byte Density { get; }

		public static bool operator ==(TerrainVoxel lhs, TerrainVoxel rhs) => lhs.Equals(rhs);

		public static bool operator !=(TerrainVoxel lhs, TerrainVoxel rhs) => !lhs.Equals(rhs);

		/// <inheritdoc />
		public bool Equals(TerrainVoxel other) => this.Material == other.Material && this.Density == other.Density;

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => HashCode.Start.And(this.Material).And(this.Density);

		/// <inheritdoc />
		public override string ToString() => $"{this.Material} {this.Density}";
	}
}
