using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Utility.Data.Core.Caching
{
	public class StashedCacheValue<TKey, TValue> : AbstractStashedCacheValue<TKey, TValue>
	{
		public StashedCacheValue(TKey key, TValue value, IExpiryStash<TKey> stash)
			: base(key, stash)
		{
			Contracts.Requires.That(value != null);

			this.GetValue = value;
		}

		/// <inheritdoc />
		protected override TValue GetValue { get; }

		/// <inheritdoc />
		protected override void ManagedDisposal()
		{
		}
	}
}
