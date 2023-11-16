using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for <see cref="Stack{T}"/>.
/// </summary>
public static class StackExtensions
{
	public static bool TryPop<T>(this Stack<T> stack, out T result)
	{
		Contracts.Requires.That(stack != null);

		if (stack.Count == 0)
		{
			result = default(T);
			return false;
		}
		else
		{
			result = stack.Pop();
			return true;
		}
	}

	public static bool TryPeek<T>(this Stack<T> stack, out T result)
	{
		Contracts.Requires.That(stack != null);

		if (stack.Count == 0)
		{
			result = default(T);
			return false;
		}
		else
		{
			result = stack.Peek();
			return true;
		}
	}
}
