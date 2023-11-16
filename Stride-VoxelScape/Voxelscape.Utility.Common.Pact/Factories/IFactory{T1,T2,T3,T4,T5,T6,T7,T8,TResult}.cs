namespace Voxelscape.Utility.Common.Pact.Factories
{
	/// <summary>
	/// Provides a standard interface for creating instances of a type given eight input arguments.
	/// </summary>
	/// <typeparam name="T1">The type of the first input argument.</typeparam>
	/// <typeparam name="T2">The type of the second input argument.</typeparam>
	/// <typeparam name="T3">The type of the third input argument.</typeparam>
	/// <typeparam name="T4">The type of the forth input argument.</typeparam>
	/// <typeparam name="T5">The type of the fifth input argument.</typeparam>
	/// <typeparam name="T6">The type of the sixth input argument.</typeparam>
	/// <typeparam name="T7">The type of the seventh input argument.</typeparam>
	/// <typeparam name="T8">The type of the eighth input argument.</typeparam>
	/// <typeparam name="TResult">The type to create.</typeparam>
	public interface IFactory<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, out TResult>
	{
		/// <summary>
		/// Creates an instance of the given type.
		/// </summary>
		/// <param name="arg1">The first input argument.</param>
		/// <param name="arg2">The second input argument.</param>
		/// <param name="arg3">The third input argument.</param>
		/// <param name="arg4">The forth input argument.</param>
		/// <param name="arg5">The fifth input argument.</param>
		/// <param name="arg6">The sixth input argument.</param>
		/// <param name="arg7">The seventh input argument.</param>
		/// <param name="arg8">The eighth input argument.</param>
		/// <returns>
		/// The instance.
		/// </returns>
		TResult Create(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);
	}
}
