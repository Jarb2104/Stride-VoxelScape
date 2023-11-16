using System.Collections.Concurrent;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for the <see cref="ConcurrentQueue{T}"/> class.
/// </summary>
/// <threadsafety static="true" instance="true" />
public static class ConcurrentQueueExtensions
{
	/// <summary>
	/// Dequeues and returns all values from a <see cref="ConcurrentQueue{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of values in the queue.</typeparam>
	/// <param name="queue">The queue to dequeue from.</param>
	/// <returns>The values dequeue from the queue.</returns>
	public static IEnumerable<T> DequeueAll<T>(this ConcurrentQueue<T> queue)
	{
		Contracts.Requires.That(queue != null);

		// ConcurrentQueue<T>'s Count property does not take a snapshot and has O(1) performance
		// http://geekswithblogs.net/BlackRabbitCoder/archive/2011/02/10/c.net-little-wonders-the-concurrent-collections-1-of-3.aspx
		List<T> result = new List<T>(queue.Count);

		T next;
		while (queue.TryDequeue(out next))
		{
			result.Add(next);
		}

		return result;
	}
}
