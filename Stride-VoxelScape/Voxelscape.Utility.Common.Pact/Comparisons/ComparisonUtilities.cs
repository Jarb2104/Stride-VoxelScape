using Voxelscape.Utility.Common.Pact.Exceptions;

namespace Voxelscape.Utility.Common.Pact.Comparisons
{
	/// <summary>
	///
	/// </summary>
	public static class ComparisonUtilities
	{
		public static ComparisonResult ConvertToEnum(int comparison)
		{
			if (comparison < 0)
			{
				return ComparisonResult.Less;
			}
			else if (comparison > 0)
			{
				return ComparisonResult.Greater;
			}
			else
			{
				return ComparisonResult.Equal;
			}
		}

		public static int ConvertToInt(this ComparisonResult comparison)
		{
			switch (comparison)
			{
				case ComparisonResult.Equal: return 0;
				case ComparisonResult.Less: return -1;
				case ComparisonResult.Greater: return 1;
				default: throw InvalidEnumArgument.CreateException(nameof(comparison), comparison);
			}
		}
	}
}
