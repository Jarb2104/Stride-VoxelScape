using System.Linq;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Stride.Utility.Pact.Meshing;

namespace Voxelscape.Stride.Utility.Core.Meshing
{
	public class MeshDataTransfer16<TVertex> : IMeshData16<TVertex>, IMeshDataTransfer<TVertex>
		where TVertex : struct
	{
		private readonly ExpandableArray<ushort> indices;

		private readonly ExpandableArray<TVertex> vertices;

		public MeshDataTransfer16(MeshDataTransferOptions options = null)
		{
			Contracts.Requires.That(options?.IsValid16Bit() ?? true);

			options = MeshDataTransferOptions.New16Bit();

			this.indices = new ExpandableArray<ushort>(options.InitialIndices);
			this.vertices = new ExpandableArray<TVertex>(options.InitialVertices);
			this.MaxVertices = options.MaxVertices;
		}

		/// <inheritdoc />
		public ushort[] Indices16 => this.indices.Array;

		/// <inheritdoc />
		public int IndicesCount { get; private set; } = 0;

		/// <inheritdoc />
		public TVertex[] Vertices => this.vertices.Array;

		/// <inheritdoc />
		public int VerticesCount { get; private set; } = 0;

		/// <inheritdoc />
		public int MaxVertices { get; }

		/// <inheritdoc />
		public void CopyIn(IDivisibleMesh<TVertex> mesh, VertexWindingOrder winding)
		{
			IMeshDataTransferContracts.CopyIn(this, mesh, winding);

			var triangles = mesh.GetTriangles(winding);
			this.IndicesCount = triangles.Count;
			this.indices.CopyIn(
				CountedEnumerable.New(triangles.Select(index => (ushort)index), triangles.Count));

			this.VerticesCount = mesh.Vertices.Count;
			this.vertices.CopyIn(mesh.Vertices.AsCounted(), this.MaxVertices);
		}
	}
}
