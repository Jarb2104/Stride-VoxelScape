using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Procedural.Core.Noise
{
	/// <summary>
	/// Combines a noise instance and a noise distorter instance into a single noise instance.
	/// </summary>
	public class DistortedNoise2D : INoise2D
	{
		private readonly INoise2D noise;

		private readonly INoiseDistorter2D distorter;

		public DistortedNoise2D(INoise2D noise, INoiseDistorter2D distorter)
		{
			Contracts.Requires.That(noise != null);
			Contracts.Requires.That(distorter != null);

			this.noise = noise;
			this.distorter = distorter;
		}

		/// <inheritdoc />
		public double Noise(double x, double y) => this.distorter.Noise(this.noise, x, y);

		/// <inheritdoc />
		public double Noise(double x) => this.distorter.Noise(this.noise, x);
	}
}
