using System.Collections.Generic;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Core.Meshing
{
	public class MutableDivisibleMesh<TVertex> : IMutableDivisibleMesh<TVertex>
		where TVertex : struct
	{
		private static readonly string PropertyContractMessage =
			"Must finish adding triangle group before accessing the mesh's components.";

		private readonly TriangleGroupBuilder builder;

		public MutableDivisibleMesh()
			: this(10)
		{
		}

		public MutableDivisibleMesh(int initialGroups)
			: this(
				  initialGroups,
				  initialGroups * MeshConstants.VerticesPerTriangle,
				  initialGroups * MeshConstants.VerticesPerTriangle)
		{
		}

		public MutableDivisibleMesh(
			int initialGroups, int initialOffsets, int initialVertices)
		{
			Contracts.Requires.That(initialGroups >= 0);
			Contracts.Requires.That(initialOffsets >= 0);
			Contracts.Requires.That(initialVertices >= 0);

			this.builder = new TriangleGroupBuilder(initialGroups, initialOffsets, initialVertices);
		}

		/// <inheritdoc />
		public IReadOnlyCollection<TriangleGroup> Groups
		{
			get
			{
				Contracts.Requires.That(this.builder.IsDisposed, PropertyContractMessage);

				return this.builder.Groups;
			}
		}

		/// <inheritdoc />
		public IReadOnlyCollection<byte> Offsets
		{
			get
			{
				Contracts.Requires.That(this.builder.IsDisposed, PropertyContractMessage);

				return this.builder.Offsets;
			}
		}

		/// <inheritdoc />
		public IReadOnlyCollection<TVertex> Vertices
		{
			get
			{
				Contracts.Requires.That(this.builder.IsDisposed, PropertyContractMessage);

				return this.builder.Vertices;
			}
		}

		/// <inheritdoc />
		public ITriangleGroupBuilder<TVertex> AddTriangleGroup(TriangleGroup group)
		{
			IDivisibleMeshBuilderContracts.AddTriangleGroup(group);

			this.builder.InitializeTriangleGroup(group);
			return this.builder;
		}

		/// <inheritdoc />
		public void Clear()
		{
			this.builder.Groups.Clear();
			this.builder.Offsets.Clear();
			this.builder.Vertices.Clear();
		}

		private class TriangleGroupBuilder : ITriangleGroupBuilder<TVertex>
		{
			public TriangleGroupBuilder(int initialGroups, int initialOffsets, int initialVertices)
			{
				Contracts.Requires.That(initialGroups >= 0);
				Contracts.Requires.That(initialOffsets >= 0);
				Contracts.Requires.That(initialVertices >= 0);

				this.Groups = new List<TriangleGroup>(initialGroups);
				this.Offsets = new List<byte>(initialOffsets);
				this.Vertices = new List<TVertex>(initialVertices);
			}

			/// <inheritdoc />
			public bool IsDisposed { get; private set; } = true;

			/// <inheritdoc />
			public TriangleGroup Required { get; private set; }

			/// <inheritdoc />
			public TriangleGroup Current { get; private set; }

			public List<TriangleGroup> Groups { get; }

			public List<byte> Offsets { get; }

			public List<TVertex> Vertices { get; }

			public void InitializeTriangleGroup(TriangleGroup group)
			{
				Contracts.Requires.That(this.IsDisposed);

				this.Groups.Add(group);
				this.Required = group;
				this.Current = TriangleGroup.Empty;
				this.IsDisposed = false;
			}

			/// <inheritdoc />
			public ITriangleGroupBuilder<TVertex> AddTriangleOffsets(byte offsetA, byte offsetB, byte offsetC)
			{
				ITriangleGroupBuilderContracts.AddTriangleOffsets(this, offsetA, offsetB, offsetC);

				this.Offsets.Add(offsetA);
				this.Offsets.Add(offsetB);
				this.Offsets.Add(offsetC);
				this.Current = new TriangleGroup((byte)(this.Current.Triangles + 1), this.Current.Vertices);
				return this;
			}

			/// <inheritdoc />
			public byte AddVertex(TVertex vertex)
			{
				ITriangleGroupBuilderContracts.AddVertex(this, vertex);

				this.Vertices.Add(vertex);
				byte vertexOffset = this.Current.Vertices;
				this.Current = new TriangleGroup(this.Current.Triangles, (byte)(vertexOffset + 1));
				return vertexOffset;
			}

			/// <inheritdoc />
			public void Dispose()
			{
				ITriangleGroupBuilderContracts.Dispose(this);

				this.IsDisposed = true;
			}
		}
	}
}
