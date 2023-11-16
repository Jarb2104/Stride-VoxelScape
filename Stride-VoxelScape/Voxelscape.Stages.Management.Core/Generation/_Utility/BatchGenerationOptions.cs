using Voxelscape.Utility.Concurrency.Dataflow.Blocks;

namespace Voxelscape.Stages.Management.Core.Generation
{
	/// <summary>
	///
	/// </summary>
	public class BatchGenerationOptions : GenerationOptions
	{
		public static Batching DefaultBatching => Batching.Dynamic;

		public Batching Batching { get; set; } = DefaultBatching;
	}
}
