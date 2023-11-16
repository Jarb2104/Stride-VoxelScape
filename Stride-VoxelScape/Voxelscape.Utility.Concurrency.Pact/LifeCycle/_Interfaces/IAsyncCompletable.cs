using System.Threading.Tasks;

namespace Voxelscape.Utility.Concurrency.Pact.LifeCycle
{
	/// <summary>
	/// Defines a type that can be asynchronously completed.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The <see cref="Complete"/> method should be safe to call multiple times.
	/// </para><para>
	/// Implementations of this interface may determine if the type can complete in a faulted state and may choose
	/// whether or not to support cancelation of the completion process. If supporting cancelation, consider
	/// implementing <see cref="IAsyncCancelable"/>.
	/// </para><para>
	/// For more information on this pattern see
	/// <see href="http://blog.stephencleary.com/2013/03/async-oop-6-disposal.html">this link</see>.
	/// </para>
	/// </remarks>
	public interface IAsyncCompletable
	{
		/// <summary>
		/// Gets a task that represents the asynchronous completion of this instance.
		/// </summary>
		/// <value>
		/// The completion task.
		/// </value>
		Task Completion { get; }

		/// <summary>
		/// Begins the asynchronous completion of this instance.
		/// </summary>
		/// <remarks>
		/// This method should be safe to call multiple times.
		/// </remarks>
		void Complete();
	}
}
