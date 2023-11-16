using System;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.Core.Stores
{
	/// <summary>
	///
	/// </summary>
	public class LockedTransactionalStore : LockedAsyncStore, ITransactionalStore
	{
		private readonly ITransactionalStore store;

		public LockedTransactionalStore(ITransactionalStore store)
			: base(store)
		{
			Contracts.Requires.That(store != null);

			this.store = store;
		}

		/// <inheritdoc />
		public async Task RunInTransactionAsync(
			Action<ITransaction> action, CancellationToken cancellation = default(CancellationToken))
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
