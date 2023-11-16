using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Progress;

/// <summary>
/// Provides extension methods for <see cref="IDiscreteProgress"/>.
/// </summary>
public static class IDiscreteProgressExtensions
{
	public static bool IsCompleted(this IDiscreteProgress progress)
	{
		Contracts.Requires.That(progress != null);

		return progress.CountCompleted == progress.TotalCount;
	}

	public static double GetPercentCompleted(this IDiscreteProgress progress)
	{
		Contracts.Requires.That(progress != null);

		if (progress.TotalCount > 0)
		{
			return ((double)progress.CountCompleted) / progress.TotalCount;
		}
		else
		{
			// no work to be done so progress considered fully completed already
			return 1;
		}
	}
}
