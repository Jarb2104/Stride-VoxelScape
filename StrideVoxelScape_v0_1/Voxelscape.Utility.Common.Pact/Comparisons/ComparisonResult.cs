namespace Voxelscape.Utility.Common.Pact.Comparisons
{
	/// <summary>
	/// An enumeration indicating the relative ordering of values.
	/// </summary>
	public enum ComparisonResult
	{
		/// <summary>
		/// The current value is equal to the other value.
		/// </summary>
		Equal = 0,

		/// <summary>
		/// The current value is less than the other value.
		/// </summary>
		Less = -1,

		/// <summary>
		/// The current value is greater than the other value.
		/// </summary>
		Greater = 1,
	}
}
