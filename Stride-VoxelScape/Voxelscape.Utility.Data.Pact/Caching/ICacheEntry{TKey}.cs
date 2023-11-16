using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Utility.Data.Pact.Caching
{
	public interface ICacheEntry<TKey> : IKeyed<TKey>
	{
		bool IsTokenExpired(ExpiryToken<TKey> expiration);

		bool TryDisposeAndRemove(ExpiryToken<TKey> expiration);
	}
}
