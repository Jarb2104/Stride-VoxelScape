using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Stages.Voxels.Pact.Terrain
{
	/// <summary>
	///
	/// </summary>
	public interface ITerrainSurfaceData :
		IResettable, ISeedWritable, IDiagonalWritable, IVertexWritable, ISurfaceAreaWritable
	{
	}
}
