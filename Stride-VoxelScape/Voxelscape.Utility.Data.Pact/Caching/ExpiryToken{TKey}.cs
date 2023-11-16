using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Utility.Data.Pact.Caching
{
	public struct ExpiryToken<TKey> : IKeyed<TKey>
	{
		private readonly ICacheEntry<TKey> entry;

		public ExpiryToken(ICacheEntry<TKey> entry, int tokenVersion)
		{
			Contracts.Requires.That(entry != null);

			this.entry = entry;
			this.TokenVersion = tokenVersion;
		}

		/// <inheritdoc />
		public TKey Key => this.entry.Key;

		public bool IsTokenExpired => this.entry.IsTokenExpired(this);

		public int TokenVersion { get; }

		public bool TryDisposeCacheValue() => this.entry.TryDisposeAndRemove(this);
	}
}
