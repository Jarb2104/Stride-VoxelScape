using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Procedural.Core.Noise
{
	/// <summary>
	///
	/// </summary>
	public class ShiftDistorter : INoiseDistorter4D
	{
		private readonly INoiseDistorter4D distorter;

		private readonly double xShift;

		private readonly double yShift;

		private readonly double zShift;

		private readonly double wShift;

		public ShiftDistorter(
			INoiseDistorter4D distorter,
			double xShift = 1,
			double yShift = 1,
			double zShift = 1,
			double wShift = 1)
		{
			Contracts.Requires.That(distorter != null);

			this.distorter = distorter;
			this.xShift = xShift;
			this.yShift = yShift;
			this.zShift = zShift;
			this.wShift = wShift;
		}

		/// <inheritdoc />
		public double Noise(INoise4D sourceNoise, double x, double y, double z, double w)
		{
			INoiseDistorter4DContracts.Noise(sourceNoise);

			return this.distorter.Noise(
				sourceNoise, x + this.xShift, y + this.yShift, z + this.zShift, w + this.wShift);
		}

		/// <inheritdoc />
		public double Noise(INoise3D sourceNoise, double x, double y, double z)
		{
			INoiseDistorter3DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x + this.xShift, y + this.yShift, z + this.zShift);
		}

		/// <inheritdoc />
		public double Noise(INoise2D sourceNoise, double x, double y)
		{
			INoiseDistorter2DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x + this.xShift, y + this.yShift);
		}

		/// <inheritdoc />
		public double Noise(INoise1D sourceNoise, double x)
		{
			INoiseDistorter1DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x + this.xShift);
		}
	}
}
