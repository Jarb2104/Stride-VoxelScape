using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.MonoGame.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class ColorSerializer
	{
		public static IEndianProvider<RGBAColorSerializer> RGBA => RGBAColorSerializer.Get;

		public static IEndianProvider<RGBColorSerializer> RGB => RGBColorSerializer.Get;
	}
}
