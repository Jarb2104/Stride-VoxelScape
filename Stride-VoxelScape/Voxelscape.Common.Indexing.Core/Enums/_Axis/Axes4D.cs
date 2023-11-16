using System;

namespace Voxelscape.Common.Indexing.Core.Enums
{
	/// <summary>
	/// An enumeration of the orthogonal axes in four dimensional space
	/// used to specify a combination of axes.
	/// </summary>
	[Flags]
	public enum Axes4D
	{
		/// <summary>
		/// No axis.
		/// </summary>
		None = 0,

		/// <summary>
		/// The x axis.
		/// </summary>
		X = 1 << 0,

		/// <summary>
		/// The y axis.
		/// </summary>
		Y = 1 << 1,

		/// <summary>
		/// The z axis.
		/// </summary>
		Z = 1 << 2,

		/// <summary>
		/// The w axis.
		/// </summary>
		W = 1 << 3,

		/// <summary>
		/// The x, y, z, and w axes.
		/// </summary>
		All = X | Y | Z | W,
	}
}
