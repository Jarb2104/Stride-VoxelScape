using System;
using System.Threading;

namespace Voxelscape.Utility.Concurrency.Core.Synchronization
{
	/// <summary>
	/// A boolean value that can be used as a guard to safely control synchronized access to critical regions.
	/// Toggling the value and discovering if the value was successfully toggled are performed as a single atomic actions
	/// without the need for any locks.
	/// </summary>
	/// <threadsafety static="true" instance="true" />
	public class SyncBool
	{
		/// <summary>
		/// The flag value it use as if it were a bool. 0 is false and 1 is true.
		/// </summary>
		/// <remarks>
		/// Use of an int instead of a bool is required because <see cref="Interlocked"/> does not support bool.
		/// </remarks>
		private int flagValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="SyncBool"/> class.
		/// </summary>
		/// <param name="value">The initial value for the bool to begin as.</param>
		public SyncBool(bool value)
		{
			this.flagValue = value ? 1 : 0;
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="SyncBool"/> is currently true or false.
		/// </summary>
		/// <value>
		///   <c>True</c> if the <see cref="SyncBool"/> if currently true; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// The very instant after checking this value it could be changed by another thread toggling the
		/// <see cref="SyncBool"/>. Algorithms that use this value must be designed accordingly.
		/// </remarks>
		public bool Value => Thread.VolatileRead(ref this.flagValue) == 1;

		/// <summary>
		/// If the bool is true, it will become false and this method will return true as a single atomic action.
		/// </summary>
		/// <returns>True if the bool was true; otherwise false.</returns>
		public bool ToggleIfTrue() => Interlocked.CompareExchange(ref this.flagValue, 0, 1) == 1;

		/// <summary>
		/// If the bool is false, it will become true and this method will return true as a single atomic action.
		/// </summary>
		/// <returns>True if the bool was false; otherwise false.</returns>
		public bool ToggleIfFalse() => Interlocked.CompareExchange(ref this.flagValue, 1, 0) == 0;

		/// <summary>
		/// Resets the toggle. This should only be called a single time per successful call to <see cref="ToggleIfTrue"/>
		/// or <see cref="ToggleIfFalse"/>.
		/// </summary>
		public void ResetToggle()
		{
			// if the flagValue is 0, set it to 1
			if (Interlocked.CompareExchange(ref this.flagValue, 1, 0) == 1)
			{
				// else, if the flagValue is 1, set it to 0
				if (Interlocked.CompareExchange(ref this.flagValue, 0, 1) == 0)
				{
					// else the flagValue changed during this call, meaning multiple threads called ResetToggle
					throw new InvalidOperationException(
						"Multiple threads are attempting to call ResetToggle at the same time. " +
						"ResetToggle should only be called a single time per successful call to one of the ToggleIf methods.");
				}
			}
		}

		/// <inheritdoc />
		public override string ToString() => this.flagValue == 1 ? "true" : "false";
	}
}
