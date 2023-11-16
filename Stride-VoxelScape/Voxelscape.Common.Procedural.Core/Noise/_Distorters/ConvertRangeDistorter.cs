using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Procedural.Core.Noise
{
	/// <summary>
	///
	/// </summary>
	public class ConvertRangeDistorter : INoiseDistorter4D
	{
		private readonly INoiseDistorter4D distorter;

		private readonly double sourceNoiseStart;

		private readonly double sourceNoiseEnd;

		private readonly double resultNoiseStart;

		private readonly double resultNoiseEnd;

		public ConvertRangeDistorter(
			INoiseDistorter4D distorter,
			double sourceNoiseStart,
			double sourceNoiseEnd,
			double resultNoiseStart,
			double resultNoiseEnd)
		{
			Contracts.Requires.That(distorter != null);
			Contracts.Requires.That(sourceNoiseStart != sourceNoiseEnd);
			Contracts.Requires.That(resultNoiseStart != resultNoiseEnd);

			this.distorter = distorter;
			this.sourceNoiseStart = sourceNoiseStart;
			this.sourceNoiseEnd = sourceNoiseEnd;
			this.resultNoiseStart = resultNoiseStart;
			this.resultNoiseEnd = resultNoiseEnd;
		}

		public ConvertRangeDistorter(INoiseDistorter4D distorter, double resultNoiseStart, double resultNoiseEnd)
			: this(distorter, -1, 1, resultNoiseStart, resultNoiseEnd)
		{
		}

		/// <inheritdoc />
		public double Noise(INoise4D sourceNoise, double x, double y, double z, double w)
		{
			INoiseDistorter4DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x, y, z, w).ConvertRange(
				this.sourceNoiseStart, this.sourceNoiseEnd, this.resultNoiseStart, this.resultNoiseEnd);
		}

		/// <inheritdoc />
		public double Noise(INoise3D sourceNoise, double x, double y, double z)
		{
			INoiseDistorter3DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x, y, z).ConvertRange(
				this.sourceNoiseStart, this.sourceNoiseEnd, this.resultNoiseStart, this.resultNoiseEnd);
		}

		/// <inheritdoc />
		public double Noise(INoise2D sourceNoise, double x, double y)
		{
			INoiseDistorter2DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x, y).ConvertRange(
				this.sourceNoiseStart, this.sourceNoiseEnd, this.resultNoiseStart, this.resultNoiseEnd);
		}

		/// <inheritdoc />
		public double Noise(INoise1D sourceNoise, double x)
		{
			INoiseDistorter1DContracts.Noise(sourceNoise);

			return this.distorter.Noise(sourceNoise, x).ConvertRange(
				this.sourceNoiseStart, this.sourceNoiseEnd, this.resultNoiseStart, this.resultNoiseEnd);
		}
	}
}
