using System.Threading;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

/// <summary>
/// Provides extension methods for the <see cref="IAsyncCancelable"/> interface.
/// </summary>
public static class IAsyncCancelableExtensions
{
	public static CancellationTokenRegistration LinkCancellation(
		this IAsyncCancelable cancelable, CancellationToken cancellation)
	{
		Contracts.Requires.That(cancelable != null);

		return cancellation.Register(() => cancelable.Cancel());
	}
}
