using System.Linq;
using Voxelscape.Common.Procedural.Core.Noise;
using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Core.Voxels;
using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Templates.NoiseWorld
{
	/// <summary>
	///
	/// </summary>
	public class NoiseWorldVoxelGridChunkPopulator : IChunkPopulator<IVoxelGridChunk>
	{
		private readonly INoise3D noise;

		private readonly INoiseDistorter3D distorter;

		private readonly TerrainMaterial material;

		public NoiseWorldVoxelGridChunkPopulator(
			INoise3D noise, double noiseScaling, int numberOfOctaves, TerrainMaterial material)
		{
			Contracts.Requires.That(noise != null);
			Contracts.Requires.That(noiseScaling > 0);
			Contracts.Requires.That(numberOfOctaves >= 1);

			this.noise = noise;
			this.distorter = NoiseDistorter.New()
				.Frequency(noiseScaling, noiseScaling, noiseScaling).Octaves(numberOfOctaves);
			this.material = material;
		}

		/// <inheritdoc />
		public void Populate(IVoxelGridChunk chunk)
		{
			IChunkPopulatorContracts.Populate(chunk);

			foreach (var index in chunk.VoxelsStageView.Select(pair => pair.Key))
			{
				double density = this.distorter.Noise(this.noise, index.X, index.Y, index.Z);
				chunk.VoxelsStageView[index] = TerrainVoxelFromNoise.New(this.material, density);
			}
		}
	}
}
