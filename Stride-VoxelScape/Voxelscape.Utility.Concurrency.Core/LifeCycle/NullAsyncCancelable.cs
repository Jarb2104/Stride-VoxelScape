using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Concurrency.Core.Tasks;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

namespace Voxelscape.Utility.Concurrency.Core.LifeCycle
{
	public class NullAsyncCancelable : AbstractAsyncCancelable
	{
		/// <inheritdoc />
		protected override Task CompleteAsync(CancellationToken cancellation) => TaskUtilities.CompletedTask;
	}
}
