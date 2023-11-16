using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Stages.Voxels.Pact.Terrain;

namespace Voxelscape.Stages.Voxels.Core.Terrain
{
	/// <summary>
	///
	/// </summary>
	public class TerrainSurfaceData : ITerrainSurfaceData
	{
		#region ISeedWriter Members

		/// <inheritdoc />
		public float OriginalSeed
		{
			get;
			set;
		}

		/// <inheritdoc />
		public float AdjustedSeed
		{
			get;
			set;
		}

		#endregion

		#region IDiagonalWriter Members

		/// <inheritdoc />
		public QuadDiagonal Diagonal
		{
			get;
			set;
		}

		#endregion

		#region IVerticesWriter Members

		/// <inheritdoc />
		public Vector3 TopLeftVertex
		{
			get;
			set;
		}

		/// <inheritdoc />
		public Vector3 TopRightVertex
		{
			get;
			set;
		}

		/// <inheritdoc />
		public Vector3 BottomLeftVertex
		{
			get;
			set;
		}

		/// <inheritdoc />
		public Vector3 BottomRightVertex
		{
			get;
			set;
		}

		#endregion

		#region ISurfaceAreaWriter Members

		/// <inheritdoc />
		public float SurfaceArea
		{
			get;
			set;
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
