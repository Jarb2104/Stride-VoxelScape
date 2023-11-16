using Nito.AsyncEx;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for <see cref="AsyncCountdownEvent"/>.
/// </summary>
public static class AsyncCountdownEventExtensions
{
	public static void TrySignal(this AsyncCountdownEvent signal)
	{
		Contracts.Requires.That(signal != null);

		if (signal.CurrentCount > 0)
		{
			signal.Signal();
		}
	}
}
