using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Stages.Voxels.Core.Terrain
{
	/// <summary>
	///
	/// </summary>
	public class TerrainMaterialSerializer : AbstractCompositeConstantSerializer<TerrainMaterial, ushort>
	{
		public TerrainMaterialSerializer(IConstantSerializerDeserializer<ushort> serialzier)
			: base(serialzier)
		{
		}

		public static IEndianProvider<TerrainMaterialSerializer> Get { get; } =
			EndianProvider.New(serialzier => new TerrainMaterialSerializer(serialzier));

		/// <inheritdoc />
		protected override TerrainMaterial ComposeValue(ushort part)
		{
			var result = (TerrainMaterial)part;
			TerrainMaterialValidator.Instance.ThrowIfInvalidEnumValue(result);
			return result;
		}

		/// <inheritdoc />
		protected override ushort DecomposeValue(TerrainMaterial value) => (ushort)value;
	}
}
