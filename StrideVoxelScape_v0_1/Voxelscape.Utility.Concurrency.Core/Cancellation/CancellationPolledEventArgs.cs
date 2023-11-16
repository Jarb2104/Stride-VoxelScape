using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Concurrency.Core.Cancellation
{
	/// <summary>
	/// Contains data associated with the cancellation status of a <see cref="InstrumentedCancellationToken"/>
	/// being polled for cancellation.
	/// </summary>
	/// <threadsafety static="true" instance="true" />
	public class CancellationPolledEventArgs : EventArgs
	{
		/// <summary>
		/// The number of ticks since the last time the cancellation was polled.
		/// </summary>
		private readonly long ticksSincePreviousPolling;

		/// <summary>
		/// A value indicating whether this is the first time the cancellation status has been polled.
		/// </summary>
		private readonly bool isFirstPolling;

		/// <summary>
		/// Initializes a new instance of the <see cref="CancellationPolledEventArgs"/> class.
		/// </summary>
		/// <param name="ticksSincePreviousPolling">The number of ticks since the last cancellation polling.</param>
		/// <param name="isFirstPolling">If set to <c>true</c> this is the first time the cancellation status has been polled.</param>
		public CancellationPolledEventArgs(long ticksSincePreviousPolling, bool isFirstPolling)
		{
			Contracts.Requires.That(ticksSincePreviousPolling >= 0);

			this.ticksSincePreviousPolling = ticksSincePreviousPolling;
			this.isFirstPolling = isFirstPolling;
		}

		/// <summary>
		/// Gets the number of ticks that have passed since the last polling of the cancellation status, or the number
		/// of ticks that have passed since the cancellation token was first created if this is the first time it has
		/// been polled for cancellation.
		/// </summary>
		/// <value>
		/// The number of ticks since the last cancellation polling.
		/// </value>
		/// <remarks>
		/// A single tick represents one hundred nanoseconds or one ten-millionth of a second. There are 10,000 ticks
		/// in a millisecond, or 10 million ticks in a second. You can divide this value by
		/// <see cref="TimeSpan.TicksPerMillisecond"/> to convert it into milliseconds.
		/// </remarks>
		public long TicksSincePreviousPolling
		{
			get
			{
				return this.ticksSincePreviousPolling;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this is the first time the cancellation status has been polled.
		/// </summary>
		/// <value>
		/// <c>true</c> if this is the first polling of the cancellation status; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// Whether or not this is the first time the cancellation status has been polled is relevant because the task
		/// may have spent considerable time waiting in queue before being started. This can result it the
		/// <see cref="TicksSincePreviousPolling"/> being much larger than usual for the first time it is polled.
		/// </remarks>
		public bool IsFirstPolling
		{
			get { return this.isFirstPolling; }
		}
	}
}
