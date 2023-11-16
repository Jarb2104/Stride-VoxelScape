using System;
using Voxelscape.Utility.Data.Core.Pools;

namespace Voxelscape.Stages.Management.Pact.Chunks
{
	public class ChunkCacheBuilderOptions<T>
	{
		public double StashCapacityMultiplier { get; set; } = 1;

		public bool EagerFillPool { get; set; } = false;

		public Action<T> ResetResource { get; set; } = PoolOptions.Default<T>().ResetAction;

		public Action<T> ReleaseResource { get; set; } = PoolOptions.Default<T>().ReleaseAction;
	}
}
