using Voxelscape.Common.Procedural.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for the <see cref="InterpolationExtensions"/> class.
/// </summary>
public static class InterpolationExtensions
{
	/// <summary>
	/// Converts a value from an original range of values to a new range of values.
	/// </summary>
	/// <param name="value">The value to convert.</param>
	/// <param name="originalStart">The start value of the original range.</param>
	/// <param name="originalEnd">The end value of the original range.</param>
	/// <param name="newStart">The start value of the new range to convert to.</param>
	/// <param name="newEnd">The end value of the new range to convert to.</param>
	/// <returns>The value converted to the new range of values.</returns>
	public static double ConvertRange(
		this double value, double originalStart, double originalEnd, double newStart, double newEnd)
	{
		Contracts.Requires.That(originalStart != originalEnd);
		Contracts.Requires.That(newStart != newEnd);

		return Interpolation.ConvertRange(value, originalStart, originalEnd, newStart, newEnd);
	}

	/// <summary>
	/// Converts a value from an original range of values to a new range of values.
	/// </summary>
	/// <param name="value">The value to convert.</param>
	/// <param name="originalStart">The start value of the original range.</param>
	/// <param name="originalEnd">The end value of the original range.</param>
	/// <param name="newStart">The start value of the new range to convert to.</param>
	/// <param name="newEnd">The end value of the new range to convert to.</param>
	/// <returns>The value converted to the new range of values.</returns>
	public static float ConvertRange(
		this float value, float originalStart, float originalEnd, float newStart, float newEnd)
	{
		Contracts.Requires.That(originalStart != originalEnd);
		Contracts.Requires.That(newStart != newEnd);

		return (float)Interpolation.ConvertRange(value, originalStart, originalEnd, newStart, newEnd);
	}
}
