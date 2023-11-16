using System;

namespace Voxelscape.Common.Indexing.Core.Enums
{
	/// <summary>
	/// An enumeration of flags used to identify an orthant in one dimensional space. An orthant is all space lying on a
	/// given side of a particular set of axes centered on a particular origin extending on infinitely in that direction.
	/// In two dimensions these are quadrants and in three dimensions these are octants. See
	/// <see href="http://en.wikipedia.org/wiki/Orthant">this link</see> for more information on Orthants.
	/// </summary>
	/// <remarks>
	/// Values of this enumeration may be combined in order to specify a specific orthant, to include or exclude the axes
	/// themselves from that orthant, or to even define multiple orthants.
	/// </remarks>
	[Flags]
	public enum Orthant1D
	{
		/// <summary>
		/// No orthant.
		/// </summary>
		None = 0,

		/// <summary>
		/// The orthant is less than the X axis.
		/// </summary>
		LessX = 1 << 0,

		/// <summary>
		/// The orthant includes the X axis.
		/// </summary>
		EqualX = 1 << 1,

		/// <summary>
		/// The orthant is greater than the X axis.
		/// </summary>
		GreaterX = 1 << 2,

		/// <summary>
		/// The orthant is less than all axes.
		/// </summary>
		AllLess = LessX,

		/// <summary>
		/// The orthant includes all axes.
		/// </summary>
		AllEqual = EqualX,

		/// <summary>
		/// The orthant is greater than all axes.
		/// </summary>
		AllGreater = GreaterX,
	}
}
