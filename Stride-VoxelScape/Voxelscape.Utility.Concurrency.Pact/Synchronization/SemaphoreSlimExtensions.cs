using System;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Pact;
using Voxelscape.Utility.Concurrency.Pact.Synchronization;

/// <summary>
/// Provides extension methods for <see cref="SemaphoreSlim"/>.
/// </summary>
public static class SemaphoreSlimExtensions
{
	public static async Task<ReleaseStruct> WaitInUsingBlockAsync(
		this SemaphoreSlim semaphore, CancellationToken cancellation = default(CancellationToken))
	{
		Contracts.Requires.That(semaphore != null);

		await semaphore.WaitAsync(cancellation).DontMarshallContext();
		return new ReleaseStruct(semaphore);
	}

	public static async Task<TryValue<ReleaseStruct>> TryWaitForUsingBlockAsync(
		this SemaphoreSlim semaphore,
		int millisecondsTimeout,
		CancellationToken cancellation = default(CancellationToken))
	{
		Contracts.Requires.That(semaphore != null);
		Contracts.Requires.That(Time.IsDuration(millisecondsTimeout));

		if (await semaphore.WaitAsync(millisecondsTimeout, cancellation).DontMarshallContext())
		{
			return TryValue.New(new ReleaseStruct(semaphore));
		}
		else
		{
			return TryValue.None<ReleaseStruct>();
		}
	}

	public static async Task<TryValue<ReleaseStruct>> TryWaitForUsingBlockAsync(
		this SemaphoreSlim semaphore,
		TimeSpan timeout,
		CancellationToken cancellation = default(CancellationToken))
	{
		Contracts.Requires.That(semaphore != null);
		Contracts.Requires.That(Time.IsDuration(timeout));

		if (await semaphore.WaitAsync(timeout, cancellation).DontMarshallContext())
		{
			return TryValue.New(new ReleaseStruct(semaphore));
		}
		else
		{
			return TryValue.None<ReleaseStruct>();
		}
	}

	public static Task<bool> TryWait(
		this SemaphoreSlim semaphore,
		int millisecondsTimeout,
		CancellationToken cancellation = default(CancellationToken))
	{
		Contracts.Requires.That(semaphore != null);
		Contracts.Requires.That(Time.IsDuration(millisecondsTimeout));

		return semaphore.WaitAsync(millisecondsTimeout, cancellation);
	}

	public static Task<bool> TryWait(
		this SemaphoreSlim semaphore,
		TimeSpan timeout,
		CancellationToken cancellation = default(CancellationToken))
	{
		Contracts.Requires.That(semaphore != null);
		Contracts.Requires.That(Time.IsDuration(timeout));

		return semaphore.WaitAsync(timeout, cancellation);
	}
}
