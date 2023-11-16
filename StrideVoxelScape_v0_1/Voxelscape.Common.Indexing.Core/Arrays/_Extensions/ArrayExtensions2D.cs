using System;
using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Enumerables;
using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Core.Utility;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Index = Voxelscape.Common.Indexing.Core.Enumerables.Index;

/// <summary>
/// Provides extensions methods for two dimensional arrays.
/// </summary>
public static class ArrayExtensions2D
{
	/// <summary>
	/// Gets a 32-bit integer that represents the number of elements in the specified dimension of the array.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the array.</typeparam>
	/// <param name="array">The array.</param>
	/// <param name="dimension">The dimension whose length needs to be determined.</param>
	/// <returns>A 32-bit integer that represents the number of elements in the specified dimension.</returns>
	public static int GetLength<T>(this T[,] array, Axis2D dimension)
	{
		Contracts.Requires.That(array != null);

		return array.GetLength((int)dimension);
	}

	/// <summary>
	/// Gets a 64-bit integer that represents the number of elements in the specified dimension of the array.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the array.</typeparam>
	/// <param name="array">The array.</param>
	/// <param name="dimension">The dimension whose length needs to be determined.</param>
	/// <returns>A 64-bit integer that represents the number of elements in the specified dimension.</returns>
	public static long GetLongLength<T>(this T[,] array, Axis2D dimension)
	{
		Contracts.Requires.That(array != null);

		return array.GetLongLength((int)dimension);
	}

	/// <summary>
	/// Gets the index of the first element of the specified dimension in the array.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the array.</typeparam>
	/// <param name="array">The array.</param>
	/// <param name="dimension">The dimension whose starting index needs to be determined.</param>
	/// <returns>The index of the first element of the specified dimension in the array.</returns>
	public static int GetLowerBound<T>(this T[,] array, Axis2D dimension)
	{
		Contracts.Requires.That(array != null);

		return array.GetLowerBound((int)dimension);
	}

	/// <summary>
	/// Gets the index of the last element of the specified dimension in the array.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the array.</typeparam>
	/// <param name="array">The array.</param>
	/// <param name="dimension">The dimension whose upper bound needs to be determined.</param>
	/// <returns>
	/// The index of the last element of the specified dimension in the array, or -1 if the specified dimension is empty.
	/// </returns>
	public static int GetUpperBound<T>(this T[,] array, Axis2D dimension)
	{
		Contracts.Requires.That(array != null);

		return array.GetUpperBound((int)dimension);
	}

	/// <summary>
	/// Gets the dimensions of an array.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the array.</typeparam>
	/// <param name="array">The array.</param>
	/// <returns>The dimensions.</returns>
	public static Index2D GetDimensions<T>(this T[,] array)
	{
		Contracts.Requires.That(array != null);

		return new Index2D(
			array.GetLength((int)Axis2D.X),
			array.GetLength((int)Axis2D.Y));
	}

	/// <summary>
	/// Gets the inclusive lower bounds of an array.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the array.</typeparam>
	/// <param name="array">The array.</param>
	/// <returns>The inclusive upper bounds.</returns>
	public static Index2D GetLowerBounds<T>(this T[,] array)
	{
		Contracts.Requires.That(array != null);

		return new Index2D(
			array.GetLowerBound((int)Axis2D.X),
			array.GetLowerBound((int)Axis2D.Y));
	}

	/// <summary>
	/// Gets the inclusive upper bounds of an array.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the array.</typeparam>
	/// <param name="array">The array.</param>
	/// <returns>The inclusive upper bounds.</returns>
	public static Index2D GetUpperBounds<T>(this T[,] array)
	{
		Contracts.Requires.That(array != null);

		return new Index2D(
			array.GetUpperBound((int)Axis2D.X),
			array.GetUpperBound((int)Axis2D.Y));
	}

	public static Index2D GetMiddleIndex<T>(this T[,] array, bool roundUp = false)
	{
		Contracts.Requires.That(array != null);

		return MiddleIndex.Get(array.GetLowerBounds(), array.GetUpperBounds(), roundUp);
	}

	/// <summary>
	/// Gets an enumerable sequence of all the indices of an array.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the array.</typeparam>
	/// <param name="array">The array.</param>
	/// <returns>The enumerable sequence of indices.</returns>
	public static IEnumerable<Index2D> GetIndices<T>(this T[,] array)
	{
		Contracts.Requires.That(array != null);

		foreach (var index in Index.Range(array.GetLowerBounds(), array.GetDimensions()))
		{
			yield return index;
		}
	}

	/// <summary>
	/// Gets an enumerable sequence of all the indices along with their associated values of an array.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the array.</typeparam>
	/// <param name="array">The array.</param>
	/// <returns>The enumerable sequence of indices paired with their values.</returns>
	public static IEnumerable<KeyValuePair<Index2D, T>> GetIndexValuePairs<T>(this T[,] array)
	{
		Contracts.Requires.That(array != null);

		foreach (var index in Index.Range(array.GetLowerBounds(), array.GetDimensions()))
		{
			yield return new KeyValuePair<Index2D, T>(index, array[index.X, index.Y]);
		}
	}

	/// <summary>
	/// Determines whether the specified index is valid to use with this array's indexer.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the array.</typeparam>
	/// <param name="array">The array.</param>
	/// <param name="index">The index to check.</param>
	/// <returns>True if the index is valid to use, otherwise false.</returns>
	public static bool IsIndexValid<T>(this T[,] array, Index2D index)
	{
		Contracts.Requires.That(array != null);

		return index.IsIn(array.GetLowerBounds(), array.GetUpperBounds());
	}

	public static T Get<T>(this T[,] array, Index2D index)
	{
		Contracts.Requires.That(array != null);
		Contracts.Requires.That(array.IsIndexValid(index));

		return array[index.X, index.Y];
	}

	public static void Set<T>(this T[,] array, Index2D index, T value)
	{
		Contracts.Requires.That(array != null);
		Contracts.Requires.That(array.IsIndexValid(index));

		array[index.X, index.Y] = value;
	}

	public static TResult[,] ConvertAll<TSource, TResult>(
		this TSource[,] array, Converter<TSource, TResult> converter)
	{
		Contracts.Requires.That(array != null);
		Contracts.Requires.That(converter != null);

		var result = array.GetDimensions().CreateArray<TResult>();
		foreach (var pair in array.GetIndexValuePairs())
		{
			result[pair.Key.X, pair.Key.Y] = converter(pair.Value);
		}

		return result;
	}
}
