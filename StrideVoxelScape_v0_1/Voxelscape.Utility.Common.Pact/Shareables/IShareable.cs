using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Shareables
{
	/// <summary>
	/// Represents a shareable resource that may require special handling once all client's are
	/// finished using it.
	/// </summary>
	/// <remarks>
	/// Clients of a shareable resource must be careful to properly call <see cref="Share"/> and
	/// <see cref="Unshare"/> to avoid potential resource leaks. Creation of a new shareable resource
	/// does not start it in a shared state; that is <see cref="IsShared"/> will return <c>false</c>.
	/// A call to <see cref="Share"/> should be made before the first client begins using the resource.
	/// </remarks>
	public interface IShareable
	{
		/// <summary>
		/// Gets a value indicating whether this instance is shared among one or more clients.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is shared; otherwise, <c>false</c>.
		/// </value>
		bool IsShared
		{
			get;
		}

		int ShareCount
		{
			get;
		}

		/// <summary>
		/// Share this instance with another client.
		/// </summary>
		/// <remarks>
		/// Each call to <see cref="Share"/> must be followed later by a call to <see cref="Unshare"/>.
		/// Use of a Try Finally block is recommended to ensure this behavior even in exceptional circumstances, or use
		/// <see cref="IShareableExtensions.ShareInUsingBlock"/> to safely share the resource inside a Using block to
		/// ensure that <see cref="Share"/> and <see cref="Unshare"/> are called properly.
		/// </remarks>
		/// <seealso cref="Share"/>
		void Share();

		/// <summary>
		/// Release this instance from the use of a client.
		/// </summary>
		/// <returns>True if this instance is not shared; otherwise false.</returns>
		/// <remarks>
		/// Each call to <see cref="Share" /> must be followed later by a call to <see cref="Unshare" />.
		/// See <see cref="Share" /> for more information.
		/// </remarks>
		bool Unshare();
	}

	public static class IShareableContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Unshare(IShareable instance)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(instance.IsShared);
		}
	}
}
