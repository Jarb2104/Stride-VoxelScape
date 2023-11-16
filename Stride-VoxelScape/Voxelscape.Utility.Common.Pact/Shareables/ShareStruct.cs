using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Shareables
{
	/// <summary>
	/// An <see cref="IDisposable"/> class that call <see cref="IShareable.Unshare"/> on an <see cref="IShareable"/> resource
	/// when used within a Using block.
	/// </summary>
	/// <remarks>
	/// This implementation is a struct and thus should only ever be used in a using block through the use of the
	/// <see cref="IShareableExtensions.ShareInUsingBlock"/> method. Multiple calls made manually to <see cref="Dispose"/>
	/// will each result in an un-sharing of the shared resource. This can easily lead to difficult to track down bugs.
	/// The default constructor should never be used as it results in an instance with no shareable resource to unshare.
	/// </remarks>
	public struct ShareStruct : IDisposable
	{
		/// <summary>
		/// The shareable resource to manage calls to.
		/// </summary>
		private readonly IShareable shareable;

		/// <summary>
		/// Initializes a new instance of the <see cref="ShareStruct"/> struct.
		/// </summary>
		/// <param name="shareable">The shareable resource.</param>
		internal ShareStruct(IShareable shareable)
		{
			Contracts.Requires.That(shareable != null);

			this.shareable = shareable;
		}

		// ?. because the default struct constructor leaves the field as null
		/// <inheritdoc />
		public void Dispose() => this.shareable?.Unshare();
	}
}
