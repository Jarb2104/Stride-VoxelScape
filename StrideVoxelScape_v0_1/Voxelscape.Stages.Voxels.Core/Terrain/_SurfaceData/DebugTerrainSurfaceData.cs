using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Stages.Voxels.Pact.Terrain;

namespace Voxelscape.Stages.Voxels.Core.Terrain
{
	/// <summary>
	///
	/// </summary>
	public class DebugTerrainSurfaceData : ITerrainSurfaceData
	{
		private float? originalSeed;

		private float? adjustedSeed;

		private QuadDiagonal? diagonal;

		private Vector3? topLeftVertex;

		private Vector3? topRightVertex;

		private Vector3? bottomLeftVertex;

		private Vector3? bottomRightVertex;

		private float? surfaceArea;

		#region ISeedWriter Members

		/// <inheritdoc />
		public float OriginalSeed
		{
			get { return this.originalSeed.Value; }
			set { this.originalSeed = value; }
		}

		/// <inheritdoc />
		public float AdjustedSeed
		{
			get { return this.adjustedSeed.Value; }
			set { this.adjustedSeed = value; }
		}

		#endregion

		#region IDiagonalWriter Members

		/// <inheritdoc />
		public QuadDiagonal Diagonal
		{
			get { return this.diagonal.Value; }
			set { this.diagonal = value; }
		}

		#endregion

		#region IVerticesWriter Members

		/// <inheritdoc />
		public Vector3 TopLeftVertex
		{
			get { return this.topLeftVertex.Value; }
			set { this.topLeftVertex = value; }
		}

		/// <inheritdoc />
		public Vector3 TopRightVertex
		{
			get { return this.topRightVertex.Value; }
			set { this.topRightVertex = value; }
		}

		/// <inheritdoc />
		public Vector3 BottomLeftVertex
		{
			get { return this.bottomLeftVertex.Value; }
			set { this.bottomLeftVertex = value; }
		}

		/// <inheritdoc />
		public Vector3 BottomRightVertex
		{
			get { return this.bottomRightVertex.Value; }
			set { this.bottomRightVertex = value; }
		}

		#endregion

		#region ISurfaceAreaWriter Members

		/// <inheritdoc />
		public float SurfaceArea
		{
			get { return this.surfaceArea.Value; }
			set { this.surfaceArea = value; }
		}

		#endregion

		#region IResettable Members

		/// <inheritdoc />
		public void Reset()
		{
			this.OriginalSeed = default(float);
			this.AdjustedSeed = default(float);
			this.Diagonal = default(QuadDiagonal);
			this.TopLeftVertex = default(Vector3);
			this.TopRightVertex = default(Vector3);
			this.BottomLeftVertex = default(Vector3);
			this.BottomRightVertex = default(Vector3);
			this.SurfaceArea = default(float);
		}

		#endregion
	}
}
