using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Utility.Data.Core.Caching
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <threadsafety static="true" instance="true" />
	public class InstantExpiryStash<TKey> : AbstractDisposable, IExpiryStash<TKey>
	{
		/// <inheritdoc />
		public void AddExpiration(ExpiryToken<TKey> expiration)
		{
			IExpiryStashContracts.AddExpiration(this);

			expiration.TryDisposeCacheValue();
		}

		/// <inheritdoc />
		public bool TryExpireToken()
		{
			IExpiryStashContracts.TryExpireToken(this);

			return false;
		}

		/// <inheritdoc />
		protected override void ManagedDisposal()
		{
		}
	}
}
