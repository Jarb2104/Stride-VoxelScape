using System;
using System.Threading.Tasks;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Core.Voxels;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandVoxelGridChunkPopulator : IAsyncChunkPopulator<IVoxelGridChunk>
	{
		private readonly SkyIslandVoxelPopulatorConfig config;

		private readonly IAsyncFactory<ChunkOverheadKey, IDisposableValue<IReadOnlySkyIslandMapChunk>> chunkFactory;

		private readonly INoise3D environmentNoise;

		private readonly INoise3D caveNoise1;

		private readonly INoise3D caveNoise2;

		public SkyIslandVoxelGridChunkPopulator(
			SkyIslandVoxelPopulatorConfig config,
			IAsyncFactory<ChunkOverheadKey, IDisposableValue<IReadOnlySkyIslandMapChunk>> chunkFactory,
			IFactory<int, INoise3D> multiNoise)
		{
			Contracts.Requires.That(config != null);
			Contracts.Requires.That(chunkFactory != null);
			Contracts.Requires.That(multiNoise != null);

			this.config = config;
			this.chunkFactory = chunkFactory;
			this.environmentNoise = multiNoise.Create(SkyIslandNoiseSubSeed.Environment);
			this.caveNoise1 = multiNoise.Create(SkyIslandNoiseSubSeed.Cave1);
			this.caveNoise2 = multiNoise.Create(SkyIslandNoiseSubSeed.Cave2);
		}

		/// <inheritdoc />
		public async Task PopulateAsync(IVoxelGridChunk chunk)
		{
			IAsyncChunkPopulatorContracts.PopulateAsync(chunk);

			using (var mapPin = await this.chunkFactory.CreateAsync(chunk.Key.ToOverheadKey()).DontMarshallContext())
			{
				var min = chunk.VoxelsStageView.LowerBounds;
				var max = chunk.VoxelsStageView.UpperBounds;
				this.GenerateValues(mapPin.Value, min, max, chunk);
			}
		}

		#region Private Helper Methods

		private void GenerateValues(
			IReadOnlySkyIslandMapChunk mapChunk, Index3D min, Index3D max, IVoxelGridChunk chunk)
		{
			Contracts.Requires.That(mapChunk != null);
			Contracts.Requires.That(chunk != null);

			for (int iX = min.X; iX <= max.X; iX++)
			{
				for (int iZ = min.Z; iZ <= max.Z; iZ++)
				{
					SkyIslandMaps map = mapChunk.MapsStageView[new Index2D(iX, iZ)];

					if (map.ShapePercentWeight > 0)
					{
						// column is inside the floating island
						for (int iY = min.Y; iY <= max.Y; iY++)
						{
							chunk.VoxelsStageView[new Index3D(iX, iY, iZ)] =
								this.GetVoxelFromColumnInsideIsland(iX, iY, iZ, map.TopHeight, map.BottomHeight);
						}
					}
					else
					{
						// column is outside of the floating island
						double edgeSeamDropoffScale =
							this.config.EdgeSeamDropoffScaleDistorter.Noise(this.environmentNoise, iX, iZ);
						double weightedDistanceFromIsland =
							((map.ShapePercentWeight * edgeSeamDropoffScale) + 1).ClampLower(0);

						if (weightedDistanceFromIsland > 0)
						{
							// column is close to edge of island so perform island edge seaming
							double weightedAboveTopGradientThickness =
								this.config.AboveTopGradientThickness * weightedDistanceFromIsland;
							double weightedBelowBottomGradientThickness =
								this.config.BelowBottomGradientThickness * weightedDistanceFromIsland;

							for (int iY = min.Y; iY <= max.Y; iY++)
							{
								var voxel = this.GetVoxelFromColumnOutsideIsland(
									iX,
									iY,
									iZ,
									weightedAboveTopGradientThickness,
									weightedBelowBottomGradientThickness,
									map.BaselineHeight,
									map.TopHeight);

								chunk.VoxelsStageView[new Index3D(iX, iY, iZ)] = voxel;
							}
						}
						else
						{
							// column is far enough away from edge of island to be just empty air
							for (int iY = min.Y; iY <= max.Y; iY++)
							{
								chunk.VoxelsStageView[new Index3D(iX, iY, iZ)] = this.config.EmptyAir;
							}
						}
					}
				}
			}
		}

		private TerrainVoxel GetVoxelFromColumnInsideIsland(
			int x, int y, int z, float mapTopHeight, float mapBottomHeight)
		{
			if (y > mapTopHeight)
			{
				// above the top map
				double distance = y - mapTopHeight;
				if (distance < this.config.AboveTopGradientThickness + this.config.GradiantBuffer)
				{
					return this.GenerateVoxel(
						x, y, z, -distance / this.config.AboveTopGradientThickness, mapTopHeight);
				}
				else
				{
					return this.config.EmptyAir;
				}
			}
			else if (y < mapBottomHeight)
			{
				// below the bottom map
				double distance = mapBottomHeight - y;
				if (distance < this.config.BelowBottomGradientThickness + this.config.GradiantBuffer)
				{
					return this.GenerateVoxel(
						x, y, z, -distance / this.config.BelowBottomGradientThickness, mapTopHeight);
				}
				else
				{
					return this.config.EmptyAir;
				}
			}
			else
			{
				// inbetween the top and bottom map
				double densityFromTop = (mapTopHeight - y) / this.config.TopGradientThickness;
				double densityFromBottom = (y - mapBottomHeight) / this.config.BottomGradientThickness;

				return this.GenerateVoxel(
					x, y, z, Math.Min(densityFromTop, densityFromBottom).ClampLower(0), mapTopHeight);
			}
		}

		private TerrainVoxel GetVoxelFromColumnOutsideIsland(
			int x,
			int y,
			int z,
			double weightedAboveTopGradientThickness,
			double weightedBelowBottomGradientThickness,
			float mapBaselineHeight,
			float mapTopHeight)
		{
			if (y > mapBaselineHeight)
			{
				// above the baseline height
				double distance = y - mapBaselineHeight;
				if (distance < weightedAboveTopGradientThickness + this.config.GradiantBuffer)
				{
					return this.GenerateVoxel(x, y, z, -distance / weightedAboveTopGradientThickness, mapTopHeight);
				}
				else
				{
					return this.config.EmptyAir;
				}
			}
			else
			{
				// below the baseline height
				double distance = mapBaselineHeight - y;
				if (distance < weightedBelowBottomGradientThickness + this.config.GradiantBuffer)
				{
					return this.GenerateVoxel(x, y, z, -distance / weightedBelowBottomGradientThickness, mapTopHeight);
				}
				else
				{
					return this.config.EmptyAir;
				}
			}
		}

		private TerrainVoxel GenerateVoxel(int x, int y, int z, double gradientDensity, float mapTopHeight)
		{
			double voxelDensity = this.GenerateDensity(x, y, z, gradientDensity, mapTopHeight);
			TerrainMaterial material = TerrainMaterial.Air;

			if (voxelDensity > 0)
			{
				if (y >= mapTopHeight)
				{
					material = TerrainMaterial.Grass;
				}
				else if (y >= mapTopHeight - 8)
				{
					material = TerrainMaterial.Dirt;
				}
				else
				{
					material = TerrainMaterial.Stone;
				}
			}

			return TerrainVoxelFromNoise.New(material, voxelDensity);
		}

		private double GenerateDensity(int x, int y, int z, double gradientDensity, float mapTopHeight)
		{
			double environmentDensity = gradientDensity +
				this.config.EnvironmentShapeDistorter.Noise(this.environmentNoise, x, y, z);
			double caveDensity = Math.Max(
				this.GenerateCaveDensity(this.caveNoise1, x, y, z, mapTopHeight),
				this.GenerateCaveDensity(this.caveNoise2, x, y, z, mapTopHeight));

			return Math.Min(environmentDensity, caveDensity);
		}

		private double GenerateCaveDensity(INoise3D noise, int x, int y, int z, float mapTopHeight)
		{
			Contracts.Requires.That(noise != null);

			double distanceFromTop = Math.Abs(mapTopHeight - y);
			double gradientWeight = (1 - (distanceFromTop / this.config.CaveEntranceGradientThickness)).Clamp(0, 1);
			double noiseDensity = this.config.CaveShapeDistorter.Noise(noise, x, y, z);

			// the original range of density is -1 to 1
			// the cave multiplier narrows that range to only a thin band of values centered around 0
			// that band is then converted from -.x to x => 1 to -1 to 1
			// anything outside that band becomes greater then 1
			// any positive value generated by this function does not generate cave
			return Math.Abs(noiseDensity * this.config.CaveTunnelMultiplier) - 1 + gradientWeight;
		}

		#endregion
	}
}
