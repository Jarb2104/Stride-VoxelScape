using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Data.Pact.Caching;

namespace Voxelscape.Stages.Management.Core.Chunks
{
	public class ChunkCache<TKey, TChunk, TReadOnlyChunk> : IChunkCache<TKey, TChunk, TReadOnlyChunk>
		where TChunk : TReadOnlyChunk
	{
		private readonly IAsyncCache<TKey, TChunk> cache;

		public ChunkCache(IAsyncCache<TKey, TChunk> cache)
		{
			Contracts.Requires.That(cache != null);

			this.cache = cache;
			this.AsReadOnly = new ReadOnlyChunkCache(cache);
		}

		/// <inheritdoc />
		public IChunkCache<TKey, TReadOnlyChunk> AsReadOnly { get; }

		/// <inheritdoc />
		public bool IsDisposed => this.cache.IsDisposed;

		/// <inheritdoc />
		public Task Completion => this.cache.Completion;

		/// <inheritdoc />
		public void Complete() => this.cache.Complete();

		/// <inheritdoc />
		public async Task<IDisposableValue<TChunk>> CreateAsync(TKey key) =>
			await this.GetPinAsync(key).DontMarshallContext();

		/// <inheritdoc />
		public Task<IPinnedValue<TKey, TChunk>> GetPinAsync(TKey key) => this.cache.GetPinAsync(key);

		private class ReadOnlyChunkCache : IChunkCache<TKey, TReadOnlyChunk>
		{
			private readonly IAsyncCache<TKey, TChunk> cache;

			public ReadOnlyChunkCache(IAsyncCache<TKey, TChunk> cache)
			{
				Contracts.Requires.That(cache != null);

				this.cache = cache;
			}

			/// <inheritdoc />
			public bool IsDisposed => this.cache.IsDisposed;

			/// <inheritdoc />
			public Task Completion => this.cache.Completion;

			/// <inheritdoc />
			public void Complete() => this.cache.Complete();

			/// <inheritdoc />
			public async Task<IDisposableValue<TReadOnlyChunk>> CreateAsync(TKey key) =>
				await this.GetPinAsync(key).DontMarshallContext();

			/// <inheritdoc />
			public async Task<IPinnedValue<TKey, TReadOnlyChunk>> GetPinAsync(TKey key) =>
				new ReadOnlyPin(await this.cache.GetPinAsync(key).DontMarshallContext());

			private class ReadOnlyPin : IPinnedValue<TKey, TReadOnlyChunk>
			{
				private readonly IPinnedValue<TKey, TChunk> pin;

				public ReadOnlyPin(IPinnedValue<TKey, TChunk> pin)
				{
					Contracts.Requires.That(pin != null);

					this.pin = pin;
				}

				/// <inheritdoc />
				public bool IsDisposed => this.pin.IsDisposed;

				/// <inheritdoc />
				public TKey Key => this.pin.Key;

				/// <inheritdoc />
				public TReadOnlyChunk Value => this.pin.Value;

				/// <inheritdoc />
				public void Dispose() => this.pin.Dispose();
			}
		}
	}
}
