using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Procedural.Core.Noise;
using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Common.Contouring.Core.DualContouring
{
	public class NoiseSeedSurfaceGenerator<TVoxel, TSurfaceData, TValue> :
		ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue>
		where TVoxel : struct
		where TSurfaceData : class, ISeedWritable
		where TValue : struct
	{
		#region Private Fields

		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> generator;

		private readonly INoise3D positiveXNoise;

		private readonly INoise3D negativeXNoise;

		private readonly INoise3D positiveYNoise;

		private readonly INoise3D negativeYNoise;

		private readonly INoise3D positiveZNoise;

		private readonly INoise3D negativeZNoise;

		#endregion

		#region Constructors

		// TODO Steven - this constructor should probably be a static factory method...
		public NoiseSeedSurfaceGenerator(
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> generator,
			IFactory<int, INoise3D> noiseFactory,
			INoiseDistorter3D noiseDistorter)
			: this(
				generator,
				new DistortedNoise3D(noiseFactory.Create(0), noiseDistorter),
				new DistortedNoise3D(noiseFactory.Create(1), noiseDistorter),
				new DistortedNoise3D(noiseFactory.Create(2), noiseDistorter),
				new DistortedNoise3D(noiseFactory.Create(3), noiseDistorter),
				new DistortedNoise3D(noiseFactory.Create(4), noiseDistorter),
				new DistortedNoise3D(noiseFactory.Create(5), noiseDistorter))
		{
			Contracts.Requires.That(generator != null);
			Contracts.Requires.That(noiseFactory != null);
			Contracts.Requires.That(noiseDistorter != null);
		}

		public NoiseSeedSurfaceGenerator(
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> generator,
			INoise3D noise)
			: this(generator, noise, noise, noise)
		{
		}

		public NoiseSeedSurfaceGenerator(
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> generator,
			INoise3D xAxisNoise,
			INoise3D yAxisNoise,
			INoise3D zAxisNoise)
			: this(generator, xAxisNoise, xAxisNoise, yAxisNoise, yAxisNoise, zAxisNoise, zAxisNoise)
		{
		}

		public NoiseSeedSurfaceGenerator(
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> generator,
			INoise3D positiveXAxisNoise,
			INoise3D negativeXAxisNoise,
			INoise3D positiveYAxisNoise,
			INoise3D negativeYAxisNoise,
			INoise3D positiveZAxisNoise,
			INoise3D negativeZAxisNoise)
		{
			Contracts.Requires.That(generator != null);
			Contracts.Requires.That(positiveXAxisNoise != null);
			Contracts.Requires.That(negativeXAxisNoise != null);
			Contracts.Requires.That(positiveYAxisNoise != null);
			Contracts.Requires.That(negativeYAxisNoise != null);
			Contracts.Requires.That(positiveZAxisNoise != null);
			Contracts.Requires.That(negativeZAxisNoise != null);

			this.generator = generator;
			this.positiveXNoise = positiveXAxisNoise;
			this.negativeXNoise = negativeXAxisNoise;
			this.positiveYNoise = positiveYAxisNoise;
			this.negativeYNoise = negativeYAxisNoise;
			this.positiveZNoise = positiveZAxisNoise;
			this.negativeZNoise = negativeZAxisNoise;
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
			Index3D origin = projection.StageIndexOfOrigin;

			switch (projection.AxisDirection)
			{
				case AxisDirection3D.PositiveX:
					projection.SurfaceData.OriginalSeed = (float)this.positiveXNoise.Noise(origin.X, origin.Y, origin.Z);
					break;

				case AxisDirection3D.NegativeX:
					projection.SurfaceData.OriginalSeed = (float)this.negativeXNoise.Noise(origin.X, origin.Y, origin.Z);
					break;

				case AxisDirection3D.PositiveY:
					projection.SurfaceData.OriginalSeed = (float)this.positiveYNoise.Noise(origin.X, origin.Y, origin.Z);
					break;

				case AxisDirection3D.NegativeY:
					projection.SurfaceData.OriginalSeed = (float)this.negativeYNoise.Noise(origin.X, origin.Y, origin.Z);
					break;

				case AxisDirection3D.PositiveZ:
					projection.SurfaceData.OriginalSeed = (float)this.positiveZNoise.Noise(origin.X, origin.Y, origin.Z);
					break;

				case AxisDirection3D.NegativeZ:
					projection.SurfaceData.OriginalSeed = (float)this.negativeZNoise.Noise(origin.X, origin.Y, origin.Z);
					break;
			}

			projection.SurfaceData.AdjustedSeed = projection.SurfaceData.OriginalSeed;

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
