using System;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Procedural.Core.Mathematics;
using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapChunkPopulator : IChunkPopulator<ISkyIslandMapChunk>
	{
		private readonly SkyIslandMapPopulatorConfig mapsConfig;

		private readonly double halfStageLength;

		private readonly double radiusStart;

		private readonly INoise2D shapeNoise;

		private readonly INoise2D baselineNoise;

		private readonly INoise2D mountainNoise;

		private readonly INoise2D topNoise;

		private readonly INoise2D bottomNoise;

		public SkyIslandMapChunkPopulator(
			SkyIslandMapPopulatorConfig mapsConfig,
			IStageBounds stageBounds,
			IFactory<int, INoise2D> multiNoise)
		{
			Contracts.Requires.That(mapsConfig != null);
			Contracts.Requires.That(stageBounds != null);
			Contracts.Requires.That(multiNoise != null);

			this.mapsConfig = mapsConfig;

			// using the lesser of the 2 dimensions ensures the radial gradient doesn't clip early out of bounds
			var stageOverheadDimensions = stageBounds.InOverheadChunks.Dimensions;
			this.halfStageLength = Math.Min(stageOverheadDimensions.X, stageOverheadDimensions.Y) / 2.0;
			this.radiusStart = this.halfStageLength * this.mapsConfig.SelectionGradientStartDistancePercent;

			this.shapeNoise = multiNoise.Create(SkyIslandNoiseSubSeed.ShapeMap);
			this.baselineNoise = multiNoise.Create(SkyIslandNoiseSubSeed.BaselineMap);
			this.mountainNoise = multiNoise.Create(SkyIslandNoiseSubSeed.MountainMap);
			this.topNoise = multiNoise.Create(SkyIslandNoiseSubSeed.TopMap);
			this.bottomNoise = multiNoise.Create(SkyIslandNoiseSubSeed.BottomMap);
		}

		/// <inheritdoc />
		public void Populate(ISkyIslandMapChunk chunk)
		{
			IChunkPopulatorContracts.Populate(chunk);

			Index2D min = chunk.MapsStageView.LowerBounds;
			Index2D max = chunk.MapsStageView.UpperBounds;

			// iX and iY are in global stage coordinates
			for (int iY = min.Y; iY <= max.Y; iY++)
			{
				for (int iX = min.X; iX <= max.X; iX++)
				{
					// TODO Steven - I don't think centerX, centerY, radiusStart, radiusEnd are defined correctly.
					// What if the stage is rectangular or not 0, 0, 0 lower bound.
					// Also the radial gradient seems to be keeping anything from getting generated...

					// creates a map that cookie cuts islands out of the top and bottom maps by removing the
					// open space around the sides and forcing the maps to seam at the island edges
					double shape = this.mapsConfig.ShapeDistorter.Noise(this.shapeNoise, iX, iY); //// +
						////Gradient.Radial(
						////	x: iX,
						////	y: iY,
						////	centerX: this.halfStageLength,
						////	centerY: this.halfStageLength,
						////	radiusStart: this.radiusStart,
						////	radiusEnd: this.halfStageLength,
						////	valueStart: 0,
						////	valueEnd: -2);
					shape = shape.Clamp(-1, 1);

					// generates a baseline for island height that will factor into the top and bottom heights
					double baselineHeight =
						this.mapsConfig.BaselineHeightDistorter.Noise(this.baselineNoise, iX, iY);

					double mountain;
					double topHeight;
					double bottomHeight;

					if (shape > 0)
					{
						// only generate meaningful values within the shape of the island

						// decides where mountains will be
						mountain = this.mapsConfig.MountainDistorter.Noise(this.mountainNoise, iX, iY);

						// shapes the top of the islands
						topHeight = Interpolation.WeightedAverage(
							this.mapsConfig.TopHeightNearEdgeDistorter.Noise(this.topNoise, iX, iY),
							this.mapsConfig.TopHeightDistorter.Noise(this.topNoise, iX, iY),
							shape);
						topHeight = (topHeight * mountain * shape) + baselineHeight;

						// shapes the bottom of the islands
						bottomHeight = Interpolation.WeightedAverage(
							this.mapsConfig.BottomHeightNearEdgeDistorter.Noise(this.bottomNoise, iX, iY),
							this.mapsConfig.BottomHeightDistorter.Noise(this.bottomNoise, iX, iY),
							shape);
						bottomHeight = (bottomHeight * shape) + baselineHeight;
					}
					else
					{
						// don't bother generating values outside the shape of the island
						mountain = 0;
						topHeight = 0;
						bottomHeight = 0;
					}

					chunk.MapsStageView[new Index2D(iX, iY)] = new SkyIslandMaps(
						shapePercentWeight: shape,
						baselineHeight: baselineHeight,
						mountainHeightMultiplier: mountain,
						topHeight: topHeight,
						bottomHeight: bottomHeight);
				}
			}
		}
	}
}
