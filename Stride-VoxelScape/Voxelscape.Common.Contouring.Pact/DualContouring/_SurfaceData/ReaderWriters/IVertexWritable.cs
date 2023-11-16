using Stride.Core.Mathematics;

namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	/// <summary>
	///
	/// </summary>
	public interface IVertexWritable : IVertexReadable
	{
		new Vector3 TopLeftVertex { get; set; }

		new Vector3 TopRightVertex { get; set; }

		new Vector3 BottomLeftVertex { get; set; }

		new Vector3 BottomRightVertex { get; set; }
	}
}
