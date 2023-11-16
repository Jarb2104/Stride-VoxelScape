using System.Collections.Generic;

namespace Voxelscape.Common.Contouring.Pact.Meshing
{
	// vertices should always be specified in clockwise winding order for this platform independent format
	public interface IDivisibleMesh<TVertex>
		where TVertex : struct
	{
		IReadOnlyCollection<TriangleGroup> Groups { get; }

		IReadOnlyCollection<byte> Offsets { get; }

		IReadOnlyCollection<TVertex> Vertices { get; }
	}
}
