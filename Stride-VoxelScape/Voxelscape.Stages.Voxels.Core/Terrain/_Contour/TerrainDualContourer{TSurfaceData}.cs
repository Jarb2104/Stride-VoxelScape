using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Core.DualContouring;
using Voxelscape.Common.Contouring.Core.Textures;
using Voxelscape.Common.Contouring.Core.Vertices;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Procedural.Core.Noise;
using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Stages.Voxels.Core.Terrain
{
	public class TerrainDualContourer<TSurfaceData> :
		IDualContourer<TerrainVoxel, TSurfaceData, NormalColorTextureVertex>
		where TSurfaceData : class, ITerrainSurfaceData
	{
		private readonly IDualContourer<TerrainVoxel, TSurfaceData, NormalColorTextureVertex> dualContourer;

		public TerrainDualContourer(IFactory<int, INoise3D> noiseFactory)
		{
			Contracts.Requires.That(noiseFactory != null);

			var materialPropertiesTable = CreateMaterialPropertiesTable(noiseFactory);
			var materialTexturer = new TerrainMaterialSurfaceGenerator<TSurfaceData>(materialPropertiesTable);

			this.dualContourer = new FlatQuadContourer<TerrainVoxel, TSurfaceData>(
				new TerrainContourDeterminer<TSurfaceData>(materialPropertiesTable),
				new VertexSurfaceWriter<TerrainVoxel, TSurfaceData>(new AverageDensitySurfaceGenerator<TSurfaceData>()),
				materialTexturer,
				materialTexturer);
		}

		/// <inheritdoc />
		public void Contour(
			IDualContourable<TerrainVoxel, TSurfaceData> contourable,
			IMutableDivisibleMesh<NormalColorTextureVertex> meshBuilder)
		{
			IDualContourerContracts.Contour(contourable, meshBuilder);

			this.dualContourer.Contour(contourable, meshBuilder);
		}

		[SuppressMessage("StyleCop", "SA1118:Parameter must not span multiple lines", Justification = "Tons of configuration.")]
		private static IReadOnlyDictionary<TerrainMaterial, ITerrainMaterialProperties<TSurfaceData>>
			CreateMaterialPropertiesTable(IFactory<int, INoise3D> noiseFactory)
		{
			Contracts.Requires.That(noiseFactory != null);

			var textureAtlasBuilder = new EnumKeyTextureAtlasBuilder<TerrainTexture>(
				atlasPixelDimensions: new Index2D(128, 128),
				swatchPixelDimensions: new Index2D(16, 16),
				invertXAxis: true,
				invertYAxis: false);

			textureAtlasBuilder.AddSwatch(TerrainTexture.None, new Index2D(0, 0));
			textureAtlasBuilder.AddSwatch(TerrainTexture.GrassFull, new Index2D(0, 1));
			textureAtlasBuilder.AddSwatch(TerrainTexture.GrassTop, new Index2D(1, 1));
			textureAtlasBuilder.AddSwatch(TerrainTexture.GrassBottom, new Index2D(2, 1));
			textureAtlasBuilder.AddSwatch(TerrainTexture.Dirt, new Index2D(0, 2));
			textureAtlasBuilder.AddSwatch(TerrainTexture.Stone, new Index2D(0, 3));

			var textureAtlas = textureAtlasBuilder.Build();
			var materialPropertiesTable =
				EnumDictionary.Create<TerrainMaterial, ITerrainMaterialProperties<TSurfaceData>>();

			float areaThreshold = .8f;
			var noiseDistorter = NoiseDistorter.New().Frequency(100, 100, 100);
			var noTexture = new TextureSwatchSurfaceGenerator<TerrainVoxel, TSurfaceData>(
				textureAtlas[TerrainTexture.None]);

			materialPropertiesTable[TerrainMaterial.Air] = new TerrainMaterialProperties<TSurfaceData>(
				false,
				new ConstantSurfaceGenerator<TerrainVoxel, TSurfaceData, Color>(Color.Pink),
				noTexture);

			materialPropertiesTable[TerrainMaterial.Dirt] = new TerrainMaterialProperties<TSurfaceData>(
				true,
				new ConstantSurfaceGenerator<TerrainVoxel, TSurfaceData, Color>(new Color(120, 96, 56)),
				new AreaThresholdSurfaceGenerator<TerrainVoxel, TSurfaceData, Vector2>(
					new NoiseSeedSurfaceGenerator<TerrainVoxel, TSurfaceData, Vector2>(
						new SeedSwitchSurfaceGenerator<TerrainVoxel, TSurfaceData, Vector2>(
							noTexture,
							new SeedRangeSurfaceGenerator<TerrainVoxel, TSurfaceData, Vector2>(
								.85f, 1f, new TextureSwatchSurfaceGenerator<TerrainVoxel, TSurfaceData>(
									textureAtlas[TerrainTexture.Dirt]))),
						noiseFactory,
						noiseDistorter),
					noTexture,
					areaThreshold));

			materialPropertiesTable[TerrainMaterial.Grass] = new TerrainMaterialProperties<TSurfaceData>(
				true,
				new ConstantSurfaceGenerator<TerrainVoxel, TSurfaceData, Color>(new Color(72, 160, 72)),
				new AreaThresholdSurfaceGenerator<TerrainVoxel, TSurfaceData, Vector2>(
					new NoiseSeedSurfaceGenerator<TerrainVoxel, TSurfaceData, Vector2>(
						new SeedSwitchSurfaceGenerator<TerrainVoxel, TSurfaceData, Vector2>(
							noTexture,
							new SeedRangeSurfaceGenerator<TerrainVoxel, TSurfaceData, Vector2>(
								.85f, 1f, new TextureSwatchSurfaceGenerator<TerrainVoxel, TSurfaceData>(
									textureAtlas[TerrainTexture.GrassFull]))),
						noiseFactory,
						noiseDistorter),
					noTexture,
					areaThreshold));

			materialPropertiesTable[TerrainMaterial.Stone] = new TerrainMaterialProperties<TSurfaceData>(
				true,
				new ConstantSurfaceGenerator<TerrainVoxel, TSurfaceData, Color>(Color.Gray),
				new AreaThresholdSurfaceGenerator<TerrainVoxel, TSurfaceData, Vector2>(
					new NoiseSeedSurfaceGenerator<TerrainVoxel, TSurfaceData, Vector2>(
						new SeedSwitchSurfaceGenerator<TerrainVoxel, TSurfaceData, Vector2>(
							noTexture,
							new SeedRangeSurfaceGenerator<TerrainVoxel, TSurfaceData, Vector2>(
								.85f, 1f, new TextureSwatchSurfaceGenerator<TerrainVoxel, TSurfaceData>(
									textureAtlas[TerrainTexture.Stone]))),
						noiseFactory,
						noiseDistorter),
					noTexture,
					areaThreshold));

			////materialPropertiesTable[TerrainMaterial.Coal] = new TerrainMaterialProperties<TSurfaceData>(
			////	true,
			////	new ConstantSurfaceGenerator<TerrainVoxel, TSurfaceData, Color>(Color.Black),
			////	noTexture);

			////materialPropertiesTable[TerrainMaterial.Iron] = new TerrainMaterialProperties<TSurfaceData>(
			////	true,
			////	new ConstantSurfaceGenerator<TerrainVoxel, TSurfaceData, Color>(Color.DarkGray),
			////	noTexture);

			////materialPropertiesTable[TerrainMaterial.Gold] = new TerrainMaterialProperties<TSurfaceData>(
			////	true,
			////	new ConstantSurfaceGenerator<TerrainVoxel, TSurfaceData, Color>(Color.Gold),
			////	noTexture);

			////materialPropertiesTable[TerrainMaterial.Diamond] = new TerrainMaterialProperties<TSurfaceData>(
			////	true,
			////	new ConstantSurfaceGenerator<TerrainVoxel, TSurfaceData, Color>(Color.BlueViolet),
			////	noTexture);

			EnumDictionary.ThrowIfMissingEnumKey(materialPropertiesTable);
			return materialPropertiesTable;
		}
	}
}
