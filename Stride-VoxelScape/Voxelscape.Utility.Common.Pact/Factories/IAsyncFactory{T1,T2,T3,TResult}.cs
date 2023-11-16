using System.Threading.Tasks;

namespace Voxelscape.Utility.Common.Pact.Factories
{
	/// <summary>
	/// Provides a standard interface for asynchronously creating instances of a type given three input arguments.
	/// </summary>
	/// <typeparam name="T1">The type of the first input argument.</typeparam>
	/// <typeparam name="T2">The type of the second input argument.</typeparam>
	/// <typeparam name="T3">The type of the third input argument.</typeparam>
	/// <typeparam name="TResult">The type to create.</typeparam>
	public interface IAsyncFactory<in T1, in T2, in T3, TResult>
	{
		/// <summary>
		/// Creates an instance of the given type asynchronously.
		/// </summary>
		/// <param name="arg1">The first input argument.</param>
		/// <param name="arg2">The second input argument.</param>
		/// <param name="arg3">The third input argument.</param>
		/// <returns>
		/// The instance.
		/// </returns>
		Task<TResult> CreateAsync(T1 arg1, T2 arg2, T3 arg3);
	}
}
