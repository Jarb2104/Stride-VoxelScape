using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Data.Pact.Stores
{
	public interface ITransactionalStore : IAsyncStore
	{
		Task RunInTransactionAsync(
			Action<ITransaction> action, CancellationToken cancellation = default(CancellationToken));
	}

	/// <summary>
	/// Provides contracts for <see cref="ITransactionalStore"/>.
	/// </summary>
	public static partial class ITransactionalStoreContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void RunInTransactionAsync(Action<ITransaction> action)
		{
			Contracts.Requires.That(action != null);
		}
	}
}
