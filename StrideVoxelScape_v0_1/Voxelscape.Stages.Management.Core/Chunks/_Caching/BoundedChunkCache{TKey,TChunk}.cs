using System;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Core.Disposables;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Stages.Management.Core.Chunks
{
	public class BoundedChunkCache<TKey, TChunk> : IAsyncCache<TKey, TChunk>
	{
		private readonly Predicate<TKey> isInBounds;

		private readonly IAsyncCache<TKey, TChunk> cache;

		private readonly IFactory<TKey, TChunk> outOfBounds;

		public BoundedChunkCache(
			Predicate<TKey> isInBounds, IAsyncCache<TKey, TChunk> cache, IFactory<TKey, TChunk> outOfBounds)
		{
			Contracts.Requires.That(isInBounds != null);
			Contracts.Requires.That(cache != null);
			Contracts.Requires.That(outOfBounds != null);

			this.isInBounds = isInBounds;
			this.cache = cache;
			this.outOfBounds = outOfBounds;
		}

		/// <inheritdoc />
		public bool IsDisposed => this.cache.IsDisposed;

		/// <inheritdoc />
		public Task Completion => this.cache.Completion;

		/// <inheritdoc />
		public void Complete() => this.cache.Complete();

		/// <inheritdoc />
		public async Task<IPinnedValue<TKey, TChunk>> GetPinAsync(TKey key)
		{
			IAsyncCacheContracts.GetPinAsync(this, key);

			if (this.isInBounds(key))
			{
				return await this.cache.GetPinAsync(key).DontMarshallContext();
			}
			else
			{
				return new FakePin(key, this.outOfBounds.Create(key));
			}
		}

		private class FakePin : DisposableWrapper<TChunk>, IPinnedValue<TKey, TChunk>
		{
			public FakePin(TKey key, TChunk chunk)
				: base(chunk)
			{
				Contracts.Requires.That(key != null);

				this.Key = key;
			}

			/// <inheritdoc />
			public TKey Key { get; }
		}
	}
}
