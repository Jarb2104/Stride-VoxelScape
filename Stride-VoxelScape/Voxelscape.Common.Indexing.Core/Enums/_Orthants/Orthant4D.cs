using System;

namespace Voxelscape.Common.Indexing.Core.Enums
{
	/// <summary>
	/// An enumeration of flags used to identify an orthant in four dimensional space. An orthant is all space lying on a
	/// given side of a particular set of axes centered on a particular origin extending on infinitely in that direction.
	/// In two dimensions these are quadrants and in three dimensions these are octants. See
	/// <see href="http://en.wikipedia.org/wiki/Orthant">this link</see> for more information on Orthants.
	/// </summary>
	/// <remarks>
	/// Values of this enumeration may be combined in order to specify a specific orthant, to include or exclude the axes
	/// themselves from that orthant, or to even define multiple orthants.
	/// </remarks>
	[Flags]
	public enum Orthant4D
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
		/// The orthant is less than the Y axis.
		/// </summary>
		LessY = 1 << 3,

		/// <summary>
		/// The orthant includes the Y axis.
		/// </summary>
		EqualY = 1 << 4,

		/// <summary>
		/// The orthant is greater than the Y axis.
		/// </summary>
		GreaterY = 1 << 5,

		/// <summary>
		/// The orthant is less than the Z axis.
		/// </summary>
		LessZ = 1 << 6,

		/// <summary>
		/// The orthant includes the Z axis.
		/// </summary>
		EqualZ = 1 << 7,

		/// <summary>
		/// The orthant is greater than the Z axis.
		/// </summary>
		GreaterZ = 1 << 8,

		/// <summary>
		/// The orthant is less than the W axis.
		/// </summary>
		LessW = 1 << 9,

		/// <summary>
		/// The orthant includes the W axis.
		/// </summary>
		EqualW = 1 << 10,

		/// <summary>
		/// The orthant is greater than the W axis.
		/// </summary>
		GreaterW = 1 << 11,

		/// <summary>
		/// The orthant is less than all axes.
		/// </summary>
		AllLess = LessX | LessY | LessZ | LessW,

		/// <summary>
		/// The orthant includes all axes.
		/// </summary>
		AllEqual = EqualX | EqualY | EqualZ | EqualW,

		/// <summary>
		/// The orthant is greater than all axes.
		/// </summary>
		AllGreater = GreaterX | GreaterY | GreaterZ | GreaterW,
	}
}
