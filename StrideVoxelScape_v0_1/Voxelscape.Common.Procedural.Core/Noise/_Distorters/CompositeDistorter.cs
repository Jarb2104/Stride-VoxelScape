using System.Collections.Generic;
using System.Linq;
using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Procedural.Core.Noise
{
	/// <summary>
	///
	/// </summary>
	public class CompositeDistorter : INoiseDistorter4D
	{
		private readonly INoiseDistorter4D[] distorters;

		public CompositeDistorter(params INoiseDistorter4D[] distorters)
		{
			Contracts.Requires.That(distorters.AllAndSelfNotNull());

			this.distorters = distorters;
		}

		public CompositeDistorter(IEnumerable<INoiseDistorter4D> distorters)
			: this(distorters?.ToArray())
		{
		}

		/// <inheritdoc />
		public double Noise(INoise4D sourceNoise, double x, double y, double z, double w)
		{
			INoiseDistorter4DContracts.Noise(sourceNoise);

			double result = 0;
			foreach (var distorter in this.distorters)
			{
				result += distorter.Noise(sourceNoise, x, y, z, w);
			}

			return result;
		}

		/// <inheritdoc />
		public double Noise(INoise3D sourceNoise, double x, double y, double z)
		{
			INoiseDistorter3DContracts.Noise(sourceNoise);

			double result = 0;
			foreach (var distorter in this.distorters)
			{
				result += distorter.Noise(sourceNoise, x, y, z);
			}

			return result;
		}

		/// <inheritdoc />
		public double Noise(INoise2D sourceNoise, double x, double y)
		{
			INoiseDistorter2DContracts.Noise(sourceNoise);

			double result = 0;
			foreach (var distorter in this.distorters)
			{
				result += distorter.Noise(sourceNoise, x, y);
			}

			return result;
		}

		/// <inheritdoc />
		public double Noise(INoise1D sourceNoise, double x)
		{
			INoiseDistorter1DContracts.Noise(sourceNoise);

			double result = 0;
			foreach (var distorter in this.distorters)
			{
				result += distorter.Noise(sourceNoise, x);
			}

			return result;
		}
	}
}
