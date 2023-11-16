using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	public class SkyIslandVoxelPopulatorConfig
	{
		private SkyIslandVoxelPopulatorConfig()
		{
		}

		public TerrainVoxel EmptyAir { get; private set; }

		public double TopGradientThickness { get; private set; }

		public double AboveTopGradientThickness { get; private set; }

		public double BottomGradientThickness { get; private set; }

		public double BelowBottomGradientThickness { get; private set; }

		public double GradiantBuffer { get; private set; }

		public double CaveTunnelRadius { get; private set; }

		public double CaveTunnelMultiplier { get; private set; }

		public double CaveEntranceGradientThickness { get; private set; }

		public INoiseDistorter3D CaveShapeDistorter { get; private set; }

		public INoiseDistorter3D EnvironmentShapeDistorter { get; private set; }

		public INoiseDistorter2D EdgeSeamDropoffScaleDistorter { get; private set; }

		public class Builder
		{
			public TerrainVoxel? EmptyAir { get; set; }

			public double? TopGradientThickness { get; set; }

			public double? AboveTopGradientThickness { get; set; }

			public double? BottomGradientThickness { get; set; }

			public double? BelowBottomGradientThickness { get; set; }

			public double? GradiantBuffer { get; set; }

			public double? CaveTunnelRadius { get; set; }

			public double? CaveEntranceGradientThickness { get; set; }

			public INoiseDistorter3D CaveShapeDistorter { get; set; }

			public INoiseDistorter3D EnvironmentShapeDistorter { get; set; }

			public INoiseDistorter2D EdgeSeamDropoffScaleDistorter { get; set; }

			public SkyIslandVoxelPopulatorConfig Build()
			{
				Contracts.Requires.That(this.EmptyAir.HasValue);
				Contracts.Requires.That(this.TopGradientThickness.HasValue);
				Contracts.Requires.That(this.AboveTopGradientThickness.HasValue);
				Contracts.Requires.That(this.BottomGradientThickness.HasValue);
				Contracts.Requires.That(this.BelowBottomGradientThickness.HasValue);
				Contracts.Requires.That(this.GradiantBuffer.HasValue);
				Contracts.Requires.That(this.CaveTunnelRadius.HasValue);
				Contracts.Requires.That(this.CaveEntranceGradientThickness.HasValue);
				Contracts.Requires.That(this.CaveShapeDistorter != null);
				Contracts.Requires.That(this.EnvironmentShapeDistorter != null);
				Contracts.Requires.That(this.EdgeSeamDropoffScaleDistorter != null);

				return new SkyIslandVoxelPopulatorConfig()
				{
					EmptyAir = this.EmptyAir.Value,
					TopGradientThickness = this.TopGradientThickness.Value,
					AboveTopGradientThickness = this.AboveTopGradientThickness.Value,
					BottomGradientThickness = this.BottomGradientThickness.Value,
					BelowBottomGradientThickness = this.BelowBottomGradientThickness.Value,
					GradiantBuffer = this.GradiantBuffer.Value,
					CaveTunnelRadius = this.CaveTunnelRadius.Value,
					CaveTunnelMultiplier = 2.0 / this.CaveTunnelRadius.Value,
					CaveEntranceGradientThickness = this.CaveEntranceGradientThickness.Value,
					CaveShapeDistorter = this.CaveShapeDistorter,
					EnvironmentShapeDistorter = this.EnvironmentShapeDistorter,
					EdgeSeamDropoffScaleDistorter = this.EdgeSeamDropoffScaleDistorter,
				};
			}
		}
	}
}
