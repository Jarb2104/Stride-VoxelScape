using System;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Common.Core.Types;

namespace Voxelscape.Stages.Management.Pact.Chunks
{
	public struct ChunkOverheadKey : IChunkKey<Index2D>, IEquatable<ChunkOverheadKey>
	{
		public ChunkOverheadKey(int x, int y)
		{
			this.Index = new Index2D(x, y);
		}

		public ChunkOverheadKey(Index2D index)
		{
			this.Index = index;
		}

		/// <inheritdoc />
		public Index2D Index { get; }

		public static ChunkOverheadKey operator +(ChunkOverheadKey lhs, ChunkOverheadKey rhs) =>
			new ChunkOverheadKey(lhs.Index + rhs.Index);

		public static ChunkOverheadKey operator -(ChunkOverheadKey lhs, ChunkOverheadKey rhs) =>
			new ChunkOverheadKey(lhs.Index - rhs.Index);

		public static bool operator ==(ChunkOverheadKey lhs, ChunkOverheadKey rhs) => lhs.Equals(rhs);

		public static bool operator !=(ChunkOverheadKey lhs, ChunkOverheadKey rhs) => !lhs.Equals(rhs);

		/// <inheritdoc />
		public bool Equals(ChunkOverheadKey other) => this.Index == other.Index;

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => this.Index.GetHashCode();

		/// <inheritdoc />
		public override string ToString() => $"({this.Index})";
	}
}
