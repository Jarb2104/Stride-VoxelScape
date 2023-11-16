using Stride.Core.Mathematics;

/// <summary>
/// Provides extension methods for <see cref="Color"/>.
/// </summary>
public static class ColorExtensions
{
	public static bool IsOpaque(this Color color) => color.A == byte.MaxValue;

	public static bool IsTransparent(this Color color) => color.A != byte.MaxValue;

	public static bool IsPartiallyTransparent(this Color color) =>
		color.A != byte.MinValue && color.A != byte.MaxValue;

	public static bool IsFullyTransparent(this Color color) => color.A == byte.MinValue;
}
