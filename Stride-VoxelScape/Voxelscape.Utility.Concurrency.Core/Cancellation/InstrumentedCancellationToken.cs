using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Events;

namespace Voxelscape.Utility.Concurrency.Core.Cancellation
{
	/// <summary>
	/// Instruments a normal <see cref="CancellationToken"/> with the ability to track how frequently it is polled
	/// for cancellation and take action accordingly.
	/// </summary>
	/// <threadsafety static="true" instance="true" />
	[SuppressMessage("StyleCop", "SA1201", Justification = "Grouping by interface.")]
	public class InstrumentedCancellationToken
	{
		/// <summary>
		/// The instrumented cancellation token.
		/// </summary>
		private readonly CancellationToken token;

		/// <summary>
		/// The lock used to protect the polling data.
		/// </summary>
		private readonly object pollingLock = new object();

		/// <summary>
		/// The time in ticks that the cancellation status was last polled at for cancellation.
		/// </summary>
		private long previousPollTicks = DateTime.Now.Ticks;

		/// <summary>
		/// A value indicating whether this is first polling of this cancellation token.
		/// </summary>
		private bool isFirstPolling = true;

		#region Constructors and Static Constructor Property

		/// <summary>
		/// Initializes a new instance of the <see cref="InstrumentedCancellationToken"/> class.
		/// </summary>
		/// <remarks>
		/// This <see cref="InstrumentedCancellationToken"/> will be non-cancelable by default.
		/// </remarks>
		public InstrumentedCancellationToken()
			: this(CancellationToken.None)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InstrumentedCancellationToken" /> class.
		/// </summary>
		/// <param name="token">The <see cref="CancellationToken" /> to instrument.</param>
		public InstrumentedCancellationToken(CancellationToken token)
		{
			this.token = token;
		}

		/// <summary>
		/// Gets an empty <see cref="InstrumentedCancellationToken"/> value.
		/// </summary>
		/// <value>
		/// Returns an empty <see cref="InstrumentedCancellationToken"/> value.
		/// </value>
		/// <remarks>
		/// The <see cref="InstrumentedCancellationToken"/> returned by this property will be non-cancelable by default.
		/// </remarks>
		public static InstrumentedCancellationToken None => new InstrumentedCancellationToken();

		#endregion

		#region Equality Operators

		public static bool operator ==(InstrumentedCancellationToken lhs, InstrumentedCancellationToken rhs) =>
			lhs.EqualsNullSafe(rhs);

		public static bool operator !=(InstrumentedCancellationToken lhs, InstrumentedCancellationToken rhs) =>
			!lhs.EqualsNullSafe(rhs);

		#endregion

		#region CancellationToken Members (augmented with tracking)

		/// <summary>
		/// Gets a value indicating whether cancellation has been requested for this token.
		/// </summary>
		/// <value>
		/// <c>true</c> if cancellation has been requested for this token; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// <para>
		/// This property indicates whether cancellation has been requested for this token, either through the token initially
		/// being constructed in a canceled state, or through calling Cancel on the token's associated CancellationTokenSource.
		/// </para><para>
		/// If this property is true, it only guarantees that cancellation has been requested.
		/// </para><para>
		/// It does not guarantee that every registered handler has finished executing, nor that cancellation requests have
		/// finished propagating to all registered handlers. Additional synchronization may be required, particularly in
		/// situations where related objects are being canceled concurrently.
		/// </para>
		/// </remarks>
		public bool IsCancellationRequested
		{
			get
			{
				this.RaiseCancellationPolled();
				return this.token.IsCancellationRequested;
			}
		}

		/// <summary>
		/// Throws a <see cref="OperationCanceledException"/> if this token has had cancellation requested.
		/// </summary>
		public void ThrowIfCancellationRequested()
		{
			this.RaiseCancellationPolled();
			this.token.ThrowIfCancellationRequested();
		}

		#endregion

		#region CancellationToken Members (straight through delegation)

		/// <summary>
		/// Gets a value indicating whether this token is capable of being in the canceled state.
		/// </summary>
		/// <value>
		/// <c>true</c> if this token is capable of being in the canceled state; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// If CanBeCanceled returns false, it is guaranteed that the token will never transition into a canceled state,
		/// meaning that <see cref="IsCancellationRequested"/> will never return true.
		/// </remarks>
		public bool CanBeCanceled => this.token.CanBeCanceled;

		/// <summary>
		/// Gets a <see cref="WaitHandle"/> that is signaled when the token is canceled.
		/// </summary>
		/// <value>
		/// A <see cref="WaitHandle"/> that is signaled when the token is canceled.
		/// </value>
		/// <remarks>
		/// Accessing this property causes a <see cref="WaitHandle"/> to be instantiated. It is preferable to only use this
		/// property when necessary, and to then dispose the associated <see cref="CancellationTokenSource"/> instance at the
		/// earliest opportunity (disposing the source will dispose of this allocated handle). The handle should not be
		/// closed or disposed directly.
		/// </remarks>
		public WaitHandle WaitHandle => this.token.WaitHandle;

		/// <summary>
		/// Registers a delegate that will be called when this <see cref="InstrumentedCancellationToken"/> is canceled.
		/// </summary>
		/// <param name="callback">The delegate to be executed when the <see cref="InstrumentedCancellationToken"/> is canceled.</param>
		/// <returns>The <see cref="CancellationTokenRegistration"/> instance that can be used to deregister the callback.</returns>
		/// <remarks>
		/// <para>
		/// If this token is already in the canceled state, the delegate will be run immediately and synchronously.
		/// Any exception the delegate generates will be propagated out of this method call.
		/// </para><para>
		/// The current <see cref="ExecutionContext"/>, if one exists, will be captured along with the delegate and will be
		/// used when executing it.
		/// </para>
		/// </remarks>
		public CancellationTokenRegistration Register(Action callback)
		{
			Contracts.Requires.That(callback != null);

			return this.token.Register(callback);
		}

