using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for the <see cref="ICollection{T}"/> interface.
/// </summary>
public static class ICollectionExtensions
{
	/// <summary>
	/// Adds the values from the specified enumerable to the specified collection.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the collection.</typeparam>
	/// <param name="collection">The collection to add values to.</param>
	/// <param name="values">The values to add to the collection.</param>
	public static void AddMany<T>(this ICollection<T> collection, IEnumerable<T> values)
	{
		Contracts.Requires.That(collection != null);
		Contracts.Requires.That(values != null);

		foreach (T value in values)
		{
			collection.Add(value);
		}
	}

	/// <summary>
	/// Removes the values in the specified enumerable from the specified collection.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the collection.</typeparam>
	/// <param name="collection">The collection to remove values from.</param>
	/// <param name="values">The values to remove from the collection.</param>
	public static void RemoveMany<T>(this ICollection<T> collection, IEnumerable<T> values)
	{
		Contracts.Requires.That(collection != null);
		Contracts.Requires.That(values != null);

		foreach (T value in values)
		{
			collection.Remove(value);
		}
	}
}
