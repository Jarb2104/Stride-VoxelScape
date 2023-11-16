using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Procedural.Core.Noise
{
	/// <summary>
	///
	/// </summary>
	public class AmplitudeDistorter : INoiseDistorter4D
	{
		private readonly INoiseDistorter4D distorter;

		private readonly double amplitude;

		public AmplitudeDistorter(INoiseDistorter4D distorter, double amplitude)
		{
			Contracts.Requires.That(distorter != null);

			this.distorter = distorter;
			this.amplitude = amplitude;
		}

		/// <inheritdoc />
		public double Noise(INoise4D sourceNoise, double x, double y, double z, double w)
		{
			INoiseDistorter4DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x, y, z, w) * this.amplitude;
		}

		/// <inheritdoc />
		public double Noise(INoise3D sourceNoise, double x, double y, double z)
		{
			INoiseDistorter3DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x, y, z) * this.amplitude;
		}

		/// <inheritdoc />
		public double Noise(INoise2D sourceNoise, double x, double y)
		{
			INoiseDistorter2DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x, y) * this.amplitude;
		}

		/// <inheritdoc />
		public double Noise(INoise1D sourceNoise, double x)
		{
			INoiseDistorter1DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x) * this.amplitude;
		}
	}
}
