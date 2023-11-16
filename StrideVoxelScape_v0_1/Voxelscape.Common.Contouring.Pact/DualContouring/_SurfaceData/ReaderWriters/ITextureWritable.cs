using Stride.Core.Mathematics;

namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	/// <summary>
	///
	/// </summary>
	public interface ITextureWritable : ITextureReadable
	{
		new Vector2 TopLeftTexture { get; set; }

		new Vector2 TopRightTexture { get; set; }

		new Vector2 BottomLeftTexture { get; set; }

		new Vector2 BottomRightTexture { get; set; }
	}
}
