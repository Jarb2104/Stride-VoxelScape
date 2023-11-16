using Stride.Core.Mathematics;

namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	/// <summary>
	///
	/// </summary>
	public interface IColorReadable
	{
		Color TopLeftColor { get; }

		Color TopRightColor { get; }

		Color BottomLeftColor { get; }

		Color BottomRightColor { get; }
	}
}
