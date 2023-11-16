using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.AsyncEx;

/// <summary>
/// Provides extension methods for <see cref="AsyncLongCountdownEvent"/>.
/// </summary>
public static class AsyncLongCountdownEventExtensions
{
	public static bool TrySignalAllRemaining(this AsyncLongCountdownEvent countdown)
	{
		Contracts.Requires.That(countdown != null);

		return countdown.TrySignal(countdown.CurrentCount);
	}
}
