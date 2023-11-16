using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for the <see cref="IEnumerableTaskExtensions"/> class.
/// </summary>
/// <threadsafety static="true" instance="true" />
public static class IEnumerableTaskExtensions
{
	/// <summary>
	/// Returns a sequence of <see cref="Task{TResult}"/> in the order they complete.
	/// </summary>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <param name="source">The original sequence of uncompleted tasks.</param>
	/// <returns>The tasks in the order they complete.</returns>
	public static IEnumerable<Task<TResult>> InCompletionOrder<TResult>(this IEnumerable<Task<TResult>> source)
	{
		Contracts.Requires.That(source.AllAndSelfNotNull());

		// starts at -1 because the first call to Interlocked.Increment will set it to 0 before returning
		int currentIndex = -1;
		var results = ArrayUtilities.New(source.Count(), () => new TaskCompletionSource<TResult>());

		foreach (var task in source)
		{
			task.ContinueByPropagatingResult(results[Interlocked.Increment(ref currentIndex)]);
		}

		return results.Select(taskSource => taskSource.Task);
	}
}
