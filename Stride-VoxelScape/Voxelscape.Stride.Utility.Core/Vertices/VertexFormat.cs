using Voxelscape.Stride.Utility.Pact.Vertices;
using Stride.Core.Mathematics;
using Stride.Graphics;

namespace Voxelscape.Stride.Utility.Core.Vertices
{
	/// <summary>
	///
	/// </summary>
	public static class VertexFormat
	{
		public static IVertexFormat<VertexPositionNormalColorTexture> PositionNormalColorTexture =>
			VertexPositionNormalColorTexture.Format;

		public static IVertexFormat<VertexPositionNormalTexture> PositionNormalTexture { get; } =
			new VertexPositionNormalTextureFormat();

		private class VertexPositionNormalTextureFormat : IVertexFormat<VertexPositionNormalTexture>
		{
			/// <inheritdoc />
			public VertexDeclaration Layout { get; } = VertexPositionNormalTexture.Layout;

			/// <inheritdoc />
			public Vector3 GetPosition(VertexPositionNormalTexture vertex) => vertex.Position;
		}
	}
}
