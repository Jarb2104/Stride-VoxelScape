using Voxelscape.Common.Procedural.Core.Noise;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public static class SkyIslandMapGenerationConfigService
	{
		public static SkyIslandMapPopulatorConfig.Builder CreatePreconfigured(
			IStageBounds stageBounds, IRasterChunkConfig chunkConfig)
		{
			Contracts.Requires.That(stageBounds != null);
			Contracts.Requires.That(chunkConfig != null);

			double baselineHeightVariation = 50;
			double halfStageHeight = stageBounds.InChunks.Dimensions.Y * chunkConfig.SideLength / 2.0;
			double baselineMin = halfStageHeight - baselineHeightVariation;
			double baselineMax = halfStageHeight + baselineHeightVariation;
			double scale = 10000;

			return new SkyIslandMapPopulatorConfig.Builder()
			{
				SelectionGradientStartDistancePercent = .6,
				ShapeDistorter = NoiseDistorter.Combine(
					NoiseDistorter.New().Frequency(20 / scale, 20 / scale).ConvertRange(-1, 1),
					NoiseDistorter.New().Frequency(40 / scale, 40 / scale).ConvertRange(-.5, .5),
					NoiseDistorter.New().Frequency(80 / scale, 80 / scale).ConvertRange(-.3, .3)),
				BaselineHeightDistorter =
					NoiseDistorter.New().Frequency(10 / scale, 10 / scale).ConvertRange(baselineMin, baselineMax),
				MountainDistorter =
					NoiseDistorter.New().Frequency(10 / scale, 10 / scale).ConvertRange(-6, 5).Clamp(1, 4),
				TopHeightDistorter = NoiseDistorter.Combine(
					NoiseDistorter.New().Frequency(20 / scale, 20 / scale).ConvertRange(0, 10),
					NoiseDistorter.New().Frequency(40 / scale, 40 / scale).ConvertRange(0, 40),
					NoiseDistorter.New().Frequency(80 / scale, 80 / scale).ConvertRange(0, 20),
					NoiseDistorter.New().Frequency(160 / scale, 160 / scale).ConvertRange(0, 10)),
				TopHeightNearEdgeDistorter = NoiseDistorter.Combine(
					NoiseDistorter.New().Frequency(20 / scale, 20 / scale).ConvertRange(0, 7.5),
					NoiseDistorter.New().Frequency(40 / scale, 40 / scale).ConvertRange(0, 30),
					NoiseDistorter.New().Frequency(80 / scale, 80 / scale).ConvertRange(0, 15),
					NoiseDistorter.New().Frequency(160 / scale, 160 / scale).ConvertRange(0, 7.5)),
				BottomHeightDistorter =
					NoiseDistorter.New().Frequency(200 / scale, 200 / scale).ConvertRange(-200, -100),
				BottomHeightNearEdgeDistorter =
					NoiseDistorter.New().Frequency(800 / scale, 800 / scale).ConvertRange(-100, -50),
			};
		}
	}
}
