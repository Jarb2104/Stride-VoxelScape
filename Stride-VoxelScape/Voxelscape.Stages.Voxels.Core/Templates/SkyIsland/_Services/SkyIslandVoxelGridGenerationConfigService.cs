using Voxelscape.Common.Procedural.Core.Noise;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public static class SkyIslandVoxelGridGenerationConfigService
	{
		public static SkyIslandVoxelPopulatorConfig.Builder CreatePreconfigured()
		{
			double scale = 10000;

			var caveShape = NoiseDistorter.New().Frequency(50 / scale, 75 / scale, 50 / scale).Octaves(5);
			var environmentShape = NoiseDistorter.Combine(
				NoiseDistorter.New().Frequency(25 / scale, 100 / scale, 25 / scale).Octaves(5).Amplitude(.4),
				NoiseDistorter.New().Frequency(1000 / scale, 1000 / scale, 1000 / scale).Octaves(5).Amplitude(.01));
			var edgeSeam = NoiseDistorter.New().Frequency(800 / scale, 800 / scale).ConvertRange(20, 40);

			return new SkyIslandVoxelPopulatorConfig.Builder()
			{
				EmptyAir = SkyIslandGenerationConstants.EmptyAir,
				TopGradientThickness = 60,
				AboveTopGradientThickness = 30,
				BottomGradientThickness = 30,
				BelowBottomGradientThickness = 15,
				GradiantBuffer = 2,
				CaveTunnelRadius = .4,
				CaveEntranceGradientThickness = 10,
				CaveShapeDistorter = caveShape,
				EnvironmentShapeDistorter = environmentShape,
				EdgeSeamDropoffScaleDistorter = edgeSeam,
			};
		}
	}
}
