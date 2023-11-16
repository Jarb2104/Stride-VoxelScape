using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Core.Vertices;
using Voxelscape.Common.MonoGame.Serialization;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Contouring.Core.Serialization
{
	/// <summary>
	///
	/// </summary>
	public class NormalColorTextureVertexSerializer :
		AbstractCompositeConstantSerializer<NormalColorTextureVertex, Vector3, Vector3, Color, Vector2>
	{
		public NormalColorTextureVertexSerializer(
			IConstantSerializerDeserializer<Vector3> positionSerializer,
			IConstantSerializerDeserializer<Vector3> normalSerializer,
			IConstantSerializerDeserializer<Color> colorSerializer,
			IConstantSerializerDeserializer<Vector2> texCoordSerializer)
			: base(positionSerializer, normalSerializer, colorSerializer, texCoordSerializer)
		{
		}

		public static IEndianProvider<NormalColorTextureVertexSerializer> WithColorAlpha { get; } = EndianProvider.New(
			New, Vector3Serializer.Get, Vector2Serializer.Get, ColorSerializer.RGBA);

		public static IEndianProvider<NormalColorTextureVertexSerializer> NoColorAlpha { get; } = EndianProvider.New(
			New, Vector3Serializer.Get, Vector2Serializer.Get, ColorSerializer.RGB);

		public static NormalColorTextureVertexSerializer New(
			IConstantSerializerDeserializer<Vector3> vector3Serializer,
			IConstantSerializerDeserializer<Vector2> vector2Serializer,
			IConstantSerializerDeserializer<Color> colorSerializer) => new NormalColorTextureVertexSerializer(
				vector3Serializer, vector3Serializer, colorSerializer, vector2Serializer);

		/// <inheritdoc />
		protected override NormalColorTextureVertex ComposeValue(
			Vector3 position, Vector3 normal, Color color, Vector2 texCoord) =>
			new NormalColorTextureVertex(position, normal, color, texCoord);

		/// <inheritdoc />
		protected override void DecomposeValue(
			NormalColorTextureVertex value, out Vector3 position, out Vector3 normal, out Color color, out Vector2 texCoord)
		{
			position = value.Position;
			normal = value.Normal;
			color = value.Color;
			texCoord = value.TextureCoordinate;
		}
	}
}
