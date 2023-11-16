using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	/// <summary>
	///
	/// </summary>
	public class Index4DSerializer : AbstractCompositeConstantSerializer<Index4D, int, int, int, int>
	{
		public Index4DSerializer(IConstantSerializerDeserializer<int> serialzier)
			: base(serialzier, serialzier, serialzier, serialzier)
		{
		}

		public Index4DSerializer(
			IConstantSerializerDeserializer<int> xSerialzier,
			IConstantSerializerDeserializer<int> ySerialzier,
			IConstantSerializerDeserializer<int> zSerialzier,
			IConstantSerializerDeserializer<int> wSerialzier)
			: base(xSerialzier, ySerialzier, zSerialzier, wSerialzier)
		{
		}

		public static IEndianProvider<Index4DSerializer> Get { get; } =
			EndianProvider.New(serialzier => new Index4DSerializer(serialzier));

		/// <inheritdoc />
		protected override Index4D ComposeValue(int x, int y, int z, int w) => new Index4D(x, y, z, w);

		/// <inheritdoc />
		protected override void DecomposeValue(Index4D value, out int x, out int y, out int z, out int w)
		{
			x = value.X;
			y = value.Y;
			z = value.Z;
			w = value.W;
		}
	}
}
