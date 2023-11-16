using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Contouring.Core.Serialization
{
	/// <summary>
	///
	/// </summary>
	public class TriangleGroupSerializer : AbstractCompositeConstantSerializer<TriangleGroup, byte, byte>
	{
		public TriangleGroupSerializer(IConstantSerializerDeserializer<byte> byteSerializer)
			: base(byteSerializer, byteSerializer)
		{
		}

		public TriangleGroupSerializer(
			IConstantSerializerDeserializer<byte> trianglesSerializer,
			IConstantSerializerDeserializer<byte> verticesSerializer)
			: base(trianglesSerializer, verticesSerializer)
		{
		}

		public static IEndianProvider<TriangleGroupSerializer> Get { get; } =
			EndianProvider.New(serializer => new TriangleGroupSerializer(serializer));

		/// <inheritdoc />
		protected override TriangleGroup ComposeValue(byte triangles, byte vertices) =>
			new TriangleGroup(triangles, vertices);

		/// <inheritdoc />
		protected override void DecomposeValue(TriangleGroup value, out byte triangles, out byte vertices)
		{
			triangles = value.Triangles;
			vertices = value.Vertices;
		}
	}
}
