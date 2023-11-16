using System;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for the <see cref="LinkedList{T}"/> class.
/// </summary>
public static class LinkedListExtensions
{
	#region RemoveAll Methods

	/// <summary>
	/// Removes all elements from the list that match the specified predicate.
	/// </summary>
	/// <typeparam name="T">The type of elements in the linked list.</typeparam>
	/// <param name="list">The list.</param>
	/// <param name="match">
	/// The predicate to determine if an element should be removed.
	/// If the predicate returns true the element will be removed.
	/// </param>
	/// <returns>The number of elements removed.</returns>
	public static int RemoveAll<T>(this LinkedList<T> list, Predicate<T> match)
	{
		Contracts.Requires.That(list != null);
		Contracts.Requires.That(match != null);

		int count = 0;
		LinkedListNode<T> node = list.First;

		while (node != null)
		{
			LinkedListNode<T> next = node.Next;
			if (match(node.Value))
			{
				list.Remove(node);
				count++;
			}

			node = next;
		}

		return count;
	}

	/// <summary>
	/// Removes all elements from the list that match the specified predicate and returns them in a new linked list.
	/// </summary>
	/// <typeparam name="T">The type of elements in the linked list.</typeparam>
	/// <param name="list">The list.</param>
	/// <param name="match">
	/// The predicate to determine if an element should be removed.
	/// If the predicate returns true the element will be removed.
	/// </param>
	/// <returns>The removed elements.</returns>
	public static LinkedList<T> RemoveAllAndGet<T>(this LinkedList<T> list, Predicate<T> match)
	{
		Contracts.Requires.That(list != null);
		Contracts.Requires.That(match != null);

		LinkedList<T> removed = new LinkedList<T>();
		LinkedListNode<T> node = list.First;

		while (node != null)
		{
			LinkedListNode<T> next = node.Next;
			if (match(node.Value))
			{
				list.Remove(node);
				removed.AddLast(node);
			}

			node = next;
		}

		return removed;
	}

	#endregion

	#region Enumeration Methods

	/// <summary>
	/// Allows for easy enumeration of the <see cref="LinkedListNode{T}"/>s of the specified <see cref="LinkedList{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of elements in the linked list.</typeparam>
	/// <param name="list">The linked list to enumerate.</param>
	/// <returns>A sequence of <see cref="LinkedListNode{T}"/>.</returns>
	public static IEnumerable<LinkedListNode<T>> Nodes<T>(this LinkedList<T> list)
	{
		Contracts.Requires.That(list != null);

		LinkedListNode<T> node = list.First;
		while (node != null)
		{
			yield return node;
			node = node.Next;
		}
	}

	/// <summary>
	/// Allows for easy enumeration of the <see cref="LinkedListNode{T}"/>s of the specified <see cref="LinkedList{T}"/> in reverse order.
	/// </summary>
	/// <typeparam name="T">The type of elements in the linked list.</typeparam>
	/// <param name="list">The linked list to enumerate in reverse order.</param>
	/// <returns>A sequence of <see cref="LinkedListNode{T}"/> in reverse order.</returns>
	public static IEnumerable<LinkedListNode<T>> ReverseNodes<T>(this LinkedList<T> list)
	{
		Contracts.Requires.That(list != null);

		LinkedListNode<T> node = list.Last;
		while (node != null)
		{
			yield return node;
			node = node.Previous;
		}
	}

	/// <summary>
	/// Inverts the order of the elements in this sequence efficiently.
	/// </summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	/// <param name="list">The list to enumerate backwards.</param>
	/// <returns>A sequence whose elements correspond to those of this sequence in reverse order.</returns>
	public static IEnumerable<T> ReverseEfficient<T>(this LinkedList<T> list)
	{
		Contracts.Requires.That(list != null);

		LinkedListNode<T> node = list.Last;
		while (node != null)
		{
			yield return node.Value;
			node = node.Previous;
		}
	}

	#endregion
}
