using System;

namespace Voxelscape.Utility.Data.Pact.Pools
{
	public interface IPoolOptions<T>
	{
		Action<T> ResetAction { get; }

		Action<T> ReleaseAction { get; }

		int BoundedCapacity { get; }
	}
}
