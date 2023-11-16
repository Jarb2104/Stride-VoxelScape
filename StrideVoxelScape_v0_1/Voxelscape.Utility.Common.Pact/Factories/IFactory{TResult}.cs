namespace Voxelscape.Utility.Common.Pact.Factories
{
	/// <summary>
	/// Provides a standard interface for creating instances of a type given no input arguments.
	/// </summary>
	/// <typeparam name="TResult">The type to create.</typeparam>
	public interface IFactory<out TResult>
	{
		/// <summary>
		/// Creates an instance of the given type.
		/// </summary>
		/// <returns>The instance.</returns>
		TResult Create();
	}
}
