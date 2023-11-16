using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Core.Meshing
{
	/// <summary>
	///
	/// </summary>
	/// <remarks>
	/// All of these classes function by 'reading through' from a wrapper to the source meshes.
	/// They do not do bulk copying of values to newly created meshes.
	/// </remarks>
	public static class DivisibleMesh
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void VerifyContracts<TVertex>(IDivisibleMesh<TVertex> mesh)
			where TVertex : struct
		{
			Contracts.Requires.That(mesh != null);

			Contracts.Requires.That(mesh.Groups != null);
			Contracts.Requires.That(mesh.Offsets != null);
			Contracts.Requires.That(mesh.Vertices != null);

			Contracts.Requires.That(!mesh.Groups.Contains(TriangleGroup.Empty));
			Contracts.Requires.That(mesh.Offsets.Count.IsDivisibleBy(MeshConstants.VerticesPerTriangle));
			Contracts.Requires.That(mesh.Offsets.Count == mesh.Groups.Select(group => group.Offsets).Sum());
			Contracts.Requires.That(mesh.Vertices.Count == mesh.Groups.Select(group => (int)group.Vertices).Sum());

			// index offsets for each cluster of polys must not index out of bounds of the vertices
			// defined for that cluster
			var offsets = mesh.Offsets.GetEnumerator();
			foreach (var group in mesh.Groups)
			{
				for (int count = 0; count < group.Offsets; count++)
				{
					var success = offsets.MoveNext();
					Contracts.Requires.That(success);
					Contracts.Requires.That(offsets.Current.IsIn(Range.FromLength(group.Vertices)));
				}
			}
		}

		public static IDivisibleMesh<TVertex> Empty<TVertex>()
			where TVertex : struct => EmptyMesh<TVertex>.Instance;

		public static IDivisibleMesh<TVertex> Combine<TVertex>(params IDivisibleMesh<TVertex>[] meshes)
			where TVertex : struct => Combine((IEnumerable<IDivisibleMesh<TVertex>>)meshes);

		public static IDivisibleMesh<TVertex> Combine<TVertex>(IEnumerable<IDivisibleMesh<TVertex>> meshes)
			where TVertex : struct
		{
			Contracts.Requires.That(meshes.AllAndSelfNotNull());

			IDivisibleMesh<TVertex> singleMesh;
			if (meshes.TryGetSingle(out singleMesh))
			{
				return singleMesh;
			}

			return new DivisibleMesh<TVertex>(
				ReadOnlyCollection.Combine(meshes.Select(mesh => mesh.Groups)),
				ReadOnlyCollection.Combine(meshes.Select(mesh => mesh.Offsets)),
				ReadOnlyCollection.Combine(meshes.Select(mesh => mesh.Vertices)));
		}

		public static IEnumerable<IDivisibleMesh<TVertex>> Split<TVertex>(
			int maxVerticesPerMesh, IDivisibleMesh<TVertex> mesh)
			where TVertex : struct
		{
			Contracts.Requires.That(maxVerticesPerMesh >= MeshConstants.VerticesPerTriangle);
			Contracts.Requires.That(mesh != null);
			Contracts.Requires.That(mesh.Groups.All(group => group.Vertices <= maxVerticesPerMesh));

			if (mesh.Vertices.Count <= maxVerticesPerMesh)
			{
				yield return mesh;
				yield break;
			}

			var combiner = new MeshCombiner<TVertex>(maxVerticesPerMesh);

			foreach (var submesh in Split(mesh, combiner))
			{
				yield return submesh;
			}

			if (combiner.VertexCount > 0)
			{
				yield return combiner.CreateMesh();
			}
		}

		public static IEnumerable<IDivisibleMesh<TVertex>> CombineAndSplit<TVertex>(
			int maxVerticesPerMesh, params IDivisibleMesh<TVertex>[] meshes)
			where TVertex : struct =>
			CombineAndSplit(maxVerticesPerMesh, (IEnumerable<IDivisibleMesh<TVertex>>)meshes);

		public static IEnumerable<IDivisibleMesh<TVertex>> CombineAndSplit<TVertex>(
			int maxVerticesPerMesh, IEnumerable<IDivisibleMesh<TVertex>> meshes)
			where TVertex : struct
		{
			Contracts.Requires.That(maxVerticesPerMesh >= MeshConstants.VerticesPerTriangle);
			Contracts.Requires.That(meshes.AllAndSelfNotNull());
			Contracts.Requires.That(
				meshes.All(mesh => mesh.Groups.All(group => group.Vertices <= maxVerticesPerMesh)));

			var combiner = new MeshCombiner<TVertex>(maxVerticesPerMesh);

			foreach (var mesh in meshes.Where(mesh => !mesh.IsEmpty()))
			{
				if (!combiner.TryAdd(mesh))
				{
					foreach (var submesh in Split(mesh, combiner))
					{
						yield return submesh;
					}
				}
			}

			if (combiner.VertexCount > 0)
			{
				yield return combiner.CreateMesh();
			}
		}

		private static IEnumerable<IDivisibleMesh<TVertex>> Split<TVertex>(
			IDivisibleMesh<TVertex> mesh, MeshCombiner<TVertex> combiner)
			where TVertex : struct
		{
			Contracts.Requires.That(mesh != null);
			Contracts.Requires.That(combiner != null);

			var splitter = new MeshSplitter<TVertex>(mesh, combiner.MaxVerticesPerMesh - combiner.VertexCount);

			foreach (var group in mesh.Groups)
			{
				// empty groups aren't allowed in the mesh so no need to check for them here
				// so instead assert that there aren't empty groups
				Contracts.Assert.That(group.Triangles >= 1);
				Contracts.Assert.That(group.Vertices >= MeshConstants.VerticesPerTriangle);

				if (!splitter.TryAdd(group))
				{
					if (splitter.HasMesh)
					{
						// the splitter is full so add it to the combiner to finish off the current mesh
						var success = combiner.TryAdd(splitter.CreateMesh());
						Contracts.Assert.That(success);

						yield return combiner.CreateMesh();

						splitter.Reset(group, combiner.MaxVerticesPerMesh);
					}
				}
			}

			if (splitter.HasMesh)
			{
				// if there is any remaining mesh in the splitter add that to the combiner
				var success = combiner.TryAdd(splitter.CreateMesh());
				Contracts.Assert.That(success);
			}
		}

		private static class EmptyMesh<TVertex>
			where TVertex : struct
		{
			public static DivisibleMesh<TVertex> Instance { get; } = new DivisibleMesh<TVertex>(
				ReadOnlyList.Empty<TriangleGroup>(), ReadOnlyList.Empty<byte>(), ReadOnlyList.Empty<TVertex>());
		}

		private class MeshCombiner<TVertex>
			where TVertex : struct
		{
			private readonly List<IReadOnlyCollection<TriangleGroup>> groups = new List<IReadOnlyCollection<TriangleGroup>>();

			private readonly List<IReadOnlyCollection<byte>> offsets = new List<IReadOnlyCollection<byte>>();

			private readonly List<IReadOnlyCollection<TVertex>> vertices = new List<IReadOnlyCollection<TVertex>>();

			public MeshCombiner(int maxVerticesPerMesh)
			{
				Contracts.Requires.That(maxVerticesPerMesh >= MeshConstants.VerticesPerTriangle);

				this.MaxVerticesPerMesh = maxVerticesPerMesh;
			}

			public int MaxVerticesPerMesh { get; }

			public int VertexCount { get; private set; } = 0;

			public bool TryAdd(IDivisibleMesh<TVertex> mesh)
			{
				Contracts.Requires.That(mesh != null);

				int newVertexCount = this.VertexCount + mesh.Vertices.Count;
				if (newVertexCount <= this.MaxVerticesPerMesh)
				{
					this.VertexCount = newVertexCount;
					this.groups.Add(mesh.Groups);
					this.offsets.Add(mesh.Offsets);
					this.vertices.Add(mesh.Vertices);
					return true;
				}

				return false;
			}

			public IDivisibleMesh<TVertex> CreateMesh()
			{
				var result = new DivisibleMesh<TVertex>(
					ReadOnlyCollection.Combine(this.groups.ToArray()),
					ReadOnlyCollection.Combine(this.offsets.ToArray()),
					ReadOnlyCollection.Combine(this.vertices.ToArray()));

				this.groups.Clear();
				this.offsets.Clear();
				this.vertices.Clear();
				this.VertexCount = 0;

				return result;
			}
		}

		private class MeshSplitter<TVertex>
			where TVertex : struct
		{
			private readonly IDivisibleMesh<TVertex> mesh;

			private int remainingVertices;

			private int groupStart = 0;

			private int offsetStart = 0;

			private int vertexStart = 0;

			private int groupCount = 0;

			private int offsetCount = 0;

			private int vertexCount = 0;

			public MeshSplitter(IDivisibleMesh<TVertex> mesh, int remainingVertices)
			{
				Contracts.Requires.That(mesh != null);
				Contracts.Requires.That(remainingVertices >= 0);

				this.mesh = mesh;
				this.remainingVertices = remainingVertices;
			}

			public bool HasMesh => this.groupCount > 0;

			public bool TryAdd(TriangleGroup group)
			{
				int newVertexCount = this.vertexCount + group.Vertices;
				if (newVertexCount <= this.remainingVertices)
				{
					this.groupCount++;
					this.offsetCount += group.Offsets;
					this.vertexCount = newVertexCount;
					return true;
				}

				return false;
			}

			public IDivisibleMesh<TVertex> CreateMesh()
			{
				// TODO use of Skip and Take could be optimized by replacing with a subrange function
				// if IDivisibleMesh used IReadOnlyList or some custom collection with subrange support
				var groups = new ReadOnlyCollection<TriangleGroup>(
					this.mesh.Groups.Skip(this.groupStart).Take(this.groupCount), this.groupCount);
				var offsets = new ReadOnlyCollection<byte>(
					this.mesh.Offsets.Skip(this.offsetStart).Take(this.offsetCount), this.offsetCount);
				var vertices = new ReadOnlyCollection<TVertex>(
					this.mesh.Vertices.Skip(this.vertexStart).Take(this.vertexCount), this.vertexCount);

				return new DivisibleMesh<TVertex>(groups, offsets, vertices);
			}

			public void Reset(TriangleGroup group, int maxVerticesPerMesh)
			{
				Contracts.Requires.That(maxVerticesPerMesh >= MeshConstants.VerticesPerTriangle);

				this.groupStart += this.groupCount;
				this.offsetStart += this.offsetCount;
				this.vertexStart += this.vertexCount;

				// last group was already iterated but couldn't fit so start the next mesh with it
				this.groupCount = 1;
				this.offsetCount = group.Offsets;
				this.vertexCount = group.Vertices;

				// combiner is cleared by creating mesh so remainingVertices is just MaxVerticesPerMesh
				this.remainingVertices = maxVerticesPerMesh;
			}
		}
	}
}
