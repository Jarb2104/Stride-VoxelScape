using Voxelscape.Stages.Management.Pact.Persistence;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapChunkPersister :
		IChunkPersister<ISkyIslandMapChunk, SerializedSkyIslandMapChunk>
	{
		private readonly IInlineSerializerDeserializer<SkyIslandMapChunkResources> serializer;

		public SkyIslandMapChunkPersister(IInlineSerializerDeserializer<SkyIslandMapChunkResources> serializer)
		{
			Contracts.Requires.That(serializer != null);

			this.serializer = serializer;
		}

		/// <inheritdoc />
		public SerializedSkyIslandMapChunk ToPersistable(ISkyIslandMapChunk chunk)
		{
			IChunkPersisterContracts.ToPersistable(chunk);

			return new SerializedSkyIslandMapChunk(chunk.Key, this.serializer.Serialize(chunk.GetResources()));
		}

		/// <inheritdoc />
		public void FromPersistable(SerializedSkyIslandMapChunk persistable, ISkyIslandMapChunk chunk)
		{
			IChunkPersisterContracts.FromPersistable(persistable, chunk);

			this.serializer.DeserializeInline(persistable.SerializedData, chunk.GetResources());
		}
	}
}
