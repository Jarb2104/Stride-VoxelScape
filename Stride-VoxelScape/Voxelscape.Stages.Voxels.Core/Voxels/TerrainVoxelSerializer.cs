using Voxelscape.Stages.Voxels.Core.Terrain;
using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Stages.Voxels.Core.Voxels
{
	/// <summary>
	///
	/// </summary>
	public class TerrainVoxelSerializer : AbstractCompositeConstantSerializer<TerrainVoxel, TerrainMaterial, byte>
	{
		public TerrainVoxelSerializer(
			IConstantSerializerDeserializer<TerrainMaterial> materialSerialzier,
			IConstantSerializerDeserializer<byte> densitySerialzier)
			: base(materialSerialzier, densitySerialzier)
		{
		}

		public static IEndianProvider<TerrainVoxelSerializer> Get { get; } = EndianProvider.New(
			(material, density) => new TerrainVoxelSerializer(material, density),
			TerrainMaterialSerializer.Get,
			Serializer.Byte);

		/// <inheritdoc />
		protected override TerrainVoxel ComposeValue(TerrainMaterial material, byte density) =>
			new TerrainVoxel(material, density);

		/// <inheritdoc />
		protected override void DecomposeValue(TerrainVoxel value, out TerrainMaterial material, out byte density)
		{
			material = value.Material;
			density = value.Density;
		}
	}
}
