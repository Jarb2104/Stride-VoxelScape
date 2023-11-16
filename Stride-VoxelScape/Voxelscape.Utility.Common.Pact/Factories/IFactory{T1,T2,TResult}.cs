namespace Voxelscape.Utility.Common.Pact.Factories
{
	/// <summary>
	/// Provides a standard interface for creating instances of a type given two input arguments.
	/// </summary>
	/// <typeparam name="T1">The type of the first input argument.</typeparam>
	/// <typeparam name="T2">The type of the second input argument.</typeparam>
	/// <typeparam name="TResult">The type to create.</typeparam>
	public interface IFactory<in T1, in T2, out TResult>
	{
		/// <summary>
		/// Creates an instance of the given type.
		/// </summary>
		/// <param name="arg1">The first input argument.</param>
		/// <param name="arg2">The second input argument.</param>
		/// <returns>
		/// The instance.
		/// </returns>
		TResult Create(T1 arg1, T2 arg2);
	}
}
