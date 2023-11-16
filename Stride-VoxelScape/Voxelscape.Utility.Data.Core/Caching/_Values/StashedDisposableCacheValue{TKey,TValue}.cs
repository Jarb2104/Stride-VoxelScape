using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Utility.Data.Core.Caching
{
	public class StashedDisposableCacheValue<TKey, TValue> : AbstractStashedCacheValue<TKey, TValue>
	{
		private readonly IDisposableValue<TValue> value;

		public StashedDisposableCacheValue(TKey key, IDisposableValue<TValue> value, IExpiryStash<TKey> stash)
			: base(key, stash)
		{
			Contracts.Requires.That(value != null);

			this.value = value;
		}

		/// <inheritdoc />
		protected override TValue GetValue => this.value.Value;

		/// <inheritdoc />
		protected override void ManagedDisposal() => this.value.Dispose();
	}
}
