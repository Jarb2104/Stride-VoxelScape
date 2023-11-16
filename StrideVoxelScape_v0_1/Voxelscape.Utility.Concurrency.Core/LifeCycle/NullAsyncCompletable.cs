using System.Threading.Tasks;
using Voxelscape.Utility.Concurrency.Core.Tasks;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

namespace Voxelscape.Utility.Concurrency.Core.LifeCycle
{
	public class NullAsyncCompletable : AbstractAsyncCompletable
	{
		/// <inheritdoc />
		protected override Task CompleteAsync() => TaskUtilities.CompletedTask;
	}
}
