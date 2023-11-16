using System.Collections.Generic;
using System.Linq;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Core.DualContouring
{
	public class SeedSwitchSurfaceGenerator<TVoxel, TSurfaceData, TValue> :
		ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue>
		where TVoxel : struct
		where TSurfaceData : class, ISeedReadable
		where TValue : struct
	{
		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> defaultGenerator;

		private readonly ISeedThreshold<TVoxel, TSurfaceData, TValue>[] generators;

		#region Constructors

		public SeedSwitchSurfaceGenerator(
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> defaultGenerator,
			params ISeedThreshold<TVoxel, TSurfaceData, TValue>[] generators)
		{
			Contracts.Requires.That(defaultGenerator != null);
			Contracts.Requires.That(generators.AllAndSelfNotNull());

			this.defaultGenerator = defaultGenerator;
			this.generators = generators;
		}

		public SeedSwitchSurfaceGenerator(
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> defaultGenerator,
			IEnumerable<ISeedThreshold<TVoxel, TSurfaceData, TValue>> generators)
			: this(defaultGenerator, generators.ToArray())
		{
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
			foreach (var generator in this.generators)
			{
				if (generator.IsWithinThreshold(projection.SurfaceData.AdjustedSeed))
				{
					generator.GenerateValues(
						projection,
						out topLeftValue,
						out topRightValue,
						out bottomLeftValue,
						out bottomRightValue);

					return;
				}
			}

			this.defaultGenerator.GenerateValues(
				projection,
				out topLeftValue,
				out topRightValue,
				out bottomLeftValue,
				out bottomRightValue);
		}

		#endregion
	}
}
