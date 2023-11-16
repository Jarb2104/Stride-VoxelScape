using System.Collections.Generic;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Pact.Pools;

/// <summary>
/// Provides extension methods for <see cref="IPool{T}"/>.
/// </summary>
public static class IPoolExtensions
{
	public static bool TryTakeLoan<T>(this IPool<T> pool, out IDisposableValue<T> value)
	{
		Contracts.Requires.That(pool != null);

		T result;
		if (pool.TryTake(out result))
		{
			value = new LoanedValue<T>(pool, result);
			return true;
		}
		else
		{
			value = null;
			return false;
		}
	}

	public static IDisposableValue<T> TakeLoan<T>(this IPool<T> pool)
	{
		Contracts.Requires.That(pool != null);

		return new LoanedValue<T>(pool, pool.Take());
	}

	public static async Task<IDisposableValue<T>> TakeLoanAsync<T>(this IPool<T> pool)
	{
		Contracts.Requires.That(pool != null);

		return new LoanedValue<T>(pool, await pool.TakeAsync().DontMarshallContext());
	}

	public static void GiveMany<T>(this IPool<T> pool, IEnumerable<T> values)
	{
		Contracts.Requires.That(pool != null);
		Contracts.Requires.That(values != null);

		foreach (var value in values)
		{
			pool.Give(value);
		}
	}

	public static void GiveUntilFull<T>(this IPool<T> pool, IFactory<T> factory)
	{
		Contracts.Requires.That(pool != null);
		Contracts.Requires.That(pool.BoundedCapacity != Capacity.Unbounded);
		Contracts.Requires.That(factory != null);

		var count = (pool.BoundedCapacity - pool.AvailableCount).ClampLower(0);
		pool.GiveMany(factory.CreateMany(count));
	}
}
