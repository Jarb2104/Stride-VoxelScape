namespace Voxelscape.Utility.Common.Core.Conversions
{
	/// <summary>
	/// An enumeration of the possible results of attempting to convert a value.
	/// </summary>
	public enum ConversionStatus
	{
		/// <summary>
		/// The conversion was successful.
		/// </summary>
		Success,

		/// <summary>
		/// No converter was found to convert the value from the source type to the result type.
		/// </summary>
		NoConverterFound,

		/// <summary>
		/// An appropriate converter was found, but the converter threw an exception.
		/// </summary>
		Exception,
	}
}
