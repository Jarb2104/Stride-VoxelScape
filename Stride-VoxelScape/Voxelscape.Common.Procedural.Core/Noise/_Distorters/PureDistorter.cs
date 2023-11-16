using Voxelscape.Common.Procedural.Pact.Noise;

namespace Voxelscape.Common.Procedural.Core.Noise
{
	/// <summary>
	///
	/// </summary>
	public class PureDistorter : INoiseDistorter4D
	{
		/// <inheritdoc />
		public double Noise(INoise4D sourceNoise, double x, double y, double z, double w)
		{
			INoiseDistorter4DContracts.Noise(sourceNoise);
			return sourceNoise.Noise(x, y, z, w);
		}

		/// <inheritdoc />
		public double Noise(INoise3D sourceNoise, double x, double y, double z)
		{
			INoiseDistorter3DContracts.Noise(sourceNoise);

			return sourceNoise.Noise(x, y, z);
		}

		/// <inheritdoc />
		public double Noise(INoise2D sourceNoise, double x, double y)
		{
			INoiseDistorter2DContracts.Noise(sourceNoise);

			return sourceNoise.Noise(x, y);
		}

		/// <inheritdoc />
		public double Noise(INoise1D sourceNoise, double x)
		{
			INoiseDistorter1DContracts.Noise(sourceNoise);

			return sourceNoise.Noise(x);
		}
	}
}
