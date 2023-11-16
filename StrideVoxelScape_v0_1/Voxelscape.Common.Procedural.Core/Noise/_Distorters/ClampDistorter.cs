using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Procedural.Core.Noise
{
	/// <summary>
	///
	/// </summary>
	public class ClampDistorter : INoiseDistorter4D
	{
		private readonly INoiseDistorter4D distorter;

		private readonly double min;

		private readonly double max;

		public ClampDistorter(INoiseDistorter4D distorter, double min, double max)
		{
			Contracts.Requires.That(distorter != null);

			this.distorter = distorter;
			this.min = min;
			this.max = max;
		}

		/// <inheritdoc />
		public double Noise(INoise4D sourceNoise, double x, double y, double z, double w)
		{
			INoiseDistorter4DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x, y, z, w).Clamp(this.min, this.max);
		}

		/// <inheritdoc />
		public double Noise(INoise3D sourceNoise, double x, double y, double z)
		{
			INoiseDistorter3DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x, y, z).Clamp(this.min, this.max);
		}

		/// <inheritdoc />
		public double Noise(INoise2D sourceNoise, double x, double y)
		{
			INoiseDistorter2DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x, y).Clamp(this.min, this.max);
		}

		/// <inheritdoc />
		public double Noise(INoise1D sourceNoise, double x)
		{
			INoiseDistorter1DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x).Clamp(this.min, this.max);
		}
	}
}
