using Stride.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.MonoGame.Serialization
{
	/// <summary>
	///
	/// </summary>
	public class RGBColorSerializer : AbstractCompositeConstantSerializer<Color, byte, byte, byte>
	{
		public RGBColorSerializer(IConstantSerializerDeserializer<byte> serialzier)
			: base(serialzier, serialzier, serialzier)
		{
		}

		public RGBColorSerializer(
			IConstantSerializerDeserializer<byte> rSerialzier,
			IConstantSerializerDeserializer<byte> gSerialzier,
			IConstantSerializerDeserializer<byte> bSerialzier)
			: base(rSerialzier, gSerialzier, bSerialzier)
		{
		}

		public static IEndianProvider<RGBColorSerializer> Get { get; } =
			EndianProvider.New(serializer => new RGBColorSerializer(serializer));

		/// <inheritdoc />
		protected override Color ComposeValue(byte red, byte green, byte blue) => new Color(red, green, blue);

		/// <inheritdoc />
		protected override void DecomposeValue(Color value, out byte red, out byte green, out byte blue)
		{
			Contracts.Assert.That(value.IsOpaque());

			red = value.R;
			green = value.G;
			blue = value.B;
		}
	}
}
