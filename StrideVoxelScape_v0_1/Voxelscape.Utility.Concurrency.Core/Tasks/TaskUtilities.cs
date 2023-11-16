using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Concurrency.Core.Tasks
{
	/// <summary>
	/// Provides utility methods for executing <see cref="Task"/> instances.
	/// </summary>
	/// <threadsafety static="true" instance="true" />
	public static class TaskUtilities
	{
		#region Completed/Canceled Singletons and From Canceled/Exception

		/// <summary>
		/// Gets a task that has already completed successfully.
		/// </summary>
		/// <value>
		/// The successfully completed task.
		/// </value>
		public static Task CompletedTask { get; } = Task.FromResult(default(VoidStruct));

		/// <summary>
		/// Gets a task that has already been canceled.
		/// </summary>
		/// <value>
		/// The canceled task.
		/// </value>
		public static Task CanceledTask { get; } = FromCanceled<VoidStruct>();

		/// <summary>
		/// Gets a task that has already been canceled.
		/// </summary>
		/// <typeparam name="TResult">The type of the result of the task.</typeparam>
		/// <returns>The canceled task.</returns>
		public static Task<TResult> FromCanceled<TResult>()
		{
			var source = new TaskCompletionSource<TResult>();
			source.SetCanceled();
			return source.Task;
		}

		/// <summary>
		/// Gets a task that has already been faulted.
		/// </summary>
		/// <param name="exception">The exception to fault the task with.</param>
		/// <returns>The faulted task.</returns>
		public static Task FromException(Exception exception) => FromException<VoidStruct>(exception);

		/// <summary>
		/// Gets a task that has already been faulted.
		/// </summary>
		/// <typeparam name="TResult">The type of the result of the task.</typeparam>
		/// <param name="exception">The exception to fault the task with.</param>
		/// <returns>The faulted task.</returns>
		public static Task<TResult> FromException<TResult>(Exception exception)
		{
			Contracts.Requires.That(exception != null);

			var source = new TaskCompletionSource<TResult>();
			source.SetException(exception);
			return source.Task;
		}

		#endregion

		#region WhenAll Cancellable

		/// <summary>
		/// Creates a task that will complete when all of the <see cref="Task{TResult}"/> objects in an enumerable collection
		/// have completed and supports early cancellation through use of a <see cref="CancellationToken"/>.
		/// </summary>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="cancellation">The cancellation token to cancel the operation early.</param>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/hh194766(v=vs.110).aspx"/>
		public static async Task<TResult[]> WhenAll<TResult>(CancellationToken cancellation, IEnumerable<Task<TResult>> tasks)
		{
			Contracts.Requires.That(tasks.AllAndSelfNotNull());

			Task<TResult[]> resultsTask = Task.WhenAll(tasks);

			// the first await returns whichever of the 2 tasks finished first
			// the second await unwraps the result or exception of that task (will rethrown if cancelled)
			await (await Task.WhenAny(resultsTask, cancellation.ToTask()).DontMarshallContext()).DontMarshallContext();

			// if canceled this line won't be reached
			return await resultsTask.DontMarshallContext();
		}

		/// <summary>
		/// Creates a task that will complete when all of the <see cref="Task{TResult}"/> objects in an array have completed
		/// and supports early cancellation through use of a <see cref="CancellationToken"/>.
		/// </summary>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="cancellation">The cancellation token to cancel the operation early.</param>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/hh194874(v=vs.110).aspx"/>
		public static async Task<TResult[]> WhenAll<TResult>(CancellationToken cancellation, params Task<TResult>[] tasks)
		{
			return await WhenAll(cancellation, (IEnumerable<Task<TResult>>)tasks).DontMarshallContext();
		}

		/// <summary>
		/// Creates a task that will complete when all of the <see cref="Task"/> objects in an enumerable collection have completed
		/// and supports early cancellation through use of a <see cref="CancellationToken"/>.
		/// </summary>
		/// <param name="cancellation">The cancellation token to cancel the operation early.</param>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/hh160384(v=vs.110).aspx"/>
		public static async Task WhenAll(CancellationToken cancellation, IEnumerable<Task> tasks)
		{
			Contracts.Requires.That(tasks.AllAndSelfNotNull());

			await Task.WhenAny(Task.WhenAll(tasks), cancellation.ToTask()).DontMarshallContext();
		}

		/// <summary>
		/// Creates a task that will complete when all of the <see cref="Task"/> objects in an array have completed
		/// and supports early cancellation through use of a <see cref="CancellationToken"/>.
		/// </summary>
		/// <param name="cancellation">The cancellation token to cancel the operation early.</param>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/hh160374(v=vs.110).aspx"/>
		public static async Task WhenAll(CancellationToken cancellation, params Task[] tasks)
		{
			await WhenAll(cancellation, (IEnumerable<Task>)tasks).DontMarshallContext();
		}

		#endregion
	}
}
