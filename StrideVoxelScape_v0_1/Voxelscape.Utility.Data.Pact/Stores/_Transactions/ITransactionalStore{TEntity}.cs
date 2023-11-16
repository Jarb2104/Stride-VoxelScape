using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Data.Pact.Stores
{
	public interface ITransactionalStore<TEntity> : IAsyncStore<TEntity>
		where TEntity : class
	{
		Task RunInTransactionAsync(
			Action<ITransaction<TEntity>> action, CancellationToken cancellation = default(CancellationToken));
	}

	/// <summary>
	/// Provides contracts for <see cref="ITransactionalStore{TEntity}"/>.
	/// </summary>
	public static partial class ITransactionalStoreContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void RunInTransactionAsync<TEntity>(Action<ITransaction<TEntity>> action)
			where TEntity : class
		{
			Contracts.Requires.That(action != null);
		}
	}
}
