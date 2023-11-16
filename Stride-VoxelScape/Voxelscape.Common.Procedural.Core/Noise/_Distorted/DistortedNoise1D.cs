using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Procedural.Core.Noise
{
	/// <summary>
	/// Combines a noise instance and a noise distorter instance into a single noise instance.
	/// </summary>
	public class DistortedNoise1D : INoise1D
	{
		private readonly INoise1D noise;

		private readonly INoiseDistorter1D distorter;

		public DistortedNoise1D(INoise1D noise, INoiseDistorter1D distorter)
		{
			Contracts.Requires.That(noise != null);
			Contracts.Requires.That(distorter != null);

			this.noise = noise;
			this.distorter = distorter;
		}

		/// <inheritdoc />
		public double Noise(double x) => this.distorter.Noise(this.noise, x);
	}
}
