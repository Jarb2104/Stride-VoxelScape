using Stride.Core.Mathematics;

namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	/// <summary>
	///
	/// </summary>
	public interface IVertexReadable
	{
		Vector3 TopLeftVertex { get; }

		Vector3 TopRightVertex { get; }

		Vector3 BottomLeftVertex { get; }

		Vector3 BottomRightVertex { get; }
	}
}
