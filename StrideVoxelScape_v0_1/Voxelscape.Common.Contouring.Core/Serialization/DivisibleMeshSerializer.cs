using Voxelscape.Common.Contouring.Core.Vertices;
using Voxelscape.Utility.Data.Core.Serialization;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Common.Contouring.Core.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class DivisibleMeshSerializer
	{
		public static class NormalColorTextureVertices
		{
			public static IEndianProvider<DivisibleMeshSerializer<NormalColorTextureVertex>> WithColorAlpha { get; } =
				EndianProvider.New(
					serializer => new DivisibleMeshSerializer<NormalColorTextureVertex>(serializer),
					NormalColorTextureVertexSerializer.WithColorAlpha);

			public static IEndianProvider<DivisibleMeshSerializer<NormalColorTextureVertex>> NoColorAlpha { get; } =
				EndianProvider.New(
					serializer => new DivisibleMeshSerializer<NormalColorTextureVertex>(serializer),
					NormalColorTextureVertexSerializer.NoColorAlpha);
		}
	}
}
