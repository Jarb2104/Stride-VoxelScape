using System.Collections.Generic;
using Voxelscape.Common.Procedural.Pact.Noise;

namespace Voxelscape.Common.Procedural.Core.Noise
{
	/// <summary>
	///
	/// </summary>
	public static class NoiseDistorter
	{
		public static INoiseDistorter4D New() => new PureDistorter();

		public static INoiseDistorter4D Combine(params INoiseDistorter4D[] distorters) =>
			new CompositeDistorter(distorters);

		public static INoiseDistorter4D Combine(IEnumerable<INoiseDistorter4D> distorters) =>
			new CompositeDistorter(distorters);
	}
}
