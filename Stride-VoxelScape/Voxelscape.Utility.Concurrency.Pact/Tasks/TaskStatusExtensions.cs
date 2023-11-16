using System.Threading.Tasks;

/// <summary>
/// Provides extension methods for the <see cref="TaskStatus"/> enumerations.
/// </summary>
public static class TaskStatusExtensions
{
	/// <summary>
	/// Determines whether the specified status represents a task that is pending to start running.
	/// </summary>
	/// <param name="status">The status.</param>
	/// <returns>
	/// True if the status is <see cref="TaskStatus.Created"/> or <see cref="TaskStatus.WaitingForActivation"/>;
	/// otherwise false.
	/// </returns>
	public static bool IsPending(this TaskStatus status) =>
		status == TaskStatus.Created ||
		status == TaskStatus.WaitingForActivation;

	/// <summary>
	/// Determines whether the specified status represents a task that has started running.
	/// </summary>
	/// <param name="status">The status.</param>
	/// <returns>
	/// True if the status is <see cref="TaskStatus.WaitingToRun"/>, <see cref="TaskStatus.Running"/>,
	/// or <see cref="TaskStatus.WaitingForChildrenToComplete"/>; otherwise false.
	/// </returns>
	public static bool IsStarted(this TaskStatus status) =>
		status == TaskStatus.WaitingToRun ||
		status == TaskStatus.Running ||
		status == TaskStatus.WaitingForChildrenToComplete;

	/// <summary>
	/// Determines whether the specified status represents a task that has completed running.
	/// </summary>
	/// <param name="status">The status.</param>
	/// <returns>
	/// True if the status is <see cref="TaskStatus.RanToCompletion"/>, <see cref="TaskStatus.Canceled"/>,
	/// or <see cref="TaskStatus.Faulted"/>; otherwise false.
	/// </returns>
	public static bool IsCompleted(this TaskStatus status) =>
		status == TaskStatus.RanToCompletion ||
		status == TaskStatus.Canceled ||
		status == TaskStatus.Faulted;

	/// <summary>
	/// Determines whether the specified status represents a task that has completed running successfully.
	/// </summary>
	/// <param name="status">The status.</param>
	/// <returns>
	/// True if the status is <see cref="TaskStatus.RanToCompletion"/>; otherwise false.
	/// </returns>
	public static bool IsCompletedSuccessfully(this TaskStatus status) => status == TaskStatus.RanToCompletion;
}
