using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Concurrency.Core.Synchronization
{
	/// <summary>
	/// A synchronization utility to allow a thread to block until a result value from another thread is assigned.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <threadsafety static="true" instance="true" />
	public class PendingResult<T> : AbstractDisposable
	{
		/// <summary>
		/// The timeout duration for waiting for a value before assuming deadlock has occurred.
		/// </summary>
		private static readonly TimeSpan Timeout = new TimeSpan(0, 0, 5);

		/// <summary>
		/// The synchronization mechanism used to block and wake the reader thread.
		/// </summary>
		private readonly ManualResetEventSlim gate;

		/// <summary>
		/// The value being waited for.
		/// </summary>
		private T value;

		/// <summary>
		/// Initializes a new instance of the <see cref="PendingResult{T}"/> class.
		/// </summary>
		public PendingResult()
		{
			this.gate = new ManualResetEventSlim();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PendingResult{T}"/> class with the result value already known.
		/// Threads will not blocking waiting for the value if this constructor is used.
		/// </summary>
		/// <param name="value">The value if it is already known.</param>
		public PendingResult(T value)
		{
			this.value = value;
			this.gate = new ManualResetEventSlim(true);
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		/// <remarks>
		/// Trying to retrieve this value before it is set will block the thread
		/// Setting the value will unblock all waiting threads and return the value to them.
		/// </remarks>
		[SuppressMessage(
			"Microsoft.Design",
			"CA1065:DoNotRaiseExceptionsInUnexpectedLocations",
			Justification = "Exception is not part of normal operation, it is purely there to help detect and fix deadlocks.")]
		public T Value
		{
			get
			{
				// wait for the value to become available
				if (!this.gate.Wait(Timeout))
				{
					// check for timeout to detect possible deadlock
					throw new TimeoutException("Waiting for Value timed out. Likely deadlock occurred or thread never started.");
				}

				lock (this.gate)
				{
					return this.value;
				}
			}

			set
			{
				lock (this.gate)
				{
					this.value = value;
				}

				this.gate.Set();
			}
		}

		/// <inheritdoc />
		protected override void ManagedDisposal()
		{
			this.gate.Dispose();
		}
	}
}
