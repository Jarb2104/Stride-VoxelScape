using Voxelscape.Utility.Data.Core.Stores;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public static class SkyIslandMapStat
	{
		public static IValueKey<float> ShapePercentWeightMin { get; } =
			new FloatValueKey($"{nameof(SkyIslandMapStat)}.{nameof(ShapePercentWeightMin)}");

		public static IValueKey<float> ShapePercentWeightMax { get; } =
			new FloatValueKey($"{nameof(SkyIslandMapStat)}.{nameof(ShapePercentWeightMax)}");

		public static IValueKey<float> BaselineHeightMin { get; } =
			new FloatValueKey($"{nameof(SkyIslandMapStat)}.{nameof(BaselineHeightMin)}");

		public static IValueKey<float> BaselineHeightMax { get; } =
			new FloatValueKey($"{nameof(SkyIslandMapStat)}.{nameof(BaselineHeightMax)}");

		public static IValueKey<float> MountainHeightMultiplierMin { get; } =
			new FloatValueKey($"{nameof(SkyIslandMapStat)}.{nameof(MountainHeightMultiplierMin)}");

		public static IValueKey<float> MountainHeightMultiplierMax { get; } =
			new FloatValueKey($"{nameof(SkyIslandMapStat)}.{nameof(MountainHeightMultiplierMax)}");

		public static IValueKey<float> TopHeightMin { get; } =
			new FloatValueKey($"{nameof(SkyIslandMapStat)}.{nameof(TopHeightMin)}");

		public static IValueKey<float> TopHeightMax { get; } =
			new FloatValueKey($"{nameof(SkyIslandMapStat)}.{nameof(TopHeightMax)}");

		public static IValueKey<float> BottomHeightMin { get; } =
			new FloatValueKey($"{nameof(SkyIslandMapStat)}.{nameof(BottomHeightMin)}");

		public static IValueKey<float> BottomHeightMax { get; } =
			new FloatValueKey($"{nameof(SkyIslandMapStat)}.{nameof(BottomHeightMax)}");
	}
}
