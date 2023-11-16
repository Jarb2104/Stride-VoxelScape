namespace Voxelscape.Utility.Common.Pact.Conversions
{
	/// <summary>
	/// Provides a way to perform two way type conversion on values.
	/// </summary>
	/// <typeparam name="TSource">The source type of the value.</typeparam>
	/// <typeparam name="TResult">The result type of the value.</typeparam>
	public interface ITwoWayConverter<TSource, TResult>
	{
		/// <summary>
		/// Converts the specified value from the source type to the result type.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>The converted value.</returns>
		TResult Convert(TSource value);

		/// <summary>
		/// Converts the specified value back from the result type to the source type.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>The converted back value.</returns>
		TSource Convert(TResult value);
	}
}
