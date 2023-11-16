using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for <see cref="Queue{T}"/>.
/// </summary>
public static class QueueExtensions
{
	public static bool TryDequeue<T>(this Queue<T> queue, out T result)
	{
		Contracts.Requires.That(queue != null);

		if (queue.Count == 0)
		{
			result = default(T);
			return false;
		}
		else
		{
			result = queue.Dequeue();
			return true;
		}
	}

	public static bool TryPeek<T>(this Queue<T> queue, out T result)
	{
		Contracts.Requires.That(queue != null);

		if (queue.Count == 0)
		{
			result = default(T);
			return false;
		}
		else
		{
			result = queue.Peek();
			return true;
		}
	}
}
