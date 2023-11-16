using System.Threading.Tasks;

namespace Voxelscape.Utility.Concurrency.Pact.LifeCycle
{
	/// <summary>
	/// Defines a type that requires asynchronous initialization and provides the result of that initialization.
	/// </summary>
	/// <seealso href="http://blog.stephencleary.com/2013/01/async-oop-2-constructors.html"/>
	public interface IAsyncInitializable
	{
		/// <summary>
		/// Gets the task that represents the asynchronous initialization of this instance.
		/// </summary>
		/// <value>
		/// The initialization of this instance.
		/// </value>
		Task Initialization { get; }
	}
}
