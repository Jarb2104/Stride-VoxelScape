using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for the <see cref="ITargetBlock{TInput}"/> interface.
/// </summary>
public static class ITargetBlockExtensions
{
	public static bool PostAll<TInput>(this ITargetBlock<TInput> target, params TInput[] items) =>
		target.PostAll((IEnumerable<TInput>)items);

	public static bool PostAll<TInput>(this ITargetBlock<TInput> target, IEnumerable<TInput> items)
	{
		Contracts.Requires.That(target != null);
		Contracts.Requires.That(items != null);

		bool result = true;
		foreach (var item in items)
		{
			if (!target.Post(item))
			{
				result = false;
			}
		}

		return result;
	}

	public static Task<bool> SendAllAsync<T>(this ITargetBlock<T> target, params T[] items) =>
		target.SendAllAsync(CancellationToken.None, items);

	public static Task<bool> SendAllAsync<T>(
		this ITargetBlock<T> target, CancellationToken cancellation, params T[] items) =>
		target.SendAllAsync(items, cancellation);

	public static async Task<bool> SendAllAsync<T>(
		this ITargetBlock<T> target, IEnumerable<T> items, CancellationToken cancellation = default(CancellationToken))
	{
		Contracts.Requires.That(target != null);
		Contracts.Requires.That(items != null);

		cancellation.ThrowIfCancellationRequested();
		bool result = true;
		foreach (var item in items)
		{
			if (!await target.SendAsync(item, cancellation).DontMarshallContext())
			{
				result = false;
			}
		}

		return result;
	}
}
