using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Stages.Voxels.Pact.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public struct SerializedVoxelGridChunk : IKeyed<ChunkKey>, ISerializedData
	{
		public SerializedVoxelGridChunk(ChunkKey key, byte[] serializedData)
		{
			Contracts.Requires.That(serializedData != null);

			this.Key = key;
			this.SerializedData = serializedData;
		}

		/// <inheritdoc />
		public ChunkKey Key { get; }

		/// <inheritdoc />
		public byte[] SerializedData { get; }
	}
}
