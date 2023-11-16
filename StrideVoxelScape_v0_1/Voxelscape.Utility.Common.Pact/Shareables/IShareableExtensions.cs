using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Shareables;

/// <summary>
/// Provides extension methods for the <see cref="IShareable"/> interface.
/// </summary>
public static class IShareableExtensions
{
	/// <summary>
	/// Share this instance with another client in the context of a Using block, guaranteeing that
	/// an accompanying call to <see cref="IShareable.Unshare"/> is made upon exiting the Using block.
	/// </summary>
	/// <param name="shareable">The shareable resource.</param>
	/// <returns>
	/// A <see cref="ShareStruct"/> that will manage calling <see cref="IShareable.Unshare"/>.
	/// </returns>
	/// <remarks>
	/// <para>
	/// This should be used within a Using block and you should not call <see cref="IShareable.Share"/> or
	/// <see cref="IShareable.Unshare"/> on the shareable resource, nor should you manually call
	/// <see cref="IDisposable.Dispose"/> on the <see cref="ShareStruct"/> returned.
	/// </para><para>
	/// Declare the disposable resource in the Using block as <c>var</c> or <c>ShareStruct</c> and not as <c>IDisposable</c>
	/// or else boxing will occur.
	/// </para>
	/// </remarks>
	public static ShareStruct ShareInUsingBlock(this IShareable shareable)
	{
		Contracts.Requires.That(shareable != null);

		shareable.Share();
		return new ShareStruct(shareable);
	}

	/// <summary>
	/// Shares the instance with another client and returns a token that can be disposed of later to make an accompanying call
	/// to <see cref="IShareable.Unshare"/>.
	/// </summary>
	/// <param name="shareable">The shareable resource.</param>
	/// <returns>An <see cref="IDisposable"/> that will call <see cref="IShareable.Unshare"/> when disposed.</returns>
	/// <remarks>
	/// If sharing in a Using block, use <see cref="ShareInUsingBlock"/> for less object creation and garbage collection.
	/// </remarks>
	public static IDisposable ShareAsDisposable(this IShareable shareable)
	{
		Contracts.Requires.That(shareable != null);

		shareable.Share();
		return new DisposableShare(shareable);
	}

	/// <summary>
	/// An <see cref="IDisposable"/> class that manages calling <see cref="IShareable.Unshare"/> when disposed.
	/// </summary>
	/// <remarks>
	/// This implementation is a class so it is safe to pass between methods and to call <see cref="IDisposable.Dispose"/> upon
	/// several times. Only the first call to dispose will unshare a shared resource.
	/// </remarks>
	private class DisposableShare : AbstractDisposable
	{
		/// <summary>
		/// The shareable resource to manage calls to.
		/// </summary>
		private IShareable shareable;

		/// <summary>
		/// Initializes a new instance of the <see cref="DisposableShare"/> class.
		/// </summary>
		/// <param name="shareable">The shareable resource.</param>
		public DisposableShare(IShareable shareable)
		{
			Contracts.Requires.That(shareable != null);

			this.shareable = shareable;
		}

		/// <inheritdoc />
		protected override void ManagedDisposal()
		{
			this.shareable.Unshare();

			// in case the disposable share is held onto after disposing, the shareable is set to null to allow garbage collection
			this.shareable = null;
		}
	}
}
