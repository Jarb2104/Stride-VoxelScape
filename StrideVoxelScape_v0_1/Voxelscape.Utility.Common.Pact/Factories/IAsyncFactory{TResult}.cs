using System.Threading.Tasks;

namespace Voxelscape.Utility.Common.Pact.Factories
{
	/// <summary>
	/// Provides a standard interface for asynchronously creating instances of a type given no input arguments.
	/// </summary>
	/// <typeparam name="TResult">The type to create.</typeparam>
	public interface IAsyncFactory<TResult>
	{
		/// <summary>
		/// Creates an instance of the given type asynchronously.
		/// </summary>
		/// <returns>
		/// The instance.
		/// </returns>
		Task<TResult> CreateAsync();
	}
}
