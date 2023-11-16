using Stride.Core.Mathematics;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.MonoGame.Serialization
{
	/// <summary>
	///
	/// </summary>
	public class Vector4Serializer : AbstractCompositeConstantSerializer<Vector4, float, float, float, float>
	{
		public Vector4Serializer(IConstantSerializerDeserializer<float> serialzier)
			: base(serialzier, serialzier, serialzier, serialzier)
		{
		}

		public Vector4Serializer(
			IConstantSerializerDeserializer<float> xSerialzier,
			IConstantSerializerDeserializer<float> ySerialzier,
			IConstantSerializerDeserializer<float> zSerialzier,
			IConstantSerializerDeserializer<float> wSerialzier)
			: base(xSerialzier, ySerialzier, zSerialzier, wSerialzier)
		{
		}

		public static IEndianProvider<Vector4Serializer> Get { get; } =
			EndianProvider.New(serializer => new Vector4Serializer(serializer));

		/// <inheritdoc />
		protected override Vector4 ComposeValue(float x, float y, float z, float w) => new Vector4(x, y, z, w);

		/// <inheritdoc />
		protected override void DecomposeValue(Vector4 value, out float x, out float y, out float z, out float w)
		{
			x = value.X;
			y = value.Y;
			z = value.Z;
			w = value.W;
		}
	}
}
