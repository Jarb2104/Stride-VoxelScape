using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Stride.Utility.Pact.Meshing;
using Voxelscape.Stride.Utility.Pact.Vertices;
using Stride.Graphics;
using Stride.Rendering;

namespace Voxelscape.Stride.Utility.Core.Meshing
{
	public class ProceduralMeshFactory32<TVertex> :
		AbstractProceduralMeshFactory<TVertex>, IFactory<IMeshData32<TVertex>, Mesh>
		where TVertex : struct
	{
		public ProceduralMeshFactory32(IVertexFormat<TVertex> format, GraphicsDevice graphicsDevice)
			: base(format, graphicsDevice)
		{
			Contracts.Requires.That(
				graphicsDevice.Supports32BitMeshIndices(),
				"Can't use 32 bit indices on feature level HW <= 9.3");
		}

		/// <inheritdoc />
		public Mesh Create(IMeshData32<TVertex> meshData)
		{
			Contracts.Requires.That(meshData != null);
			MeshData.VerifyContracts(meshData);

			var indexBuffer = new IndexBufferBinding(
				Buffer.Index.New(this.GraphicsDevice, meshData.Indices32, GraphicsResourceUsage.Immutable),
				is32Bit: true,
				count: meshData.IndicesCount);

			return this.CreateMesh(indexBuffer, meshData);
		}
	}
}
