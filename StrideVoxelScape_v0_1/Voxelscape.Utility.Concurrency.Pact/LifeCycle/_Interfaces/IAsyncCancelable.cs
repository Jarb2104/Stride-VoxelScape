using System.Threading;

namespace Voxelscape.Utility.Concurrency.Pact.LifeCycle
{
	/// <summary>
	/// Defines a type that can be asynchronously completed or canceled.
	/// </summary>
	/// <remarks>
	/// For more information on this pattern see
	/// <see href="http://blog.stephencleary.com/2013/03/async-oop-6-disposal.html">this link</see>.
	/// </remarks>
	public interface IAsyncCancelable : IAsyncCompletable
	{
		CancellationToken CancellationToken { get; }

		/// <summary>
		/// Attempts to cancel this instance.
		/// </summary>
		/// <remarks>
		/// This method should be safe to call multiple times and to call either before or after
		/// <see cref="IAsyncCompletable.Complete()"/>.
		/// </remarks>
		void Cancel();
	}
}
