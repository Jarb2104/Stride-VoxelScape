using Stride.Core.Mathematics;

namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	/// <summary>
	///
	/// </summary>
	public interface ITextureReadable
	{
		Vector2 TopLeftTexture { get; }

		Vector2 TopRightTexture { get; }

		Vector2 BottomLeftTexture { get; }

		Vector2 BottomRightTexture { get; }
	}
}
