using System.Threading;
using System.Threading.Tasks;

namespace Voxelscape.Utility.Data.Pact.Stores
{
	/// <summary>
	///
	/// </summary>
	public interface IAsyncStorePartitioner
	{
		Task<IAsyncStore<TEntity>> GetStoreAsync<TEntity>(
			CancellationToken cancellation = default(CancellationToken))
			where TEntity : class;
	}
}
