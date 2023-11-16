using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Indexing.Core.Serialization
{
	/// <summary>
	///
	/// </summary>
	public class Index1DSerializer : AbstractCompositeConstantSerializer<Index1D, int>
	{
		public Index1DSerializer(IConstantSerializerDeserializer<int> serialzier)
			: base(serialzier)
		{
		}

		public static IEndianProvider<Index1DSerializer> Get { get; } =
			EndianProvider.New(serialzier => new Index1DSerializer(serialzier));

		/// <inheritdoc />
		protected override Index1D ComposeValue(int x) => new Index1D(x);

		/// <inheritdoc />
		protected override int DecomposeValue(Index1D value) => value.X;
	}
}
