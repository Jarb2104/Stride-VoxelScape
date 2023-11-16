using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Voxelscape.Utility.Common.Pact.Disposables
{
	/// <summary>
	/// Provides a base class implementation of <see cref="IVisiblyDisposable"/> that follows the disposable pattern,
	/// providing an already correctly implemented destructor and <see cref="Dispose"/> method. The disposal state of
	/// this base class is thread safe.
	/// </summary>
	/// <threadsafety static="true" instance="true" />
	public abstract class AbstractDisposable : IVisiblyDisposable
	{
		/// <summary>
		/// A value indicating whether this instance is disposed of (1) or not yet disposed (0).
		/// </summary>
		private int isDisposed = 0;

		/// <inheritdoc />
		public virtual bool IsDisposed => Thread.VolatileRead(ref this.isDisposed) == 1;

		/// <inheritdoc />
		[SuppressMessage("Microsoft.Design", "CA1063", Justification = "Better disposable pattern.")]
		public virtual void Dispose()
		{
			if (this.TrySetDisposed())
			{
				this.ManagedDisposal();

#if DEBUG
				System.GC.SuppressFinalize(this);
#endif
			}
		}

		/// <summary>
		/// Releases managed resources.
		/// </summary>
		/// <remarks>
		/// Implementers do not need to ensure that this method is called only once. However, do not ever call this method
		/// from your code. Only ever call the the public <see cref="Dispose"/> method. Implementers should still call the
		/// base class's protected <see cref="ManagedDisposal"/> method at the end of their method if they are part of a
		/// class hierarchy.
		/// </remarks>
		protected abstract void ManagedDisposal();

		/// <summary>
		/// Tries to set <see cref="IsDisposed"/> to true if it hasn't already been set.
		/// </summary>
		/// <returns>True if able to set <see cref="IsDisposed"/> to true; otherwise false.</returns>
		private bool TrySetDisposed()
		{
			// if isDisposed is 0, set it to 1 and return true (CompareExchange returns 0 which 0 == 0)
			// otherwise just return false (CompareExchange returns 1 which 1 != 0)
			return Interlocked.CompareExchange(ref this.isDisposed, 1, 0) == 0;
		}
	}
}
