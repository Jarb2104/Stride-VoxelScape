using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Procedural.Core.Noise
{
	/// <summary>
	///
	/// </summary>
	public class OctavesDistorter : INoiseDistorter4D
	{
		private readonly INoiseDistorter4D distorter;

		private readonly int numberOfOctaves;

		public OctavesDistorter(INoiseDistorter4D distorter, int numberOfOctaves)
		{
			Contracts.Requires.That(distorter != null);
			Contracts.Requires.That(numberOfOctaves >= 1);

			this.distorter = distorter;
			this.numberOfOctaves = numberOfOctaves;
		}

		/// <inheritdoc />
		public double Noise(INoise4D sourceNoise, double x, double y, double z, double w)
		{
			INoiseDistorter4DContracts.Noise(sourceNoise);

			double result = this.distorter.Noise(sourceNoise, x, y, z, w);

			int weight = 2;
			for (int octave = 2; octave <= this.numberOfOctaves; octave++)
			{
				result += this.distorter.Noise(sourceNoise, x * weight, y * weight, z * weight, w * weight) / weight;
				weight *= 2;
			}

			return result;
		}

		/// <inheritdoc />
		public double Noise(INoise3D sourceNoise, double x, double y, double z)
		{
			INoiseDistorter3DContracts.Noise(sourceNoise);

			double result = this.distorter.Noise(sourceNoise, x, y, z);

			int weight = 2;
			for (int octave = 2; octave <= this.numberOfOctaves; octave++)
			{
				result += this.distorter.Noise(sourceNoise, x * weight, y * weight, z * weight) / weight;
				weight *= 2;
			}

			return result;
		}

		/// <inheritdoc />
		public double Noise(INoise2D sourceNoise, double x, double y)
		{
			INoiseDistorter2DContracts.Noise(sourceNoise);

			double result = this.distorter.Noise(sourceNoise, x, y);

			int weight = 2;
			for (int octave = 2; octave <= this.numberOfOctaves; octave++)
			{
				result += this.distorter.Noise(sourceNoise, x * weight, y * weight) / weight;
				weight *= 2;
			}

			return result;
		}

		/// <inheritdoc />
		public double Noise(INoise1D sourceNoise, double x)
		{
			INoiseDistorter1DContracts.Noise(sourceNoise);

			double result = this.distorter.Noise(sourceNoise, x);

			int weight = 2;
			for (int octave = 2; octave <= this.numberOfOctaves; octave++)
			{
				result += this.distorter.Noise(sourceNoise, x * weight) / weight;
				weight *= 2;
			}

			return result;
		}
	}
}
