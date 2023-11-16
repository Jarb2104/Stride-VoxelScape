using System.Threading;
using System.Threading.Tasks;

namespace Voxelscape.Utility.Data.Pact.Stores
{
	public interface IAsyncStoreMigrator
	{
		Task MigrateAsync(CancellationToken cancellation = default(CancellationToken));
	}
}
