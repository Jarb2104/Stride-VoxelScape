using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Common.Core.Disposables
{
	/// <summary>
	/// A wrapper that will handle calling <see cref="IDisposable.Dispose" /> on a type whose <see cref="IDisposable.Dispose" />
	/// method may throw exceptions. This class's <see cref="IDisposable.Dispose" /> method will safely swallow those exceptions,
	/// thus preventing the exceptions from being thrown while in a finally block. See
	/// <see href="http://blog.marcgravell.com/2008/11/dontdontuse-using.html">this link</see> for more information
	/// on the issue.
	/// </summary>
	/// <typeparam name="T">The type of the unsafe disposable instance to wrap.</typeparam>
	public class SafeDisposalWrapper<T> : AbstractDisposable
		where T : class, IDisposable
	{
		/// <summary>
		/// The disposable being wrapped to provide safe disposal.
		/// </summary>
		private T disposable;

		/// <summary>
		/// Initializes a new instance of the <see cref="SafeDisposalWrapper{T}"/> class.
		/// </summary>
		/// <param name="disposable">The disposable to wrap in a safely disposable instance.</param>
		public SafeDisposalWrapper(T disposable)
		{
			Contracts.Requires.That(disposable != null);

			this.disposable = disposable;
		}

		/// <inheritdoc />
		protected override void ManagedDisposal()
		{
			if (this.disposable != null)
			{
				try
				{
					this.disposable.Dispose();
				}
				catch
				{
					// Swallow all exceptions. This keeps an exception from being thrown in the implicit finally
					// block of a using block if the disposable type throws exceptions in its Dispose method.
					// (Throwing an exception from Dispose is an active violation of MSDN best practices).
				}

				this.disposable = null;
			}
		}
	}
}
