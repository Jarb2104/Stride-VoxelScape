using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Core.Collections;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.Core.Stores
{
	/// <summary>
	///
	/// </summary>
	public class AsyncStorePartitioner : IAsyncStorePartitioner
	{
		private readonly ConcurrentTypeSet stores = new ConcurrentTypeSet();

		private readonly IPersistenceConfigFactory configFactory;

		private readonly IAsyncStoreFactory storeFactory;

		public AsyncStorePartitioner(IPersistenceConfigFactory configFactory, IAsyncStoreFactory storeFactory)
		{
			Contracts.Requires.That(configFactory != null);
			Contracts.Requires.That(storeFactory != null);

			this.configFactory = configFactory;
			this.storeFactory = storeFactory;
		}

		/// <inheritdoc />
		public async Task<IAsyncStore<TEntity>> GetStoreAsync<TEntity>(
			CancellationToken cancellation = default(CancellationToken))
			where TEntity : class
		{
			cancellation.ThrowIfCancellationRequested();
			return await this.stores.GetOrAddLazyAsync(
				() => this.storeFactory.CreateStoreAsync<TEntity>(
					this.configFactory.CreateConfig<TEntity>(), cancellation)).DontMarshallContext();
		}
	}
}
