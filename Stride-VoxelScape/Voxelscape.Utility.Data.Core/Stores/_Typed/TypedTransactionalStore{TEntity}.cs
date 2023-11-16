using System;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class TypedTransactionalStore<TEntity> : TypedAsyncStore<TEntity>, ITransactionalStore<TEntity>
		where TEntity : class
	{
		private readonly ITransactionalStore store;

		public TypedTransactionalStore(ITransactionalStore store)
			: base(store)
		{
			Contracts.Requires.That(store != null);

			this.store = store;
		}

		/// <inheritdoc />
		public async Task RunInTransactionAsync(
			Action<ITransaction<TEntity>> action, CancellationToken cancellation = default(CancellationToken))
		{
			ITransactionalStoreContracts.RunInTransactionAsync(action);

			cancellation.ThrowIfCancellationRequested();
			await this.store.RunInTransactionAsync(
				transaction => action(new TypedTransaction<TEntity>(transaction)), cancellation).DontMarshallContext();
		}
	}
}
