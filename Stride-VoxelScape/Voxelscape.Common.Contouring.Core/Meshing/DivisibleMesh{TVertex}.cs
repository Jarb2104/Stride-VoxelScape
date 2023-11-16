using System.Collections.Generic;
using Voxelscape.Common.Contouring.Pact.Meshing;

namespace Voxelscape.Common.Contouring.Core.Meshing
{
	public class DivisibleMesh<TVertex> : IDivisibleMesh<TVertex>
		where TVertex : struct
	{
		public DivisibleMesh(
			IReadOnlyCollection<TriangleGroup> groups,
			IReadOnlyCollection<byte> offsets,
			IReadOnlyCollection<TVertex> vertices)
		{
			this.Groups = groups;
			this.Offsets = offsets;
			this.Vertices = vertices;

			DivisibleMesh.VerifyContracts(this);
		}

		/// <inheritdoc />
		public IReadOnlyCollection<TriangleGroup> Groups { get; }

		/// <inheritdoc />
		public IReadOnlyCollection<byte> Offsets { get; }

		/// <inheritdoc />
		public IReadOnlyCollection<TVertex> Vertices { get; }
	}
}
