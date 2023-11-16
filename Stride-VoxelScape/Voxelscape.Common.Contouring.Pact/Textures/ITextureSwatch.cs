
using Stride.Core.Mathematics;

namespace Voxelscape.Common.Contouring.Pact.Textures
{
	/// <summary>
	///
	/// </summary>
	public interface ITextureSwatch
	{
		Vector2 TopLeftCoordinate { get; }

		Vector2 TopRightCoordinate { get; }

		Vector2 BottomLeftCoordinate { get; }

		Vector2 BottomRightCoordinate { get; }
	}
}
