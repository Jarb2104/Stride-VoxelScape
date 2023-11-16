using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Pact;
using Voxelscape.Utility.Concurrency.Pact.Tasks;

/// <summary>
/// Provides extension methods for the <see cref="Task"/> and <see cref="Task{TResult}"/> classes.
/// </summary>
/// <threadsafety static="true" instance="true" />
public static class TaskExtensions
{
	/// <summary>
	/// Gets whether this <see cref="Task"/> instance ran to completion successfully.
	/// </summary>
	/// <param name="task">The task instance.</param>
	/// <returns>
	/// True if the task's status is <see cref="TaskStatus.RanToCompletion"/>; otherwise false.
	/// </returns>
	public static bool IsCompletedSuccessfully(this Task task)
	{
		Contracts.Requires.That(task != null);

		return task.Status == TaskStatus.RanToCompletion;
	}

	#region ContinueByPropagatingResult

	public static Task ContinueByPropagatingResult(this Task task, Voxelscape.Utility.Concurrency.Pact.Tasks.TaskCompletionSource completionSource)
	{
		Contracts.Requires.That(task != null);
		Contracts.Requires.That(completionSource != null);

		// ContinueWith is normally dangerous;
		// http://blog.stephencleary.com/2013/10/continuewith-is-dangerous-too.html
		// No TaskScheduler is specified here because TaskContinuationOptions.ExecuteSynchronously is being used.
		return task.ContinueWith(
			completedTask =>
			{
				switch (completedTask.Status)
				{
					case TaskStatus.Canceled:
						completionSource.SetCanceled(); break;
					case TaskStatus.Faulted:
						completionSource.SetException(completedTask.Exception.InnerExceptions); break;
					case TaskStatus.RanToCompletion:
						completionSource.SetResult(); break;
				}
			},
			TaskContinuationOptions.ExecuteSynchronously);
	}

	public static Task ContinueByPropagatingResult<TResult>(
		this Task<TResult> task, TaskCompletionSource<TResult> completionSource)
	{
		Contracts.Requires.That(task != null);
		Contracts.Requires.That(completionSource != null);

		// ContinueWith is normally dangerous;
		// http://blog.stephencleary.com/2013/10/continuewith-is-dangerous-too.html
		// No TaskScheduler is specified here because TaskContinuationOptions.ExecuteSynchronously is being used.
		return task.ContinueWith(
			completedTask =>
			{
				switch (completedTask.Status)
				{
					case TaskStatus.Canceled:
						completionSource.SetCanceled(); break;
					case TaskStatus.Faulted:
						completionSource.SetException(completedTask.Exception.InnerExceptions); break;
					case TaskStatus.RanToCompletion:
						completionSource.SetResult(completedTask.Result); break;
				}
			},
			TaskContinuationOptions.ExecuteSynchronously);
	}

	#endregion

	#region TryStart

	/// <summary>
	/// Tries to start the task.
	/// </summary>
	/// <param name="task">The task to start.</param>
	/// <param name="scheduler">The scheduler to run the task through.</param>
	/// <returns>True if the task was successfully started; otherwise false.</returns>
	public static bool TryStart(this Task task, TaskScheduler scheduler = null)
	{
		Exception unused;
		return task.TryStart(out unused, scheduler);
	}

	/// <summary>
	/// Tries to start the task.
	/// </summary>
	/// <param name="task">The task to start.</param>
	/// <param name="exception">The exception thrown by the task when trying to start it, if starting the exception fails.</param>
	/// <param name="scheduler">The scheduler to run the task through.</param>
	/// <returns>True if the task was successfully started; otherwise false.</returns>
	public static bool TryStart(this Task task, out Exception exception, TaskScheduler scheduler = null)
	{
		Contracts.Requires.That(task != null);

		scheduler = scheduler ?? TaskScheduler.Current;
		try
		{
			task.Start(scheduler);
			exception = null;
			return true;
		}
		catch (ObjectDisposedException caughtException)
		{
			exception = caughtException;
			return false;
		}
		catch (InvalidOperationException caughtException)
		{
			exception = caughtException;
			return false;
		}
	}

	#endregion

