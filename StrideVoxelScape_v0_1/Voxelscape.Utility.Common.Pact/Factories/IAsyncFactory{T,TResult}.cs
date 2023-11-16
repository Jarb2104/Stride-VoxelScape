using System.Threading.Tasks;

namespace Voxelscape.Utility.Common.Pact.Factories
{
	/// <summary>
	/// Provides a standard interface for asynchronously creating instances of a type given one input arguments.
	/// </summary>
	/// <typeparam name="T">The type of the input argument.</typeparam>
	/// <typeparam name="TResult">The type to create.</typeparam>
	public interface IAsyncFactory<in T, TResult>
	{
		/// <summary>
		/// Creates an instance of the given type asynchronously.
		/// </summary>
		/// <param name="arg">The input argument.</param>
		/// <returns>
		/// The instance.
		/// </returns>
		Task<TResult> CreateAsync(T arg);
	}
}
