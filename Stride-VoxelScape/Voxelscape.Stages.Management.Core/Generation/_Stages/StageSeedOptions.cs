using System.Threading;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

namespace Voxelscape.Stages.Management.Core.Generation
{
	/// <summary>
	///
	/// </summary>
	public class StageSeedOptions
	{
		public static IAsyncCompletable DefaultPostGeneration { get; } = null;

		public static CancellationToken DefaultCancellationToken { get; } = CancellationToken.None;

		public IAsyncCompletable PostGeneration { get; set; } = DefaultPostGeneration;

		public CancellationToken CancellationToken { get; set; } = DefaultCancellationToken;
	}
}
