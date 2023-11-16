using Stride.Core.Mathematics;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.MonoGame.Serialization
{
	/// <summary>
	///
	/// </summary>
	public class Vector3Serializer : AbstractCompositeConstantSerializer<Vector3, float, float, float>
	{
		public Vector3Serializer(IConstantSerializerDeserializer<float> serialzier)
			: base(serialzier, serialzier, serialzier)
		{
		}

		public Vector3Serializer(
			IConstantSerializerDeserializer<float> xSerialzier,
			IConstantSerializerDeserializer<float> ySerialzier,
			IConstantSerializerDeserializer<float> zSerialzier)
			: base(xSerialzier, ySerialzier, zSerialzier)
		{
		}

		public static IEndianProvider<Vector3Serializer> Get { get; } =
			EndianProvider.New(serializer => new Vector3Serializer(serializer));

		/// <inheritdoc />
		protected override Vector3 ComposeValue(float x, float y, float z) => new Vector3(x, y, z);

		/// <inheritdoc />
		protected override void DecomposeValue(Vector3 value, out float x, out float y, out float z)
		{
			x = value.X;
			y = value.Y;
			z = value.Z;
		}
	}
}
