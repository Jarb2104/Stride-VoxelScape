using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Pact.Tasks;

// Original implementation by Stephen Cleary:
// https://github.com/StephenCleary/AsyncEx/blob/master/Source/Nito.AsyncEx%20(NET45,%20Win8,%20WP8,%20WPA81)/AsyncCountdownEvent.cs
// This version increases the count size from Int to Long.
// Original idea by Stephen Toub: http://blogs.msdn.com/b/pfxteam/archive/2012/02/11/10266930.aspx
namespace Voxelscape.Utility.Concurrency.AsyncEx
{
	/// <summary>
	/// An async-compatible countdown event.
	/// </summary>
	[DebuggerDisplay("Id = {Id}, CurrentCount = {_count}")]
	[DebuggerTypeProxy(typeof(DebugView))]
	public sealed class AsyncLongCountdownEvent
	{
		/// <summary>
		/// The TCS used to signal this event.
		/// </summary>
		private readonly Pact.Tasks.TaskCompletionSource completionSource;

		/// <summary>
		/// The remaining count on this event.
		/// </summary>
		private long count;

		/// <summary>
		/// Initializes a new instance of the <see cref="AsyncLongCountdownEvent"/> class.
		/// </summary>
		/// <param name="count">
		/// The number of signals this event will need before it becomes set. Must be greater than zero.
		/// </param>
		public AsyncLongCountdownEvent(long count)
		{
			this.completionSource = new Pact.Tasks.TaskCompletionSource();
			this.count = count;
		}

		/// <summary>
		/// Gets a semi-unique identifier for this asynchronous countdown event.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public int Id => this.completionSource.Task.Id;

		/// <summary>
		/// Gets the current number of remaining signals before this event becomes set.
		/// </summary>
		/// <value>
		/// The current count.
		/// </value>
		public long CurrentCount => Interlocked.CompareExchange(ref this.count, 0, 0);

		/// <summary>
		/// Asynchronously waits for this event to be set.
		/// </summary>
		/// <returns>The task to await.</returns>
		public Task WaitAsync() => this.completionSource.Task;

		/// <summary>
		/// Synchronously waits for this event to be set. This method may block the calling thread.
		/// </summary>
		public void Wait() => this.WaitAsync().Wait();

		/// <summary>
		/// Synchronously waits for this event to be set. This method may block the calling thread.
		/// </summary>
		/// <param name="cancellationToken">
		/// The cancellation token used to cancel the wait.
		/// If this token is already canceled, this method will first check whether the event is set.
		/// </param>
		public void Wait(CancellationToken cancellationToken)
		{
			var ret = this.WaitAsync();
			if (ret.IsCompleted)
			{
				return;
			}

			ret.Wait(cancellationToken);
		}

		/// <summary>
		/// Attempts to add the specified value to the current count.
		/// </summary>
		/// <param name="signalCount">The amount to change the current count. This must be greater than zero.</param>
		/// <returns>
		/// <c>false</c> if the count is already at zero or if the new count would be greater than
		/// <see cref="long.MaxValue"/>. Otherwise <c>true</c>.
		/// </returns>
		public bool TryAddCount(long signalCount)
		{
			Contracts.Requires.That(signalCount > 0);

			return this.ModifyCount(signalCount);
		}

		/// <summary>
		/// Attempts to add one to the current count.
		/// </summary>
		/// <returns>
		/// <c>false</c> if the count is already at zero or if the new count would be greater than
		/// <see cref="long.MaxValue"/>. Otherwise <c>true</c>.
		/// </returns>
		public bool TryAddCount() => this.TryAddCount(1);

		/// <summary>
		/// Attempts to subtract the specified value from the current count.
		/// </summary>
		/// <param name="signalCount">The amount to change the current count. This must be greater than zero.</param>
		/// <returns>
		/// <c>false</c> if the count is already at zero or if the new count would be less than zero.
		/// Otherwise <c>true</c>.
		/// </returns>
		public bool TrySignal(long signalCount)
		{
			Contracts.Requires.That(signalCount > 0);

			return this.ModifyCount(-signalCount);
		}

		/// <summary>
		/// Attempts to subtract one from the current count.
		/// </summary>
		/// <returns>
		/// <c>false</c> if the count is already at zero or if the new count would be less than zero.
		/// Otherwise <c>true</c>.
		/// </returns>
		public bool TrySignal() => this.TrySignal(1);

		/// <summary>
		/// Attempts to add the specified value to the current count.
		/// </summary>
		/// <param name="signalCount">The amount to change the current count. This must be greater than zero.</param>
		/// <exception cref="System.InvalidOperationException">
		/// Count is already at zero or if the new count would be greater than <see cref="long.MaxValue"/>.
		/// </exception>
		public void AddCount(long signalCount)
		{
			Contracts.Requires.That(signalCount > 0);

			if (!this.ModifyCount(signalCount))
			{
				throw new InvalidOperationException("Cannot increment count.");
			}
		}

		/// <summary>
		/// Attempts to add one to the current count.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">
		/// Count is already at zero or if the new count would be greater than <see cref="long.MaxValue"/>.
		/// </exception>
		public void AddCount() => this.AddCount(1);

		/// <summary>
		/// Attempts to subtract the specified value from the current count.
		/// </summary>
		/// <param name="signalCount">The amount to change the current count. This must be greater than zero.</param>
		/// <exception cref="System.InvalidOperationException">
		/// Count is already at zero or if the new count would be less than zero.
		/// </exception>
		public void Signal(long signalCount)
		{
			Contracts.Requires.That(signalCount > 0);

			if (!this.ModifyCount(-signalCount))
			{
				throw new InvalidOperationException("Cannot decrement count.");
			}
		}

		/// <summary>
		/// Attempts to subtract one from the current count.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">
		/// Count is already at zero or if the new count would be less than zero.
		/// </exception>
		public void Signal() => this.Signal(1);

		/// <summary>
		/// Attempts to modify the current count by the specified amount.
		/// </summary>
		/// <param name="signalCount">
		/// The amount to change the current count. This may be positive or negative, but not zero.
		/// </param>
		/// <returns>
		/// <c>false</c> if the new current count value would be invalid, or if the count has already reached zero.
		/// Otherwise <c>true</c>.
		/// </returns>
		private bool ModifyCount(long signalCount)
		{
			while (true)
			{
				long oldCount = this.CurrentCount;
				if (oldCount == 0)
				{
					return false;
				}

				long newCount = oldCount + signalCount;
				if (newCount < 0)
				{
					return false;
				}

				if (Interlocked.CompareExchange(ref this.count, newCount, oldCount) == oldCount)
				{
					if (newCount == 0)
					{
						this.completionSource.SetResult();
					}

					return true;
				}
			}
		}

		[DebuggerNonUserCode]
		private sealed class DebugView
		{
			private readonly AsyncLongCountdownEvent countdownEvent;

			public DebugView(AsyncLongCountdownEvent countdownEvent)
			{
				this.countdownEvent = countdownEvent;
			}

			public int Id => this.countdownEvent.Id;

			public long CurrentCount => this.countdownEvent.CurrentCount;

			public Task Task => this.countdownEvent.completionSource.Task;
		}
	}
}
