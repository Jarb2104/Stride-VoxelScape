using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for the <see cref="LinkedListNode{T}"/> class.
/// </summary>
public static class LinkedListNodeExtensions
{
	/// <summary>
	/// Allows for easy enumeration of the <see cref="LinkedListNode{T}"/>s of a <see cref="LinkedList{T}"/>
	/// starting from a specified <see cref="LinkedListNode{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of elements in the linked list.</typeparam>
	/// <param name="node">The linked list node to start enumeration from.</param>
	/// <returns>A sequence of <see cref="LinkedListNode{T}"/> starting from the specified node.</returns>
	/// <remarks>
	/// Use <see cref="Enumerable.Skip"/> with a count of 1 to skip enumerating the <see cref="LinkedListNode{T}"/> this
	/// method was called on if you don't want it as the first enumerated node.
	/// </remarks>
	public static IEnumerable<LinkedListNode<T>> Nodes<T>(this LinkedListNode<T> node)
	{
		Contracts.Requires.That(node != null);

		yield return node;
		node = node.Next;
		while (node != null)
		{
			yield return node;
			node = node.Next;
		}
	}

	/// <summary>
	/// Allows for easy enumeration of the <see cref="LinkedListNode{T}"/>s of a <see cref="LinkedList{T}"/> in reverse order
	/// starting from a specified <see cref="LinkedListNode{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of elements in the linked list.</typeparam>
	/// <param name="node">The linked list node to start enumeration in reverse from.</param>
	/// <returns>A sequence of <see cref="LinkedListNode{T}"/> in reverse order starting from the specified node.</returns>
	/// <remarks>
	/// Use <see cref="Enumerable.Skip"/> with a count of 1 to skip enumerating the <see cref="LinkedListNode{T}"/> this
	/// method was called on if you don't want it as the first enumerated node.
	/// </remarks>
	public static IEnumerable<LinkedListNode<T>> ReverseNodes<T>(this LinkedListNode<T> node)
	{
		Contracts.Requires.That(node != null);

		yield return node;
		node = node.Previous;
		while (node != null)
		{
			yield return node;
			node = node.Previous;
		}
	}
}
