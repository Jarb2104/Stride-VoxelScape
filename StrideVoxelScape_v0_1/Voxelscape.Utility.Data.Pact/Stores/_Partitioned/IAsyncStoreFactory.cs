using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Data.Pact.Stores
{
	/// <summary>
	///
	/// </summary>
	public interface IAsyncStoreFactory
	{
		Task<IAsyncStore<TEntity>> CreateStoreAsync<TEntity>(
			IPersistenceConfig config, CancellationToken cancellation = default(CancellationToken))
			where TEntity : class;
	}

	public static class IAsyncStoreFactoryContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CreateStoreAsync(IPersistenceConfig config)
		{
			Contracts.Requires.That(config != null);
		}
	}
}
