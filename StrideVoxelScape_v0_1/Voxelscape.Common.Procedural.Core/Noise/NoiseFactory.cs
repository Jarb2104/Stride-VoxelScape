using System;
using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Utility.Common.Core.Factories;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Common.Pact.Mathematics;

namespace Voxelscape.Common.Procedural.Core.Noise
{
	/// <summary>
	///
	/// </summary>
	public class NoiseFactory :
		IFactory<int, INoise1D>, IFactory<int, INoise2D>, IFactory<int, INoise3D>, IFactory<int, INoise4D>
	{
		private static readonly IFactory<int, INoise4D> DefaultNoiseFactory =
			Factory.From<int, INoise4D>(seed => new SimplexNoise(seed));

		private readonly int seed;

		private readonly IFactory<int, INoise4D> noiseFactory;

		public NoiseFactory()
			: this(DefaultNoiseFactory)
		{
		}

		public NoiseFactory(int seed)
			: this(seed, DefaultNoiseFactory)
		{
		}

		public NoiseFactory(IFactory<int, INoise4D> noiseFactory)
			: this(ThreadLocalRandom.Instance, noiseFactory)
		{
		}

		public NoiseFactory(int seed, IFactory<int, INoise4D> noiseFactory)
			: this(new Random(seed), noiseFactory)
		{
		}

		private NoiseFactory(Random random, IFactory<int, INoise4D> noiseFactory)
		{
			Contracts.Requires.That(random != null);
			Contracts.Requires.That(noiseFactory != null);

			this.seed = random.Next(int.MinValue, int.MaxValue);
			this.noiseFactory = noiseFactory;
		}

		/// <inheritdoc />
		INoise1D IFactory<int, INoise1D>.Create(int subseed) => this.Create(subseed);

		/// <inheritdoc />
		INoise2D IFactory<int, INoise2D>.Create(int subseed) => this.Create(subseed);

		/// <inheritdoc />
		INoise3D IFactory<int, INoise3D>.Create(int subseed) => this.Create(subseed);

		/// <inheritdoc />
		public INoise4D Create(int subseed) =>
			this.noiseFactory.Create(unchecked(new Random(subseed).Next(int.MinValue, int.MaxValue) + this.seed));
	}
}
