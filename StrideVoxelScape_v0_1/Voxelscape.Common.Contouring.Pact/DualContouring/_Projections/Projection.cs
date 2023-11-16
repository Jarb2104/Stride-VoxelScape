using Voxelscape.Common.Indexing.Core.Indices;

namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	/// <summary>
	/// Provides constants to use when working with a <see cref="IVoxelProjection{TVoxel, TSurfaceData>"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The relative origin index (0, 0, 0) defines the voxel immediately behind the quad of surface being contoured (the voxel
	/// that the surface visually represents). (0, 0, -1) defines the voxel in front of the surface (typically air or some other
	/// transparent material). This perspective can be best thought of as looking at a wall. The x-axis is horizontal along the
	/// wall, positive is to the right and negative to the left. The y-axis is vertical, positive is up and negative is down.
	/// The z-axis is distance from the wall, positive is towards or into the wall and negative is backing away from it.
	/// </para><para>
	/// The terms 'Below' and 'Above' refer to voxel below or above the surface being contoured. (0, 0, 0) is below the surface
	/// and (0, 0, -1) is above the surface.
	/// </para>
	/// </remarks>
	public static class Projection
	{
		#region Top

		/// <summary>
		/// The relative index of the top-left-below surface voxel.
		/// </summary>
		public static readonly Index3D TopLeftBelow = new Index3D(-1, 1, 0);

		/// <summary>
		/// The relative index of the top-left-above surface voxel.
		/// </summary>
		public static readonly Index3D TopLeftAbove = new Index3D(-1, 1, -1);

		/// <summary>
		/// The relative index of the top-below surface voxel.
		/// </summary>
		public static readonly Index3D TopBelow = new Index3D(0, 1, 0);

		/// <summary>
		/// The relative index of the top-above surface voxel.
		/// </summary>
		public static readonly Index3D TopAbove = new Index3D(0, 1, -1);

		/// <summary>
		/// The relative index of the top-right-below surface voxel.
		/// </summary>
		public static readonly Index3D TopRightBelow = new Index3D(1, 1, 0);

		/// <summary>
		/// The relative index of the top-right-above surface voxel.
		/// </summary>
		public static readonly Index3D TopRightAbove = new Index3D(1, 1, -1);

		#endregion

		#region Center

		/// <summary>
		/// The relative index of the left-below surface voxel.
		/// </summary>
		public static readonly Index3D LeftBelow = new Index3D(-1, 0, 0);

		/// <summary>
		/// The relative index of the left-above surface voxel.
		/// </summary>
		public static readonly Index3D LeftAbove = new Index3D(-1, 0, -1);

		/// <summary>
		/// The relative index of the below surface voxel.
		/// </summary>
		public static readonly Index3D Below = new Index3D(0, 0, 0);

		/// <summary>
		/// The relative index of the above surface voxel.
		/// </summary>
		public static readonly Index3D Above = new Index3D(0, 0, -1);

		/// <summary>
		/// The relative index of the right-below surface voxel.
		/// </summary>
		public static readonly Index3D RightBelow = new Index3D(1, 0, 0);

		/// <summary>
		/// The relative index of the right-above surface voxel.
		/// </summary>
		public static readonly Index3D RightAbove = new Index3D(1, 0, -1);

		#endregion

		#region Bottom

		/// <summary>
		/// The relative index of the bottom-left-below surface voxel.
		/// </summary>
		public static readonly Index3D BotLeftBelow = new Index3D(-1, -1, 0);

		/// <summary>
		/// The relative index of the bottom-left-above surface voxel.
		/// </summary>
		public static readonly Index3D BotLeftAbove = new Index3D(-1, -1, -1);

		/// <summary>
		/// The relative index of the bottom-below surface voxel.
		/// </summary>
		public static readonly Index3D BotBelow = new Index3D(0, -1, 0);

		/// <summary>
		/// The relative index of the bottom-above surface voxel.
		/// </summary>
		public static readonly Index3D BotAbove = new Index3D(0, -1, -1);

		/// <summary>
		/// The relative index of the bottom-right-below surface voxel.
		/// </summary>
		public static readonly Index3D BotRightBelow = new Index3D(1, -1, 0);

		/// <summary>
		/// The relative index of the bottom-right-above surface voxel.
		/// </summary>
		public static readonly Index3D BotRightAbove = new Index3D(1, -1, -1);

		#endregion
	}
}
