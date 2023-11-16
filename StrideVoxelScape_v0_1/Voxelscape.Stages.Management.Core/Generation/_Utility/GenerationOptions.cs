using System.Threading;
using System.Threading.Tasks.Dataflow;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Core.Generation
{
	/// <summary>
	///
	/// </summary>
	public class GenerationOptions
	{
		private int maxDegreeOfParallelism = DefaultMaxDegreeOfParallelism;

		public static int DefaultMaxDegreeOfParallelism { get; } = DataflowBlockOptions.Unbounded;

		public static CancellationToken DefaultCancellationToken { get; } = CancellationToken.None;

		public int MaxDegreeOfParallelism
		{
			get
			{
				return this.maxDegreeOfParallelism;
			}

			set
			{
				Contracts.Requires.That(value > 0 || value == DataflowBlockOptions.Unbounded);

				this.maxDegreeOfParallelism = value;
			}
		}

		public CancellationToken CancellationToken { get; set; } = DefaultCancellationToken;
	}
}
