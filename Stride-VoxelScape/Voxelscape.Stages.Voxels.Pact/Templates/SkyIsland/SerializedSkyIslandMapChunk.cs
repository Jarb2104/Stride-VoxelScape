using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public struct SerializedSkyIslandMapChunk : IKeyed<ChunkOverheadKey>, ISerializedData
	{
		public SerializedSkyIslandMapChunk(ChunkOverheadKey key, byte[] serializedData)
		{
			Contracts.Requires.That(serializedData != null);

			this.Key = key;
			this.SerializedData = serializedData;
		}

		/// <inheritdoc />
		public ChunkOverheadKey Key { get; }

		/// <inheritdoc />
		public byte[] SerializedData { get; }
	}
}
