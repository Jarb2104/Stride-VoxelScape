using System;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Mathematics;

/// <summary>
/// Provides extension methods for <see cref="IList{T}"/> and <see cref="IReadOnlyList{T}"/>.
/// </summary>
public static class IListExtensions
{
	#region Shuffle

	/// <summary>
	/// Shuffles by randomly swapping every index in the list.
	/// Performance will suffer if not used on a list with O(1) access time.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the list.</typeparam>
	/// <param name="list">The list to be shuffled.</param>
	public static void Shuffle<T>(this IList<T> list)
	{
		Contracts.Requires.That(list != null);

		Shuffle(list, ThreadLocalRandom.Instance);
	}

	/// <summary>
	/// Shuffles by randomly swapping every index in the list.
	/// Performance will suffer if not used on a list with O(1) access time.
	/// </summary>
	/// <typeparam name="T">The type of values stored in the list.</typeparam>
	/// <param name="list">The list to be shuffled.</param>
	/// <param name="random">The Random used to determine the shuffling. This can be used to allow deterministic shuffling.</param>
	public static void Shuffle<T>(this IList<T> list, Random random)
	{
		Contracts.Requires.That(list != null);
		Contracts.Requires.That(random != null);

		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = random.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	#endregion

	#region "Read Only" Methods

	/// <summary>
	/// Inverts the order of the elements in this sequence efficiently.
	/// </summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	/// <param name="list">The list to enumerate backwards.</param>
	/// <returns>A sequence whose elements correspond to those of this sequence in reverse order.</returns>
	public static IEnumerable<T> ReverseEfficient<T>(this IReadOnlyList<T> list)
	{
		Contracts.Requires.That(list != null);

		for (int index = list.Count - 1; index >= 0; index--)
		{
			yield return list[index];
		}
	}

	/// <summary>
	/// Inverts the order of the elements in this sequence efficiently.
	/// </summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	/// <param name="list">The list to enumerate backwards.</param>
	/// <returns>A sequence whose elements correspond to those of this sequence in reverse order.</returns>
	public static IEnumerable<T> ReverseListEfficient<T>(this IList<T> list)
	{
		Contracts.Requires.That(list != null);

		for (int index = list.Count - 1; index >= 0; index--)
		{
			yield return list[index];
		}
	}

	#endregion
}
