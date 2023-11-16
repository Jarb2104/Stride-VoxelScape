using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Utility.Data.Pact.Caching
{
	public interface IExpiryStashPool<TKey, TValue> : IExpiryStash<TKey>, IPool<TValue>
	{
	}
}
