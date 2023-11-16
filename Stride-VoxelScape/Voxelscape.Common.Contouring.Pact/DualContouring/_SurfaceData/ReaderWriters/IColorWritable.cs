using Stride.Core.Mathematics;

namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	/// <summary>
	///
	/// </summary>
	public interface IColorWritable : IColorReadable
	{
		new Color TopLeftColor { get; set; }

		new Color TopRightColor { get; set; }

		new Color BottomLeftColor { get; set; }

		new Color BottomRightColor { get; set; }
	}
}
