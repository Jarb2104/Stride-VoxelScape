using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for one dimensional arrays.
/// </summary>
public static class ArrayExtensions
{
	/// <summary>
	/// Gets the type-safe enumerator for an array.
	/// </summary>
	/// <typeparam name="T">The type of values to enumerate.</typeparam>
	/// <param name="array">The source array.</param>
	/// <returns>The type-safe enumerator for the array.</returns>
	public static IEnumerator<T> GetEnumerator<T>(this T[] array)
	{
		Contracts.Requires.That(array != null);

		return ((IEnumerable<T>)array).GetEnumerator();
	}

	public static T[] Copy<T>(this T[] array)
	{
		Contracts.Requires.That(array != null);

		T[] result = new T[array.LongLength];
		array.CopyTo(result, 0);
		return result;
	}
}
