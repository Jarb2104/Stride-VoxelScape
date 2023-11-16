using Stride.Core.Mathematics;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.MonoGame.Serialization
{
	/// <summary>
	///
	/// </summary>
	public class Vector2Serializer : AbstractCompositeConstantSerializer<Vector2, float, float>
	{
		public Vector2Serializer(IConstantSerializerDeserializer<float> serialzier)
			: base(serialzier, serialzier)
		{
		}

		public Vector2Serializer(
			IConstantSerializerDeserializer<float> xSerialzier, IConstantSerializerDeserializer<float> ySerialzier)
			: base(xSerialzier, ySerialzier)
		{
		}

		public static IEndianProvider<Vector2Serializer> Get { get; } =
			EndianProvider.New(serializer => new Vector2Serializer(serializer));

		/// <inheritdoc />
		protected override Vector2 ComposeValue(float x, float y) => new Vector2(x, y);

		/// <inheritdoc />
		protected override void DecomposeValue(Vector2 value, out float x, out float y)
		{
			x = value.X;
			y = value.Y;
		}
	}
}
