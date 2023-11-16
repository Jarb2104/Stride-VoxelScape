using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Pools;

/// <summary>
/// Provides extension methods for <see cref="IPoolOptions{T}"/>.
/// </summary>
public static class IPoolOptionsExtensions
{
	public static bool IsValid<T>(this IPoolOptions<T> options)
	{
		Contracts.Requires.That(options != null);

		if (options.ResetAction == null)
		{
			return false;
		}

		if (options.ReleaseAction == null)
		{
			return false;
		}

		if (options.BoundedCapacity != Capacity.Unbounded)
		{
			if (options.BoundedCapacity <= 0)
			{
				return false;
			}
		}

		return true;
	}
}
