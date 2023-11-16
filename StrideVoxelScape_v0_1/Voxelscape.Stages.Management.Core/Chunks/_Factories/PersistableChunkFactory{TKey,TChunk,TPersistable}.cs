using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Persistence;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Stages.Management.Core.Chunks
{
	public class PersistableChunkFactory<TKey, TChunk, TPersistable> : IAsyncFactory<TKey, TPersistable>
	{
		private readonly IAsyncFactory<TKey, IDisposableValue<TChunk>> factory;

		private readonly IChunkPersister<TChunk, TPersistable> converter;

		public PersistableChunkFactory(
			IAsyncFactory<TKey, IDisposableValue<TChunk>> factory,
			IChunkPersister<TChunk, TPersistable> converter)
		{
			Contracts.Requires.That(factory != null);
			Contracts.Requires.That(converter != null);

			this.factory = factory;
			this.converter = converter;
		}

		/// <inheritdoc />
		public async Task<TPersistable> CreateAsync(TKey key)
		{
			using (var disposableChunk = await this.factory.CreateAsync(key).DontMarshallContext())
			{
				return this.converter.ToPersistable(disposableChunk.Value);
			}
		}
	}
}
