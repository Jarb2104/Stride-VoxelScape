using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	/// <summary>
	///
	/// </summary>
	public class Index3DSerializer : AbstractCompositeConstantSerializer<Index3D, int, int, int>
	{
		public Index3DSerializer(IConstantSerializerDeserializer<int> serialzier)
			: base(serialzier, serialzier, serialzier)
		{
		}

		public Index3DSerializer(
			IConstantSerializerDeserializer<int> xSerialzier,
			IConstantSerializerDeserializer<int> ySerialzier,
			IConstantSerializerDeserializer<int> zSerialzier)
			: base(xSerialzier, ySerialzier, zSerialzier)
		{
		}

		public static IEndianProvider<Index3DSerializer> Get { get; } =
			EndianProvider.New(serialzier => new Index3DSerializer(serialzier));

		/// <inheritdoc />
		protected override Index3D ComposeValue(int x, int y, int z) => new Index3D(x, y, z);

		/// <inheritdoc />
		protected override void DecomposeValue(Index3D value, out int x, out int y, out int z)
		{
			x = value.X;
			y = value.Y;
			z = value.Z;
		}
	}
}
