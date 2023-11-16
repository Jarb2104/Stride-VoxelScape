using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

/// <summary>
/// Provides extension methods for the <see cref="IAsyncCompletable"/> interface.
/// </summary>
public static class IAsyncCompletableExtensions
{
	/// <summary>
	/// Starts the asynchronous completion and awaits its completion.
	/// </summary>
	/// <param name="completable">The completable instance.</param>
	/// <returns>The completion task to await.</returns>
	public static Task CompleteAndAwaitAsync(this IAsyncCompletable completable)
	{
		Contracts.Requires.That(completable != null);

		completable.Complete();
		return completable.Completion;
	}
}
