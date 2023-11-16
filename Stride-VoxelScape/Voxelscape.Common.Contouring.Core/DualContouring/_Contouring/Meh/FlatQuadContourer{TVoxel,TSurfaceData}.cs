using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Core.Vertices;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Common.MonoGame.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Core.DualContouring
{
	public class FlatQuadContourer<TVoxel, TSurfaceData> :
		IDualContourer<TVoxel, TSurfaceData, NormalColorTextureVertex>
		where TVoxel : struct
		where TSurfaceData : class, IDiagonalWritable
	{
		private readonly IContourDeterminer<TVoxel> contourDeterminer;

		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, Vector3> positioner;

		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, Color> colorer;

		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, Vector2> texturer;

		public FlatQuadContourer(
			IContourDeterminer<TVoxel> contourDeterminer,
			ISurfaceValueGenerator<TVoxel, TSurfaceData, Vector3> positioner,
			ISurfaceValueGenerator<TVoxel, TSurfaceData, Color> colorer,
			ISurfaceValueGenerator<TVoxel, TSurfaceData, Vector2> texturer)
		{
			Contracts.Requires.That(contourDeterminer != null);
			Contracts.Requires.That(positioner != null);
			Contracts.Requires.That(colorer != null);
			Contracts.Requires.That(texturer != null);

			this.contourDeterminer = contourDeterminer;
			this.positioner = positioner;
			this.colorer = colorer;
			this.texturer = texturer;
		}

		/// <inheritdoc />
		public void Contour(
			IDualContourable<TVoxel, TSurfaceData> contourable,
			IMutableDivisibleMesh<NormalColorTextureVertex> meshBuilder)
		{
			IDualContourerContracts.Contour(contourable, meshBuilder);

			foreach (var projection in contourable.GetContourableProjections(this.contourDeterminer))
			{
				Vector3 topLeftVertex, topRightVertex, botLeftVertex, botRightVertex;
				this.positioner.GenerateValues(
					projection,
					out topLeftVertex,
					out topRightVertex,
					out botLeftVertex,
					out botRightVertex);

				Vector3 normal;
				QuadDiagonal diagonal;

				if (projection.AbsoluteIndexOfOrigin.SumCoordinatesLong().IsEven())
				{
					diagonal = QuadDiagonal.Ascending;
					normal = PolyMath.GetNormalizedAverage(
						PolyMath.GetSurfaceNormal(topLeftVertex, botLeftVertex, topRightVertex),
						PolyMath.GetSurfaceNormal(botLeftVertex, botRightVertex, topRightVertex));
				}
				else
				{
					diagonal = QuadDiagonal.Descending;
					normal = PolyMath.GetNormalizedAverage(
						PolyMath.GetSurfaceNormal(topLeftVertex, botLeftVertex, botRightVertex),
						PolyMath.GetSurfaceNormal(topLeftVertex, botRightVertex, topRightVertex));
				}

				projection.SurfaceData.Diagonal = diagonal;

				Color topLeftColor, topRightColor, botLeftColor, botRightColor;
				this.colorer.GenerateValues(
					projection,
					out topLeftColor,
					out topRightColor,
					out botLeftColor,
					out botRightColor);

				Vector2 topLeftTexture, topRightTexture, botLeftTexture, botRightTexture;
				this.texturer.GenerateValues(
					projection,
					out topLeftTexture,
					out topRightTexture,
					out botLeftTexture,
					out botRightTexture);

				meshBuilder.AddFlatQuad(
					topLeft: new NormalColorTextureVertex(topLeftVertex, normal, topLeftColor, topLeftTexture),
					topRight: new NormalColorTextureVertex(topRightVertex, normal, topRightColor, topRightTexture),
					bottomLeft: new NormalColorTextureVertex(botLeftVertex, normal, botLeftColor, botLeftTexture),
					bottomRight: new NormalColorTextureVertex(botRightVertex, normal, botRightColor, botRightTexture),
					diagonal: diagonal);
			}
		}
	}
}