		/// <summary>
		/// Registers a delegate that will be called when this <see cref="InstrumentedCancellationToken"/> is canceled.
		/// </summary>
		/// <param name="callback">The delegate to be executed when the <see cref="InstrumentedCancellationToken"/> is canceled.</param>
		/// <param name="useSynchronizationContext">
		/// A Boolean value that indicates whether to capture the current <see cref="SynchronizationContext"/> and use it
		/// when invoking the callback.
		/// </param>
		/// <returns>The <see cref="CancellationTokenRegistration"/> instance that can be used to deregister the callback.</returns>
		/// <remarks>
		/// <para>
		/// If this token is already in the canceled state, the delegate will be run immediately and synchronously.
		/// Any exception the delegate generates will be propagated out of this method call.
		/// </para><para>
		/// The current <see cref="ExecutionContext"/>, if one exists, will be captured along with the delegate and will be
		/// used when executing it.
		/// </para>
		/// </remarks>
		public CancellationTokenRegistration Register(Action callback, bool useSynchronizationContext)
		{
			Contracts.Requires.That(callback != null);

			return this.token.Register(callback, useSynchronizationContext);
		}

		/// <summary>
		/// Registers a delegate that will be called when this <see cref="InstrumentedCancellationToken"/> is canceled.
		/// </summary>
		/// <param name="callback">The delegate to be executed when the <see cref="InstrumentedCancellationToken"/> is canceled.</param>
		/// <param name="state">The state to pass to the callback when the delegate is invoked. This may be null.</param>
		/// <returns>The <see cref="CancellationTokenRegistration"/> instance that can be used to deregister the callback.</returns>
		/// <remarks>
		/// <para>
		/// If this token is already in the canceled state, the delegate will be run immediately and synchronously.
		/// Any exception the delegate generates will be propagated out of this method call.
		/// </para><para>
		/// The current <see cref="ExecutionContext"/>, if one exists, will be captured along with the delegate and will be
		/// used when executing it.
		/// </para>
		/// </remarks>
		public CancellationTokenRegistration Register(Action<object> callback, object state)
		{
			Contracts.Requires.That(callback != null);

			return this.token.Register(callback, state);
		}

		/// <summary>
		/// Registers a delegate that will be called when this <see cref="InstrumentedCancellationToken"/> is canceled.
		/// </summary>
		/// <param name="callback">The delegate to be executed when the <see cref="InstrumentedCancellationToken"/> is canceled.</param>
		/// <param name="state">The state to pass to the callback when the delegate is invoked. This may be null.</param>
		/// <param name="useSynchronizationContext">
		/// A Boolean value that indicates whether to capture the current <see cref="SynchronizationContext"/> and use it
		/// when invoking the callback.
		/// </param>
		/// <returns>The <see cref="CancellationTokenRegistration"/> instance that can be used to deregister the callback.</returns>
		/// <remarks>
		/// <para>
		/// If this token is already in the canceled state, the delegate will be run immediately and synchronously.
		/// Any exception the delegate generates will be propagated out of this method call.
		/// </para><para>
		/// The current <see cref="ExecutionContext"/>, if one exists, will be captured along with the delegate and will be
		/// used when executing it.
		/// </para>
		/// </remarks>
		public CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext)
		{
			Contracts.Requires.That(callback != null);

			return this.token.Register(callback, state, useSynchronizationContext);
		}

		#endregion

		#region Object Overrides

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (this.EqualsByReferenceNullSafe(obj))
			{
				return true;
			}

			var other = obj as InstrumentedCancellationToken;
			if (other == null)
			{
				return false;
			}

			return this.token == other.token;
		}

		/// <inheritdoc />
		public override int GetHashCode() => this.token.GetHashCode();

		/// <inheritdoc />
		public override string ToString() => this.token.ToString();

		#endregion

		#region Cancellation Polling

		/// <summary>
		/// Occurs when the cancellation status is polled.
		/// </summary>
		[SuppressMessage("Microsoft.Design", "CA1009", Justification = "Type-safe event handler pattern.")]
		public event TypeSafeEventHandler<InstrumentedCancellationToken, CancellationPolledEventArgs> CancellationPolled;

		/// <summary>
		/// Raises the <see cref="CancellationPolled"/> event.
		/// </summary>
		/// <param name="eventArgs">The <see cref="CancellationPolledEventArgs"/> instance containing the event data.</param>
		protected virtual void RaiseCancellationPolled(CancellationPolledEventArgs eventArgs)
		{
			Contracts.Requires.That(eventArgs != null);

			this.CancellationPolled?.Invoke(this, eventArgs);
		}

		/// <summary>
		/// Raises the <see cref="CancellationPolled"/> event with the appropriate event args to match the current state of
		/// this cancellation token and updates the cancellation polling tracking data.
		/// </summary>
		private void RaiseCancellationPolled()
		{
			CancellationPolledEventArgs eventArgs;

			lock (this.pollingLock)
			{
				long currentTicks = DateTime.Now.Ticks;

				eventArgs = new CancellationPolledEventArgs(
					currentTicks - this.previousPollTicks, this.isFirstPolling);

				this.previousPollTicks = currentTicks;

				if (this.isFirstPolling)
				{
					this.isFirstPolling = false;
				}
			}

			this.RaiseCancellationPolled(eventArgs);
		}

		#endregion
	}
}
