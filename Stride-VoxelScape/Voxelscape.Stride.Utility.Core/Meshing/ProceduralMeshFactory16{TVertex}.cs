using Stride.Graphics;
using Stride.Rendering;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Stride.Utility.Pact.Meshing;
using Voxelscape.Stride.Utility.Pact.Vertices;

namespace Voxelscape.Stride.Utility.Core.Meshing
{
	public class ProceduralMeshFactory16<TVertex> :
		AbstractProceduralMeshFactory<TVertex>, IFactory<IMeshData16<TVertex>, Mesh>
		where TVertex : struct
	{
		public ProceduralMeshFactory16(IVertexFormat<TVertex> format, GraphicsDevice graphicsDevice)
			: base(format, graphicsDevice)
		{
		}

		/// <inheritdoc />
		public Mesh Create(IMeshData16<TVertex> meshData)
		{
			Contracts.Requires.That(meshData != null);
			MeshData.VerifyContracts(meshData);

			var indexBuffer = new IndexBufferBinding(
				Buffer.Index.New(this.GraphicsDevice, meshData.Indices16, GraphicsResourceUsage.Immutable),
				is32Bit: false,
				count: meshData.IndicesCount);

			return this.CreateMesh(indexBuffer, meshData);
		}
	}
}
