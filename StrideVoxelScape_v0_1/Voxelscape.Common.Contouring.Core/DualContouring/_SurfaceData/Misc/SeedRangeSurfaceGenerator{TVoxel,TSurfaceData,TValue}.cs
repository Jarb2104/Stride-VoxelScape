using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Core.DualContouring
{
	public class SeedRangeSurfaceGenerator<TVoxel, TSurfaceData, TValue> : ISeedThreshold<TVoxel, TSurfaceData, TValue>
		where TVoxel : struct
		where TSurfaceData : class, ISeedReadable
		where TValue : struct
	{
		private readonly float min;
		private readonly float max;

		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> generator;

		public SeedRangeSurfaceGenerator(float min, float max, ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> generator)
		{
			Contracts.Requires.That(min <= max);
			Contracts.Requires.That(generator != null);

			this.min = min;
			this.max = max;
			this.generator = generator;
		}

		#region ISeedThreshold<...> Members

		public bool IsWithinThreshold(float seed)
		{
			return seed.IsIn(Range.New(this.min, this.max));
		}

		#endregion

		#region ISurfaceValueGenerator<...> Members

		public void GenerateValues(
			IVoxelProjection<TVoxel, TSurfaceData> projection,
			out TValue topLeftValue,
			out TValue topRightValue,
			out TValue bottomLeftValue,
			out TValue bottomRightValue)
		{
			this.generator.GenerateValues(
				projection,
				out topLeftValue,
				out topRightValue,
				out bottomLeftValue,
				out bottomRightValue);
		}

		#endregion
	}
}
