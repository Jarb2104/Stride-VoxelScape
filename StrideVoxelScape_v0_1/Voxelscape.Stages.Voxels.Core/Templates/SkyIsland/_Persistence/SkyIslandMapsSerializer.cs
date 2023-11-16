using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapsSerializer :
		AbstractCompositeConstantSerializer<SkyIslandMaps, float, float, float, float, float>
	{
		public SkyIslandMapsSerializer(IConstantSerializerDeserializer<float> serialzier)
			: base(serialzier, serialzier, serialzier, serialzier, serialzier)
		{
		}

		public SkyIslandMapsSerializer(
			IConstantSerializerDeserializer<float> shapeWeightSerialzier,
			IConstantSerializerDeserializer<float> baselineHeightSerialzier,
			IConstantSerializerDeserializer<float> mountainMultiplierSerialzier,
			IConstantSerializerDeserializer<float> topHeightSerialzier,
			IConstantSerializerDeserializer<float> bottomHeightSerialzier)
			: base(
				  shapeWeightSerialzier,
				  baselineHeightSerialzier,
				  mountainMultiplierSerialzier,
				  topHeightSerialzier,
				  bottomHeightSerialzier)
		{
		}

		public static IEndianProvider<SkyIslandMapsSerializer> Get { get; } =
			EndianProvider.New(serialzier => new SkyIslandMapsSerializer(serialzier));

		/// <inheritdoc />
		protected override SkyIslandMaps ComposeValue(
			float shapePercentWeight,
			float baselineHeight,
			float mountainHeightMultiplier,
			float topHeight,
			float bottomHeight) => new SkyIslandMaps(
				shapePercentWeight: shapePercentWeight,
				baselineHeight: baselineHeight,
				mountainHeightMultiplier: mountainHeightMultiplier,
				topHeight: topHeight,
				bottomHeight: bottomHeight);

		/// <inheritdoc />
		protected override void DecomposeValue(
			SkyIslandMaps value,
			out float shapePercentWeight,
			out float baselineHeight,
			out float mountainHeightMultiplier,
			out float topHeight,
			out float bottomHeight)
		{
			shapePercentWeight = value.ShapePercentWeight;
			baselineHeight = value.BaselineHeight;
			mountainHeightMultiplier = value.MountainHeightMultiplier;
			topHeight = value.TopHeight;
			bottomHeight = value.BottomHeight;
		}
	}
}
