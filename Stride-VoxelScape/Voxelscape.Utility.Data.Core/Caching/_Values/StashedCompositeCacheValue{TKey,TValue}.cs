using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Utility.Data.Core.Caching
{
	public class StashedCompositeCacheValue<TKey, TValue> : AbstractStashedCacheValue<TKey, TValue>
	{
		private readonly IDisposable disposable;

		public StashedCompositeCacheValue(TKey key, TValue value, IDisposable disposable, IExpiryStash<TKey> stash)
			: base(key, stash)
		{
			Contracts.Requires.That(value != null);
			Contracts.Requires.That(disposable != null);

			this.GetValue = value;
			this.disposable = disposable;
		}

		/// <inheritdoc />
		protected override TValue GetValue { get; }

		/// <inheritdoc />
		protected override void ManagedDisposal() => this.disposable.Dispose();
	}
}
