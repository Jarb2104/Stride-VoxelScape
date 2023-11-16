using System;

namespace Voxelscape.Utility.Common.Core.Control
{
	/// <summary>
	/// Represent the evaluation of multiple conditions for use in switch statements instead of using deeply nested if statements.
	/// </summary>
	[Flags]
	public enum SwitchFlag
	{
		/// <summary>
		/// All conditions are false.
		/// </summary>
		AllFalse = 0,

		/// <summary>
		/// The first condition is true.
		/// </summary>
		FirstTrue = 1 << 0,

		/// <summary>
		/// The second condition is true.
		/// </summary>
		SecondTrue = 1 << 1,

		/// <summary>
		/// The third condition is true.
		/// </summary>
		ThirdTrue = 1 << 2,

		/// <summary>
		/// The forth condition is true.
		/// </summary>
		ForthTrue = 1 << 3,

		/// <summary>
		/// The fifth condition is true.
		/// </summary>
		FifthTrue = 1 << 4,

		/// <summary>
		/// The sixth condition is true.
		/// </summary>
		SixthTrue = 1 << 5,

		/// <summary>
		/// The seventh condition is true.
		/// </summary>
		SeventhTrue = 1 << 6,

		/// <summary>
		/// The eighth condition is true.
		/// </summary>
		EighthTrue = 1 << 7,

		/// <summary>
		/// All conditions are true.
		/// </summary>
		AllTrue = FirstTrue | SecondTrue | ThirdTrue | ForthTrue | FifthTrue | SixthTrue | SeventhTrue | EighthTrue,
	}
}
