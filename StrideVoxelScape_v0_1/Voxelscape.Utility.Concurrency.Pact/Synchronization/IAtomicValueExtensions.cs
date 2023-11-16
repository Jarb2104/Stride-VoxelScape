using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Pact.Synchronization;

/// <summary>
/// Provides extension methods for <see cref="IAtomicValue{T}"/>.
/// </summary>
public static class IAtomicValueExtensions
{
	public static bool TryDecrementClampLower<T>(this IAtomicValue<T> variable, T minimumValue)
		where T : IComparable<T>
	{
		Contracts.Requires.That(variable != null);

		T unused;
		return variable.TryDecrementClampLower(minimumValue, out unused);
	}

	public static bool TryDecrementClampLower<T>(this IAtomicValue<T> variable, T minimumValue, out T result)
		where T : IComparable<T>
	{
		Contracts.Requires.That(variable != null);

		result = variable.Decrement();
		if (result.IsGreaterThanOrEqual(minimumValue))
		{
			return true;
		}
		else
		{
			result = variable.Increment();
			return false;
		}
	}

	public static bool TryIncrementClampUpper<T>(this IAtomicValue<T> variable, T maximumValue)
		where T : IComparable<T>
	{
		Contracts.Requires.That(variable != null);

		T unused;
		return variable.TryIncrementClampUpper(maximumValue, out unused);
	}

	public static bool TryIncrementClampUpper<T>(this IAtomicValue<T> variable, T maximumValue, out T result)
		where T : IComparable<T>
	{
		Contracts.Requires.That(variable != null);

		result = variable.Increment();
		if (result.IsLessThanOrEqual(maximumValue))
		{
			return true;
		}
		else
		{
			result = variable.Decrement();
			return false;
		}
	}
}
