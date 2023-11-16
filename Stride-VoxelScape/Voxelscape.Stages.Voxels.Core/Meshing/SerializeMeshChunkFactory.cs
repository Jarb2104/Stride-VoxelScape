using System.Threading.Tasks;
using Voxelscape.Common.Contouring.Core.Vertices;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Meshing;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Stages.Voxels.Core.Meshing
{
	/// <summary>
	///
	/// </summary>
	public class SerializeMeshChunkFactory : IAsyncFactory<ChunkKey, SerializedMeshChunk>
	{
		private readonly IAsyncFactory<ChunkKey, IDisposableValue<IDivisibleMesh<NormalColorTextureVertex>>> meshFactory;

		private readonly ISerializerDeserializer<IDivisibleMesh<NormalColorTextureVertex>> serializer;

		public SerializeMeshChunkFactory(
			IAsyncFactory<ChunkKey, IDisposableValue<IDivisibleMesh<NormalColorTextureVertex>>> meshFactory,
			ISerializerDeserializer<IDivisibleMesh<NormalColorTextureVertex>> serializer)
		{
			Contracts.Requires.That(meshFactory != null);
			Contracts.Requires.That(serializer != null);

			this.meshFactory = meshFactory;
			this.serializer = serializer;
		}

		/// <inheritdoc />
		public async Task<SerializedMeshChunk> CreateAsync(ChunkKey key)
		{
			using (var mesh = await this.meshFactory.CreateAsync(key).DontMarshallContext())
			{
				var data = this.serializer.Serialize(mesh.Value);
				return new SerializedMeshChunk(key, data);
			}
		}
	}
}
