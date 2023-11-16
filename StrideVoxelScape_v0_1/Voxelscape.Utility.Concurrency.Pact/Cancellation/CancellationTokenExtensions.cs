using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Concurrency.Pact.Tasks;
using TaskCompletionSource = Voxelscape.Utility.Concurrency.Pact.Tasks.TaskCompletionSource;

/// <summary>
/// Provides extension methods for the <see cref="CancellationToken"/> struct.
/// </summary>
public static class CancellationTokenExtensions
{
	/// <summary>
	/// Returns a task that will enter the canceled state when the <see cref="CancellationToken"/> is signaled.
	/// If the <see cref="CancellationToken"/> is never signaled, the returned task will never complete.
	/// </summary>
	/// <param name="cancellation">The cancellation token.</param>
	/// <returns>A task representing the cancellation of the specified token.</returns>
	public static Task ToTask(this CancellationToken cancellation)
	{
		TaskCompletionSource taskSource = new Voxelscape.Utility.Concurrency.Pact.Tasks.TaskCompletionSource();
		cancellation.Register(() => taskSource.SetCanceled());
		return taskSource.Task;
	}
}
