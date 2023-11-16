using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Procedural.Core.Noise
{
	/// <summary>
	///
	/// </summary>
	public class FrequencyDistorter : INoiseDistorter4D
	{
		private readonly INoiseDistorter4D distorter;

		private readonly double xFrequency;

		private readonly double yFrequency;

		private readonly double zFrequency;

		private readonly double wFrequency;

		public FrequencyDistorter(
			INoiseDistorter4D distorter,
			double xFrequency = 1,
			double yFrequency = 1,
			double zFrequency = 1,
			double wFrequency = 1)
		{
			Contracts.Requires.That(distorter != null);

			this.distorter = distorter;
			this.xFrequency = xFrequency;
			this.yFrequency = yFrequency;
			this.zFrequency = zFrequency;
			this.wFrequency = wFrequency;
		}

		/// <inheritdoc />
		public double Noise(INoise4D sourceNoise, double x, double y, double z, double w)
		{
			INoiseDistorter4DContracts.Noise(sourceNoise);

			return this.distorter.Noise(
				sourceNoise, x * this.xFrequency, y * this.yFrequency, z * this.zFrequency, w * this.wFrequency);
		}

		/// <inheritdoc />
		public double Noise(INoise3D sourceNoise, double x, double y, double z)
		{
			INoiseDistorter3DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x * this.xFrequency, y * this.yFrequency, z * this.zFrequency);
		}

		/// <inheritdoc />
		public double Noise(INoise2D sourceNoise, double x, double y)
		{
			INoiseDistorter2DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x * this.xFrequency, y * this.yFrequency);
		}

		/// <inheritdoc />
		public double Noise(INoise1D sourceNoise, double x)
		{
			INoiseDistorter1DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x * this.xFrequency);
		}
	}
}
