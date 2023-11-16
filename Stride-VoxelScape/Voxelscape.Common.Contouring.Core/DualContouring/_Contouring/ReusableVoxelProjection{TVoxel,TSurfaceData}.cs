using System;
using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Common.Contouring.Core.DualContouring
{
	/// <summary>
	/// A projection of voxels that can be used when dual contouring a three dimensional volume and can be reused throughout
	/// the contouring process to reduce object allocations and garbage collection pressure.
	/// </summary>
	/// <typeparam name="TVoxel">The type of the voxel.</typeparam>
	/// <typeparam name="TSurfaceData">The type of the data.</typeparam>
	/// <remarks>
	/// <para>
	/// The relative origin index (0, 0, 0) defines the voxel immediately behind the quad of surface being contoured (the voxel
	/// that the surface visually represents). (0, 0, -1) defines the voxel in front of the surface (typically air or some other
	/// transparent material). This perspective can be best thought of as looking at a wall. The x-axis is horizontal along the
	/// wall, positive is to the right and negative to the left. The y-axis is vertical, positive is up and negative is down.
	/// The z-axis is distance from the wall, positive is towards or into the wall and negative is backing away from it.
	/// </para>
	/// <para>
	/// Use the SetupProjection methods to set which orthogonal direction the projection is facing. A new instance that hasn't
	/// had any SetupProjection method called on it yet defaults to straight matching the backing indexable of voxels with no
	/// offset or rotation applied.
	/// </para>
	/// </remarks>
	public class ReusableVoxelProjection<TVoxel, TSurfaceData> : IVoxelProjection<TVoxel, TSurfaceData>
		where TVoxel : struct
		where TSurfaceData : class, IResettable
	{
		#region Private Rotation Function Singletons

		/// <summary>
		/// The function used to convert a relative index to its equivalent absolute index when projecting a surface contour that
		/// is facing towards negative infinity on the x axis. This applies rotation only.
		/// </summary>
		private static readonly Func<Index3D, Index3D> ProjectIndexNegativeXAxis =
			index => new Index3D(index.Z, index.Y, -index.X);

		/// <summary>
		/// The function used to convert a relative index to its equivalent absolute index when projecting a surface contour that
		/// is facing towards positive infinity on the x axis. This applies rotation only.
		/// </summary>
		private static readonly Func<Index3D, Index3D> ProjectIndexPositiveXAxis =
			index => new Index3D(-index.Z, index.Y, index.X);

		/// <summary>
		/// The function used to convert a relative index to its equivalent absolute index when projecting a surface contour that
		/// is facing towards negative infinity on the y axis. This applies rotation only.
		/// </summary>
		private static readonly Func<Index3D, Index3D> ProjectIndexNegativeYAxis =
			index => new Index3D(-index.X, index.Z, index.Y);

		/// <summary>
		/// The function used to convert a relative index to its equivalent absolute index when projecting a surface contour that
		/// is facing towards positive infinity on the y axis. This applies rotation only.
		/// </summary>
		private static readonly Func<Index3D, Index3D> ProjectIndexPositiveYAxis =
			index => new Index3D(index.X, -index.Z, index.Y);

		/// <summary>
		/// The function used to convert a relative index to its equivalent absolute index when projecting a surface contour that
		/// is facing towards negative infinity on the z axis. This applies rotation only.
		/// </summary>
		private static readonly Func<Index3D, Index3D> ProjectIndexNegativeZAxis =
			index => new Index3D(index.X, index.Y, index.Z);

		/// <summary>
		/// The function used to convert a relative index to its equivalent absolute index when projecting a surface contour that
		/// is facing towards positive infinity on the z axis. This applies rotation only.
		/// </summary>
		private static readonly Func<Index3D, Index3D> ProjectIndexPositiveZAxis =
			index => new Index3D(-index.X, index.Y, -index.Z);

		/// <summary>
		/// The function used to convert a relative vector to its equivalent absolute vector when projecting a surface contour that
		/// is facing towards negative infinity on the x axis. This applies rotation only.
		/// </summary>
		private static readonly Func<Vector3, Vector3> ProjectVectorNegativeXAxis =
			vector => new Vector3(vector.Z, vector.Y, -vector.X);

		/// <summary>
		/// The function used to convert a relative vector to its equivalent absolute vector when projecting a surface contour that
		/// is facing towards positive infinity on the x axis. This applies rotation only.
		/// </summary>
		private static readonly Func<Vector3, Vector3> ProjectVectorPositiveXAxis =
			vector => new Vector3(-vector.Z, vector.Y, vector.X);

		/// <summary>
		/// The function used to convert a relative vector to its equivalent absolute vector when projecting a surface contour that
		/// is facing towards negative infinity on the y axis. This applies rotation only.
		/// </summary>
		private static readonly Func<Vector3, Vector3> ProjectVectorNegativeYAxis =
			vector => new Vector3(-vector.X, vector.Z, vector.Y);

		/// <summary>
		/// The function used to convert a relative vector to its equivalent absolute vector when projecting a surface contour that
		/// is facing towards positive infinity on the y axis. This applies rotation only.
		/// </summary>
		private static readonly Func<Vector3, Vector3> ProjectVectorPositiveYAxis =
			vector => new Vector3(vector.X, -vector.Z, vector.Y);

		/// <summary>
		/// The function used to convert a relative vector to its equivalent absolute vector when projecting a surface contour that
		/// is facing towards negative infinity on the z axis. This applies rotation only.
		/// </summary>
		private static readonly Func<Vector3, Vector3> ProjectVectorNegativeZAxis =
			vector => new Vector3(vector.X, vector.Y, vector.Z);

		/// <summary>
		/// The function used to convert a relative vector to its equivalent absolute vector when projecting a surface contour that
		/// is facing towards positive infinity on the z axis. This applies rotation only.
		/// </summary>
		private static readonly Func<Vector3, Vector3> ProjectVectorPositiveZAxis =
			vector => new Vector3(-vector.X, vector.Y, -vector.Z);

		#endregion

		#region Private Fields

		/// <summary>
		/// The temporary data that is generated while creating the surface.
		/// </summary>
		private readonly TSurfaceData data;

		/// <summary>
		/// The indexable volume of voxels to provide projections of.
		/// </summary>
		private readonly IReadOnlyIndexable<Index3D, TVoxel> voxels;

		/// <summary>
		/// The stage index of the zero index origin of the indexable voxels.
		/// </summary>
		private readonly Index3D stageIndexOrigin;

		/// <summary>
		/// The function used by the current projection to transform a relative index to its equivalent absolute index by rotating it.
		/// </summary>
		private Func<Index3D, Index3D> indexRotationTransformation;

		/// <summary>
		/// The function used by the current projection to transform a relative vector to its equivalent absolute vector by rotating it.
		/// </summary>
		private Func<Vector3, Vector3> vectorRotationTransformation;

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="ReusableVoxelProjection{TVoxel, TSurfaceData}" /> class.
		/// </summary>
		/// <param name="data">The container used to hold the temporary data that is generated while creating each surface.</param>
		/// <param name="voxels">The voxels to provide projections of.</param>
		/// <param name="stageIndexOrigin">The stage index of the zero index origin of the indexable voxels.</param>
		public ReusableVoxelProjection(TSurfaceData data, IReadOnlyIndexable<Index3D, TVoxel> voxels, Index3D stageIndexOrigin)
		{
			Contracts.Requires.That(data != null);
			Contracts.Requires.That(voxels != null);

			this.data = data;
			this.voxels = voxels;
			this.stageIndexOrigin = stageIndexOrigin;

			this.SetupProjectionTowardsNegativeZAxis(Index3D.Zero);
		}

		#region IVoxelProjection<TVoxel, TSurfaceData> Members

		/// <inheritdoc />
		public TSurfaceData SurfaceData => this.data;

		/// <inheritdoc />
		public Index3D AbsoluteIndexOfOrigin
		{
			get;
			private set;
		}

		/// <inheritdoc />
		public Vector3 AbsoluteVectorOfOrigin
		{
			get;
			private set;
		}

		/// <inheritdoc />
		public Index3D StageIndexOfOrigin
		{
			get;
			private set;
		}

		/// <inheritdoc />
		public AxisDirection3D AxisDirection
		{
			get;
			private set;
		}

		/// <inheritdoc />
		public TVoxel this[Index3D relativeIndex]
		{
			get
			{
				IVoxelProjectionContracts.Indexer(this, relativeIndex);

				return this.voxels[this.ConvertToAbsoluteIndex(relativeIndex)];
			}
		}

		/// <inheritdoc />
		public TVoxel GetVoxelAtAbsoluteIndex(Index3D absoluteIndex)
		{
			IVoxelProjectionContracts.GetVoxelAtAbsoluteIndex(this, absoluteIndex);

			return this.voxels[absoluteIndex];
		}

		/// <inheritdoc />
		public TVoxel GetVoxelAtStageIndex(Index3D stageIndex)
		{
			IVoxelProjectionContracts.GetVoxelAtStageIndex(this, stageIndex);

			return this.GetVoxelAtAbsoluteIndex(stageIndex - this.stageIndexOrigin);
		}

		/// <inheritdoc />
		public bool IsRelativeIndexValid(Index3D relativeIndex) =>
			this.voxels.IsIndexValid(this.ConvertToAbsoluteIndex(relativeIndex));

		/// <inheritdoc />
		public bool IsAbsoluteIndexValid(Index3D absoluteIndex) =>
			this.voxels.IsIndexValid(absoluteIndex);

		/// <inheritdoc />
		public bool IsStageIndexValid(Index3D stageIndex) =>
			this.IsAbsoluteIndexValid(stageIndex - this.stageIndexOrigin);

		/// <inheritdoc />
		public Index3D ConvertToAbsoluteIndex(Index3D relativeIndex) =>
			this.indexRotationTransformation(relativeIndex) + this.AbsoluteIndexOfOrigin;

		/// <inheritdoc />
		public Index3D ConvertToStageIndex(Index3D relativeIndex) =>
			this.ConvertToAbsoluteIndex(relativeIndex) + this.stageIndexOrigin;

		/// <inheritdoc />
		public Vector3 ConvertToAbsoluteVectorPosition(Vector3 relativeVector) =>
			this.vectorRotationTransformation(relativeVector) + this.AbsoluteVectorOfOrigin;

		/// <inheritdoc />
		public Vector3 ConvertToAbsoluteVectorNormal(Vector3 relativeVector) =>
			this.vectorRotationTransformation(relativeVector);

		#endregion

		#region Setup Projection

		/// <summary>
		/// Setups the projection to be facing towards negative infinity on the x axis.
		/// </summary>
		/// <param name="originOfProjection">The index to set the origin of the projection at.</param>
		public void SetupProjectionTowardsNegativeXAxis(Index3D originOfProjection)
		{
			Contracts.Requires.That(this.IsAbsoluteIndexValid(originOfProjection));

			this.InitializeProjection(originOfProjection);

			this.AxisDirection = AxisDirection3D.NegativeX;
			this.indexRotationTransformation = ProjectIndexNegativeXAxis;
			this.vectorRotationTransformation = ProjectVectorNegativeXAxis;
		}

		/// <summary>
		/// Setups the projection to be facing towards positive infinity on the x axis.
		/// </summary>
		/// <param name="originOfProjection">The index to set the origin of the projection at.</param>
		public void SetupProjectionTowardsPositiveXAxis(Index3D originOfProjection)
		{
			Contracts.Requires.That(this.IsAbsoluteIndexValid(originOfProjection));

			this.InitializeProjection(originOfProjection);

			this.AxisDirection = AxisDirection3D.PositiveX;
			this.indexRotationTransformation = ProjectIndexPositiveXAxis;
			this.vectorRotationTransformation = ProjectVectorPositiveXAxis;
		}

		/// <summary>
		/// Setups the projection to be facing towards negative infinity on the y axis.
		/// </summary>
		/// <param name="originOfProjection">The index to set the origin of the projection at.</param>
		public void SetupProjectionTowardsNegativeYAxis(Index3D originOfProjection)
		{
			Contracts.Requires.That(this.IsAbsoluteIndexValid(originOfProjection));

			this.InitializeProjection(originOfProjection);

			this.AxisDirection = AxisDirection3D.NegativeY;
			this.indexRotationTransformation = ProjectIndexNegativeYAxis;
			this.vectorRotationTransformation = ProjectVectorNegativeYAxis;
		}

		/// <summary>
		/// Setups the projection to be facing towards positive infinity on the y axis.
		/// </summary>
		/// <param name="originOfProjection">The index to set the origin of the projection at.</param>
		public void SetupProjectionTowardsPositiveYAxis(Index3D originOfProjection)
		{
			Contracts.Requires.That(this.IsAbsoluteIndexValid(originOfProjection));

			this.InitializeProjection(originOfProjection);

			this.AxisDirection = AxisDirection3D.PositiveY;
			this.indexRotationTransformation = ProjectIndexPositiveYAxis;
			this.vectorRotationTransformation = ProjectVectorPositiveYAxis;
		}

		/// <summary>
		/// Setups the projection to be facing towards negative infinity on the z axis.
		/// </summary>
		/// <param name="originOfProjection">The index to set the origin of the projection at.</param>
		public void SetupProjectionTowardsNegativeZAxis(Index3D originOfProjection)
		{
			Contracts.Requires.That(this.IsAbsoluteIndexValid(originOfProjection));

			this.InitializeProjection(originOfProjection);

			this.AxisDirection = AxisDirection3D.NegativeZ;
			this.indexRotationTransformation = ProjectIndexNegativeZAxis;
			this.vectorRotationTransformation = ProjectVectorNegativeZAxis;
		}

		/// <summary>
		/// Setups the projection to be facing towards positive infinity on the z axis.
		/// </summary>
		/// <param name="originOfProjection">The index to set the origin of the projection at.</param>
		public void SetupProjectionTowardsPositiveZAxis(Index3D originOfProjection)
		{
			Contracts.Requires.That(this.IsAbsoluteIndexValid(originOfProjection));

			this.InitializeProjection(originOfProjection);

			this.AxisDirection = AxisDirection3D.PositiveZ;
			this.indexRotationTransformation = ProjectIndexPositiveZAxis;
			this.vectorRotationTransformation = ProjectVectorPositiveZAxis;
		}

		#endregion

		/// <summary>
		/// Handles the common setup of a new projection.
		/// </summary>
		/// <param name="originOfProjection">The index to set the origin of the projection at.</param>
		private void InitializeProjection(Index3D originOfProjection)
		{
			this.data.Reset();
			this.AbsoluteIndexOfOrigin = originOfProjection;
			this.AbsoluteVectorOfOrigin = new Vector3(originOfProjection.X, originOfProjection.Y, originOfProjection.Z);
			this.StageIndexOfOrigin = this.stageIndexOrigin + originOfProjection;
		}
	}
}
