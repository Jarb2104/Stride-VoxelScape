namespace Voxelscape.Utility.Common.Core.Types
{
	public static class TryValue
	{
		/// <summary>
		/// Creates a successful try operation result containing the specified value.
		/// </summary>
		/// <typeparam name="T">The type of the value contained within the try operation result.</typeparam>
		/// <param name="value">The value.</param>
		/// <returns>
		/// The successful try operation result.
		/// </returns>
		public static TryValue<T> New<T>(T value) => new TryValue<T>(value);

		/// <summary>
		/// Creates a failed try operation result containing no value.
		/// </summary>
		/// <typeparam name="T">The type of the value contained within the try operation result.</typeparam>
		/// <returns>The failed try operation result.</returns>
		public static TryValue<T> None<T>() => default(TryValue<T>);
	}
}
