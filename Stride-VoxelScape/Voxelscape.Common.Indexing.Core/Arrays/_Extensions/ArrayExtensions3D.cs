using System;
using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Enumerables;
using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Core.Utility;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Index = Voxelscape.Common.Indexing.Core.Enumerables.Index;

/// <summary>
/// Provides extensions methods for three dimensional arrays.
/// </summary>
public static class ArrayExtensions3D
{
	/// <summary>
	/// Gets a 32-bit integer that represents the number of elements in the specified dimension of the array.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the array.</typeparam>
	/// <param name="array">The array.</param>
	/// <param name="dimension">The dimension whose length needs to be determined.</param>
	/// <returns>A 32-bit integer that represents the number of elements in the specified dimension.</returns>
	public static int GetLength<T>(this T[,,] array, Axis3D dimension)
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
	public static long GetLongLength<T>(this T[,,] array, Axis3D dimension)
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
	public static int GetLowerBound<T>(this T[,,] array, Axis3D dimension)
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
	public static int GetUpperBound<T>(this T[,,] array, Axis3D dimension)
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
	public static Index3D GetDimensions<T>(this T[,,] array)
	{
		Contracts.Requires.That(array != null);

		return new Index3D(
			array.GetLength((int)Axis3D.X),
			array.GetLength((int)Axis3D.Y),
			array.GetLength((int)Axis3D.Z));
	}

	/// <summary>
	/// Gets the inclusive lower bounds of an array.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the array.</typeparam>
	/// <param name="array">The array.</param>
	/// <returns>The inclusive upper bounds.</returns>
	public static Index3D GetLowerBounds<T>(this T[,,] array)
	{
		Contracts.Requires.That(array != null);

		return new Index3D(
			array.GetLowerBound((int)Axis3D.X),
			array.GetLowerBound((int)Axis3D.Y),
			array.GetLowerBound((int)Axis3D.Z));
	}

	/// <summary>
	/// Gets the inclusive upper bounds of an array.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the array.</typeparam>
	/// <param name="array">The array.</param>
	/// <returns>The inclusive upper bounds.</returns>
	public static Index3D GetUpperBounds<T>(this T[,,] array)
	{
		Contracts.Requires.That(array != null);

		return new Index3D(
			array.GetUpperBound((int)Axis3D.X),
			array.GetUpperBound((int)Axis3D.Y),
			array.GetUpperBound((int)Axis3D.Z));
	}

	public static Index3D GetMiddleIndex<T>(this T[,,] array, bool roundUp = false)
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
	public static IEnumerable<Index3D> GetIndices<T>(this T[,,] array)
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
	public static IEnumerable<KeyValuePair<Index3D, T>> GetIndexValuePairs<T>(this T[,,] array)
	{
		Contracts.Requires.That(array != null);

		foreach (var index in Index.Range(array.GetLowerBounds(), array.GetDimensions()))
		{
			yield return new KeyValuePair<Index3D, T>(index, array[index.X, index.Y, index.Z]);
		}
	}

	/// <summary>
	/// Determines whether the specified index is valid to use with this array's indexer.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the array.</typeparam>
	/// <param name="array">The array.</param>
	/// <param name="index">The index to check.</param>
	/// <returns>True if the index is valid to use, otherwise false.</returns>
	public static bool IsIndexValid<T>(this T[,,] array, Index3D index)
	{
		Contracts.Requires.That(array != null);

		return index.IsIn(array.GetLowerBounds(), array.GetUpperBounds());
	}

	public static T Get<T>(this T[,,] array, Index3D index)
	{
		Contracts.Requires.That(array != null);
		Contracts.Requires.That(array.IsIndexValid(index));

		return array[index.X, index.Y, index.Z];
	}

	public static void Set<T>(this T[,,] array, Index3D index, T value)
	{
		Contracts.Requires.That(array != null);
		Contracts.Requires.That(array.IsIndexValid(index));

		array[index.X, index.Y, index.Z] = value;
	}

	public static TResult[,,] ConvertAll<TSource, TResult>(
		this TSource[,,] array, Converter<TSource, TResult> converter)
	{
		Contracts.Requires.That(array != null);
		Contracts.Requires.That(converter != null);

		var result = array.GetDimensions().CreateArray<TResult>();
		foreach (var pair in array.GetIndexValuePairs())
		{
			result[pair.Key.X, pair.Key.Y, pair.Key.Z] = converter(pair.Value);
		}

		return result;
	}
}
