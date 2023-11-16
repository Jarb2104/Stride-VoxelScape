using System;

namespace Voxelscape.Common.Indexing.Core.Enums
{
	/// <summary>
	/// An enumeration of the orthogonal axes in one dimensional space
	/// used to specify a combination of axes.
	/// </summary>
	[Flags]
	public enum Axes1D
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
		/// The x axis.
		/// </summary>
		All = X,
	}
}
