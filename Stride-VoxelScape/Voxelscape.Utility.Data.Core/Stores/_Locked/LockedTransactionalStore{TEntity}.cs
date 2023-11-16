using System;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public class LockedTransactionalStore<TEntity> : LockedAsyncStore<TEntity>, ITransactionalStore<TEntity>
		where TEntity : class
	{
		private readonly ITransactionalStore<TEntity> store;

		public LockedTransactionalStore(ITransactionalStore<TEntity> store)
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
			using (await this.Lock.WriterLockAsync(cancellation).DontMarshallContext())
			{
				cancellation.ThrowIfCancellationRequested();
				await this.store.RunInTransactionAsync(action, cancellation).DontMarshallContext();
			}
		}
	}
}
