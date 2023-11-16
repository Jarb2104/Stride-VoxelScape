using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapPopulatorConfig
	{
		private SkyIslandMapPopulatorConfig()
		{
		}

		public double SelectionGradientStartDistancePercent { get; private set; }

		public INoiseDistorter2D ShapeDistorter { get; private set; }

		public INoiseDistorter2D BaselineHeightDistorter { get; private set; }

		public INoiseDistorter2D MountainDistorter { get; private set; }

		public INoiseDistorter2D TopHeightDistorter { get; private set; }

		public INoiseDistorter2D TopHeightNearEdgeDistorter { get; private set; }

		public INoiseDistorter2D BottomHeightDistorter { get; private set; }

		public INoiseDistorter2D BottomHeightNearEdgeDistorter { get; private set; }

		public class Builder
		{
			public double? SelectionGradientStartDistancePercent { get; set; }

			public INoiseDistorter2D ShapeDistorter { get; set; }

			public INoiseDistorter2D BaselineHeightDistorter { get; set; }

			public INoiseDistorter2D MountainDistorter { get; set; }

			public INoiseDistorter2D TopHeightDistorter { get; set; }

			public INoiseDistorter2D TopHeightNearEdgeDistorter { get; set; }

			public INoiseDistorter2D BottomHeightDistorter { get; set; }

			public INoiseDistorter2D BottomHeightNearEdgeDistorter { get; set; }

			public SkyIslandMapPopulatorConfig Build()
			{
				Contracts.Requires.That(this.SelectionGradientStartDistancePercent.HasValue);
				Contracts.Requires.That(this.SelectionGradientStartDistancePercent.Value.IsIn(Range.New(0.0, 1.0)));
				Contracts.Requires.That(this.ShapeDistorter != null);
				Contracts.Requires.That(this.BaselineHeightDistorter != null);
				Contracts.Requires.That(this.MountainDistorter != null);
				Contracts.Requires.That(this.TopHeightDistorter != null);
				Contracts.Requires.That(this.TopHeightNearEdgeDistorter != null);
				Contracts.Requires.That(this.BottomHeightDistorter != null);
				Contracts.Requires.That(this.BottomHeightNearEdgeDistorter != null);

				return new SkyIslandMapPopulatorConfig()
				{
					SelectionGradientStartDistancePercent = this.SelectionGradientStartDistancePercent.Value,
					ShapeDistorter = this.ShapeDistorter,
					BaselineHeightDistorter = this.BaselineHeightDistorter,
					MountainDistorter = this.MountainDistorter,
					TopHeightDistorter = this.TopHeightDistorter,
					TopHeightNearEdgeDistorter = this.TopHeightNearEdgeDistorter,
					BottomHeightDistorter = this.BottomHeightDistorter,
					BottomHeightNearEdgeDistorter = this.BottomHeightNearEdgeDistorter,
				};
			}
		}
	}
}
