using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	/// <summary>
	///
	/// </summary>
	public class Index2DSerializer : AbstractCompositeConstantSerializer<Index2D, int, int>
	{
		public Index2DSerializer(IConstantSerializerDeserializer<int> serialzier)
			: base(serialzier, serialzier)
		{
		}

		public Index2DSerializer(
			IConstantSerializerDeserializer<int> xSerialzier,
			IConstantSerializerDeserializer<int> ySerialzier)
			: base(xSerialzier, ySerialzier)
		{
		}

		public static IEndianProvider<Index2DSerializer> Get { get; } =
			EndianProvider.New(serialzier => new Index2DSerializer(serialzier));

		/// <inheritdoc />
		protected override Index2D ComposeValue(int x, int y) => new Index2D(x, y);

		/// <inheritdoc />
		protected override void DecomposeValue(Index2D value, out int x, out int y)
		{
			x = value.X;
			y = value.Y;
		}
	}
}
