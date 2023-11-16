using Voxelscape.Common.Procedural.Core.Noise;
using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for the <see cref="INoiseDistorter4D"/> interface.
/// </summary>
public static class INoiseDistorterFluentExtensions
{
	public static INoiseDistorter4D Amplitude(
		this INoiseDistorter4D distorter,
		double amplitude)
	{
		Contracts.Requires.That(distorter != null);

		return new AmplitudeDistorter(distorter, amplitude);
	}

	public static INoiseDistorter4D Clamp(
		this INoiseDistorter4D distorter,
		double min,
		double max)
	{
		Contracts.Requires.That(distorter != null);

		return new ClampDistorter(distorter, min, max);
	}

	public static INoiseDistorter4D ClampLower(
		this INoiseDistorter4D distorter,
		double min,
		double max)
	{
		Contracts.Requires.That(distorter != null);

		return new ClampDistorter(distorter, min, double.PositiveInfinity);
	}

	public static INoiseDistorter4D ClampUpper(
		this INoiseDistorter4D distorter,
		double min,
		double max)
	{
		Contracts.Requires.That(distorter != null);

		return new ClampDistorter(distorter, double.NegativeInfinity, max);
	}

	public static INoiseDistorter4D ConvertRange(
		this INoiseDistorter4D distorter,
		double sourceNoiseStart,
		double sourceNoiseEnd,
		double resultNoiseStart,
		double resultNoiseEnd)
	{
		Contracts.Requires.That(distorter != null);

		return new ConvertRangeDistorter(
			distorter, sourceNoiseStart, sourceNoiseEnd, resultNoiseStart, resultNoiseEnd);
	}

	public static INoiseDistorter4D ConvertRange(
		this INoiseDistorter4D distorter,
		double resultNoiseStart,
		double resultNoiseEnd)
	{
		Contracts.Requires.That(distorter != null);

		return new ConvertRangeDistorter(distorter, resultNoiseStart, resultNoiseEnd);
	}

	public static INoiseDistorter4D ConvertRangeClamped(
		this INoiseDistorter4D distorter,
		double sourceNoiseStart,
		double sourceNoiseEnd,
		double resultNoiseStart,
		double resultNoiseEnd)
	{
		Contracts.Requires.That(distorter != null);

		return new ConvertRangeDistorter(
			distorter, sourceNoiseStart, sourceNoiseEnd, resultNoiseStart, resultNoiseEnd)
			.Clamp(resultNoiseStart, resultNoiseEnd);
	}

	public static INoiseDistorter4D ConvertRangeClamped(
		this INoiseDistorter4D distorter,
		double resultNoiseStart,
		double resultNoiseEnd)
	{
		Contracts.Requires.That(distorter != null);

		return new ConvertRangeDistorter(
			distorter, resultNoiseStart, resultNoiseEnd).Clamp(resultNoiseStart, resultNoiseEnd);
	}

	public static INoiseDistorter4D Frequency(
		this INoiseDistorter4D distorter,
		double xFrequency = 1,
		double yFrequency = 1,
		double zFrequency = 1,
		double wFrequency = 1)
	{
		Contracts.Requires.That(distorter != null);

		return new FrequencyDistorter(distorter, xFrequency, yFrequency, zFrequency, wFrequency);
	}

	public static INoiseDistorter4D Octaves(
		this INoiseDistorter4D distorter,
		int numberOfOctaves)
	{
		Contracts.Requires.That(distorter != null);

		return new OctavesDistorter(distorter, numberOfOctaves);
	}

	public static INoiseDistorter4D Shift(
		this INoiseDistorter4D distorter,
		double xShift = 1,
		double yShift = 1,
		double zShift = 1,
		double wShift = 1)
	{
		Contracts.Requires.That(distorter != null);

		return new ShiftDistorter(distorter, xShift, yShift, zShift, wShift);
	}
}