	#region TimeoutAfterAsync

	/// <summary>
	/// Determines whether a <see cref="Task"/> finishes within a specified time interval. Awaiting on
	/// this will return when either the task completes or the timeout expires, whichever happens first.
	/// </summary>
	/// <param name="task">The task.</param>
	/// <param name="millisecondsTimeout">
	/// The timeout in milliseconds. A timeout of 0 will timeout immediately if the task isn't already complete.
	/// A timeout of <see cref="Timeout.Infinite"/> will never timeout.
	/// </param>
	/// <returns>
	/// True if the task completes within the specified time interval; otherwise false.
	/// </returns>
	public static Task<bool> TimeoutAfterAsync(this Task task, int millisecondsTimeout = Timeout.Infinite)
	{
		Contracts.Requires.That(task != null);
		Contracts.Requires.That(Time.IsDuration(millisecondsTimeout));

		return task.TimeoutAfterAsync(TimeSpan.FromMilliseconds(millisecondsTimeout), CancellationToken.None);
	}

	/// <summary>
	/// Determines whether a <see cref="Task"/> finishes within a specified time interval. Awaiting on
	/// this will return when either the task completes or the timeout expires, whichever happens first.
	/// </summary>
	/// <param name="task">The task.</param>
	/// <param name="timeout">
	/// The timeout. A timeout of <see cref="TimeSpan.Zero"/> will timeout immediately if the task isn't already complete.
	/// A timeout of <see cref="Timeout.InfiniteTimeSpan"/> will never timeout.
	/// </param>
	/// <returns>
	/// True if the task completes within the specified time interval; otherwise false.
	/// </returns>
	public static Task<bool> TimeoutAfterAsync(this Task task, TimeSpan timeout)
	{
		Contracts.Requires.That(task != null);
		Contracts.Requires.That(Time.IsDuration(timeout));

		return task.TimeoutAfterAsync(timeout, CancellationToken.None);
	}

	/// <summary>
	/// Determines whether a <see cref="Task"/> finishes before an infinite timeout is canceled. Awaiting on
	/// this will return when either the task completes or the timeout is canceled, whichever happens first.
	/// </summary>
	/// <param name="task">The task.</param>
	/// <param name="cancellation">The cancellation token used to end the infinite timeout.</param>
	/// <returns>
	/// True if the task completes before the timeout is canceled; otherwise false.
	/// </returns>
	/// <remarks>
	/// The timeout will never expire on its own, it must be canceled. Canceling the timeout does not cancel the task.
	/// If the task has not yet finished by the time the timeout is canceled then this method will return false.
	/// </remarks>
	public static Task<bool> TimeoutAfterAsync(this Task task, CancellationToken cancellation)
	{
		Contracts.Requires.That(task != null);

		return task.TimeoutAfterAsync(Timeout.InfiniteTimeSpan, cancellation);
	}

	/// <summary>
	/// Determines whether a <see cref="Task"/> finishes within a specified time interval. Awaiting on
	/// this will return when either the task completes or the timeout expires, whichever happens first.
	/// </summary>
	/// <param name="task">The task.</param>
	/// <param name="millisecondsTimeout">
	/// The timeout in milliseconds. A timeout of 0 will timeout immediately if the task isn't already complete.
	/// A timeout of <see cref="Timeout.Infinite"/> will never timeout but can be canceled.
	/// </param>
	/// <param name="cancellation">The cancellation token used to end the timeout period early.</param>
	/// <returns>
	/// True if the task completes within the specified time interval; otherwise false.
	/// </returns>
	/// <remarks>
	/// Canceling the timeout only causes it timeout earlier. If the task has not yet finished by the time
	/// the timeout is canceled then this method will still return false. Canceling the timeout does not
	/// cancel the task.
	/// </remarks>
	public static Task<bool> TimeoutAfterAsync(this Task task, int millisecondsTimeout, CancellationToken cancellation)
	{
		Contracts.Requires.That(task != null);
		Contracts.Requires.That(Time.IsDuration(millisecondsTimeout));

		return task.TimeoutAfterAsync(TimeSpan.FromMilliseconds(millisecondsTimeout), cancellation);
	}

