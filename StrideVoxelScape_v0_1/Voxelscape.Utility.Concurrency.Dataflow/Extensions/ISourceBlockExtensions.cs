using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Pact;

/// <summary>
/// Provides extension methods for the <see cref="ISourceBlock{TOutput}"/> interface.
/// </summary>
public static class ISourceBlockExtensions
{
	/// <summary>
	/// Tries to asynchronously receives a value from a specified source.
	/// </summary>
	/// <typeparam name="TOutput">The type of data contained in the source to receive.</typeparam>
	/// <param name="source">The source from which to receive the value.</param>
	/// <returns>
	/// A task that represents the asynchronous receive operation. When an item value is successfully received from the source,
	/// the returned task is completed and its <see cref="Task{T}.Result"/> returns a successful <see cref="TryValue{TOutput}"/>
	/// containing the received value. If an item value cannot be retrieved because the source is empty and completed, the returned
	/// task is completed and its <see cref="Task{T}.Result"/> returns <see cref="TryValue.None()"/>.
	/// </returns>
	public static async Task<TryValue<TOutput>> TryReceiveAsync<TOutput>(this ISourceBlock<TOutput> source)
	{
		Contracts.Requires.That(source != null);

		try
		{
			return TryValue.New(await source.ReceiveAsync().DontMarshallContext());
		}
		catch (InvalidOperationException)
		{
			return TryValue.None<TOutput>();
		}
	}

	/// <summary>
	/// Tries to asynchronously receives a value from a specified source.
	/// </summary>
	/// <typeparam name="TOutput">The type of data contained in the source to receive.</typeparam>
	/// <param name="source">The source from which to receive the value.</param>
	/// <param name="cancellation">The token to use to cancel the receive operation.</param>
	/// <returns>
	/// A task that represents the asynchronous receive operation. When an item value is successfully received from the source,
	/// the returned task is completed and its <see cref="Task{T}.Result"/> returns a successful <see cref="TryValue{TOutput}"/>
	/// containing the received value. If an item value cannot be retrieved because the source is empty and completed, the returned
	/// task is completed and its <see cref="Task{T}.Result"/> returns <see cref="TryValue.None()"/>.
	/// </returns>
	public static async Task<TryValue<TOutput>> TryReceiveAsync<TOutput>(
		this ISourceBlock<TOutput> source, CancellationToken cancellation)
	{
		Contracts.Requires.That(source != null);

		try
		{
			return TryValue.New(await source.ReceiveAsync(cancellation).DontMarshallContext());
		}
		catch (InvalidOperationException)
		{
			return TryValue.None<TOutput>();
		}
	}

	/// <summary>
	/// Tries to asynchronously receives a value from a specified source.
	/// </summary>
	/// <typeparam name="TOutput">The type of data contained in the source to receive.</typeparam>
	/// <param name="source">The source from which to receive the value.</param>
	/// <param name="timeout">
	/// The maximum time interval, in milliseconds, to wait for the synchronous operation to complete,
	/// or an interval that represents -1 milliseconds to wait indefinitely.
	/// </param>
	/// <returns>
	/// A task that represents the asynchronous receive operation. When an item value is successfully received from the source,
	/// the returned task is completed and its <see cref="Task{T}.Result"/> returns a successful <see cref="TryValue{TOutput}"/>
	/// containing the received value. If an item value cannot be retrieved because the source is empty and completed, the returned
	/// task is completed and its <see cref="Task{T}.Result"/> returns <see cref="TryValue.None()"/>.
	/// </returns>
	public static async Task<TryValue<TOutput>> TryReceiveAsync<TOutput>(
		this ISourceBlock<TOutput> source, TimeSpan timeout)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(timeout.IsDuration());

		try
		{
			return TryValue.New(await source.ReceiveAsync(timeout).DontMarshallContext());
		}
		catch (InvalidOperationException)
		{
			return TryValue.None<TOutput>();
		}
	}

	/// <summary>
	/// Tries to asynchronously receives a value from a specified source.
	/// </summary>
	/// <typeparam name="TOutput">The type of data contained in the source to receive.</typeparam>
	/// <param name="source">The source from which to receive the value.</param>
	/// <param name="timeout">
	/// The maximum time interval, in milliseconds, to wait for the synchronous operation to complete,
	/// or an interval that represents -1 milliseconds to wait indefinitely.
	/// </param>
	/// <param name="cancellation">The token to use to cancel the receive operation.</param>
	/// <returns>
	/// A task that represents the asynchronous receive operation. When an item value is successfully received from the source,
	/// the returned task is completed and its <see cref="Task{T}.Result"/> returns a successful <see cref="TryValue{TOutput}"/>
	/// containing the received value. If an item value cannot be retrieved because the source is empty and completed, the returned
	/// task is completed and its <see cref="Task{T}.Result"/> returns <see cref="TryValue.None()"/>.
	/// </returns>
	public static async Task<TryValue<TOutput>> TryReceiveAsync<TOutput>(
		this ISourceBlock<TOutput> source, TimeSpan timeout, CancellationToken cancellation)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(timeout.IsDuration());

		try
		{
			return TryValue.New(await source.ReceiveAsync(timeout, cancellation).DontMarshallContext());
		}
		catch (InvalidOperationException)
		{
			return TryValue.None<TOutput>();
		}
	}
}
