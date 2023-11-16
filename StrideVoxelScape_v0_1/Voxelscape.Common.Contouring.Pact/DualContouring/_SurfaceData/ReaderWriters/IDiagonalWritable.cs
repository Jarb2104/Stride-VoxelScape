using Voxelscape.Common.Contouring.Pact.Meshing;

namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	/// <summary>
	///
	/// </summary>
	public interface IDiagonalWritable : IDiagonalReadable
	{
		new QuadDiagonal Diagonal { get; set; }
	}
}
