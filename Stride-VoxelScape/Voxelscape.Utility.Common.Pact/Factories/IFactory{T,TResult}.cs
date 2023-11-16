namespace Voxelscape.Utility.Common.Pact.Factories
{
	/// <summary>
	/// Provides a standard interface for creating instances of a type given one input argument.
	/// </summary>
	/// <typeparam name="T">The type of the input argument.</typeparam>
	/// <typeparam name="TResult">The type to create.</typeparam>
	public interface IFactory<in T, out TResult>
	{
		/// <summary>
		/// Creates an instance of the given type.
		/// </summary>
		/// <param name="arg">The input argument.</param>
		/// <returns>
		/// The instance.
		/// </returns>
		TResult Create(T arg);
	}
}
