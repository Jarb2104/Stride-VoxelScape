using Voxelscape.Common.Contouring.Core.Vertices;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Stages.Voxels.Pact.Meshing
{
	/// <summary>
	///
	/// </summary>
	public interface IMeshChunk : IKeyed<ChunkKey>
	{
		IDivisibleMesh<NormalColorTextureVertex> Mesh { get; }
	}
}
