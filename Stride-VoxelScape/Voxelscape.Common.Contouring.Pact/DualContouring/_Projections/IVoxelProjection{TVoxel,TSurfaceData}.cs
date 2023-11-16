using Stride.Core.Mathematics;
using System.Diagnostics;
using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	/// <summary>
	/// Defines a projection of voxel data being dual contoured oriented such that relative access of voxels
	/// is consistent regardless of which way the actual surface being contoured is facing.
	/// </summary>
	/// <typeparam name="TVoxel">The type of the voxel.</typeparam>
	/// <typeparam name="TSurfaceData">The type of the data.</typeparam>
	/// <remarks>
	/// The term 'Absolute' means the actual indices or vector position without the projection applied.
	/// 'Relative' is with the projection applied to account for the position and orientation of the contour.
	/// </remarks>
	public interface IVoxelProjection<TVoxel, TSurfaceData>
		where TVoxel : struct
		where TSurfaceData : class
	{
		/// <summary>
		/// Gets the temporary surface data that is generated while the surface is being created.
		/// </summary>
		/// <value>
		/// The temporary surface data.
		/// </value>
		TSurfaceData SurfaceData { get; }

		/// <summary>
		/// Gets the absolute index of the relative origin of this projection.
		/// </summary>
		/// <value>
		/// The absolute index of the relative origin of this projection.
		/// </value>
		Index3D AbsoluteIndexOfOrigin { get; }

		/// <summary>
		/// Gets the absolute vector position of the relative origin of this projection.
		/// </summary>
		/// <value>
		/// The absolute vector position of the relative origin of this projection.
		/// </value>
		Vector3 AbsoluteVectorOfOrigin { get; }

		/// <summary>
		/// Gets the stage index of the relative origin of this projection.
		/// </summary>
		/// <value>
		/// The stage index of the relative origin of this projection.
		/// </value>
		Index3D StageIndexOfOrigin { get; }

		/// <summary>
		/// Gets the axis and direction along that axis that the contour surface represented by
		/// this projection is facing.
		/// </summary>
		/// <value>
		/// The axis direction of this projection.
		/// </value>
		AxisDirection3D AxisDirection { get; }

		/// <summary>
		/// Gets the <typeparamref name="TVoxel"/> at the specified relative index.
		/// </summary>
		/// <value>
		/// The <typeparamref name="TVoxel"/>.
		/// </value>
		/// <param name="relativeIndex">The relative index of the voxel to retrieve.</param>
		/// <returns>The voxel at the specified relative index.</returns>
		TVoxel this[Index3D relativeIndex]
		{
			get;
		}

		/// <summary>
		/// Gets the <typeparamref name="TVoxel"/> at the specified absolute index.
		/// </summary>
		/// <param name="absoluteIndex">The absolute index of the voxel to retrieve.</param>
		/// <returns>The voxel at the specified absolute index.</returns>
		TVoxel GetVoxelAtAbsoluteIndex(Index3D absoluteIndex);

		/// <summary>
		/// Gets the <typeparamref name="TVoxel"/> at the specified stage index.
		/// </summary>
		/// <param name="stageIndex">The stage index of the voxel to retrieve.</param>
		/// <returns>The voxel at the specified stage index.</returns>
		TVoxel GetVoxelAtStageIndex(Index3D stageIndex);

		/// <summary>
		/// Determines whether the specified relative index is valid.
		/// </summary>
		/// <param name="relativeIndex">The relative index to check.</param>
		/// <returns>True if the relative index is valid; otherwise false.</returns>
		bool IsRelativeIndexValid(Index3D relativeIndex);

		/// <summary>
		/// Determines whether the specified absolute index is valid.
		/// </summary>
		/// <param name="absoluteIndex">The absolute index to check.</param>
		/// <returns>True if the absolute index is valid; otherwise false.</returns>
		bool IsAbsoluteIndexValid(Index3D absoluteIndex);

		/// <summary>
		/// Determines whether the specified stage index is valid.
		/// </summary>
		/// <param name="stageIndex">The stage index to check.</param>
		/// <returns>True if the stage index is valid; otherwise false.</returns>
		bool IsStageIndexValid(Index3D stageIndex);

		/// <summary>
		/// Converts the relative index to its equivalent absolute index.
		/// </summary>
		/// <param name="relativeIndex">The relative index to convert.</param>
		/// <returns>The absolute index that is the equivalent of the relative index.</returns>
		Index3D ConvertToAbsoluteIndex(Index3D relativeIndex);

		/// <summary>
		/// Converts the relative index to its equivalent stage index.
		/// </summary>
		/// <param name="relativeIndex">The relative index to convert.</param>
		/// <returns>The stage index that is the equivalent of the relative index.</returns>
		Index3D ConvertToStageIndex(Index3D relativeIndex);

		/// <summary>
		/// Converts the relative vector position to its equivalent absolute vector position.
		/// </summary>
		/// <param name="relativeVector">The relative vector to convert.</param>
		/// <returns>The absolute vector position that is the equivalent of the relative vector position.</returns>
		Vector3 ConvertToAbsoluteVectorPosition(Vector3 relativeVector);

		/// <summary>
		/// Converts the relative vector normal to its equivalent absolute vector normal.
		/// </summary>
		/// <param name="relativeVector">The relative vector to convert.</param>
		/// <returns>The absolute vector normal that is the equivalent of the relative vector normal.</returns>
		/// <remarks>
		/// An absolute vector normal doesn't have the offset for the origin of the projection added to it.
		/// Converting between it and the relative projection space only accounts for rotation. Thus this concept
		/// is well suited for surface normals while the <see cref="ConvertToAbsoluteVectorPosition"/> method is
		/// for vertex placement. That is the difference between these two methods.
		/// </remarks>
		Vector3 ConvertToAbsoluteVectorNormal(Vector3 relativeVector);
	}

	public static class IVoxelProjectionContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Indexer<TVoxel, TSurfaceData>(
			IVoxelProjection<TVoxel, TSurfaceData> instance, Index3D relativeIndex)
			where TVoxel : struct
			where TSurfaceData : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(instance.IsRelativeIndexValid(relativeIndex));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void GetVoxelAtAbsoluteIndex<TVoxel, TSurfaceData>(
			IVoxelProjection<TVoxel, TSurfaceData> instance, Index3D absoluteIndex)
			where TVoxel : struct
			where TSurfaceData : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(instance.IsAbsoluteIndexValid(absoluteIndex));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void GetVoxelAtStageIndex<TVoxel, TSurfaceData>(
			IVoxelProjection<TVoxel, TSurfaceData> instance, Index3D stageIndex)
			where TVoxel : struct
			where TSurfaceData : class
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(instance.IsStageIndexValid(stageIndex));
		}
	}
}
