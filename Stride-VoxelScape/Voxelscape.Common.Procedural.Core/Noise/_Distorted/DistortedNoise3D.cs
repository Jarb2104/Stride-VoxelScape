using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Procedural.Core.Noise
{
	/// <summary>
	/// Combines a noise instance and a noise distorter instance into a single noise instance.
	/// </summary>
	public class DistortedNoise3D : INoise3D
	{
		private readonly INoise3D noise;

		private readonly INoiseDistorter3D distorter;

		public DistortedNoise3D(INoise3D noise, INoiseDistorter3D distorter)
		{
			Contracts.Requires.That(noise != null);
			Contracts.Requires.That(distorter != null);

			this.noise = noise;
			this.distorter = distorter;
		}

		/// <inheritdoc />
		public double Noise(double x, double y, double z) => this.distorter.Noise(this.noise, x, y, z);

		/// <inheritdoc />
		public double Noise(double x, double y) => this.distorter.Noise(this.noise, x, y);

		/// <inheritdoc />
		public double Noise(double x) => this.distorter.Noise(this.noise, x);
	}
}
