using Voxelscape.Stages.Management.Pact.Persistence;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Stages.Voxels.Core.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public class VoxelGridChunkPersister : IChunkPersister<IVoxelGridChunk, SerializedVoxelGridChunk>
	{
		private readonly IInlineSerializerDeserializer<VoxelGridChunkResources> serializer;

		public VoxelGridChunkPersister(IInlineSerializerDeserializer<VoxelGridChunkResources> serializer)
		{
			Contracts.Requires.That(serializer != null);

			this.serializer = serializer;
		}

		/// <inheritdoc />
		public SerializedVoxelGridChunk ToPersistable(IVoxelGridChunk chunk)
		{
			IChunkPersisterContracts.ToPersistable(chunk);

			return new SerializedVoxelGridChunk(chunk.Key, this.serializer.Serialize(chunk.GetResources()));
		}

		/// <inheritdoc />
		public void FromPersistable(SerializedVoxelGridChunk persistable, IVoxelGridChunk chunk)
		{
			IChunkPersisterContracts.FromPersistable(persistable, chunk);

			this.serializer.DeserializeInline(persistable.SerializedData, chunk.GetResources());
		}
	}
}
