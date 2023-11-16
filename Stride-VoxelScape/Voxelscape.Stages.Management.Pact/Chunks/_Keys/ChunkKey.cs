using System;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Common.Core.Types;

namespace Voxelscape.Stages.Management.Pact.Chunks
{
	public struct ChunkKey : IChunkKey<Index3D>, IEquatable<ChunkKey>
	{
		public ChunkKey(int x, int y, int z)
		{
			this.Index = new Index3D(x, y, z);
		}

		public ChunkKey(Index3D index)
		{
			this.Index = index;
		}

		/// <inheritdoc />
		public Index3D Index { get; }

		public static ChunkKey operator +(ChunkKey lhs, ChunkKey rhs) => new ChunkKey(lhs.Index + rhs.Index);

		public static ChunkKey operator -(ChunkKey lhs, ChunkKey rhs) => new ChunkKey(lhs.Index - rhs.Index);

		public static bool operator ==(ChunkKey lhs, ChunkKey rhs) => lhs.Equals(rhs);

		public static bool operator !=(ChunkKey lhs, ChunkKey rhs) => !lhs.Equals(rhs);

		/// <inheritdoc />
		public bool Equals(ChunkKey other) => this.Index == other.Index;

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => this.Index.GetHashCode();

		/// <inheritdoc />
		public override string ToString() => $"({this.Index})";

		public ChunkOverheadKey ToOverheadKey() => new ChunkOverheadKey(this.Index.ProjectDownYAxis());
	}
}
