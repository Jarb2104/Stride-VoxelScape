using Stride.Core.Mathematics;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.MonoGame.Serialization
{
	/// <summary>
	///
	/// </summary>
	public class RGBAColorSerializer : AbstractCompositeConstantSerializer<Color, byte, byte, byte, byte>
	{
		public RGBAColorSerializer(IConstantSerializerDeserializer<byte> serialzier)
			: base(serialzier, serialzier, serialzier, serialzier)
		{
		}

		public RGBAColorSerializer(
			IConstantSerializerDeserializer<byte> rSerialzier,
			IConstantSerializerDeserializer<byte> gSerialzier,
			IConstantSerializerDeserializer<byte> bSerialzier,
			IConstantSerializerDeserializer<byte> aSerialzier)
			: base(rSerialzier, gSerialzier, bSerialzier, aSerialzier)
		{
		}

		public static IEndianProvider<RGBAColorSerializer> Get { get; } =
			EndianProvider.New(serializer => new RGBAColorSerializer(serializer));

		/// <inheritdoc />
		protected override Color ComposeValue(byte red, byte green, byte blue, byte alpha) =>
			new Color(red, green, blue, alpha);

		/// <inheritdoc />
		protected override void DecomposeValue(
			Color value, out byte red, out byte green, out byte blue, out byte alpha)
		{
			red = value.R;
			green = value.G;
			blue = value.B;
			alpha = value.A;
		}
	}
}
