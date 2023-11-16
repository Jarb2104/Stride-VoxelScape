using System;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapStatsAggregator
	{
		private float shapePercentWeightMin = float.MaxValue;
		private float shapePercentWeightMax = float.MinValue;

		private float baselineHeightMin = float.MaxValue;
		private float baselineHeightMax = float.MinValue;

		private float mountainHeightMultiplierMin = float.MaxValue;
		private float mountainHeightMultiplierMax = float.MinValue;

		private float topHeightMin = float.MaxValue;
		private float topHeightMax = float.MinValue;

		private float bottomHeightMin = float.MaxValue;
		private float bottomHeightMax = float.MinValue;

		public SkyIslandMaps Min => new SkyIslandMaps(
			shapePercentWeight: this.shapePercentWeightMin,
			baselineHeight: this.baselineHeightMin,
			mountainHeightMultiplier: this.mountainHeightMultiplierMin,
			topHeight: this.topHeightMin,
			bottomHeight: this.bottomHeightMin);

		public SkyIslandMaps Max => new SkyIslandMaps(
			shapePercentWeight: this.shapePercentWeightMax,
			baselineHeight: this.baselineHeightMax,
			mountainHeightMultiplier: this.mountainHeightMultiplierMax,
			topHeight: this.topHeightMax,
			bottomHeight: this.bottomHeightMax);

		public void Update(SkyIslandMaps value)
		{
			this.shapePercentWeightMin =
				Math.Min(value.ShapePercentWeight, this.shapePercentWeightMin);
			this.baselineHeightMin =
				Math.Min(value.BaselineHeight, this.baselineHeightMin);
			this.mountainHeightMultiplierMin =
				Math.Min(value.MountainHeightMultiplier, this.mountainHeightMultiplierMin);
			this.topHeightMin =
				Math.Min(value.TopHeight, this.topHeightMin);
			this.bottomHeightMin =
				Math.Min(value.BottomHeight, this.bottomHeightMin);

			this.shapePercentWeightMax =
				Math.Max(value.ShapePercentWeight, this.shapePercentWeightMax);
			this.baselineHeightMax =
				Math.Max(value.BaselineHeight, this.baselineHeightMax);
			this.mountainHeightMultiplierMax =
				Math.Max(value.MountainHeightMultiplier, this.mountainHeightMultiplierMax);
			this.topHeightMax =
				Math.Max(value.TopHeight, this.topHeightMax);
			this.bottomHeightMax =
				Math.Max(value.BottomHeight, this.bottomHeightMax);
		}
	}
}
