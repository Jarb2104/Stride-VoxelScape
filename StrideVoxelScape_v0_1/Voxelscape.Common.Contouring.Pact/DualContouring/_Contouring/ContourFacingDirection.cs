using System;

namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	/// <summary>
	/// A flags enumeration value indicating which way a surface contour is facing.
	/// </summary>
	[Flags]
	public enum ContourFacingDirection
	{
		/// <summary>
		/// The is no surface contour.
		/// </summary>
		None = 0,

		/// <summary>
		/// The surface contour is facing towards negative infinity.
		/// </summary>
		TowardsNegative = 1 << 0,

		/// <summary>
		/// The surface contour is facing towards positive infinity.
		/// </summary>
		TowardsPositive = 1 << 1,

		/// <summary>
		/// The surface contour faces both directions.
		/// </summary>
		Both = TowardsNegative | TowardsPositive,
	}
}
