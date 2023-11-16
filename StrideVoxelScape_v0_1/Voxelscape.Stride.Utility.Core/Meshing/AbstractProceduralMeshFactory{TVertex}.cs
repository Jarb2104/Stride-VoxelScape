using System;
using System.Linq;
using Stride.Core.Mathematics;
using Stride.Graphics;
using Stride.Rendering;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Stride.Utility.Pact.Meshing;
using Voxelscape.Stride.Utility.Pact.Vertices;
using Voxelscape.Stride.Utility.Core.Vertices;
using Buffer = Stride.Graphics.Buffer;

namespace Voxelscape.Stride.Utility.Core.Meshing
{
	public abstract class AbstractProceduralMeshFactory<TVertex>
		where TVertex : struct
	{
		public AbstractProceduralMeshFactory(IVertexFormat<TVertex> format, GraphicsDevice graphicsDevice)
		{
			Contracts.Requires.That(format != null);
			Contracts.Requires.That(graphicsDevice != null);

			this.Format = format;
			this.GraphicsDevice = graphicsDevice;
		}

		protected IVertexFormat<TVertex> Format { get; }

		protected GraphicsDevice GraphicsDevice { get; }

		protected Mesh CreateMesh(IndexBufferBinding indexBuffer, IMeshData<TVertex> meshData)
		{
			Contracts.Requires.That(indexBuffer != null);
			Contracts.Requires.That(meshData != null);

			VertexPositionNormalColorTexture[] vertices = meshData.Vertices.Select(v => (VertexPositionNormalColorTexture)Convert.ChangeType(v, typeof(VertexPositionNormalColorTexture))).ToArray();
			Buffer<VertexPositionNormalColorTexture> verticesBuffer = Buffer.Vertex.New(GraphicsDevice, vertices, GraphicsResourceUsage.Dynamic);

			var vertexBuffers = new[]
			{
				new VertexBufferBinding(
					verticesBuffer,
					this.Format.Layout,
					meshData.VerticesCount),
			};

			var meshDraw = new MeshDraw()
			{
				IndexBuffer = indexBuffer,
				VertexBuffers = vertexBuffers,
				DrawCount = meshData.IndicesCount,
				PrimitiveType = PrimitiveType.TriangleList,
			};

			// create the bounding volumes
			var boundingBox = BoundingBox.Empty;
			for (int index = 0; index < meshData.VerticesCount; index++)
			{
				var position = this.Format.GetPosition(meshData.Vertices[index]);
				BoundingBox.Merge(ref boundingBox, ref position, out boundingBox);
			}

			var boundingSphere = BoundingSphere.FromBox(boundingBox);

			return new Mesh
			{
				Draw = meshDraw,
				BoundingBox = boundingBox,
				BoundingSphere = boundingSphere,
			};
		}
	}
}
