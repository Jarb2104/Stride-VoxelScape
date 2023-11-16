using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

namespace Voxelscape.Stages.Management.Core.Chunks
{
	public class AsyncCompletableChunkFactory<TKey, TChunk> :
		IAsyncFactory<TKey, TChunk>, IAsyncCompletable
	{
		private readonly IAsyncFactory<TKey, TChunk> factory;

		private readonly IAsyncCompletable completable;

		public AsyncCompletableChunkFactory(IAsyncFactory<TKey, TChunk> factory, IAsyncCompletable completable)
		{
			Contracts.Requires.That(factory != null);
			Contracts.Requires.That(completable != null);

			this.factory = factory;
			this.completable = completable;
		}

		/// <inheritdoc />
		public Task Completion => this.completable.Completion;

		/// <inheritdoc />
		public void Complete() => this.completable.Complete();

		/// <inheritdoc />
		public Task<TChunk> CreateAsync(TKey key) => this.factory.CreateAsync(key);
	}
}
