using System.Collections.Generic;
using Voxelscape.Common.Contouring.Core.Meshing;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Contouring.Core.Serialization
{
	public class DivisibleMeshSerializer<TVertex> : AbstractCompositeSerializer<
		IDivisibleMesh<TVertex>,
		IReadOnlyCollection<TriangleGroup>,
		IReadOnlyCollection<byte>,
		IReadOnlyCollection<TVertex>>
		where TVertex : struct
	{
		public DivisibleMeshSerializer(IConstantSerializerDeserializer<TVertex> vertexSerializer)
			: this(
				  TriangleGroupSerializer.Get[vertexSerializer.Endianness],
				  Serializer.Byte[vertexSerializer.Endianness],
				  vertexSerializer,
				  Serializer.Int[vertexSerializer.Endianness])
		{
		}

		public DivisibleMeshSerializer(
			IConstantSerializerDeserializer<TriangleGroup> groupSerializer,
			IConstantSerializerDeserializer<byte> offsetSerializer,
			IConstantSerializerDeserializer<TVertex> vertexSerializer)
			: this(groupSerializer, offsetSerializer, vertexSerializer, Serializer.Int[vertexSerializer.Endianness])
		{
		}

		public DivisibleMeshSerializer(
			IConstantSerializerDeserializer<TriangleGroup> groupSerializer,
			IConstantSerializerDeserializer<byte> offsetSerializer,
			IConstantSerializerDeserializer<TVertex> vertexSerializer,
			IConstantSerializerDeserializer<int> countSerializer)
			: this(
				  new ReadOnlyCollectionConstantSerializer<TriangleGroup>(groupSerializer, countSerializer),
				  new ReadOnlyCollectionConstantSerializer<byte>(offsetSerializer, countSerializer),
				  new ReadOnlyCollectionConstantSerializer<TVertex>(vertexSerializer, countSerializer))
		{
		}

		public DivisibleMeshSerializer(
			ISerializerDeserializer<IReadOnlyCollection<TriangleGroup>> groupsSerializer,
			ISerializerDeserializer<IReadOnlyCollection<byte>> offsetsSerializer,
			ISerializerDeserializer<IReadOnlyCollection<TVertex>> verticesSerializer)
			: base(groupsSerializer, offsetsSerializer, verticesSerializer)
		{
		}

		/// <inheritdoc />
		protected override IDivisibleMesh<TVertex> ComposeValue(
			IReadOnlyCollection<TriangleGroup> groups,
			IReadOnlyCollection<byte> offsets,
			IReadOnlyCollection<TVertex> vertices) =>
			new DivisibleMesh<TVertex>(groups, offsets, vertices);

		/// <inheritdoc />
		protected override void DecomposeValue(
			IDivisibleMesh<TVertex> value,
			out IReadOnlyCollection<TriangleGroup> groups,
			out IReadOnlyCollection<byte> offsets,
			out IReadOnlyCollection<TVertex> vertices)
		{
			groups = value.Groups;
			offsets = value.Offsets;
			vertices = value.Vertices;
		}
	}
}