	/// <summary>
	/// Determines whether a <see cref="Task"/> finishes within a specified time interval. Awaiting on
	/// this will return when either the task completes or the timeout expires, whichever happens first.
	/// </summary>
	/// <param name="task">The task.</param>
	/// <param name="timeout">
	/// The timeout. A timeout of <see cref="TimeSpan.Zero"/> will timeout immediately if the task isn't already complete.
	/// A timeout of <see cref="Timeout.InfiniteTimeSpan"/> will never timeout but can be canceled.
	/// </param>
	/// <param name="cancellation">The cancellation token used to end the timeout period early.</param>
	/// <returns>
	/// True if the task completes within the specified time interval; otherwise false.
	/// </returns>
	/// <remarks>
	/// Canceling the timeout only causes it timeout earlier. If the task has not yet finished by the time
	/// the timeout is canceled then this method will still return false. Canceling the timeout does not
	/// cancel the task.
	/// </remarks>
	public static async Task<bool> TimeoutAfterAsync(this Task task, TimeSpan timeout, CancellationToken cancellation)
	{
		Contracts.Requires.That(task != null);
		Contracts.Requires.That(Time.IsDuration(timeout));

		// check if there is no way the task can timeout
		if (task.IsCompleted || (timeout == Timeout.InfiniteTimeSpan && !cancellation.CanBeCanceled))
		{
			// await user task to ensure it is completed before returning and/or rethrows any exceptions
			await task.DontMarshallContext();
			return true;
		}

		// check if the task has already timed out
		if (timeout == TimeSpan.Zero || cancellation.IsCancellationRequested)
		{
			return false;
		}

		// used to end the delay early if/when the task completes, or if/when the user chooses to cancel the timeout
		CancellationTokenSource cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(cancellation);

		if (task == await Task.WhenAny(task, Task.Delay(timeout, cancellationSource.Token)).DontMarshallContext())
		{
			// user task completed first, ensure the delay is canceled to free up system resources
			cancellationSource.Cancel();

			// await user task to ensure it is completed before returning and/or rethrows any exceptions
			await task.DontMarshallContext();
			return true;
		}
		else
		{
			return false;
		}
	}

	#endregion

	#region DontMarshallContext

	/// <summary>
	/// Configures the await to not attempt to marshal the continuation back to the original context captured.
	/// </summary>
	/// <param name="task">The task to configure the await for.</param>
	/// <returns>An object used to await this task.</returns>
	/// <remarks>
	/// This method makes it so that the code after the await can be ran on any available thread pool thread
	/// instead of being marshalled back to the same thread that the code was running on before the await.
	/// Avoiding this marshalling helps to slightly improve performance and helps avoid potential deadlocks. See
	/// <see href="http://blog.ciber.no/2014/05/19/using-task-configureawaitfalse-to-prevent-deadlocks-in-async-code/">
	/// this link</see> for more information.
	/// </remarks>
	public static ConfiguredTaskAwaitable DontMarshallContext(this Task task)
	{
		Contracts.Requires.That(task != null);

		return task.ConfigureAwait(false);
	}

	/// <summary>
	/// Configures the await to not attempt to marshal the continuation back to the original context captured.
	/// </summary>
	/// <typeparam name="T">The type of the value returned by the task.</typeparam>
	/// <param name="task">The task to configure the await for.</param>
	/// <returns>
	/// An object used to await this task.
	/// </returns>
	/// <remarks>
	/// This method makes it so that the code after the await can be ran on any available thread pool thread
	/// instead of being marshalled back to the same thread that the code was running on before the await.
	/// Avoiding this marshalling helps to slightly improve performance and helps avoid potential deadlocks. See
	/// <see href="http://blog.ciber.no/2014/05/19/using-task-configureawaitfalse-to-prevent-deadlocks-in-async-code/">
	/// this link</see> for more information.
	/// </remarks>
	public static ConfiguredTaskAwaitable<T> DontMarshallContext<T>(this Task<T> task)
	{
		Contracts.Requires.That(task != null);

		return task.ConfigureAwait(false);
	}

	#endregion
}
